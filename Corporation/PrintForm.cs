using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;




namespace Corporation
{
    public partial class PrintForm : Form
    {
        private enum Months { Январь = 1, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Октябрь, Ноябрь, Декабрь };
        private CorporationDataSet ds;
        private string strMainDir;
        private string strBillMonth;
        private string firmName;
        private decimal decDiscount;

        private string CreateStatisticFile(string invoiceNumber,string firmName)
        {
            string strOutText; // = "test";
            StringBuilder strBild = new StringBuilder();

            #region Формирование шапки файла

            strBild.AppendLine("Общая информация по счёту");
            strBild.AppendLine("-------------------------");
            strBild.AppendLine("            Организация: " + firmName);
            strBild.AppendLine("            Номер счёта: " + invoiceNumber);

            var qr =
                from i in ds.invoce
                where i.invNumber == invoiceNumber
                select new
                {
                    Invdate = i.invDate,
                    Discount=i.Discount,
                    Packagecost=i.PackageCost,
                  //  Outpackagecost=i.OutPackageCost,
                 //   Additional=i.AdditionalServices,
                    Content=i.ContentServices,
                    Roaming=i.RoamingServices,
                    Special=i.SpecialServices,
                    PF=i.PF,
                    VAT=i.VAT
                }
                ;

            decimal decTmpInvSumm = 0;
            foreach (var q in qr)
            {
                strBild.AppendLine("Дата формирования счёта: " + q.Invdate.ToShortDateString());
                strBild.AppendLine("                 Период: "
                + Convert.ToDateTime("01." + Convert.ToDateTime(q.Invdate).Month.ToString() + "." + Convert.ToDateTime(q.Invdate).Year.ToString()).ToShortDateString() +
                "-" + q.Invdate.ToShortDateString());
                strBild.AppendLine();
                strBild.AppendLine("     Скидка без налогов: " + Math.Abs((decimal)q.Discount).ToString());
                strBild.AppendLine("      Скидка c налогами: " + (Math.Round(Math.Abs((decimal)q.Discount) * 1.275M, 2)).ToString());
                strBild.AppendLine("         Процент скидки: " + Math.Round((((decimal)q.Discount / ((decimal)q.Packagecost ))) * -100M,4).ToString() + "%");
                //decDiscount=Math.Round((((decimal)q.Discount / ((decimal)q.Packagecost + 
               //     (decimal)q.Outpackagecost +
                //    (decimal)q.Additional ))));
                decDiscount = Math.Round(Math.Abs((decimal)q.Discount), 4);

                strBild.AppendLine();
                strBild.AppendLine();
                strBild.AppendLine("Информация по группам:");
                strBild.AppendLine();
                strBild.AppendLine("                            |  Без скидки |    Скидка   |   К оплате  ");
                strBild.AppendLine("".PadRight(71, '-'));
                decTmpInvSumm = q.Packagecost + q.Special + q.Roaming  + q.Content+q.PF+q.VAT+q.Discount;
            }




            #endregion

            var q2 =
            from bill in ds.bills
            join cont in ds.contract on bill.contractNumber equals cont.contractNumber
            join users in ds.user on cont.userID equals users.userID
            join super in ds.Supervisor on users.supervisorID equals super.supervisorID
            where bill.invNumber == invoiceNumber.ToString()
            group bill by super.name into g1
            orderby g1.Key
            select new
            {
                sname = g1.Key,
                amm = g1.Sum(bill => bill.AmmountT),
                dis = Math.Abs( g1.Sum(bill => bill.Discount)),
                tot = g1.Sum(bill => bill.AmmountT) 
            };


            decimal decTmpSumm = 0;
            decimal decTmpDiscount = 0;
            

            foreach(var v in q2)
            {
                strBild.AppendLine(v.sname.ToString().PadRight(28) + "|" + Math.Round(v.amm+v.dis, 2).ToString().PadLeft(13) + "|" +
                   Math.Round(v.dis, 2).ToString().PadLeft(13) + "|" + Math.Round(v.tot, 2).ToString().PadLeft(13));
                decTmpSumm += Math.Round(v.tot, 2);
                decTmpDiscount += Math.Round(v.dis, 2);
            }

            strBild.AppendLine("".PadRight(71, '-'));
            strBild.AppendLine("Итого:|".PadLeft(43) + decTmpDiscount.ToString().PadLeft(13) + "|" + decTmpSumm.ToString().PadLeft(13));
            strBild.AppendLine("По счёту:|".PadLeft(43) + Math.Round(decDiscount*1.275M, 2).ToString().PadLeft(13) + "|" + decTmpInvSumm.ToString().PadLeft(13));
            strBild.AppendLine("Разница (факт-счёт):|".PadLeft(43) + (decTmpDiscount - Math.Round(Math.Abs(decDiscount) * 1.275M, 2)).ToString().PadLeft(13)
               + "|" + (decTmpSumm - decTmpInvSumm).ToString().PadLeft(13));

            strOutText = strBild.ToString();
            return strOutText;
            
        }


