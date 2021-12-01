namespace LifestyleTrader
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_chart = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_state = new System.Windows.Forms.TextBox();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.txt_perf = new System.Windows.Forms.TextBox();
            this.cmb_mode = new System.Windows.Forms.ComboBox();
            this.dtpicker_st = new System.Windows.Forms.DateTimePicker();
            this.dtpicker_en = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_symbol = new System.Windows.Forms.ComboBox();
            this.btn_eval = new System.Windows.Forms.Button();
            this.panel_eval = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listView_pos = new System.Windows.Forms.ListView();
            this.col_Symbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_Cmd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_Lots = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_OpenPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_OpenTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_eval = new System.Windows.Forms.ListView();
            this.col_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_his = new System.Windows.Forms.ListView();
            this.col_symbol_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_command_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_lots_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_price_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_time_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel_eval.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(342, 17);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(342, 45);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_chart
            // 
            this.btn_chart.Location = new System.Drawing.Point(313, 74);
            this.btn_chart.Name = "btn_chart";
            this.btn_chart.Size = new System.Drawing.Size(104, 23);
            this.btn_chart.TabIndex = 2;
            this.btn_chart.Text = "Connect Chart";
            this.btn_chart.UseVisualStyleBackColor = true;
            this.btn_chart.Click += new System.EventHandler(this.btn_chart_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(342, 145);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 3;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "MODE :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Performance :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Start :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "End :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "State :";
            // 
            // txt_state
            // 
            this.txt_state.Location = new System.Drawing.Point(76, 67);
            this.txt_state.Name = "txt_state";
            this.txt_state.ReadOnly = true;
            this.txt_state.Size = new System.Drawing.Size(200, 20);
            this.txt_state.TabIndex = 9;
            // 
            // txt_log
            // 
            this.txt_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_log.Location = new System.Drawing.Point(12, 145);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.Size = new System.Drawing.Size(405, 378);
            this.txt_log.TabIndex = 10;
            // 
            // txt_perf
            // 
            this.txt_perf.Location = new System.Drawing.Point(76, 42);
            this.txt_perf.Name = "txt_perf";
            this.txt_perf.ReadOnly = true;
            this.txt_perf.Size = new System.Drawing.Size(200, 20);
            this.txt_perf.TabIndex = 11;
            // 
            // cmb_mode
            // 
            this.cmb_mode.FormattingEnabled = true;
            this.cmb_mode.Location = new System.Drawing.Point(76, 17);
            this.cmb_mode.Name = "cmb_mode";
            this.cmb_mode.Size = new System.Drawing.Size(100, 21);
            this.cmb_mode.TabIndex = 12;
            // 
            // dtpicker_st
            // 
            this.dtpicker_st.Location = new System.Drawing.Point(76, 93);
            this.dtpicker_st.Name = "dtpicker_st";
            this.dtpicker_st.Size = new System.Drawing.Size(200, 20);
            this.dtpicker_st.TabIndex = 13;
            // 
            // dtpicker_en
            // 
            this.dtpicker_en.Location = new System.Drawing.Point(76, 119);
            this.dtpicker_en.Name = "dtpicker_en";
            this.dtpicker_en.Size = new System.Drawing.Size(200, 20);
            this.dtpicker_en.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Symbol :";
            // 
            // cmb_symbol
            // 
            this.cmb_symbol.FormattingEnabled = true;
            this.cmb_symbol.Location = new System.Drawing.Point(234, 17);
            this.cmb_symbol.Name = "cmb_symbol";
            this.cmb_symbol.Size = new System.Drawing.Size(102, 21);
            this.cmb_symbol.TabIndex = 16;
            // 
            // btn_eval
            // 
            this.btn_eval.Location = new System.Drawing.Point(326, 109);
            this.btn_eval.Name = "btn_eval";
            this.btn_eval.Size = new System.Drawing.Size(91, 23);
            this.btn_eval.TabIndex = 17;
            this.btn_eval.Text = "Evaluation >>";
            this.btn_eval.UseVisualStyleBackColor = true;
            this.btn_eval.Click += new System.EventHandler(this.btn_eval_Click);
            // 
            // panel_eval
            // 
            this.panel_eval.Controls.Add(this.label9);
            this.panel_eval.Controls.Add(this.label8);
            this.panel_eval.Controls.Add(this.label7);
            this.panel_eval.Controls.Add(this.listView_pos);
            this.panel_eval.Controls.Add(this.listView_eval);
            this.panel_eval.Controls.Add(this.listView_his);
            this.panel_eval.Location = new System.Drawing.Point(423, 17);
            this.panel_eval.Name = "panel_eval";
            this.panel_eval.Size = new System.Drawing.Size(642, 506);
            this.panel_eval.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(461, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Evaluation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Transactions";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Opened Positions";
            // 
            // listView_pos
            // 
            this.listView_pos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_Symbol,
            this.col_Cmd,
            this.col_Lots,
            this.col_OpenPrice,
            this.col_OpenTime});
            this.listView_pos.GridLines = true;
            this.listView_pos.HideSelection = false;
            this.listView_pos.Location = new System.Drawing.Point(0, 25);
            this.listView_pos.Name = "listView_pos";
            this.listView_pos.Size = new System.Drawing.Size(458, 169);
            this.listView_pos.TabIndex = 2;
            this.listView_pos.UseCompatibleStateImageBehavior = false;
            this.listView_pos.View = System.Windows.Forms.View.Details;
            // 
            // col_Symbol
            // 
            this.col_Symbol.Text = "Symbol";
            // 
            // col_Cmd
            // 
            this.col_Cmd.Text = "Command";
            // 
            // col_Lots
            // 
            this.col_Lots.Text = "Lots";
            // 
            // col_OpenPrice
            // 
            this.col_OpenPrice.Text = "OpenPrice";
            // 
            // col_OpenTime
            // 
            this.col_OpenTime.Text = "OpenTime";
            this.col_OpenTime.Width = 120;
            // 
            // listView_eval
            // 
            this.listView_eval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_eval.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_name,
            this.col_value});
            this.listView_eval.GridLines = true;
            this.listView_eval.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_eval.HideSelection = false;
            this.listView_eval.Location = new System.Drawing.Point(464, 25);
            this.listView_eval.Name = "listView_eval";
            this.listView_eval.Size = new System.Drawing.Size(164, 169);
            this.listView_eval.TabIndex = 1;
            this.listView_eval.UseCompatibleStateImageBehavior = false;
            this.listView_eval.View = System.Windows.Forms.View.Details;
            // 
            // col_name
            // 
            this.col_name.Width = 80;
            // 
            // col_value
            // 
            this.col_value.Width = 80;
            // 
            // listView_his
            // 
            this.listView_his.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_his.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_symbol_,
            this.col_command_,
            this.col_lots_,
            this.col_price_,
            this.col_time_});
            this.listView_his.GridLines = true;
            this.listView_his.HideSelection = false;
            this.listView_his.Location = new System.Drawing.Point(0, 216);
            this.listView_his.Name = "listView_his";
            this.listView_his.Size = new System.Drawing.Size(628, 290);
            this.listView_his.TabIndex = 0;
            this.listView_his.UseCompatibleStateImageBehavior = false;
            this.listView_his.View = System.Windows.Forms.View.Details;
            // 
            // col_symbol_
            // 
            this.col_symbol_.Text = "Symbol";
            // 
            // col_command_
            // 
            this.col_command_.Text = "Command";
            // 
            // col_lots_
            // 
            this.col_lots_.Text = "Lots";
            // 
            // col_price_
            // 
            this.col_price_.Text = "Price";
            // 
            // col_time_
            // 
            this.col_time_.Text = "Time";
            this.col_time_.Width = 120;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 535);
            this.Controls.Add(this.panel_eval);
            this.Controls.Add(this.btn_eval);
            this.Controls.Add(this.cmb_symbol);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.dtpicker_en);
            this.Controls.Add(this.dtpicker_st);
            this.Controls.Add(this.cmb_mode);
            this.Controls.Add(this.txt_perf);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.txt_state);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_chart);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Name = "Form1";
            this.Text = "LifestyleTrader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel_eval.ResumeLayout(false);
            this.panel_eval.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_chart;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_state;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.TextBox txt_perf;
        private System.Windows.Forms.ComboBox cmb_mode;
        private System.Windows.Forms.DateTimePicker dtpicker_st;
        private System.Windows.Forms.DateTimePicker dtpicker_en;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_symbol;
        private System.Windows.Forms.Button btn_eval;
        private System.Windows.Forms.Panel panel_eval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView_pos;
        private System.Windows.Forms.ListView listView_eval;
        private System.Windows.Forms.ListView listView_his;
        private System.Windows.Forms.ColumnHeader col_Symbol;
        private System.Windows.Forms.ColumnHeader col_Cmd;
        private System.Windows.Forms.ColumnHeader col_Lots;
        private System.Windows.Forms.ColumnHeader col_OpenPrice;
        private System.Windows.Forms.ColumnHeader col_OpenTime;
        private System.Windows.Forms.ColumnHeader col_symbol_;
        private System.Windows.Forms.ColumnHeader col_command_;
        private System.Windows.Forms.ColumnHeader col_lots_;
        private System.Windows.Forms.ColumnHeader col_price_;
        private System.Windows.Forms.ColumnHeader col_time_;
        private System.Windows.Forms.ColumnHeader col_name;
        private System.Windows.Forms.ColumnHeader col_value;
    }
}

