using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Text.RegularExpressions;
using System.Globalization;

namespace Corporation
{
    public partial class BillsForm : Form
    {
        private string strInvoice;
        private string strInvoiceHader;
        private string strContacts;
        private NumberFormatInfo provider;
        private string FirmAccount;
        private CorporationDataSet ds;
        private int iFirmID;
        private decimal decDiscount;
        private string strDataFileName;


        public BillsForm()
        {
            InitializeComponent();
        }

        public BillsForm(CorporationDataSet dsc,string strDataName)
            : this()
        {
            ds = dsc;
            strDataFileName = strDataName;
        }

        private void button1_Click(object sender, EventArgs e)
        {

             provider = new NumberFormatInfo();
             provider.CurrencyDecimalSeparator = ".";

             OpenFileDialog dlg = new OpenFileDialog();
             if (dlg.ShowDialog() == DialogResult.OK)
             {
                 try
                 {
                     StreamReader sr = new StreamReader(dlg.FileName.ToString(), Encoding.GetEncoding(1251));
                     strInvoice = sr.ReadToEnd();
                     sr.Close();
                    
                     //Удаление нулевых строк
                     strInvoice = Regex.Replace(strInvoice, @".*\s0\.0000\n", "");

                     strInvoiceHader = strInvoice.Substring(0, strInvoice.IndexOf("РЕКОМЕНДОВАНИЙ AВАНС"));
                    // richTextBox1.Text = strInvoiceHader;
                                          
                     #region Получение шапки счёта
                     string[] astr = Regex.Split(strInvoiceHader, @"\n", RegexOptions.Multiline);

                     foreach (string str in astr)
                     {
                         if (Regex.IsMatch(str, @"Номер рахунку"))
                         {
                             txtinvNumber.Text = Regex.Match(str, @"\d\d\d\d\d\d\d\d\d\d").Value.ToString();
                             txtinvDate.Text = Regex.Match(str, @"\d\d\.\d\d\.\d\d\d\d").Value.ToString();
                         }


#region Формирование полей для таблицы счетов
                         if (Regex.IsMatch(str, @"ЩОМІСЯЧНА ВАРТІСТЬ ПАКЕТА ТА ПОСЛУГИ, НАДАНІ ЗА МЕЖАМИ ПАКЕТА:"))
                         {
                             txtPackageCost.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         //if (Regex.IsMatch(str, @"ПОСЛУГИ, НАДАНІ ЗА МЕЖАМИ ПАКЕТА:"))
                         //{
                         //    txtOutPackageCost.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         //}
                         //if (Regex.IsMatch(str, @"ЗАМОВЛЕНІ ДОДАТКОВІ ПОСЛУГИ:"))
                         //{
                         //    txtAdditionalServices.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         //}
                         if (Regex.IsMatch(str, @"КОНТЕНТ-ПОСЛУГИ:"))
                         {
                             txtContentServices.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         if (Regex.IsMatch(str, @"ПОСЛУГИ МІЖНАРОДНОГО РОУМІНГУ:"))
                         {
                             txtRoamingServices.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }

                         //При внесении счёта после миграции
                         if (Regex.IsMatch(str, @"ЗНИЖКА"))
                         {
                             txtDiscount.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                        if (Regex.IsMatch(str, @"ДОДАТКОВА ЗНИЖКА: "))
                        {
                            txtAddDiscount.Text=Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                            //txtDiscount.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                        }
                        //

                        if (Regex.IsMatch(str, @"СПЕЦІАЛЬНІ ПОСЛУГИ МОБІЛЬНОГО ЗВ’ЯЗКУ ТА ДОСТУПУ ДО ІНТЕРНЕТ:"))
                         {
                             txtSpacialServices.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         if (Regex.IsMatch(str, @"ПОДАТОК НА ДОДАНУ ВАРТІСТЬ"))
                         {
                             txtVAT.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         if (Regex.IsMatch(str, @"ЗБІР В ПЕНСІЙНИЙ ФОНД"))
                         {
                             txtPF.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         if (Regex.IsMatch(str, @"ЗАГАЛОМ НАРАХОВАНО ЗА ПЕРІОД ЗА ВСІМА КОНТРАКТАМИ РАЗОМ З ПДВ ТА ПФ:"))
                         {
                             txtAllAmmountInBill.Text = Regex.Match(str, @"-*\d*\.\d\d").Value.ToString();
                         }
                         if (Regex.IsMatch(str, @"Особовий рахунок:"))
                         {
                             FirmAccount = str.Substring(str.IndexOf(':') + 1, str.Length - str.IndexOf(':') - 1).Trim();
                         }
                     }


                     decimal AllAmmount = 0;

                     if (txtPackageCost.Text != "") AllAmmount += Convert.ToDecimal(txtPackageCost.Text, provider);
                     if (txtOutPackageCost.Text != "") AllAmmount += Convert.ToDecimal(txtOutPackageCost.Text, provider);
                     if (txtAdditionalServices.Text != "") AllAmmount += Convert.ToDecimal(txtAdditionalServices.Text, provider);
                     if (txtContentServices.Text != "") AllAmmount += Convert.ToDecimal(txtContentServices.Text, provider);
                     if (txtRoamingServices.Text != "") AllAmmount += Convert.ToDecimal(txtRoamingServices.Text, provider);
                     if (txtSpacialServices.Text != "") AllAmmount += Convert.ToDecimal(txtSpacialServices.Text, provider);
                     if (txtDiscount.Text!="")  AllAmmount +=Convert.ToDecimal(txtDiscount.Text,provider);

                     //Доп. скидка при миграции
                     if (txtAddDiscount.Text != "") AllAmmount += Convert.ToDecimal(txtAddDiscount.Text, provider);


                    if (txtVAT.Text != "") AllAmmount += Convert.ToDecimal(txtVAT.Text, provider);
                     if (txtPF.Text != "") AllAmmount += Convert.ToDecimal(txtPF.Text, provider);
#endregion

                     

                     // if (Math.Round(AllAmmount).ToString(provider) == txtAllAmmountInBill.Text.Substring(0, txtAllAmmountInBill.Text.Length-3))

                    if (Math.Round(AllAmmount) == Math.Round( Convert.ToDecimal(txtAllAmmountInBill.Text, provider)))
                        {
                         lblAllAmmount.Visible = true;
                         lblAllAmmount.Text += AllAmmount.ToString();
                         lblAllAmmount.ForeColor = Color.Green;

                               foreach (DataRow dr in ds.firm.Rows)
                     {
                         if (dr["account"].ToString()==FirmAccount)
                         {
                             MessageBox.Show(dr["firmID"]+" "+dr["name"]);
                             iFirmID=(int) dr["firmID"];
                         }
                     }

                               #region Удаление всех счетов по организации
                               DataTable tmpInvoices = ds.Tables["invoce"];
                               IEnumerable<DataRow> query =
                                                   from tmpInvoice in tmpInvoices.AsEnumerable()
                                                   where tmpInvoice.Field<int>("firmID") == iFirmID || tmpInvoice.Field<int>("firmID")==0
                                                   select tmpInvoice;
                               List<string> iInvNumberToDelete=new List<string>();
                               foreach (DataRow p in query)
                               {
                                  // MessageBox.Show(p.Field<string>("invNumber").ToString());
                                   iInvNumberToDelete.Add(p.Field<string>("invNumber").ToString());
                               }
                                foreach(string str in iInvNumberToDelete)
                                {
                                    DataRow dr=ds.invoce.Rows.Find(str);
                                    dr.Delete();
                                }
                               #endregion


                                #region Внесение информации о новом счёте
                                DataRow drInvoice = ds.invoce.NewRow();

                        drInvoice["firmID"] = iFirmID;
                        drInvoice["invNumber"] = txtinvNumber.Text;
                        drInvoice["invDate"] = txtinvDate.Text;
                        drInvoice["Discount"] = Convert.ToDecimal(txtDiscount.Text, provider);
                        drInvoice["PackageCost"] = Convert.ToDecimal(txtPackageCost.Text, provider);
//                        drInvoice["OutPackageCost"] = Convert.ToDecimal(txtOutPackageCost.Text, provider);
                        drInvoice["RoamingServices"] = Convert.ToDecimal(txtRoamingServices.Text, provider);
//                        drInvoice["AdditionalServices"] = Convert.ToDecimal(txtAdditionalServices.Text, provider);
                        drInvoice["ContentServices"] = Convert.ToDecimal(txtContentServices.Text, provider);
                        drInvoice["SpecialServices"] = Convert.ToDecimal(txtSpacialServices.Text, provider);
                        drInvoice["VAT"] = Convert.ToDecimal(txtVAT.Text, provider);
                        drInvoice["PF"] = Convert.ToDecimal(txtPF.Text, provider);




                        ds.invoce.Rows.Add(drInvoice);

                              //         decDiscount = Math.Abs(Convert.ToDecimal(txtDiscount.Text, provider) / (Convert.ToDecimal(txtPackageCost.Text, provider) + Convert.ToDecimal(txtOutPackageCost.Text, provider) + Convert.ToDecimal(txtAdditionalServices.Text, provider)));
                              decDiscount= Convert.ToDecimal(txtDiscount.Text, provider); 


                                   //    decDiscount = 0.25M;

                        //   label1.Text = decDiscount.ToString();

                        #endregion

                    }
                     else
                     {
                         lblAllAmmount.Visible = true;
                         lblAllAmmount.Text += "Ошибка импорта файла!";
                         lblAllAmmount.ForeColor = Color.Red;
                     }

                     #endregion


                     #region Получение данных о контрактах
                     strContacts = strInvoice.Substring(strInvoice.IndexOf("Контракт №"), strInvoice.Length - strInvoice.IndexOf("Контракт №") - 1);
//                     richTextBox1.Text = strContacts;

                     string[] aContracts = Regex.Split(strContacts, @"Контракт №");

                     string[] aContractDetail;
                     string strActualContract = "";
                     string strActualServiceName = "";
                     int iActualServiceID=0;




                    foreach (string str in aContracts)
                    {
                        aContractDetail = Regex.Split(str, @"\n");

                        //		Regex.Match(aContractDetail[0],@"\x20*\d{7}\x20").ToString().Trim()

                        strActualContract = Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20").ToString().Trim();



                        #region Обработка информации по каждому контракту
                        if (aContractDetail.Length > 1)
                        {

                            int count = (from c in ds.contract
                                         where c.contractNumber == strActualContract
                                         select c).Count();


                            //Проверка на существование контракта в базе и его обновление
                            if (count == 0)
                            {
                                DataRow dr = ds.contract.NewRow();
                                dr["ContractNumber"] = strActualContract;
                                dr["phone"] = Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20*$").ToString().Trim();
                                dr["rateplan"] = aContractDetail[1].Remove(0, "Тарифний Пакет:".Length).Trim();
                                dr["userID"] = 1;
                                //dr["rateplan"] = "Test";
                                ds.contract.Rows.Add(dr);

                                ContractForm f = new ContractForm(ds, strDataFileName, strActualContract);
                                f.ShowDialog();

                            }
                            else if (count == 1)
                            {
                                DataRow dr1 = ds.contract.Rows.Find(strActualContract);
                                dr1["phone"] = Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20*$").ToString().Trim();
                                dr1["rateplan"] = aContractDetail[1].Remove(0, "Тарифний Пакет:".Length).Trim();
                            }



                            // Выполнить один раз для преобразования номеров контрактов
                            #region Преобразование контрактов к новым номерам
                            //int count = (from c in ds.contract
                            //             where c.phone == (Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20*$").ToString().Trim()).Substring(3,9)
                            //             select c).Count();

                            //if (count == 0)
                            //{
                            //    DataRow dr = ds.contract.NewRow();
                            //    dr["ContractNumber"] = strActualContract;
                            //    dr["phone"] = Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20").ToString().Trim();
                            //    dr["rateplan"] = aContractDetail[1].Remove(0, "Тарифний Пакет:".Length).Trim();
                            //    dr["userID"] = 1;
                            //    //dr["rateplan"] = "Test";
                            //    ds.contract.Rows.Add(dr);

                            //    ContractForm f = new ContractForm(ds, strDataFileName, strActualContract);
                            //    f.ShowDialog();

                            //}
                            //else if (count == 1)
                            //{
                            //    var cN = from c in ds.contract
                            //             where c.phone == (Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20*$").ToString().Trim()).Substring(3, 9)
                            //             select c.contractNumber;

                            //    foreach(string c in cN)
                            //    { 
                            //    DataRow dr1 = ds.contract.Rows.Find(c);
                            //    dr1["ContractNumber"] = strActualContract;
                            //    dr1["phone"] = Regex.Match(aContractDetail[0], @"\x20*\d{12}\x20*$").ToString().Trim();
                            //    dr1["rateplan"] = aContractDetail[1].Remove(0, "Тарифний Пакет:".Length).Trim();

                            //        int eeee = 0;
                            //    }
                            //}
                            #endregion


                            #region Обработка услуг

                            //  Обработка услуг
                            for (int i = 2; i < aContractDetail.Length; i++)
                            {
                                strActualServiceName = "";
                                iActualServiceID = 0;

                                if (Regex.Matches(aContractDetail[i], @"\x20\.\x20\.*").Count > 0) //В названии услуги есть множество точек
                                {
                                    strActualServiceName = (Regex.Split(aContractDetail[i], @"\x20\.\x20\.*")[0]).ToString().Trim();
                                    var QueryResult =
                                        (from n in ds.Services
                                         where n.ServiceName == (Regex.Split(aContractDetail[i], @"\x20\.\x20\.*")[0]).ToString().Trim()
                                         select n).Count();

                                    // Добавление новой услуги
                                    if (QueryResult == 0)
                                    {
                                        DataRow drService = ds.Services.NewRow();
                                        drService["ServiceName"] = (Regex.Split(aContractDetail[i], @"\x20\.\x20\.*")[0]).ToString().Trim();
                                        drService["ServiceType"] = SERVICETYPE.main;
                                        drService["koeff"] = 1.275m;
                                        ds.Services.Rows.Add(drService);


                                        ServicesForm f = new ServicesForm(ds, strDataFileName, (Int32)drService["serviceID"]);
                                        f.ShowDialog();

                                    }
                                    else if (QueryResult == 1)
                                    {

                                        /*
                                        ServicesForm f = new ServicesForm();
                                        f.ShowDialog();
                                         */
                                    }

                                    //Конец обработки контракта
                                    if (Regex.Split(aContractDetail[i], @"\x20\.\x20\.*")[0].ToString().Trim() == "ЗАГАЛОМ ЗА КОНТРАКТОМ (БЕЗ ПДВ ТА ПФ):")
                                    {

                                        decimal qsum =
                                            (from c in ds.bills
                                             join s in ds.Services on c.ServiceID equals s.ServiceID
                                             where c.contractNumber == strActualContract && c.invNumber == txtinvNumber.Text && s.ServiceType!="discount"
                                             select c.Ammount).Sum();
                                        
                                        decimal qdis =
                                             (from c in ds.bills
                                              join s in ds.Services on c.ServiceID equals s.ServiceID
                                              where c.contractNumber == strActualContract && c.invNumber == txtinvNumber.Text && s.ServiceType == "discount"
                                              select c.Ammount).Sum();




                                        if (qsum != (Convert.ToDecimal(Regex.Match(aContractDetail[i], @"-*\d*\.\d\d\d\d$").ToString(), provider)- qdis))
                                        {
                                            MessageBox.Show("Не совпадает сумма " + strActualContract);
                                        }

                                        break;
                                    }

                                    //continue;
                                }
                                else
                                {
                                    if (Regex.Matches(aContractDetail[i], @"-*\d*\.\d\d\d\d").Count > 0) //в названии услуги нет множества точек
                                    {
                                        strActualServiceName = (Regex.Split(aContractDetail[i], @"-*\d*\.\d\d\d\d")[0]).ToString().Trim();
                                        var QueryResult =
                                           (from n in ds.Services
                                            where n.ServiceName == (Regex.Split(aContractDetail[i], @"-*\d*\.\d\d\d\d")[0]).ToString().Trim()
                                            select n).Count();

                                        if (QueryResult == 0)
                                        {
                                            DataRow drService = ds.Services.NewRow();
                                            drService["ServiceName"] = (Regex.Split(aContractDetail[i], @"-*\d*\.\d\d\d\d")[0]).ToString().Trim();
                                            drService["ServiceType"] = SERVICETYPE.main;
                                            ds.Services.Rows.Add(drService);
                                        }



                                    }
                                }


                                DataTable tmpServices = ds.Tables["Services"];

                                IEnumerable<DataRow> query =
                                                    from tmpService in tmpServices.AsEnumerable()
                                                    where tmpService.Field<string>("ServiceName") == strActualServiceName
                                                    select tmpService;



                                foreach (DataRow p in query)
                                {
                                    //Console.WriteLine(p.Field<string>("ServiceName"));
                                    //MessageBox.Show((p.Field<string>("ServiceName").ToString()) + "---" + (p.Field<string>("ServiceType").ToString()));

                                    if ((p.Field<string>("ServiceType").ToString() != "total"))
                                    {
                                        iActualServiceID = p.Field<Int32>("ServiceID");//Convert.ToInt32(.ToString());


                                        //          int ggguuu = 0;
                                        try
                                        {
                                            DataRow br = ds.bills.NewRow();
                                            br["invNumber"] = txtinvNumber.Text;
                                            br["contractNumber"] = strActualContract;
                                            br["ServiceID"] = iActualServiceID;
                                            br["Ammount"] = Convert.ToDecimal(Regex.Match(aContractDetail[i], @"-*\d*\.\d\d\d\d$").ToString(), provider);
                                            br["AmmountT"] = Convert.ToDecimal(Regex.Match(aContractDetail[i], @"-*\d*\.\d\d\d\d$").ToString(), provider) * p.Field<decimal>("koeff");

                                            if ((p.Field<string>("ServiceType").ToString() == "discount"))
                                            {
                                                //br["Ammount"] = 0;
                                                //br["AmmountT"] = 0;
                                                br["Discount"]= Convert.ToDecimal(Regex.Match(aContractDetail[i], @"-*\d*\.\d\d\d\d$").ToString(), provider) * p.Field<decimal>("koeff"); 
                                            }
                                            else
                                            {

                                                br["Discount"] = 0;
                                            }

                                            //var cn = (from c in ds.contract
                                            //          where c.contractNumber == strActualContract
                                            //          select c.rateplan).FirstOrDefault();

                                            //int kdfkjfdjkdfkdjf = 0;
                                            ////var cn = ds.contract.Where(c => c.contractNumber == txtinvNumber.Text).Select(c => c.rateplan).FirstOrDefault().ToString();
                                            //if (cn != @"GSM ' МТС ULTRA'")
                                            //{
                                            //    if (p.Field<string>("ServiceType") == SERVICETYPE.main.ToString())
                                            //    {
                                            //        br["Discount"] = ((decimal)br["AmmountT"]) * decDiscount;
                                            //    }
                                            //    else
                                            //    {
                                            //        br["Discount"] = 0;
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    br["Discount"] = 0;
                                            //}
                                            ds.bills.Rows.Add(br);
                                        }

                                        catch
                                        {
                                            MessageBox.Show("Исключение при заполнении услуг!");
                                        }


                                    }
                                }

                                #endregion
                                /*
                                var qr =
                                    from c in ds.Services
                                    where c.ServiceName == strActualServiceName
                                    select c.ServiceID
                                    ;
                               foreach (int s in qr)
                                {
                                  //  MessageBox.Show(s.ToString());

                                   iActualServiceID = s;//Convert.ToInt32(.ToString());

                                   try
                                   {
                                       DataRow br = ds.bills.NewRow();
                                       br["invNumber"] = txtinvNumber.Text;
                                       br["contractNumber"] = strActualContract;
                                       br["ServiceID"] = iActualServiceID;
                                       br["Ammount"] = Convert.ToDecimal(Regex.Match(aContractDetail[i], @"-*\d*\.\d\d$").ToString(), provider);
                                       ds.bills.Rows.Add(br);
                                   }

                                   catch
                                   {
                                       MessageBox.Show("Исключение при заполнении услуг!");
                                   }

                                }
                                */


                                //          MessageBox.Show("Конец обработки услуги " + strActualServiceName + "->" 
                                //              + Regex.Match(aContractDetail[i], @"-*\d*\.\d\d$").ToString() + " контракта " + strActualContract);


                                //Конец цикла обработки услуг контракта
                            }
                        }

                        #endregion

                        //MessageBox.Show(ds.bills.Rows.Count.ToString());
                    }




                    #endregion

                    MessageBox.Show("ddddd "+ds.bills.Rows.Count.ToString());
                     ds.WriteXml(strDataFileName);

                 }
                 catch (Exception e1)
                 {

                     MessageBox.Show("LLL");
                 }
             }


        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
           // ds = new CorporationDataSet();
           // ds.ReadXml(@"d:\firms.xml");
        }
    }
}
