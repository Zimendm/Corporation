using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;




namespace Corporation
{
    public partial class UsersForm : Form
    {
        private CorporationDataSet ds;
        private BindingSource bSource;
        private BindingSource bSourceContracts;
        private BindingSource bSourceSupervisor;
        private string strDataFile;



        public class DisplaySupervisor
        {
            private string mysupervisorID;
            private string mysupervisorName;

            public DisplaySupervisor(string strSupervisorID, string strSupervisorName)
            {
                this.mysupervisorID = strSupervisorID;
                this.mysupervisorName = strSupervisorName;
            }

            public string SupervisorID
            {
                get
                {
                    return mysupervisorID;
                }
            }

            public string SupervisorName
            {
                get
                {
                    return mysupervisorName;
                }

            }

        }
            

        public UsersForm()
        {
            InitializeComponent();
            //ds = new CorporationDataSet();
           
        }

        public UsersForm(CorporationDataSet dsc):this()
        {
            ds = dsc;
            //bSource = new BindingSource();
            //bSourceContracts = new BindingSource();
            //bSourceSupervisor = new BindingSource();
        }


        public UsersForm(CorporationDataSet dsc, string strDFN)
            : this()
        {
            ds = dsc;
            //bSource = new BindingSource();
            //bSourceContracts = new BindingSource();
            //bSourceSupervisor = new BindingSource();

            strDataFile = strDFN;
        }



        private void UsersForm_Load(object sender, EventArgs e)
        {
          
            #region Заполнение сведений о супервизорах

            /*
            listBox1.BeginUpdate();
             * 
             */
            /*
            ArrayList supervisors = new ArrayList();

            
            var qr =
                from c in ds.Supervisor
                orderby c.name
                select new
                {
                  SupervisorID=c.supervisorID,
                  Name=c.name
                }
                ;

                    
           
            
            foreach (var d in qr)
            {
                supervisors.Add(new DisplaySupervisor(d.SupervisorID.ToString(),d.Name.ToString()));
            }
            */
           /*
            listBox1.DataSource = supervisors;
            listBox1.DisplayMember = "SupervisorName";
            listBox1.ValueMember = "SupervisorID";
            listBox1.EndUpdate();
            */
            //comboSupervisors.BeginUpdate();
            //comboSupervisors.DataSource = supervisors;

            bSourceSupervisor = new BindingSource();
            bSourceSupervisor.DataSource = ds.Supervisor;
            comboSupervisors.DataSource = bSourceSupervisor;

            comboSupervisors.DisplayMember = "name";
            comboSupervisors.ValueMember = "SupervisorID";
            //comboSupervisors.EndUpdate();
            #endregion

            bSource = new BindingSource();
            bSource.DataSource = ds.user;
            //bSource.Filter = "supervisorID=" + ((DisplaySupervisor)comboSupervisors.SelectedItem).SupervisorID.ToString();
            bSource.Filter = "supervisorID=" + comboSupervisors.SelectedValue.ToString();
            dataGridView1.DataSource = bSource;
            //dataGridView1.Columns[1].Visible = false;


            bSourceContracts = new BindingSource();
            bSourceContracts.DataSource = ds.contract;
            //bSourceContracts.Filter=;

            dataGridView2.DataSource = bSourceContracts;
            bSourceContracts.Filter = "userID="+ dataGridView1.CurrentRow.Cells[0].Value.ToString();

            dataGridView2.Columns[3].Visible = false;   
            
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
         //   MessageBox.Show(listBox1.SelectedValue.ToString());
        }

        private void comboSupervisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bSource.Filter = "supervisorID=" + comboSupervisors.SelectedValue.ToString();
            //MessageBox.Show(((DisplaySupervisor)comboSupervisors.SelectedItem).SupervisorID.ToString()  );
            //bSource.Filter = "supervisorID=" + ((DisplaySupervisor)comboSupervisors.SelectedItem).SupervisorID.ToString();
           // bSource.Filter = "supervisorID=" + comboSupervisors.SelectedValue.ToString();


        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "supervisorID")
            {
                //MessageBox.Show("!!!!");

              //  dataGridView1.Rows[e.RowIndex][0].
                //dataGridView1.Rows[e.RowIndex].Cells[1].Value = ((DisplaySupervisor)comboSupervisors.SelectedItem).SupervisorID;
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = comboSupervisors.SelectedValue;
                bSource.Filter = "supervisorID=" + comboSupervisors.SelectedValue.ToString();
                    /*
                if (e.FormattedValue == null &&
                    String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText =
                        "Company Name must not be empty";
                    e.Cancel = true;
                }
                    */
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bSource.EndEdit();
            ds.WriteXml(strDataFile);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                bSourceContracts.Filter = "userID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            catch (NullReferenceException ex)
            {

            }
        }

        private void UsersForm_Deactivate(object sender, EventArgs e)
        {
            //bSource.Filter = "";
            //bSourceContracts.Filter = "";
        }

        private void comboSupervisors_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                bSource.Filter = "supervisorID=" + comboSupervisors.SelectedValue.ToString();
            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void UsersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bSource.Filter = "";
            bSourceContracts.Filter = ""; 
            bSourceSupervisor.Filter = "";

        }

        

    
    }
}
