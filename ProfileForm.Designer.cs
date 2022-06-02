
namespace TradeAggregator
{
    partial class ProfileForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxRespPerson = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUrName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxINN = new System.Windows.Forms.TextBox();
            this.textBoxKPP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDirector = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxUrAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxBankName = new System.Windows.Forms.TextBox();
            this.textBoxBankBIK = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxBankAccount = new System.Windows.Forms.TextBox();
            this.textBoxCorrAccount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAddRespPerson = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Код профиля";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Enabled = false;
            this.textBoxCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCode.Location = new System.Drawing.Point(131, 34);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(127, 26);
            this.textBoxCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(280, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ответственное лицо";
            // 
            // comboBoxRespPerson
            // 
            this.comboBoxRespPerson.Enabled = false;
            this.comboBoxRespPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRespPerson.FormattingEnabled = true;
            this.comboBoxRespPerson.Location = new System.Drawing.Point(455, 34);
            this.comboBoxRespPerson.Name = "comboBoxRespPerson";
            this.comboBoxRespPerson.Size = new System.Drawing.Size(301, 26);
            this.comboBoxRespPerson.TabIndex = 3;
            this.comboBoxRespPerson.Leave += new System.EventHandler(this.comboBoxRespPerson_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Наименование";
            // 
            // textBoxName
            // 
            this.textBoxName.Enabled = false;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(245, 80);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(330, 26);
            this.textBoxName.TabIndex = 5;
            this.textBoxName.Leave += new System.EventHandler(this.textBoxName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Юридическое наименование";
            // 
            // textBoxUrName
            // 
            this.textBoxUrName.Enabled = false;
            this.textBoxUrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUrName.Location = new System.Drawing.Point(245, 112);
            this.textBoxUrName.Name = "textBoxUrName";
            this.textBoxUrName.Size = new System.Drawing.Size(330, 26);
            this.textBoxUrName.TabIndex = 7;
            this.textBoxUrName.Leave += new System.EventHandler(this.textBoxUrName_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "ИНН";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(388, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "КПП";
            // 
            // textBoxINN
            // 
            this.textBoxINN.Enabled = false;
            this.textBoxINN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxINN.Location = new System.Drawing.Point(245, 144);
            this.textBoxINN.Name = "textBoxINN";
            this.textBoxINN.Size = new System.Drawing.Size(127, 26);
            this.textBoxINN.TabIndex = 10;
            this.textBoxINN.Leave += new System.EventHandler(this.textBoxINN_Leave);
            // 
            // textBoxKPP
            // 
            this.textBoxKPP.Enabled = false;
            this.textBoxKPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxKPP.Location = new System.Drawing.Point(448, 144);
            this.textBoxKPP.Name = "textBoxKPP";
            this.textBoxKPP.Size = new System.Drawing.Size(127, 26);
            this.textBoxKPP.TabIndex = 11;
            this.textBoxKPP.Leave += new System.EventHandler(this.textBoxKPP_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "ФИО директора";
            // 
            // textBoxDirector
            // 
            this.textBoxDirector.Enabled = false;
            this.textBoxDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDirector.Location = new System.Drawing.Point(245, 176);
            this.textBoxDirector.Name = "textBoxDirector";
            this.textBoxDirector.Size = new System.Drawing.Size(330, 26);
            this.textBoxDirector.TabIndex = 13;
            this.textBoxDirector.Leave += new System.EventHandler(this.textBoxDirector_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(12, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Юридический адрес";
            // 
            // textBoxUrAddress
            // 
            this.textBoxUrAddress.Enabled = false;
            this.textBoxUrAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUrAddress.Location = new System.Drawing.Point(245, 208);
            this.textBoxUrAddress.Name = "textBoxUrAddress";
            this.textBoxUrAddress.Size = new System.Drawing.Size(330, 26);
            this.textBoxUrAddress.TabIndex = 15;
            this.textBoxUrAddress.Leave += new System.EventHandler(this.textBoxUrAddress_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(12, 245);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Наименование банка";
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.Enabled = false;
            this.textBoxBankName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBankName.Location = new System.Drawing.Point(245, 240);
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.Size = new System.Drawing.Size(330, 26);
            this.textBoxBankName.TabIndex = 17;
            this.textBoxBankName.Leave += new System.EventHandler(this.textBoxBankName_Leave);
            // 
            // textBoxBankBIK
            // 
            this.textBoxBankBIK.Enabled = false;
            this.textBoxBankBIK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBankBIK.Location = new System.Drawing.Point(245, 272);
            this.textBoxBankBIK.Name = "textBoxBankBIK";
            this.textBoxBankBIK.Size = new System.Drawing.Size(186, 26);
            this.textBoxBankBIK.TabIndex = 18;
            this.textBoxBankBIK.Leave += new System.EventHandler(this.textBoxBankBIK_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(12, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Банковский счет";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(12, 275);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 20);
            this.label11.TabIndex = 20;
            this.label11.Text = "БИК банка";
            // 
            // textBoxBankAccount
            // 
            this.textBoxBankAccount.Enabled = false;
            this.textBoxBankAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBankAccount.Location = new System.Drawing.Point(245, 304);
            this.textBoxBankAccount.Name = "textBoxBankAccount";
            this.textBoxBankAccount.Size = new System.Drawing.Size(186, 26);
            this.textBoxBankAccount.TabIndex = 21;
            this.textBoxBankAccount.Leave += new System.EventHandler(this.textBoxBankAccount_Leave);
            // 
            // textBoxCorrAccount
            // 
            this.textBoxCorrAccount.Enabled = false;
            this.textBoxCorrAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCorrAccount.Location = new System.Drawing.Point(245, 336);
            this.textBoxCorrAccount.Name = "textBoxCorrAccount";
            this.textBoxCorrAccount.Size = new System.Drawing.Size(186, 26);
            this.textBoxCorrAccount.TabIndex = 22;
            this.textBoxCorrAccount.Leave += new System.EventHandler(this.textBoxCorrAccount_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(12, 339);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(201, 20);
            this.label12.TabIndex = 23;
            this.label12.Text = "Корреспондентский счет";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(644, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 29);
            this.button1.TabIndex = 24;
            this.button1.Text = "Редактировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAddRespPerson
            // 
            this.buttonAddRespPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddRespPerson.Location = new System.Drawing.Point(762, 33);
            this.buttonAddRespPerson.Name = "buttonAddRespPerson";
            this.buttonAddRespPerson.Size = new System.Drawing.Size(26, 28);
            this.buttonAddRespPerson.TabIndex = 25;
            this.buttonAddRespPerson.Text = "+";
            this.buttonAddRespPerson.UseVisualStyleBackColor = true;
            this.buttonAddRespPerson.Click += new System.EventHandler(this.buttonAddRespPerson_Click);
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 377);
            this.Controls.Add(this.buttonAddRespPerson);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxCorrAccount);
            this.Controls.Add(this.textBoxBankAccount);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxBankBIK);
            this.Controls.Add(this.textBoxBankName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxUrAddress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxDirector);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxKPP);
            this.Controls.Add(this.textBoxINN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxUrName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxRespPerson);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.label1);
            this.Name = "ProfileForm";
            this.ShowIcon = false;
            this.Text = "Профиль";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileForm_FormClosing);
            this.Load += new System.EventHandler(this.ProfileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxRespPerson;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUrName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxINN;
        private System.Windows.Forms.TextBox textBoxKPP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDirector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxUrAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxBankName;
        private System.Windows.Forms.TextBox textBoxBankBIK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxBankAccount;
        private System.Windows.Forms.TextBox textBoxCorrAccount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAddRespPerson;
    }
}