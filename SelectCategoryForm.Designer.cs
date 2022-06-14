namespace TradeAggregator
{
    partial class SelectCategoryForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeViewCategory = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnChoiseCategory = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.treeViewCategory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(12, 12, 0, 12);
            this.panel1.Size = new System.Drawing.Size(313, 362);
            this.panel1.TabIndex = 3;
            // 
            // treeViewCategory
            // 
            this.treeViewCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCategory.Location = new System.Drawing.Point(12, 12);
            this.treeViewCategory.Name = "treeViewCategory";
            this.treeViewCategory.Size = new System.Drawing.Size(301, 338);
            this.treeViewCategory.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChoiseCategory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(313, 0);
            this.panel2.MinimumSize = new System.Drawing.Size(50, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 12, 12, 12);
            this.panel2.Size = new System.Drawing.Size(149, 362);
            this.panel2.TabIndex = 4;
            // 
            // btnChoiseCategory
            // 
            this.btnChoiseCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoiseCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnChoiseCategory.Location = new System.Drawing.Point(54, 318);
            this.btnChoiseCategory.Name = "btnChoiseCategory";
            this.btnChoiseCategory.Size = new System.Drawing.Size(86, 32);
            this.btnChoiseCategory.TabIndex = 2;
            this.btnChoiseCategory.Text = "Выбрать";
            this.btnChoiseCategory.UseVisualStyleBackColor = true;
            this.btnChoiseCategory.Click += new System.EventHandler(this.btnChoiseCategory_Click);
            // 
            // SelectCategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 362);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SelectCategoryForm";
            this.ShowIcon = false;
            this.Text = "Выбор категории товаров";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectCategoryForm_FormClosing);
            this.Load += new System.EventHandler(this.FormSelectCategory_Load);
            this.Resize += new System.EventHandler(this.SelectCategoryForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeViewCategory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnChoiseCategory;
    }
}