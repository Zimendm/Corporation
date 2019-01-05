using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Collections;

namespace Corporation
{
    public partial class ServicesForm : Form
    {
        private CorporationDataSet ds;
        private int iServiceFilter;
        private string strDataFile;



        public class DisplayService
        {
            private string myServiceID;
            private string myServiceName;

            public DisplayService(string strServiceID, string strServiceName)
            {
                this.myServiceID = strServiceID;
                this.myServiceName = strServiceName;
            }

            public string ServiceID
            {
                get
                {
                    return myServiceID;
                }
            }

            public string ServiceName
            {
                get
                {
                    return myServiceName;
                }

            }

        }


        public ServicesForm()
        {
            InitializeComponent();
        }

        //Конструктор с параметром
        public ServicesForm(CorporationDataSet dsc) : this()
        {
            ds = dsc;
            iServiceFilter = -1;

        }

        public ServicesForm(CorporationDataSet dsc,string strDataFileName)
            : this()
        {
            ds = dsc;
            iServiceFilter = -1;
            strDataFile = strDataFileName;
        }


        public ServicesForm(CorporationDataSet dsc,string strDataFileName,Int32 serID)
            : this()
        {
            ds = dsc;
            iServiceFilter = serID;
            strDataFile = strDataFileName;

        }





        private void ServicesForm_Load(object sender, EventArgs e)
        {
        
            #region Заполнение сведений об услугах

            BindingSource bs = new BindingSource();

            bs.DataSource = ds.Services;


            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "ServiceName";
            comboBox1.ValueMember = "ServiceID";
       
            if (iServiceFilter >= 0)
            {
                comboBox1.SelectedValue = iServiceFilter;
            }
            #endregion
            
    
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ds.Services.FindByServiceID(Convert.ToInt32(((DisplayService)comboBox1.SelectedItem).ServiceID));
            //
            //DataRow dr = ds.Services.FindByServiceID(Convert.ToInt32(((DisplayService)comboBox1.SelectedItem).ServiceID));
            //dr["ServiceType"] = SERVICETYPE.roaming;

          //  DataRow dr = ds.Services.FindByServiceID((Int32)comboBox1.SelectedValue);

            //ServiceType st = new ServiceType(); 
            SERVICETYPE st;
            st = SERVICETYPE.main;
            decimal sval = 0;

            
            foreach (RadioButton rb in groupBox2.Controls)
            {
                if (rb.Checked)
                {
                    switch(rb.Text)
                    {
                        case "Основная":
                            //dr["ServiceType"] = SERVICETYPE.main;
                            st = SERVICETYPE.main;
                            break;
                        case "Специальная":
                            //dr["ServiceType"] = SERVICETYPE.special;
                            st = SERVICETYPE.special;
                            break;
                        case  "Роуминг":
                            //dr["ServiceType"] = SERVICETYPE.roaming;
                            st = SERVICETYPE.roaming;
                            break;
                        case "Контент":
                            //dr["ServiceType"] = SERVICETYPE.content;
                            st = SERVICETYPE.content;
                            break;
                        case "Скидка":
                            //dr["ServiceType"] = SERVICETYPE.content;
                            st = SERVICETYPE.discount;
                            break;
                        case "Обобщение (группировка)":
                            //dr["ServiceType"] = SERVICETYPE.total;
                            st = SERVICETYPE.total;
                            break;
                    }
                 }
            }
            

            foreach (RadioButton rb1 in groupBox1.Controls)
            {
                if (rb1.Checked)
                {
                    switch (rb1.Text)
                    {
                        case "0":
                            //dr["Koeff"] = 0;
                            sval = 0;
                            break;
                        case "1":
                            //dr["Koeff"] = 1;
                            sval = 1;
                            break;
                        case "1,2":
                            //dr["Koeff"] = 1.2m;
                            sval = 1.2m;
                            break;
                        case "1,275":
                            //dr["Koeff"] = 1.275m;
                            sval = 1.275m;
                            break;
                        case "Не определён":
                            //dr["Koeff"] = 0;
                            sval = 0;
                            break;
                    }
                }
            }


            DataRow dr = ds.Services.FindByServiceID((Int32)comboBox1.SelectedValue);

            dr["ServiceType"] = st;
            dr["Koeff"] = sval;


            //int hhhh = 0;
            ds.WriteXml(strDataFile);
            //Convert.ToInt32( ( (DisplayService)comboBox1.SelectedItem).ServiceID   );

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

          //  MessageBox.Show("Selected val ch"); 

            for (int i = 0; i < ds.Services.Rows.Count; i++)
            {
                if (ds.Services.Rows[i]["ServiceID"].ToString() == comboBox1.SelectedValue.ToString())
                {
                    switch (ds.Services.Rows[i]["ServiceType"].ToString())
                    {
                        case "main":
                            radioButtonMain.Checked = true;
                            break;
                        case "special":
                            radioButtonSpecial.Checked = true;
                            break;
                        case "roaming":
                            radioButtonRoaming.Checked = true;
                            break;
                        case "content":
                            radioButtonContent.Checked = true;
                            break;
                        case "total":
                            radioButtonTotal.Checked = true;
                            break;
                    }

                    switch (ds.Services.Rows[i]["Koeff"].ToString())
                    {
                        case "0":
                            radioButton0.Checked = true;
                            break;
                        case "1":
                            radioButton1.Checked = true;
                            break;
                        case "1,2":
                            radioButton12.Checked = true;
                            break;
                        case "1,275":
                            radioButton1275.Checked = true;
                            break;
                        default:
                            radioButtonUndef.Checked = true;
                            break;
                    }

                }
            }

        }
     
    }
}
