
namespace EDGV
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class SetFilterForm : Form
    {
        private FilterType filterType;
        private ResourceManager RM;
        private Control val1contol;
        private Control val2contol;
        private bool dateWithTime;
        private bool timeFilter;
        private IContainer components;
        private Button okButton;
        private Button cancelButton;
        private Label ColumnNameLabel;
        private ComboBox FilterTypeComboBox;
        private Label AndLabel;
        private string filterString;
        private string viewfilterString;
        private ErrorProvider errorProvider;

        private SetFilterForm()
        {
            this.dateWithTime = true;
            this.InitializeComponent();
        }

        public SetFilterForm(System.Type dataType, bool DateWithTime = true, bool TimeFilter = false) : this()
        {
            this.filterType = !(dataType == typeof(DateTime)) ? (((dataType == typeof(int)) || ((dataType == typeof(long)) || ((dataType == typeof(short)) || ((dataType == typeof(uint)) || ((dataType == typeof(ulong)) || ((dataType == typeof(ushort)) || ((dataType == typeof(byte)) || (dataType == typeof(sbyte))))))))) ? FilterType.Integer : (((dataType == typeof(float)) || ((dataType == typeof(double)) || (dataType == typeof(decimal)))) ? FilterType.Float : (!(dataType == typeof(string)) ? FilterType.Unknown : FilterType.String))) : FilterType.DateTime;
            this.dateWithTime = DateWithTime;
            this.timeFilter = TimeFilter;
            switch (this.filterType)
            {
                case FilterType.DateTime:
                    {
                        this.val1contol = new DateTimePicker();
                        this.val2contol = new DateTimePicker();
                        if (!this.timeFilter)
                        {
                            (this.val1contol as DateTimePicker).Format = DateTimePickerFormat.Short;
                            (this.val2contol as DateTimePicker).Format = DateTimePickerFormat.Short;
                        }
                        else
                        {
                            DateTimeFormatInfo dateTimeFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                            (this.val1contol as DateTimePicker).CustomFormat = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;
                            (this.val2contol as DateTimePicker).CustomFormat = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;
                            (this.val1contol as DateTimePicker).Format = DateTimePickerFormat.Custom;
                            (this.val2contol as DateTimePicker).Format = DateTimePickerFormat.Custom;
                        }
                        string[] items = new string[] { "Равно", "Не равно", "Раньше чем", "Позже чем", "Между" };
                        this.FilterTypeComboBox.Items.AddRange(items);
                        break;
                    }
                case FilterType.Float:
                case FilterType.Integer:
                    {
                        this.val1contol = new TextBox();
                        this.val2contol = new TextBox();
                        this.val1contol.TextChanged += new EventHandler(this.eControlTextChanged);
                        this.val2contol.TextChanged += new EventHandler(this.eControlTextChanged);
                        string[] items = new string[] { "Равно", "Не равно", "Больше чем", "Больше или равно", "Меньше чем", "Меньше или равно", "Между" };
                        this.FilterTypeComboBox.Items.AddRange(items);
                        this.val1contol.Tag = true;
                        this.val2contol.Tag = true;
                        this.okButton.Enabled = false;
                        break;
                    }
                default:
                    {
                        this.val1contol = new TextBox();
                        this.val2contol = new TextBox();
                        string[] items = new string[] { "Равно", "Не равно", "Начинается с", "Не начинается с", "Заканчивается на", "Не заканчивается на", "Содержит", "Не содержит" };
                        this.FilterTypeComboBox.Items.AddRange(items);
                        break;
                    }
            }
            this.FilterTypeComboBox.SelectedIndex = 0;
            this.val1contol.Name = "val1contol";
            this.val1contol.Location = new Point(30, 0x42);
            this.val1contol.Size = new Size(0xa6, 20);
            this.val1contol.TabIndex = 4;
            this.val1contol.Visible = true;
            this.val1contol.KeyDown += new KeyEventHandler(this.eControlKeyDown);
            this.val2contol.Name = "val2contol";
            this.val2contol.Location = new Point(30, 0x6c);
            this.val2contol.Size = new Size(0xa6, 20);
            this.val2contol.TabIndex = 5;
            this.val2contol.Visible = false;
            this.val2contol.VisibleChanged += new EventHandler(this.val2contol_VisibleChanged);
            this.val2contol.KeyDown += new KeyEventHandler(this.eControlKeyDown);
            base.Controls.Add(this.val1contol);
            base.Controls.Add(this.val2contol);
            this.errorProvider.SetIconAlignment(this.val1contol, ErrorIconAlignment.MiddleRight);
            this.errorProvider.SetIconPadding(this.val1contol, -18);
            this.errorProvider.SetIconAlignment(this.val2contol, ErrorIconAlignment.MiddleRight);
            this.errorProvider.SetIconPadding(this.val2contol, -18);
            this.val1contol.Select();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.viewfilterString = null;
            this.filterString = null;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void eControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender != this.val1contol)
                {
                    this.OKButton_Click(this.okButton, new EventArgs());
                }
                else if (this.val2contol.Visible)
                {
                    this.val2contol.Focus();
                }
                else
                {
                    this.OKButton_Click(this.okButton, new EventArgs());
                }
                e.SuppressKeyPress = false;
                e.Handled = true;
            }
        }

        private void eControlTextChanged(object sender, EventArgs e)
        {
            bool flag = false;
            switch (this.filterType)
            {
                case FilterType.Float:
                    double num2;
                    flag = !double.TryParse((sender as TextBox).Text, out num2);
                    break;

                case FilterType.Integer:
                    long num;
                    flag = !long.TryParse((sender as TextBox).Text, out num);
                    break;

                default:
                    break;
            }
            (sender as Control).Tag = flag ? ((object)1) : ((object)((sender as TextBox).Text.Length == 0));
            if (flag && ((sender as TextBox).Text.Length > 0))
            {
                this.errorProvider.SetError(sender as Control, "Неправильное значение");
            }
            else
            {
                this.errorProvider.SetError(sender as Control, "");
            }
            this.okButton.Enabled = !this.HasErrors();
        }

        private void FilterTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.val2contol.Visible = this.FilterTypeComboBox.Text == "Между";
            if (string.IsNullOrEmpty(this.val1contol.Text) || !this.val2contol.Visible)
            {
                this.val1contol.Select();
            }
            else if (this.val2contol.Visible)
            {
                this.val2contol.Select();
            }
            this.okButton.Enabled = !this.HasErrors();
        }

        private string FormatString(string Text)
        {
            string str = "";
            string[] source = new string[] { "%", "[", "]", "*", "\"", "`", @"\" };
            for (int i = 0; i < Text.Length; i++)
            {
                string str2 = Text[i].ToString();
                str = !source.Contains<string>(str2) ? (str + str2) : (str + "[" + str2 + "]");
            }
            return str.Replace("'", "''");
        }

        private bool HasErrors()
        {
            return ((!this.val1contol.Visible || ((this.val1contol.Tag == null) || !((bool)this.val1contol.Tag))) ? (this.val2contol.Visible && ((this.val2contol.Tag != null) && ((bool)this.val2contol.Tag))) : true);
        }

        private void InitializeComponent()
        {
            this.RM = new ResourceManager("РасчетКУ.Extends.EDGV.Localization.EDGVStrings", typeof(SetFilterForm).Assembly);
            this.components = new Container();
            this.okButton = new Button();
            this.cancelButton = new Button();
            this.ColumnNameLabel = new Label();
            this.FilterTypeComboBox = new ComboBox();
            this.AndLabel = new Label();
            this.errorProvider = new ErrorProvider(this.components);
            ((ISupportInitialize)this.errorProvider).BeginInit();
            base.SuspendLayout();
            this.okButton.DialogResult = DialogResult.OK;
            this.okButton.Location = new Point(0x1d, 0x8b);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(0x4b, 0x17);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ок";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new EventHandler(this.OKButton_Click);
            this.cancelButton.DialogResult = DialogResult.Cancel;
            this.cancelButton.Location = new Point(0x7b, 0x8b);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(0x4b, 0x17);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
            this.ColumnNameLabel.AutoSize = true;
            this.ColumnNameLabel.Location = new Point(4, 9);
            this.ColumnNameLabel.Name = "ColumnNameLabel";
            this.ColumnNameLabel.Size = new Size(140, 13);
            this.ColumnNameLabel.TabIndex = 2;
            this.ColumnNameLabel.Text = "Показать строки, где значение";
            this.FilterTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FilterTypeComboBox.FormattingEnabled = true;
            this.FilterTypeComboBox.Location = new Point(7, 0x20);
            this.FilterTypeComboBox.Name = "FilterTypeComboBox";
            this.FilterTypeComboBox.Size = new Size(0xbd, 0x15);
            this.FilterTypeComboBox.TabIndex = 3;
            this.FilterTypeComboBox.SelectedIndexChanged += new EventHandler(this.FilterTypeComboBox_SelectedIndexChanged);
            this.AndLabel.AutoSize = true;
            this.AndLabel.Location = new Point(7, 0x59);
            this.AndLabel.Name = "AndLabel";
            this.AndLabel.Size = new Size(13, 13);
            this.AndLabel.TabIndex = 6;
            this.AndLabel.Text = "и";
            this.AndLabel.Visible = false;
            this.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xcd, 0xa9);
            base.Controls.Add(this.AndLabel);
            base.Controls.Add(this.ColumnNameLabel);
            base.Controls.Add(this.FilterTypeComboBox);
            base.Controls.Add(this.cancelButton);
            base.Controls.Add(this.okButton);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "SetFilterForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.CancelButton = this.cancelButton;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Добавить фильтр";
            base.TopMost = true;
            ((ISupportInitialize)this.errorProvider).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.HasErrors())
            {
                this.okButton.Enabled = false;
            }
            else
            {
                string str = "[{0}] ";
                if (this.filterType == FilterType.Unknown)
                {
                    str = "Convert([{0}],System.String) ";
                }
                this.filterString = str;
                switch (this.filterType)
                {
                    case FilterType.DateTime:
                        {
                            DateTime time = ((DateTimePicker)this.val1contol).Value;
                            if (this.FilterTypeComboBox.Text == "Равно")
                            {
                                this.filterString = !this.dateWithTime ? (this.filterString + "= '" + time.ToShortDateString() + "'") : (!this.timeFilter ? ("Convert([{0}], System.String) LIKE '" + time.ToShortDateString() + "%'") : (this.filterString + "= '" + time.ToString("o") + "'"));
                            }
                            else if (this.FilterTypeComboBox.Text == "Раньше чем")
                            {
                                this.filterString = !this.timeFilter ? (this.filterString + "< '" + time.ToShortDateString() + "'") : (this.filterString + "< '" + time.ToString("o") + "'");
                            }
                            else if (this.FilterTypeComboBox.Text == "Позже чем")
                            {
                                this.filterString = !this.timeFilter ? (this.filterString + "> '" + time.ToShortDateString() + "'") : (this.filterString + "> '" + time.ToString("o") + "'");
                            }
                            else if (this.FilterTypeComboBox.Text != "Между")
                            {
                                if (this.FilterTypeComboBox.Text == "Не равно")
                                {
                                    this.filterString = !this.dateWithTime ? (this.filterString + "<> '" + time.ToShortDateString() + "'") : (!this.timeFilter ? ("Convert([{0}], System.String) NOT LIKE '" + time.ToShortDateString() + "%'") : (this.filterString + "<> '" + time.ToString("o") + "'"));
                                }
                            }
                            else
                            {
                                DateTime time2 = ((DateTimePicker)this.val2contol).Value;
                                if (time2 < time)
                                {
                                    time = time2;
                                    time2 = time;
                                }
                                if (this.timeFilter)
                                {
                                    this.filterString = this.filterString + ">= '" + time.ToString("o") + "'";
                                    string[] strArray = new string[] { this.filterString, " AND ", str, "<= '", time2.ToString("o"), "'" };
                                    this.filterString = string.Concat(strArray);
                                }
                                else
                                {
                                    this.filterString = this.filterString + ">= '" + time.ToShortDateString() + "'";
                                    string[] strArray2 = new string[] { this.filterString, " AND ", str, "<= '", time2.ToShortDateString(), "'" };
                                    this.filterString = string.Concat(strArray2);
                                }
                            }
                            break;
                        }
                    case FilterType.Float:
                    case FilterType.Integer:
                        {
                            string text = this.val1contol.Text;
                            if (this.filterType == FilterType.Float)
                            {
                                text = text.Replace(",", ".");
                            }
                            if (this.FilterTypeComboBox.Text == "Равно")
                            {
                                this.filterString = this.filterString + "= " + text;
                            }
                            else if (this.FilterTypeComboBox.Text == "Между")
                            {
                                string s = this.val2contol.Text;
                                if (this.filterType == FilterType.Float)
                                {
                                    s = s.Replace(",", ".");
                                }
                                if (double.Parse(text) > double.Parse(s))
                                {
                                    text = s;
                                    s = text;
                                }
                                string[] strArray3 = new string[] { this.filterString, ">= ", text, " AND ", str, "<= ", s };
                                this.filterString = string.Concat(strArray3);
                            }
                            else if (this.FilterTypeComboBox.Text == "Не равно")
                            {
                                this.filterString = this.filterString + "<> " + text;
                            }
                            else if (this.FilterTypeComboBox.Text == "Больше чем")
                            {
                                this.filterString = this.filterString + "> " + text;
                            }
                            else if (this.FilterTypeComboBox.Text == "Больше или равно")
                            {
                                this.filterString = this.filterString + ">= " + text;
                            }
                            else if (this.FilterTypeComboBox.Text == "Меньше чем")
                            {
                                this.filterString = this.filterString + "< " + text;
                            }
                            else if (this.FilterTypeComboBox.Text == "Меньше или равно")
                            {
                                this.filterString = this.filterString + "<= " + text;
                            }
                            break;
                        }
                    default:
                        {
                            string str5 = this.FormatString(this.val1contol.Text);
                            if (this.FilterTypeComboBox.Text == "Равно")
                            {
                                this.filterString = this.filterString + "LIKE '" + str5 + "'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Не равно")
                            {
                                this.filterString = this.filterString + "NOT LIKE '" + str5 + "'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Начинается с")
                            {
                                this.filterString = this.filterString + "LIKE '" + str5 + "%'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Заканчивается на")
                            {
                                this.filterString = this.filterString + "LIKE '%" + str5 + "'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Не начинается с")
                            {
                                this.filterString = this.filterString + "NOT LIKE '" + str5 + "%'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Не заканчивается на")
                            {
                                this.filterString = this.filterString + "NOT LIKE '%" + str5 + "'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Содержит")
                            {
                                this.filterString = this.filterString + "LIKE '%" + str5 + "%'";
                            }
                            else if (this.FilterTypeComboBox.Text == "Не содержит")
                            {
                                this.filterString = this.filterString + "NOT LIKE '%" + str5 + "%'";
                            }
                            break;
                        }
                }
                if (this.filterString == str)
                {
                    this.filterString = null;
                    this.viewfilterString = null;
                    base.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    string[] strArray4 = new string[] { "Показать строки, где значение", " ", this.FilterTypeComboBox.Text.ToLower(), " \"", this.val1contol.Text, "\"" };
                    this.viewfilterString = string.Concat(strArray4);
                    if (this.val2contol.Visible)
                    {
                        string[] strArray5 = new string[] { this.viewfilterString, " ", this.AndLabel.Text, " \"", this.val2contol.Text, "\"" };
                        this.viewfilterString = string.Concat(strArray5);
                    }
                    base.DialogResult = DialogResult.OK;
                }
                base.Close();
            }
        }

        private void val2contol_VisibleChanged(object sender, EventArgs e)
        {
            this.AndLabel.Visible = this.val2contol.Visible;
        }

        public string FilterString
        {
            get
            {
                return this.filterString;
            }
        }

        public string ViewFilterString
        {
            get
            {
                return this.viewfilterString;
            }
        }

        private enum FilterType
        {
            Unknown,
            DateTime,
            String,
            Float,
            Integer
        }
    }
}
