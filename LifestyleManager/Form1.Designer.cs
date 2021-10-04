namespace LifestyleManager
{
    partial class Form1
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
            this.btn_connectDB = new System.Windows.Forms.Button();
            this.btn_connectIB = new System.Windows.Forms.Button();
            this.btn_downRate = new System.Windows.Forms.Button();
            this.btn_loadRate = new System.Windows.Forms.Button();
            this.btn_cur = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_symbol = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker_st = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_en = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_debug = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.col_t = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_o = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_h = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_l = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_c = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btn_connectDB
            // 
            this.btn_connectDB.Location = new System.Drawing.Point(12, 12);
            this.btn_connectDB.Name = "btn_connectDB";
            this.btn_connectDB.Size = new System.Drawing.Size(193, 23);
            this.btn_connectDB.TabIndex = 0;
            this.btn_connectDB.Text = "Connect DB";
            this.btn_connectDB.UseVisualStyleBackColor = true;
            this.btn_connectDB.Click += new System.EventHandler(this.btn_connectDB_Click);
            // 
            // btn_connectIB
            // 
            this.btn_connectIB.Location = new System.Drawing.Point(263, 12);
            this.btn_connectIB.Name = "btn_connectIB";
            this.btn_connectIB.Size = new System.Drawing.Size(194, 23);
            this.btn_connectIB.TabIndex = 1;
            this.btn_connectIB.Text = "Connect IB";
            this.btn_connectIB.UseVisualStyleBackColor = true;
            this.btn_connectIB.Click += new System.EventHandler(this.btn_connectIB_Click);
            // 
            // btn_downRate
            // 
            this.btn_downRate.Location = new System.Drawing.Point(12, 41);
            this.btn_downRate.Name = "btn_downRate";
            this.btn_downRate.Size = new System.Drawing.Size(193, 23);
            this.btn_downRate.TabIndex = 2;
            this.btn_downRate.Text = "Down Rates";
            this.btn_downRate.UseVisualStyleBackColor = true;
            this.btn_downRate.Click += new System.EventHandler(this.btn_downRate_Click);
            // 
            // btn_loadRate
            // 
            this.btn_loadRate.Location = new System.Drawing.Point(263, 41);
            this.btn_loadRate.Name = "btn_loadRate";
            this.btn_loadRate.Size = new System.Drawing.Size(194, 23);
            this.btn_loadRate.TabIndex = 3;
            this.btn_loadRate.Text = "Load Rates";
            this.btn_loadRate.UseVisualStyleBackColor = true;
            this.btn_loadRate.Click += new System.EventHandler(this.btn_loadRate_Click);
            // 
            // btn_cur
            // 
            this.btn_cur.Location = new System.Drawing.Point(380, 129);
            this.btn_cur.Name = "btn_cur";
            this.btn_cur.Size = new System.Drawing.Size(75, 23);
            this.btn_cur.TabIndex = 4;
            this.btn_cur.Text = "Current";
            this.btn_cur.UseVisualStyleBackColor = true;
            this.btn_cur.Click += new System.EventHandler(this.btn_cur_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Instrument";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "TimeFrame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Start";
            // 
            // cmb_symbol
            // 
            this.cmb_symbol.FormattingEnabled = true;
            this.cmb_symbol.Location = new System.Drawing.Point(84, 81);
            this.cmb_symbol.Name = "cmb_symbol";
            this.cmb_symbol.Size = new System.Drawing.Size(121, 21);
            this.cmb_symbol.TabIndex = 8;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(336, 81);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // dateTimePicker_st
            // 
            this.dateTimePicker_st.Location = new System.Drawing.Point(84, 108);
            this.dateTimePicker_st.Name = "dateTimePicker_st";
            this.dateTimePicker_st.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_st.TabIndex = 10;
            // 
            // dateTimePicker_en
            // 
            this.dateTimePicker_en.Location = new System.Drawing.Point(84, 132);
            this.dateTimePicker_en.Name = "dateTimePicker_en";
            this.dateTimePicker_en.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_en.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "End";
            // 
            // txt_debug
            // 
            this.txt_debug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_debug.Location = new System.Drawing.Point(307, 108);
            this.txt_debug.Name = "txt_debug";
            this.txt_debug.ReadOnly = true;
            this.txt_debug.Size = new System.Drawing.Size(150, 13);
            this.txt_debug.TabIndex = 13;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_t,
            this.col_o,
            this.col_h,
            this.col_l,
            this.col_c});
            this.listView1.Location = new System.Drawing.Point(12, 158);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(445, 329);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // col_t
            // 
            this.col_t.Text = "Time";
            this.col_t.Width = 140;
            // 
            // col_o
            // 
            this.col_o.Text = "Open";
            this.col_o.Width = 70;
            // 
            // col_h
            // 
            this.col_h.Text = "High";
            this.col_h.Width = 70;
            // 
            // col_l
            // 
            this.col_l.Text = "Low";
            this.col_l.Width = 70;
            // 
            // col_c
            // 
            this.col_c.Text = "Close";
            this.col_c.Width = 70;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 499);
            this.Controls.Add(this.txt_debug);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.dateTimePicker_en);
            this.Controls.Add(this.dateTimePicker_st);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.cmb_symbol);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cur);
            this.Controls.Add(this.btn_loadRate);
            this.Controls.Add(this.btn_downRate);
            this.Controls.Add(this.btn_connectIB);
            this.Controls.Add(this.btn_connectDB);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_connectDB;
        private System.Windows.Forms.Button btn_connectIB;
        private System.Windows.Forms.Button btn_downRate;
        private System.Windows.Forms.Button btn_loadRate;
        private System.Windows.Forms.Button btn_cur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_symbol;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_st;
        private System.Windows.Forms.DateTimePicker dateTimePicker_en;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_debug;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader col_t;
        private System.Windows.Forms.ColumnHeader col_o;
        private System.Windows.Forms.ColumnHeader col_h;
        private System.Windows.Forms.ColumnHeader col_l;
        private System.Windows.Forms.ColumnHeader col_c;
    }
}

