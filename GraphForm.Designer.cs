namespace TradeAggregator
{
    partial class GraphForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сдвигДатыРасчётаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вВордToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вЭксельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excel2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewGraph = new System.Windows.Forms.DataGridView();
            this.KU_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vendor_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buyer_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buyer_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_calc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GraphStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GraphSumP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GraphSumN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Turnover = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Graph_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCancelCalc = new System.Windows.Forms.Button();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.buttonCalcAll = new System.Windows.Forms.Button();
            this.buttonCalcBonus = new System.Windows.Forms.Button();
            this.buttonApprove = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarForAsincBonus = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GraphRetro = new System.Windows.Forms.TabPage();
            this.GraphDocs = new System.Windows.Forms.TabPage();
            this.dataGridViewGraphDocs = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGraph)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.GraphRetro.SuspendLayout();
            this.GraphDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGraphDocs)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.экспортToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1215, 29);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сдвигДатыРасчётаToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(99, 25);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // сдвигДатыРасчётаToolStripMenuItem
            // 
            this.сдвигДатыРасчётаToolStripMenuItem.Name = "сдвигДатыРасчётаToolStripMenuItem";
            this.сдвигДатыРасчётаToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.сдвигДатыРасчётаToolStripMenuItem.Text = "Все настройки";
            // 
            // экспортToolStripMenuItem
            // 
            this.экспортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вВордToolStripMenuItem,
            this.вЭксельToolStripMenuItem,
            this.excel2ToolStripMenuItem});
            this.экспортToolStripMenuItem.Name = "экспортToolStripMenuItem";
            this.экспортToolStripMenuItem.Size = new System.Drawing.Size(65, 25);
            this.экспортToolStripMenuItem.Text = "Отчет";
            // 
            // вВордToolStripMenuItem
            // 
            this.вВордToolStripMenuItem.Name = "вВордToolStripMenuItem";
            this.вВордToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.вВордToolStripMenuItem.Text = "АКТ-счет";
            // 
            // вЭксельToolStripMenuItem
            // 
            this.вЭксельToolStripMenuItem.Name = "вЭксельToolStripMenuItem";
            this.вЭксельToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.вЭксельToolStripMenuItem.Text = "Отчет-сверка 1";
            // 
            // excel2ToolStripMenuItem
            // 
            this.excel2ToolStripMenuItem.Name = "excel2ToolStripMenuItem";
            this.excel2ToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.excel2ToolStripMenuItem.Text = "Отчет-сверка 2";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1203, 479);
            this.panel1.TabIndex = 3;
            // 
            // dataGridViewGraph
            // 
            this.dataGridViewGraph.AllowUserToAddRows = false;
            this.dataGridViewGraph.AllowUserToDeleteRows = false;
            this.dataGridViewGraph.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGraph.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KU_id,
            this.Vendor_id,
            this.Buyer_Id,
            this.Buyer_Name,
            this.ContractCode,
            this.Percent,
            this.Period,
            this.Date_from,
            this.Date_to,
            this.Date_calc,
            this.GraphStatus,
            this.GraphSumP,
            this.GraphSumN,
            this.Turnover,
            this.Graph_Id});
            this.dataGridViewGraph.Location = new System.Drawing.Point(-45, 0);
            this.dataGridViewGraph.Name = "dataGridViewGraph";
            this.dataGridViewGraph.ReadOnly = true;
            this.dataGridViewGraph.Size = new System.Drawing.Size(1231, 451);
            this.dataGridViewGraph.TabIndex = 0;
            // 
            // KU_id
            // 
            this.KU_id.HeaderText = "Номер коммерческого условия";
            this.KU_id.Name = "KU_id";
            this.KU_id.ReadOnly = true;
            // 
            // Vendor_id
            // 
            this.Vendor_id.HeaderText = "Номер поставщика";
            this.Vendor_id.Name = "Vendor_id";
            this.Vendor_id.ReadOnly = true;
            this.Vendor_id.Visible = false;
            // 
            // Buyer_Id
            // 
            this.Buyer_Id.HeaderText = "Номер торговой компании";
            this.Buyer_Id.Name = "Buyer_Id";
            this.Buyer_Id.ReadOnly = true;
            this.Buyer_Id.Visible = false;
            // 
            // Buyer_Name
            // 
            this.Buyer_Name.HeaderText = "Покупатель (Торговая сеть)";
            this.Buyer_Name.Name = "Buyer_Name";
            this.Buyer_Name.ReadOnly = true;
            // 
            // ContractCode
            // 
            this.ContractCode.HeaderText = "Номер договора";
            this.ContractCode.Name = "ContractCode";
            this.ContractCode.ReadOnly = true;
            // 
            // Percent
            // 
            this.Percent.HeaderText = "Процент бонуса, %";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            // 
            // Period
            // 
            this.Period.HeaderText = "Период";
            this.Period.Name = "Period";
            this.Period.ReadOnly = true;
            // 
            // Date_from
            // 
            this.Date_from.HeaderText = "Дата начала";
            this.Date_from.Name = "Date_from";
            this.Date_from.ReadOnly = true;
            // 
            // Date_to
            // 
            this.Date_to.HeaderText = "Дата конца";
            this.Date_to.Name = "Date_to";
            this.Date_to.ReadOnly = true;
            // 
            // Date_calc
            // 
            this.Date_calc.HeaderText = "Дата расчёта";
            this.Date_calc.Name = "Date_calc";
            this.Date_calc.ReadOnly = true;
            // 
            // GraphStatus
            // 
            this.GraphStatus.HeaderText = "Статус";
            this.GraphStatus.Name = "GraphStatus";
            this.GraphStatus.ReadOnly = true;
            // 
            // GraphSumP
            // 
            this.GraphSumP.HeaderText = "Сумма премии, руб.";
            this.GraphSumP.Name = "GraphSumP";
            this.GraphSumP.ReadOnly = true;
            // 
            // GraphSumN
            // 
            this.GraphSumN.HeaderText = "Сумма по накладным, руб.";
            this.GraphSumN.Name = "GraphSumN";
            this.GraphSumN.ReadOnly = true;
            // 
            // Turnover
            // 
            this.Turnover.HeaderText = "Товарооборот, шт.";
            this.Turnover.Name = "Turnover";
            this.Turnover.ReadOnly = true;
            // 
            // Graph_Id
            // 
            this.Graph_Id.HeaderText = "Graph_Id";
            this.Graph_Id.Name = "Graph_Id";
            this.Graph_Id.ReadOnly = true;
            this.Graph_Id.Visible = false;
            // 
            // buttonCancelCalc
            // 
            this.buttonCancelCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancelCalc.Location = new System.Drawing.Point(1028, 538);
            this.buttonCancelCalc.Name = "buttonCancelCalc";
            this.buttonCancelCalc.Size = new System.Drawing.Size(159, 38);
            this.buttonCancelCalc.TabIndex = 62;
            this.buttonCancelCalc.Text = "Отменить расчёт";
            this.buttonCancelCalc.UseVisualStyleBackColor = true;
            // 
            // labelTo
            // 
            this.labelTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTo.Location = new System.Drawing.Point(220, 543);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(30, 20);
            this.labelTo.TabIndex = 67;
            this.labelTo.Text = "По";
            // 
            // labelFrom
            // 
            this.labelFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFrom.AutoSize = true;
            this.labelFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFrom.Location = new System.Drawing.Point(4, 543);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(20, 20);
            this.labelFrom.TabIndex = 66;
            this.labelFrom.Text = "С";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFrom.Location = new System.Drawing.Point(30, 543);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(183, 24);
            this.dateTimePickerFrom.TabIndex = 63;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerTo.Location = new System.Drawing.Point(256, 543);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(183, 24);
            this.dateTimePickerTo.TabIndex = 64;
            // 
            // buttonCalcAll
            // 
            this.buttonCalcAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCalcAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalcAll.Location = new System.Drawing.Point(158, 573);
            this.buttonCalcAll.Name = "buttonCalcAll";
            this.buttonCalcAll.Size = new System.Drawing.Size(159, 38);
            this.buttonCalcAll.TabIndex = 65;
            this.buttonCalcAll.Text = "Рассчитать все";
            this.buttonCalcAll.UseVisualStyleBackColor = true;
            // 
            // buttonCalcBonus
            // 
            this.buttonCalcBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalcBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalcBonus.Location = new System.Drawing.Point(735, 538);
            this.buttonCalcBonus.Name = "buttonCalcBonus";
            this.buttonCalcBonus.Size = new System.Drawing.Size(162, 38);
            this.buttonCalcBonus.TabIndex = 60;
            this.buttonCalcBonus.Text = "Расчёт премии";
            this.buttonCalcBonus.UseVisualStyleBackColor = true;
            // 
            // buttonApprove
            // 
            this.buttonApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonApprove.Location = new System.Drawing.Point(903, 538);
            this.buttonApprove.Name = "buttonApprove";
            this.buttonApprove.Size = new System.Drawing.Size(119, 38);
            this.buttonApprove.TabIndex = 61;
            this.buttonApprove.Text = "Согласовать";
            this.buttonApprove.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Номер коммерческого условия";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Номер поставщика";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Номер торговой компании";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Покупатель (Торговая сеть)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Номер договора";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Процент бонуса, %";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Период";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Дата начала";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Дата конца";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Дата расчёта";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Статус";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Сумма премии, руб.";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Сумма по накладным, руб.";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Товарооборот, шт.";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "Graph_Id";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Visible = false;
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.BackColor = System.Drawing.Color.Gainsboro;
            this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProgress.Location = new System.Drawing.Point(832, 6);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(20, 16);
            this.labelProgress.TabIndex = 68;
            this.labelProgress.Text = "%";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelProgress.Visible = false;
            // 
            // progressBarForAsincBonus
            // 
            this.progressBarForAsincBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarForAsincBonus.Location = new System.Drawing.Point(877, 6);
            this.progressBarForAsincBonus.Name = "progressBarForAsincBonus";
            this.progressBarForAsincBonus.Size = new System.Drawing.Size(323, 23);
            this.progressBarForAsincBonus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarForAsincBonus.TabIndex = 69;
            this.progressBarForAsincBonus.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.GraphRetro);
            this.tabControl1.Controls.Add(this.GraphDocs);
            this.tabControl1.Location = new System.Drawing.Point(3, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 473);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
           
            // GraphRetro
            // 
            this.GraphRetro.Controls.Add(this.dataGridViewGraph);
            this.GraphRetro.Location = new System.Drawing.Point(4, 22);
            this.GraphRetro.Name = "GraphRetro";
            this.GraphRetro.Padding = new System.Windows.Forms.Padding(3);
            this.GraphRetro.Size = new System.Drawing.Size(1192, 447);
            this.GraphRetro.TabIndex = 0;
            this.GraphRetro.Text = "График выплат премий";
            this.GraphRetro.UseVisualStyleBackColor = true;
            // 
            // GraphDocs
            // 
            this.GraphDocs.Controls.Add(this.dataGridViewGraphDocs);
            this.GraphDocs.Location = new System.Drawing.Point(4, 22);
            this.GraphDocs.Name = "GraphDocs";
            this.GraphDocs.Padding = new System.Windows.Forms.Padding(3);
            this.GraphDocs.Size = new System.Drawing.Size(1192, 447);
            this.GraphDocs.TabIndex = 1;
            this.GraphDocs.Text = "График договоров";
            this.GraphDocs.UseVisualStyleBackColor = true;
            // 
            // dataGridViewGraphDocs
            // 
            this.dataGridViewGraphDocs.AllowUserToAddRows = false;
            this.dataGridViewGraphDocs.AllowUserToDeleteRows = false;
            this.dataGridViewGraphDocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGraphDocs.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewGraphDocs.Name = "dataGridViewGraphDocs";
            this.dataGridViewGraphDocs.ReadOnly = true;
            this.dataGridViewGraphDocs.Size = new System.Drawing.Size(1192, 451);
            this.dataGridViewGraphDocs.TabIndex = 0;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 656);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBarForAsincBonus);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.buttonCancelCalc);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.buttonCalcAll);
            this.Controls.Add(this.buttonCalcBonus);
            this.Controls.Add(this.buttonApprove);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.Resize += new System.EventHandler(this.KUGraphForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGraph)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.GraphRetro.ResumeLayout(false);
            this.GraphDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGraphDocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сдвигДатыРасчётаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вВордToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вЭксельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excel2ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewGraph;
        private System.Windows.Forms.Button buttonCancelCalc;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button buttonCalcAll;
        private System.Windows.Forms.Button buttonCalcBonus;
        private System.Windows.Forms.Button buttonApprove;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn KU_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vendor_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buyer_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buyer_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContractCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_calc;
        private System.Windows.Forms.DataGridViewTextBoxColumn GraphStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GraphSumP;
        private System.Windows.Forms.DataGridViewTextBoxColumn GraphSumN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Turnover;
        private System.Windows.Forms.DataGridViewTextBoxColumn Graph_Id;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ProgressBar progressBarForAsincBonus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage GraphRetro;
        private System.Windows.Forms.TabPage GraphDocs;
        private System.Windows.Forms.DataGridView dataGridViewGraphDocs;
    }
}