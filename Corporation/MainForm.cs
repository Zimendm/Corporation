using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corporation
{
    public partial class MainForm : Form
    {
        CorporationDataSet ds;
        string strDataFileName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintForm f = new PrintForm(ds);
            f.ShowDialog();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            UsersForm f = new UsersForm(ds,strDataFileName);
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BillsForm f = new BillsForm(ds,strDataFileName);
            f.ShowDialog();

            textBox1.Text = "";
            var query =
                from fir in ds.firm
                join invoice in ds.invoce
                on fir.firmID equals invoice.firmID
                select new
                {
                    firmID = fir.name,
                    invNumber = invoice.invNumber,
                    invDat = invoice.invDate
                }
                ;

            foreach (var order in query)
            {
                textBox1.AppendText((order.invNumber.ToString() + " от " + order.invDat.ToShortDateString() + "   " + order.firmID.ToString() )+ "\n");
            }
             
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServicesForm f = new ServicesForm(ds,strDataFileName);
            f.ShowDialog();
         
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            ds = new CorporationDataSet();
        }

        private void Form1_ChangeUICues(object sender, UICuesEventArgs e)
        {
            //MessageBox.Show("Активируется");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           // MessageBox.Show("Enter");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ContractForm f = new ContractForm(ds,strDataFileName);
            f.ShowDialog();

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            // savedlg.InitialDirectory = Path.GetDirectoryName(strFileName);
            // savedlg.FileName = Path.GetFileNameWithoutExtension(strFileName);
            // savedlg.AddExtension = true;
            // savedlg.Filter = "Text File (*.txt)|.txt";



            openFileDialog1.Reset();

            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

//            openFileDialog1.Filter = "Документ XML (*.xml)|.xml";
                
                
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //StreamReader sr = new StreamReader(dlg.FileName.ToString(), Encoding.Default);
                    //strInvoice = sr.ReadToEnd();
                    //sr.Close();

                    label2.Text += openFileDialog1.FileName.ToString();
                    strDataFileName = openFileDialog1.FileName.ToString();

                    try
                    {
                        ds.ReadXml(strDataFileName); //@"D:\firms.xml");
                        button1.Enabled = true;  // Кнопка статистики
                        button3.Enabled = true;  // Кнопка загрузки файла
                        button5.Enabled = true;  // Кнопка работы с контрактами
                        button4.Enabled = true;  // Кнопка работы с услугами
                        button2.Enabled = true;  // Кнопка работы с пользователями
                    }
                    catch
                    { }
                    textBox1.Visible = true;
                    
                    

                    var query =
                        from fir in ds.firm
                        join invoice in ds.invoce
                        on fir.firmID equals invoice.firmID
                        select new
                        {
                            firmID = fir.name,
                            invNumber = invoice.invNumber,
                            invDat = invoice.invDate
                        }
                        ;


                    foreach (var order in query)
                    {
                        textBox1.AppendText((order.invNumber.ToString() + " от " + order.invDat.ToShortDateString() + "   " + order.firmID.ToString()) + "\n");
                    }
                }
                catch
                { }

            }
        }

      

      
    }
}
