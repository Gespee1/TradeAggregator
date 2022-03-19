
namespace TradeAggregator
{
    partial class AccountForm
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
            this.labelMain = new System.Windows.Forms.Label();
            this.buttonProfile = new System.Windows.Forms.Button();
            this.buttonVendors = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonKP = new System.Windows.Forms.Button();
            this.buttonContracts = new System.Windows.Forms.Button();
            this.buttonGraph = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMain
            // 
            this.labelMain.AutoSize = true;
            this.labelMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMain.Location = new System.Drawing.Point(32, 21);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(230, 20);
            this.labelMain.TabIndex = 1;
            this.labelMain.Text = "Вы авторизовались как ...";
            // 
            // buttonProfile
            // 
            this.buttonProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProfile.Location = new System.Drawing.Point(12, 253);
            this.buttonProfile.Name = "buttonProfile";
            this.buttonProfile.Size = new System.Drawing.Size(240, 30);
            this.buttonProfile.TabIndex = 6;
            this.buttonProfile.Text = "Профиль";
            this.buttonProfile.UseVisualStyleBackColor = true;
            // 
            // buttonVendors
            // 
            this.buttonVendors.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonVendors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonVendors.Location = new System.Drawing.Point(12, 289);
            this.buttonVendors.Name = "buttonVendors";
            this.buttonVendors.Size = new System.Drawing.Size(240, 30);
            this.buttonVendors.TabIndex = 7;
            this.buttonVendors.Text = "Поставщики";
            this.buttonVendors.UseVisualStyleBackColor = true;
            // 
            // buttonOrder
            // 
            this.buttonOrder.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOrder.Location = new System.Drawing.Point(12, 325);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(240, 30);
            this.buttonOrder.TabIndex = 8;
            this.buttonOrder.Text = "Создать заказ";
            this.buttonOrder.UseVisualStyleBackColor = true;
            // 
            // buttonKP
            // 
            this.buttonKP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonKP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonKP.Location = new System.Drawing.Point(12, 361);
            this.buttonKP.Name = "buttonKP";
            this.buttonKP.Size = new System.Drawing.Size(240, 30);
            this.buttonKP.TabIndex = 9;
            this.buttonKP.Text = "Коммерческие предложения";
            this.buttonKP.UseVisualStyleBackColor = true;
            // 
            // buttonContracts
            // 
            this.buttonContracts.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonContracts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonContracts.Location = new System.Drawing.Point(12, 397);
            this.buttonContracts.Name = "buttonContracts";
            this.buttonContracts.Size = new System.Drawing.Size(240, 30);
            this.buttonContracts.TabIndex = 10;
            this.buttonContracts.Text = "Договоры";
            this.buttonContracts.UseVisualStyleBackColor = true;
            // 
            // buttonGraph
            // 
            this.buttonGraph.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGraph.Location = new System.Drawing.Point(12, 433);
            this.buttonGraph.Name = "buttonGraph";
            this.buttonGraph.Size = new System.Drawing.Size(240, 30);
            this.buttonGraph.TabIndex = 11;
            this.buttonGraph.Text = "Статус договоров";
            this.buttonGraph.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 475);
            this.Controls.Add(this.buttonGraph);
            this.Controls.Add(this.buttonContracts);
            this.Controls.Add(this.buttonKP);
            this.Controls.Add(this.buttonOrder);
            this.Controls.Add(this.buttonVendors);
            this.Controls.Add(this.buttonProfile);
            this.Controls.Add(this.labelMain);
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.Text = "Личный кабинет";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.Button buttonProfile;
        private System.Windows.Forms.Button buttonVendors;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonKP;
        private System.Windows.Forms.Button buttonContracts;
        private System.Windows.Forms.Button buttonGraph;
    }
}