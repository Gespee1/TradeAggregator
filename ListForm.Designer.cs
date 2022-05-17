namespace TradeAggregator
{
    partial class ListForm
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
            this.buttonAllProd = new System.Windows.Forms.Button();
            this.extendedDataGridView1 = new EDGV.ExtendedDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.extendedDataGridView1)).BeginInit();
            this.labelHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAllProd
            // 
            this.buttonAllProd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAllProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAllProd.Location = new System.Drawing.Point(624, 73);
            this.buttonAllProd.Name = "buttonAllProd";
            this.buttonAllProd.Size = new System.Drawing.Size(143, 30);
            this.buttonAllProd.TabIndex = 13;
            this.buttonAllProd.Text = "Все товары";
            this.buttonAllProd.UseVisualStyleBackColor = true;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeader.Location = new System.Drawing.Point(274, 9);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(99, 20);
            this.labelHeader.TabIndex = 14;
            this.labelHeader.Text = "Заголовок";
            // 
            // extendedDataGridView1
            // 
            this.extendedDataGridView1.AutoGenerateContextFilters = true;
            this.extendedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extendedDataGridView1.DateWithTime = false;
            this.extendedDataGridView1.Location = new System.Drawing.Point(102, 126);
            this.extendedDataGridView1.Name = "extendedDataGridView1";
            this.extendedDataGridView1.Size = new System.Drawing.Size(561, 269);
            this.extendedDataGridView1.TabIndex = 14;
            this.extendedDataGridView1.TimeFilter = false;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.extendedDataGridView1);
            this.Controls.Add(this.buttonAllProd);
            this.Name = "ListForm";
            this.ShowIcon = false;
            this.Text = "Ассортимент";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Assortment_FormClosing);
            this.Load += new System.EventHandler(this.AssortmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.extendedDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAllProd;
        private System.Windows.Forms.Label labelHeader;
        private EDGV.ExtendedDataGridView extendedDataGridView1;
    }
}