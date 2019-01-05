namespace Corporation
{
    partial class UsersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.supervisorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.corporationDataSet = new Corporation.CorporationDataSet();
            this.corporationDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supervisorBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.comboSupervisors = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.supervisorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corporationDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corporationDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supervisorBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // supervisorBindingSource
            // 
            this.supervisorBindingSource.DataMember = "Supervisor";
            this.supervisorBindingSource.DataSource = this.corporationDataSet;
            // 
            // corporationDataSet
            // 
            this.corporationDataSet.DataSetName = "CorporationDataSet";
            this.corporationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // corporationDataSetBindingSource
            // 
            this.corporationDataSetBindingSource.DataSource = this.corporationDataSet;
            this.corporationDataSetBindingSource.Position = 0;
            // 
            // supervisorBindingSource1
            // 
            this.supervisorBindingSource1.DataMember = "Supervisor";
            this.supervisorBindingSource1.DataSource = this.corporationDataSet;
            // 
            // comboSupervisors
            // 
            this.comboSupervisors.FormattingEnabled = true;
            this.comboSupervisors.Location = new System.Drawing.Point(12, 12);
            this.comboSupervisors.Name = "comboSupervisors";
            this.comboSupervisors.Size = new System.Drawing.Size(374, 21);
            this.comboSupervisors.TabIndex = 3;
            this.comboSupervisors.SelectedIndexChanged += new System.EventHandler(this.comboSupervisors_SelectedIndexChanged);
            this.comboSupervisors.SelectedValueChanged += new System.EventHandler(this.comboSupervisors_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(747, 280);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // userBindingSource
            // 
            this.userBindingSource.DataMember = "user";
            this.userBindingSource.DataSource = this.corporationDataSetBindingSource;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(684, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Enabled = false;
            this.dataGridView2.Location = new System.Drawing.Point(12, 356);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(583, 188);
            this.dataGridView2.TabIndex = 6;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 556);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboSupervisors);
            this.Name = "UsersForm";
            this.Text = "Users";
            this.Deactivate += new System.EventHandler(this.UsersForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UsersForm_FormClosed);
            this.Load += new System.EventHandler(this.UsersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.supervisorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corporationDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corporationDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supervisorBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource supervisorBindingSource;
        private CorporationDataSet corporationDataSet;
        private System.Windows.Forms.BindingSource supervisorBindingSource1;
        private System.Windows.Forms.BindingSource corporationDataSetBindingSource;
        private System.Windows.Forms.ComboBox comboSupervisors;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource userBindingSource;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}