using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Corporation
{
    public partial class ContractForm : Form
    {
        private CorporationDataSet ds;
        private BindingSource bsourceCF;
        private BindingSource bsourceUsersCF;
        private string strDataFile;




        public ContractForm()
        {
            InitializeComponent();
        }

        public ContractForm(CorporationDataSet dsc,string strDFN)
            : this()
        {
            ds = dsc;
            bsourceCF = new BindingSource();
            bsourceUsersCF = new BindingSource();
            strDataFile = strDFN;
            bsourceCF.Filter = "";
            bsourceUsersCF.Filter = "";
        }

        public ContractForm (CorporationDataSet dsc,string strDFN,string contrNum) :this()
        {
            ds = dsc;
            bsourceCF = new BindingSource();
            bsourceUsersCF = new BindingSource();
            bsourceCF.Filter = "contractNumber="+contrNum;
            strDataFile = strDFN;
        }

        private void ContractForm_Load(object sender, EventArgs e)
        {


            //bsource.RemoveFilter();

            

            //bsource.Filter = "contractNumber=''";
            //ds.contract.Reset();

            //bsource.Filter = "";
            bsourceCF.DataSource = ds.contract;

          //  listBox1.DataSource = bsource;
          //  listBox1.DisplayMember = "phone";
          //  listBox1.ValueMember = "contractNumber";
//
            //if (bsource.Filter.Length == 0)
            //{
             //   bsource.RemoveFilter();
            //}
            dataGridView1.DataSource = bsourceCF;


            //bsourceUsers.Filter = "";
            //ds.user.Reset();
           // bsourceUsers.RemoveFilter();
            
            bsourceUsersCF.DataSource = ds.user;
            bsourceUsersCF.Sort = "name";
            listBox2.DataSource = bsourceUsersCF;
            listBox2.DisplayMember = "name";
            listBox2.ValueMember = "userID";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Trim().Length > 5)
            {
                if (textBox1.Text.ToString().Trim().Length==12)
                {
                    bsourceCF.Filter = "phone='" + textBox1.Text.ToString().Trim()+"'";
                    //bsource.Filter = @"phone=503237000";
                }
                else 
                {
                    bsourceCF.Filter = "contractNumber=" + textBox1.Text.ToString().Trim();
                }
            }
            else
            {
                bsourceCF.Filter = "";
                MessageBox.Show("Необходимо указать фильтр!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.SelectedRows[0].Cells[3].Value = listBox2.SelectedItem;
            dataGridView1.CurrentRow.Cells[3].Value = listBox2.SelectedValue;
            //dataGridView1.EndEdit();
            ds.WriteXml(strDataFile);
        }

       
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            listBox2.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
        }

    }
}
