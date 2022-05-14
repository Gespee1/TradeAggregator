
namespace EDGV
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public class SearchToolBar : ToolStrip
    {
        private SearchToolBarSearchEventHandler _Search;
        private DataGridViewColumnCollection columnsList;
        private IContainer components;
        private ToolStripButton closeButton;
        private ToolStripLabel searchLabel;
        private ToolStripComboBox columnComboBox;
        private ToolStripTextBox searchTextBox;
        private ToolStripButton fromBeginButton;
        private ToolStripButton caseSensButton;
        private ToolStripButton searchButton;
        private ToolStripButton wholeWordButton;
        private ToolStripSeparator searchSeparator;

        public event SearchToolBarSearchEventHandler Search
        {
            add
            {
                SearchToolBarSearchEventHandler search = this._Search;
                while (true)
                {
                    SearchToolBarSearchEventHandler comparand = search;
                    SearchToolBarSearchEventHandler handler3 = comparand + value;
                    search = Interlocked.CompareExchange<SearchToolBarSearchEventHandler>(ref this._Search, handler3, comparand);
                    if (ReferenceEquals(search, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                SearchToolBarSearchEventHandler search = this._Search;
                while (true)
                {
                    SearchToolBarSearchEventHandler comparand = search;
                    SearchToolBarSearchEventHandler handler3 = comparand - value;
                    search = Interlocked.CompareExchange<SearchToolBarSearchEventHandler>(ref this._Search, handler3, comparand);
                    if (ReferenceEquals(search, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public SearchToolBar()
        {
            this.InitializeComponent();
            this.searchTextBox.Text = this.searchTextBox.ToolTipText;
            this.columnComboBox.SelectedIndex = 0;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SearchToolBar));
            this.closeButton = new ToolStripButton();
            this.searchLabel = new ToolStripLabel();
            this.columnComboBox = new ToolStripComboBox();
            this.searchTextBox = new ToolStripTextBox();
            this.fromBeginButton = new ToolStripButton();
            this.caseSensButton = new ToolStripButton();
            this.searchButton = new ToolStripButton();
            this.wholeWordButton = new ToolStripButton();
            this.searchSeparator = new ToolStripSeparator();
            base.SuspendLayout();
            manager.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.closeButton.Name = "closeButton";
            this.closeButton.Overflow = ToolStripItemOverflow.Never;
            this.closeButton.Click += new EventHandler(this.closeButton_Click);
            manager.ApplyResources(this.searchLabel, "searchLabel");
            this.searchLabel.Name = "searchLabel";
            this.columnComboBox.Name = "columnComboBox";
            this.columnComboBox.AutoToolTip = true;
            this.columnComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] items = new object[] { manager.GetString("columnComboBox.Items") };
            this.columnComboBox.Items.AddRange(items);
            this.columnComboBox.Margin = new Padding(0, 2, 8, 2);
            manager.ApplyResources(this.columnComboBox, "columnComboBox");
            manager.ApplyResources(this.searchTextBox, "searchTextBox");
            this.searchTextBox.ForeColor = Color.LightGray;
            this.searchTextBox.Margin = new Padding(0, 2, 8, 2);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Overflow = ToolStripItemOverflow.Never;
            this.searchTextBox.Enter += new EventHandler(this.searchTextBox_Enter);
            this.searchTextBox.Leave += new EventHandler(this.searchTextBox_Leave);
            this.searchTextBox.KeyDown += new KeyEventHandler(this.searchTextBox_KeyDown);
            this.searchTextBox.TextChanged += new EventHandler(this.searchTextBox_TextChanged);
            manager.ApplyResources(this.fromBeginButton, "fromBeginButton");
            this.fromBeginButton.Checked = true;
            this.fromBeginButton.CheckOnClick = true;
            this.fromBeginButton.CheckState = CheckState.Checked;
            this.fromBeginButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.fromBeginButton.Name = "fromBeginButton";
            manager.ApplyResources(this.caseSensButton, "caseSensButton");
            this.caseSensButton.CheckOnClick = true;
            this.caseSensButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.caseSensButton.Name = "caseSensButton";
            manager.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.searchButton.Name = "searchButton";
            this.searchButton.Overflow = ToolStripItemOverflow.Never;
            this.searchButton.Click += new EventHandler(this.searchButton_Click);
            manager.ApplyResources(this.wholeWordButton, "wholeWordButton");
            this.wholeWordButton.CheckOnClick = true;
            this.wholeWordButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.wholeWordButton.Margin = new Padding(1, 1, 1, 2);
            this.wholeWordButton.Name = "wholeWordButton";
            manager.ApplyResources(this.searchSeparator, "searchSeparator");
            this.searchSeparator.Name = "searchSeparator";
            manager.ApplyResources(this, "$this");
            base.AllowMerge = false;
            base.GripStyle = ToolStripGripStyle.Hidden;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.closeButton, this.searchLabel, this.columnComboBox, this.searchTextBox, this.fromBeginButton, this.wholeWordButton, this.caseSensButton, this.searchSeparator, this.searchButton };
            this.Items.AddRange(toolStripItems);
            this.MaximumSize = new Size(0, 0x1b);
            this.MinimumSize = new Size(0, 0x1b);
            base.RenderMode = ToolStripRenderMode.Professional;
            base.Resize += new EventHandler(this.SearchToolBar_Resize);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ResizeBoxes(int width, ref int w1, ref int w2)
        {
            int num = (int)Math.Round((double)(((double)(width - base.Width)) / 2.0), 0, MidpointRounding.AwayFromZero);
            int num2 = w1;
            if (base.Width < width)
            {
                w1 = Math.Max(w1 - num, 0x4b);
                w2 = Math.Max(w2 - num, 0x4b);
            }
            else
            {
                w1 = Math.Min(w1 - num, 150);
                w2 += ((base.Width - width) + num2) - w1;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if ((this.searchTextBox.TextLength > 0) && ((this.searchTextBox.Text != this.searchTextBox.ToolTipText) && (this._Search != null)))
            {
                DataGridViewColumn column = null;
                if ((this.columnComboBox.SelectedIndex > 0) && ((this.columnsList != null) && (this.columnsList.GetColumnCount(DataGridViewElementStates.Visible) > 0)))
                {
                    DataGridViewColumn[] columnArray = (from col in this.columnsList.Cast<DataGridViewColumn>()
                                                        where col.Visible
                                                        select col).ToArray<DataGridViewColumn>();
                    if ((columnArray.Length == (this.columnComboBox.Items.Count - 1)) && (columnArray[this.columnComboBox.SelectedIndex - 1].HeaderText == this.columnComboBox.SelectedItem.ToString()))
                    {
                        column = columnArray[this.columnComboBox.SelectedIndex - 1];
                    }
                }
                SearchToolBarSearchEventArgs args = new SearchToolBarSearchEventArgs(this.searchTextBox.Text, column, this.caseSensButton.Checked, this.wholeWordButton.Checked, this.fromBeginButton.Checked);
                this._Search(this, args);
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if ((this.searchTextBox.Text == this.searchTextBox.ToolTipText) && (this.searchTextBox.ForeColor == Color.LightGray))
            {
                this.searchTextBox.Text = "";
            }
            else
            {
                this.searchTextBox.SelectAll();
            }
            this.searchTextBox.ForeColor = SystemColors.WindowText;
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((this.searchTextBox.TextLength > 0) && ((this.searchTextBox.Text != this.searchTextBox.ToolTipText) && (e.KeyData == Keys.Enter)))
            {
                e.SuppressKeyPress = false;
                e.Handled = true;
                this.searchButton_Click(this.searchButton, new EventArgs());
            }
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (this.searchTextBox.Text.Trim() == "")
            {
                this.searchTextBox.Text = this.searchTextBox.ToolTipText;
                this.searchTextBox.ForeColor = Color.LightGray;
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            this.searchButton.Enabled = (this.searchTextBox.TextLength > 0) && (this.searchTextBox.Text != this.searchTextBox.ToolTipText);
        }

        private void SearchToolBar_Resize(object sender, EventArgs e)
        {
            base.SuspendLayout();
            int num = 150;
            int num2 = 150;
            int num3 = this.columnComboBox.Width + this.searchTextBox.Width;
            foreach (ToolStripItem item in this.Items)
            {
                item.Overflow = ToolStripItemOverflow.Never;
                item.Visible = true;
            }
            int width = ((base.PreferredSize.Width - num3) + num) + num2;
            if (base.Width >= width)
            {
                this.ResizeBoxes(width, ref num, ref num2);
            }
            else
            {
                this.searchLabel.Visible = false;
                this.ResizeBoxes(((base.PreferredSize.Width - num3) + num) + num2, ref num, ref num2);
                width = ((base.PreferredSize.Width - num3) + num) + num2;
                if (base.Width < width)
                {
                    this.caseSensButton.Overflow = ToolStripItemOverflow.Always;
                    this.ResizeBoxes(((base.PreferredSize.Width - num3) + num) + num2, ref num, ref num2);
                    width = ((base.PreferredSize.Width - num3) + num) + num2;
                }
                if (base.Width < width)
                {
                    this.wholeWordButton.Overflow = ToolStripItemOverflow.Always;
                    this.ResizeBoxes(((base.PreferredSize.Width - num3) + num) + num2, ref num, ref num2);
                    width = ((base.PreferredSize.Width - num3) + num) + num2;
                }
                if (base.Width < width)
                {
                    this.fromBeginButton.Overflow = ToolStripItemOverflow.Always;
                    this.searchSeparator.Visible = false;
                    this.ResizeBoxes(((base.PreferredSize.Width - num3) + num) + num2, ref num, ref num2);
                    width = ((base.PreferredSize.Width - num3) + num) + num2;
                }
                if (base.Width < width)
                {
                    this.columnComboBox.Overflow = ToolStripItemOverflow.Always;
                    this.searchTextBox.Overflow = ToolStripItemOverflow.Always;
                    num = 150;
                    num2 = Math.Max(((base.Width - base.PreferredSize.Width) - this.searchTextBox.Margin.Left) - this.searchTextBox.Margin.Right, 0x4b);
                    this.searchTextBox.Overflow = ToolStripItemOverflow.Never;
                    width = (base.PreferredSize.Width - this.searchTextBox.Width) + num2;
                }
                if (base.Width < width)
                {
                    this.searchButton.Overflow = ToolStripItemOverflow.Always;
                    num2 = Math.Max((base.Width - base.PreferredSize.Width) + this.searchTextBox.Width, 0x4b);
                    width = (base.PreferredSize.Width - this.searchTextBox.Width) + num2;
                }
                if (base.Width < width)
                {
                    this.closeButton.Overflow = ToolStripItemOverflow.Always;
                    this.searchTextBox.Margin = new Padding(8, 2, 8, 2);
                    num2 = Math.Max((base.Width - base.PreferredSize.Width) + this.searchTextBox.Width, 0x4b);
                    width = (base.PreferredSize.Width - this.searchTextBox.Width) + num2;
                }
                if (base.Width < width)
                {
                    num2 = Math.Max((base.Width - base.PreferredSize.Width) + this.searchTextBox.Width, 20);
                    width = (base.PreferredSize.Width - this.searchTextBox.Width) + num2;
                }
                if (width > base.Width)
                {
                    this.searchTextBox.Overflow = ToolStripItemOverflow.Always;
                    this.searchTextBox.Margin = new Padding(0, 2, 8, 2);
                    num2 = 150;
                }
            }
            if (this.columnComboBox.Width != num)
            {
                this.columnComboBox.Width = num;
            }
            if (this.searchTextBox.Width != num2)
            {
                this.searchTextBox.Width = num2;
            }
            base.ResumeLayout();
        }

        public void SetColumns(DataGridViewColumnCollection columns)
        {
            this.columnsList = columns;
            this.columnComboBox.BeginUpdate();
            this.columnComboBox.Items.Clear();
            object[] items = new object[] { new ComponentResourceManager(typeof(SearchToolBar)).GetString("columnComboBox.Items") };
            this.columnComboBox.Items.AddRange(items);
            if (this.columnsList != null)
            {
                foreach (DataGridViewColumn column in this.columnsList)
                {
                    if (column.Visible)
                    {
                        this.columnComboBox.Items.Add(column.HeaderText);
                    }
                }
            }
            this.columnComboBox.SelectedIndex = 0;
            this.columnComboBox.EndUpdate();
        }
    }
}