        private void CreateDetailedFile(string invoiceNumber)
        {
            string strTextOut="";
            StringBuilder strBild = new StringBuilder();
            StringBuilder sb;
            StringBuilder sbshort;
            StringBuilder sbUser;

            decimal decContrTotal = 0;
            decimal decUserTotal=0;

            sbUser = new StringBuilder();

            var mainqeury =
                from user in ds.user
                join contract in ds.contract on user.userID equals contract.userID
                join bill in ds.bills on contract.contractNumber equals bill.contractNumber
                where bill.invNumber == invoiceNumber
                group user by new { user.userID, user.name } into g
                select new
                {
                    userID = g.Key.userID,
                    userName = g.Key.name,
                   // shortbill=g.Key.shortbill
                 };

            foreach(var mainv in mainqeury)
            {
                sb = new StringBuilder();
                sbshort = new StringBuilder();
                decUserTotal = 0;

                


                var query =
                    from contract in ds.contract
                    join user in ds.user on contract.userID equals user.userID
                    join bill in ds.bills on contract.contractNumber equals bill.contractNumber
                    where contract.userID == Convert.ToInt32(mainv.userID) && bill.invNumber == invoiceNumber
                    group contract by new { contract.contractNumber,contract.phone,contract.rateplan } into g
                    select new
                    {
                        contr = g.Key.contractNumber,
                        phone = g.Key.phone,
                        tariff = g.Key.rateplan
                    }
                    ;

                if (query.Count() > 0)
                {

                    foreach (var v in query)
                    {
         
                        sb.AppendLine(("Контракт №: " + v.contr.ToString().Trim()).PadRight(20) +
                              ("Моб.номер: " + v.phone.ToString().Trim()).PadLeft(25) + "     " + v.tariff.ToString().Trim());
                        sb.AppendLine("".PadLeft(80, '-'));
                        
                        decContrTotal = 0;
         
                        var query1 =
                            from bill in ds.bills
                            join service in ds.Services on bill.ServiceID equals service.ServiceID
                            where bill.contractNumber == v.contr.ToString().Trim() && bill.invNumber == invoiceNumber
                            select new
                            {
                                servName = service.ServiceName,
                                ammount = bill.AmmountT
                            }
                            ;

                        if (query1.Count() > 0)
                        {
         
                            #region
                            foreach (var v1 in query1)
                            {
                                if (!v1.servName.Contains("Зниж"))
                                {
                                    decContrTotal += (decimal)v1.ammount;

                                    sb.AppendLine(v1.servName.ToString().Trim() + Math.Round(v1.ammount, 2).ToString().Trim().PadLeft((80 - v1.servName.ToString().Trim().Length)));
                                }
                            }


           
                            sb.AppendLine("".PadLeft(50) + "".PadLeft(30, '-'));
                            sb.AppendLine(("Итого по контракту: " + Math.Round(decContrTotal,2).ToString().Trim()).PadLeft(80));
                            sbshort.AppendLine(("(" + v.contr.ToString().Trim() + ")").PadRight(10) + v.phone.ToString().Trim().PadLeft(12) + " -->  " + Math.Round(decContrTotal, 2).ToString().Trim());
                            sb.AppendLine();

                            decUserTotal += decContrTotal;
                            decContrTotal = 0;
                            #endregion
                           
                           
                        }

   

                    }


                   
                }


                
                sb.AppendLine("Итого по всем контрактам: " + Math.Round(decUserTotal, 2));
                sbUser.AppendLine(mainv.userName.ToString().Trim() + ";" + Math.Round(decUserTotal, 2).ToString());
                decUserTotal = 0;


                StreamWriter fstr_out;
                strTextOut = sb.ToString();
                try
                {
                    fstr_out = new StreamWriter(strBillMonth + @"\" + mainv.userName.ToString().Trim() + ".txt");
                    fstr_out.Write(strTextOut);
                    fstr_out.Close();
                    textBox1.AppendText(strBillMonth + @"\" + mainv.userName.ToString().Trim() + ".txt" +Environment.NewLine);// +"\n";

                    var q3 =
                        from user in ds.user
                        where user.name == mainv.userName.ToString().Trim() && !user.IsshortbillNull()
                        select user;

                    foreach (var v3 in q3)
                    {
                            fstr_out = new StreamWriter(strBillMonth + @"\" + mainv.userName.ToString().Trim() + "_short" + ".txt");
                            fstr_out.Write(sbshort.ToString());
                            fstr_out.Close();
                            textBox1.AppendText(strBillMonth + @"\" + mainv.userName.ToString().Trim() + "_short" + ".txt" + Environment.NewLine);// +"\n";
                    }

                   
                }
                catch
                {
                    MessageBox.Show("Ошибка записи файла!");
                }


            }

            StreamWriter fstr_out1;
            fstr_out1 = new StreamWriter(strBillMonth + @"\" + "aaaa" + invoiceNumber + ".txt");
            fstr_out1.Write(sbUser.ToString());
            fstr_out1.Close();
            textBox1.AppendText(strBillMonth + @"\" + "aaaa" + invoiceNumber + ".txt" +Environment.NewLine+ Environment.NewLine);


           
            //return strTextOut;
        }

        
        public PrintForm()
        {
            InitializeComponent();
        }

        public PrintForm(CorporationDataSet cds)
            : this()
        {
            ds = cds;
            //strMainDir = @"D:\СчетаКорпорация";
        }

        private void button1_Click(object sender, EventArgs e)
        {

           FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории";
            //DirDialog.SelectedPath = @"C:\";

            try
            {
                if (DirDialog.ShowDialog() == DialogResult.OK)
                {
                    //PathArcTextBox.Text = DirDialog.SelectedPath;

                    strMainDir = DirDialog.SelectedPath;
                   // textBox1.Text += "\n"+strMainDir;
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать папку!");
                    this.Close();
                }

            }
            catch (Exception edir)
            {
            }

            
            
            
            foreach(DataRow dr in ds.invoce )
            
            {
                strBillMonth = strMainDir + @"\" + Enum.GetName(typeof(Months), Convert.ToDateTime(dr["invDate"]).Month) + " " + Convert.ToDateTime(dr["invDate"]).Year.ToString().Trim();
      


                if (Directory.Exists(strBillMonth + @"\Статистика"))
                {
                }
                else
                {
                    Directory.CreateDirectory(strBillMonth + @"\Статистика");
                }


                string strFileName;

                var qr =
                    from c in ds.invoce
                    join f in ds.firm on c.firmID equals f.firmID
                    where c.invNumber==dr["invNumber"]
                    select f.name
                    ;

                strFileName = "";
                firmName = "";

                foreach (var q1 in qr)
                {
                    firmName = q1.ToString().Trim();
                    strFileName = firmName + "_" + Convert.ToDateTime(dr["invDate"]).Month.ToString().Trim() + "_" + Convert.ToDateTime(dr["invDate"].ToString().Trim()).Year.ToString() + ".txt";
                }
                // savedlg.InitialDirectory = Path.GetDirectoryName(strFileName);
                // savedlg.FileName = Path.GetFileNameWithoutExtension(strFileName);
                // savedlg.AddExtension = true;
                // savedlg.Filter = "Text File (*.txt)|.txt";



                //strTmpDir+@"\Статистика"

                label1.Text += "\n";


                StreamWriter fstr_out;
                try
                {
                    fstr_out = new StreamWriter(strBillMonth + @"\Статистика" + @"\" + strFileName);
                    fstr_out.Write(CreateStatisticFile(dr["invNumber"].ToString(), firmName));
                    fstr_out.Close();

                    textBox1.AppendText(strBillMonth + @"\Статистика" + @"\" + strFileName  +Environment.NewLine);

                }
                finally
                {
                }


                #region Формирование счетов по каждому пользователю
                CreateDetailedFile(dr["invNumber"].ToString());


                #endregion


               
            }
            textBox1.Text += "записаны в: " + strMainDir;
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
        }
    }
}
