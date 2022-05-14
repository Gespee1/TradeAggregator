
namespace EDGV
{
    using EDGV.Properties;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class EDGVFilterMenu : ContextMenuStrip
    {
        private ToolStripMenuItem SortASCMenuItem;
        private ToolStripMenuItem SortDESCMenuItem;
        private ToolStripMenuItem CancelSortMenuItem;
        private ToolStripSeparator toolStripSeparator1MenuItem;
        private ToolStripMenuItem CancelFilterMenuItem;
        private ToolStripMenuItem FiltersMenuItem;
        private ToolStripMenuItem SetupFilterMenuItem;
        private ToolStripSeparator toolStripSeparator2MenuItem;
        private ToolStripSeparator toolStripSeparator3MenuItem;
        private ToolStripMenuItem lastfilter1MenuItem;
        private ToolStripMenuItem lastfilter2MenuItem;
        private ToolStripMenuItem lastfilter3MenuItem;
        private ToolStripMenuItem lastfilter4MenuItem;
        private ToolStripMenuItem lastfilter5MenuItem;
        //private System.Windows.Forms.TreeView CheckList;
        //private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        //private ToolStripControlHost CheckFilterListControlHost;//            +
        private ToolStripControlHost CheckFilterListButtonsControlHost;
        private ToolStripControlHost ResizeBoxControlHost;
        //private Panel CheckFilterListPanel;//                                +
        private Panel CheckFilterListButtonsPanel;
        private Dictionary<int, string> months;
        //private ResourceManager RM;
        private EDGVFilterMenuFilterType activeFilterType;
        private EDGVFilterMenuSortType activeSortType;
        private string sortString;
        private string filterString;
        //private TripleTreeNode[] startingNodes;
        //private TripleTreeNode[] filterNodes;
        private static Point resizeStartPoint = new Point(1, 1);
        private Point resizeEndPoint = new Point(-1, -1);
        private EventHandler _SortChanged;
        private EventHandler _FilterChanged;

        public event EventHandler FilterChanged
        {
            add
            {
                EventHandler filterChanged = this._FilterChanged;
                while (true)
                {
                    EventHandler comparand = filterChanged;
                    EventHandler handler3 = comparand + value;
                    filterChanged = Interlocked.CompareExchange<EventHandler>(ref this._FilterChanged, handler3, comparand);
                    if (ReferenceEquals(filterChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler filterChanged = this._FilterChanged;
                while (true)
                {
                    EventHandler comparand = filterChanged;
                    EventHandler handler3 = comparand - value;
                    filterChanged = Interlocked.CompareExchange<EventHandler>(ref this._FilterChanged, handler3, comparand);
                    if (ReferenceEquals(filterChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler SortChanged
        {
            add
            {
                EventHandler sortChanged = this._SortChanged;
                while (true)
                {
                    EventHandler comparand = sortChanged;
                    EventHandler handler3 = comparand + value;
                    sortChanged = Interlocked.CompareExchange<EventHandler>(ref this._SortChanged, handler3, comparand);
                    if (ReferenceEquals(sortChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler sortChanged = this._SortChanged;
                while (true)
                {
                    EventHandler comparand = sortChanged;
                    EventHandler handler3 = comparand - value;
                    sortChanged = Interlocked.CompareExchange<EventHandler>(ref this._SortChanged, handler3, comparand);
                    if (ReferenceEquals(sortChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public EDGVFilterMenu(System.Type DataType)
        {
            this.DataType = DataType;
            this.DateWithTime = true;
            this.TimeFilter = false;
            //this.RM = new ResourceManager("РасчетКУ.Extends.EDGV.Localization.EDGVStrings", typeof(EDGVFilterMenu).Assembly);
            this.months = new Dictionary<int, string>();
            this.months.Add(1, "Январь");
            this.months.Add(2, "Февраль");
            this.months.Add(3, "Март");
            this.months.Add(4, "Апрель");
            this.months.Add(5, "Май");
            this.months.Add(6, "Июнь");
            this.months.Add(7, "Июль");
            this.months.Add(8, "Август");
            this.months.Add(9, "Сентябрь");
            this.months.Add(10, "Октябрь");
            this.months.Add(11, "Ноябрь");
            this.months.Add(12, "Декабрь");
            this.SortASCMenuItem = new ToolStripMenuItem();
            this.SortDESCMenuItem = new ToolStripMenuItem();
            this.CancelSortMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1MenuItem = new ToolStripSeparator();
            this.CancelFilterMenuItem = new ToolStripMenuItem();
            this.FiltersMenuItem = new ToolStripMenuItem();
            this.SetupFilterMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2MenuItem = new ToolStripSeparator();
            this.lastfilter1MenuItem = new ToolStripMenuItem();
            this.lastfilter2MenuItem = new ToolStripMenuItem();
            this.lastfilter3MenuItem = new ToolStripMenuItem();
            this.lastfilter4MenuItem = new ToolStripMenuItem();
            this.lastfilter5MenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3MenuItem = new ToolStripSeparator();
            //this.CheckList = new System.Windows.Forms.TreeView();
            //this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            //this.CheckFilterListPanel = new Panel();
            this.CheckFilterListButtonsPanel = new Panel();
            this.CheckFilterListButtonsControlHost = new ToolStripControlHost(this.CheckFilterListButtonsPanel);
            //this.CheckFilterListControlHost = new ToolStripControlHost(this.CheckFilterListPanel);
            this.ResizeBoxControlHost = new ToolStripControlHost(new Control());
            base.SuspendLayout();
            base.BackColor = SystemColors.ControlLightLight;
            this.AutoSize = false;
            base.Padding = new Padding(0);
            base.Margin = new Padding(0);
            base.Size = new Size(0x11f, 340);
            this.MaximumSize = new Size(300, 160);
            base.Closed += new ToolStripDropDownClosedEventHandler(this.FilterContextMenu_Closed);
            base.LostFocus += new EventHandler(this.FilterContextMenu_LostFocus);
            this.SortASCMenuItem.Name = "SortASCMenuItem";
            this.SortASCMenuItem.AutoSize = false;
            this.SortASCMenuItem.Size = new Size(base.Width - 1, 0x16);
            this.SortASCMenuItem.Click += new EventHandler(this.SortASCMenuItem_Click);
            this.SortASCMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
            this.SortASCMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            this.SortDESCMenuItem.Name = "SortDESCMenuItem";
            this.SortDESCMenuItem.AutoSize = false;
            this.SortDESCMenuItem.Size = new Size(base.Width - 1, 0x16);
            this.SortDESCMenuItem.Click += new EventHandler(this.SortDESCMenuItem_Click);
            this.SortDESCMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
            this.SortDESCMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            this.CancelSortMenuItem.Name = "CancelSortMenuItem";
            this.CancelSortMenuItem.Enabled = false;
            this.CancelSortMenuItem.AutoSize = false;
            this.CancelSortMenuItem.Size = new Size(base.Width - 1, 0x16);
            this.CancelSortMenuItem.Text = "Убрать сортировку";
            this.CancelSortMenuItem.Click += new EventHandler(this.CancelSortMenuItem_Click);
            this.CancelSortMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
            this.toolStripSeparator1MenuItem.Name = "toolStripSeparator1MenuItem";
            this.toolStripSeparator1MenuItem.Size = new Size(base.Width - 4, 6);
            this.CancelFilterMenuItem.Name = "CancelFilterMenuItem";
            this.CancelFilterMenuItem.Enabled = false;
            this.CancelFilterMenuItem.AutoSize = false;
            this.CancelFilterMenuItem.Size = new Size(base.Width - 1, 0x16);
            this.CancelFilterMenuItem.Text = "Очистить фильтр";
            this.CancelFilterMenuItem.Click += new EventHandler(this.CancelFilterMenuItem_Click);
            this.CancelFilterMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
            this.SetupFilterMenuItem.Name = "SetupFilterMenuItem";
            this.SetupFilterMenuItem.Size = new Size(0x98, 0x16);
            this.SetupFilterMenuItem.Text = "Настроить";
            this.SetupFilterMenuItem.Click += new EventHandler(this.SetupFilterMenuItem_Click);
            this.toolStripSeparator2MenuItem.Name = "toolStripSeparator2MenuItem";
            this.toolStripSeparator2MenuItem.Size = new Size(0x95, 6);
            this.toolStripSeparator2MenuItem.Visible = false;
            this.lastfilter1MenuItem.Name = "lastfilter1MenuItem";
            this.lastfilter1MenuItem.Size = new Size(0x98, 0x16);
            this.lastfilter1MenuItem.Tag = "0";
            this.lastfilter1MenuItem.Text = null;
            this.lastfilter1MenuItem.Visible = false;
            this.lastfilter1MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
            this.lastfilter1MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
            this.lastfilter1MenuItem.VisibleChanged += new EventHandler(this.lastfilter1MenuItem_VisibleChanged);
            this.lastfilter2MenuItem.Name = "lastfilter2MenuItem";
            this.lastfilter2MenuItem.Size = new Size(0x98, 0x16);
            this.lastfilter2MenuItem.Tag = "1";
            this.lastfilter2MenuItem.Text = null;
            this.lastfilter2MenuItem.Visible = false;
            this.lastfilter2MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
            this.lastfilter2MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
            this.lastfilter3MenuItem.Name = "lastfilter3MenuItem";
            this.lastfilter3MenuItem.Size = new Size(0x98, 0x16);
            this.lastfilter3MenuItem.Tag = "2";
            this.lastfilter3MenuItem.Text = null;
            this.lastfilter3MenuItem.Visible = false;
            this.lastfilter3MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
            this.lastfilter3MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
            this.lastfilter4MenuItem.Name = "lastfilter4MenuItem";
            this.lastfilter4MenuItem.Size = new Size(0x98, 0x16);
            this.lastfilter4MenuItem.Tag = "3";
            this.lastfilter4MenuItem.Text = null;
            this.lastfilter4MenuItem.Visible = false;
            this.lastfilter4MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
            this.lastfilter4MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
            this.lastfilter5MenuItem.Name = "lastfilter5MenuItem";
            this.lastfilter5MenuItem.Size = new Size(0x98, 0x16);
            this.lastfilter5MenuItem.Tag = "4";
            this.lastfilter5MenuItem.Text = null;
            this.lastfilter5MenuItem.Visible = false;
            this.lastfilter5MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
            this.lastfilter5MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
            this.FiltersMenuItem.Name = "FiltersMenuItem";
            this.FiltersMenuItem.AutoSize = false;
            this.FiltersMenuItem.Size = new Size(base.Width - 1, 0x16);
            this.FiltersMenuItem.Image = Resources.Filter;
            this.FiltersMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.SetupFilterMenuItem, this.toolStripSeparator2MenuItem, this.lastfilter1MenuItem, this.lastfilter2MenuItem, this.lastfilter3MenuItem, this.lastfilter4MenuItem, this.lastfilter5MenuItem };
            this.FiltersMenuItem.DropDownItems.AddRange(toolStripItems);
            this.FiltersMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
            this.FiltersMenuItem.Paint += new PaintEventHandler(this.FiltersMenuItem_Paint);
            this.toolStripSeparator3MenuItem.Name = "toolStripSeparator3MenuItem";
            this.toolStripSeparator3MenuItem.Size = new Size(base.Width - 4, 6);
            //this.okButton.Name = "okButton";
            //this.okButton.BackColor = Control.DefaultBackColor;
            //this.okButton.UseVisualStyleBackColor = true;
            //this.okButton.Margin = new Padding(0);
            //this.okButton.Size = new Size(0x4b, 0x17);
            //this.okButton.Text = "Ок";
            //this.okButton.Click += new EventHandler(this.okButton_Click);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.BackColor = Control.DefaultBackColor;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Margin = new Padding(0);
            this.cancelButton.Size = new Size(0x4b, 0x17);
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
            this.ResizeBoxControlHost.Name = "ResizeBoxControlHost";
            this.ResizeBoxControlHost.Control.Cursor = Cursors.SizeNWSE;
            this.ResizeBoxControlHost.AutoSize = false;
            this.ResizeBoxControlHost.Padding = new Padding(0);
            this.ResizeBoxControlHost.Margin = new Padding(base.Width - 0x2d, 0, 0, 0);
            this.ResizeBoxControlHost.Size = new Size(10, 10);
            this.ResizeBoxControlHost.Paint += new PaintEventHandler(this.ResizeGrip_Paint);
            this.ResizeBoxControlHost.MouseDown += new MouseEventHandler(this.ResizePictureBox_MouseDown);
            this.ResizeBoxControlHost.MouseUp += new MouseEventHandler(this.ResizePictureBox_MouseUp);
            this.ResizeBoxControlHost.MouseMove += new MouseEventHandler(this.ResizePictureBox_MouseMove);
            /*this.CheckFilterListControlHost.Name = "CheckFilterListControlHost";
            this.CheckFilterListControlHost.AutoSize = false;
            this.CheckFilterListControlHost.Size = new Size(base.Width - 0x23, 180);
            this.CheckFilterListControlHost.Padding = new Padding(0);
            this.CheckFilterListControlHost.Margin = new Padding(0);*/
            this.CheckFilterListButtonsControlHost.Name = "CheckFilterListButtonsControlHost";
            this.CheckFilterListButtonsControlHost.AutoSize = false;
            this.CheckFilterListButtonsControlHost.Size = new Size(base.Width - 0x23, 0x18);
            this.CheckFilterListButtonsControlHost.Padding = new Padding(0);
            this.CheckFilterListButtonsControlHost.Margin = new Padding(0);
            /*this.CheckFilterListPanel.Name = "CheckFilterListPanel";
            this.CheckFilterListPanel.AutoSize = false;
            this.CheckFilterListPanel.Size = this.CheckFilterListControlHost.Size;
            this.CheckFilterListPanel.Padding = new Padding(0);
            this.CheckFilterListPanel.Margin = new Padding(0);
            this.CheckFilterListPanel.BackColor = base.BackColor;
            this.CheckFilterListPanel.BorderStyle = BorderStyle.None;*/
            //this.CheckFilterListPanel.Controls.Add(this.CheckList);
            //this.CheckList.Name = "CheckList";
            //this.CheckList.AutoSize = false;
            //this.CheckList.Padding = new Padding(0);
            //this.CheckList.Margin = new Padding(0);
            //this.CheckList.Bounds = new Rectangle(4, 4, this.CheckFilterListPanel.Width - 8, this.CheckFilterListPanel.Height - 8);
            //this.CheckList.StateImageList = this.GetCheckImages();
            //this.CheckList.CheckBoxes = false;
            //this.CheckList.MouseLeave += new EventHandler(this.CheckList_MouseLeave);
            //this.CheckList.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.CheckList_NodeMouseClick);
            //this.CheckList.KeyDown += new KeyEventHandler(this.CheckList_KeyDown);
            //this.CheckList.MouseEnter += new EventHandler(this.CheckList_MouseEnter);
            //this.CheckList.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.CheckList_NodeMouseDoubleClick);
            this.CheckFilterListButtonsPanel.Name = "CheckFilterListButtonsPanel";
            this.CheckFilterListButtonsPanel.AutoSize = false;
            this.CheckFilterListButtonsPanel.Size = this.CheckFilterListButtonsControlHost.Size;
            this.CheckFilterListButtonsPanel.Padding = new Padding(0);
            this.CheckFilterListButtonsPanel.Margin = new Padding(0);
            this.CheckFilterListButtonsPanel.BackColor = base.BackColor;
            this.CheckFilterListButtonsPanel.BorderStyle = BorderStyle.None;
            //Control[] controls = new Control[] { this.okButton, this.cancelButton };
            //this.CheckFilterListButtonsPanel.Controls.AddRange(controls);
            this.CheckFilterListButtonsPanel.Controls.Add(this.cancelButton);
            //this.okButton.Location = new Point(this.CheckFilterListButtonsPanel.Width - 0xa4, 0);
            this.cancelButton.Location = new Point(this.CheckFilterListButtonsPanel.Width - 0x4f, 0);
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.SortASCMenuItem, this.SortDESCMenuItem, this.CancelSortMenuItem, 
                this.toolStripSeparator1MenuItem, this.CancelFilterMenuItem, this.FiltersMenuItem, this.toolStripSeparator3MenuItem, 
                /*this.CheckFilterListControlHost,*/ this.CheckFilterListButtonsControlHost, this.ResizeBoxControlHost };
            //itemArray2[9] = this.ResizeBoxControlHost;
            this.Items.AddRange(itemArray2);
            base.ResumeLayout(false);
            if (this.DataType == typeof(DateTime))
            {
                this.FiltersMenuItem.Text = "Фильтр дат";
                this.SortASCMenuItem.Text = "Сортировать по возрастанию дат";
                this.SortDESCMenuItem.Text = "Сортировать по убыванию дат";
                this.SortASCMenuItem.Image = Resources.ASCnum;
                this.SortDESCMenuItem.Image = Resources.DESCnum;
            }
            /*else if (this.DataType == typeof(bool))
            {
                this.FiltersMenuItem.Text = this.RM.GetString("filtersmenuitem_text_text");
                this.SortASCMenuItem.Text = this.RM.GetString("sortascmenuitem_text_boolean");
                this.SortDESCMenuItem.Text = this.RM.GetString("sortdescmenuitem_text_boolean");
                this.SortASCMenuItem.Image = Resources.ASCbool;
                this.SortDESCMenuItem.Image = Resources.DESCbool;
            }*/
            else if ((this.DataType != typeof(int)) && ((this.DataType != typeof(long)) && ((this.DataType != typeof(short)) && ((this.DataType != typeof(uint)) && ((this.DataType != typeof(ulong)) && ((this.DataType != typeof(ushort)) && ((this.DataType != typeof(byte)) && ((this.DataType != typeof(sbyte)) && ((this.DataType != typeof(decimal)) && ((this.DataType != typeof(float)) && (this.DataType != typeof(double))))))))))))
            {
                this.FiltersMenuItem.Text = "Текстовый фильтр";
                this.SortASCMenuItem.Text = "Сортировать от А до Я";
                this.SortDESCMenuItem.Text = "Сортировать от Я до А";
                this.SortASCMenuItem.Image = Resources.ASCtxt;
                this.SortDESCMenuItem.Image = Resources.DESCtxt;
            }
            else
            {
                this.FiltersMenuItem.Text = "Числовой фильтр";
                this.SortASCMenuItem.Text = "Сортировать по возрастанию";
                this.SortDESCMenuItem.Text = "Сортировать по убыванию";
                this.SortASCMenuItem.Image = Resources.ASCnum;
                this.SortDESCMenuItem.Image = Resources.DESCnum;
            }
            this.FiltersMenuItem.Enabled = this.DataType != typeof(bool);
            this.FiltersMenuItem.Checked = this.ActiveFilterType == EDGVFilterMenuFilterType.Custom;
            this.MinimumSize = new Size(base.PreferredSize.Width, base.PreferredSize.Height);
            this.ResizeMenu(this.MinimumSize.Width, this.MinimumSize.Height);
        }

        private void AddCustomFilter(string FilterString, string ViewFilterString)
        {
            int filtersMenuItemIndex = -1;
            int num2 = 2;
            while (true)
            {
                if ((num2 < this.FiltersMenuItem.DropDownItems.Count) && this.FiltersMenuItem.DropDown.Items[num2].Available)
                {
                    if ((this.FiltersMenuItem.DropDownItems[num2].Text != ViewFilterString) || (this.FiltersMenuItem.DropDownItems[num2].Tag.ToString() != FilterString))
                    {
                        num2++;
                        continue;
                    }
                    filtersMenuItemIndex = num2;
                }
                if (filtersMenuItemIndex < 2)
                {
                    int num3 = this.FiltersMenuItem.DropDownItems.Count - 2;
                    while (true)
                    {
                        if (num3 <= 1)
                        {
                            filtersMenuItemIndex = 2;
                            this.FiltersMenuItem.DropDownItems[2].Text = ViewFilterString;
                            this.FiltersMenuItem.DropDownItems[2].Tag = FilterString;
                            break;
                        }
                        if (this.FiltersMenuItem.DropDownItems[num3].Available)
                        {
                            this.FiltersMenuItem.DropDownItems[num3 + 1].Text = this.FiltersMenuItem.DropDownItems[num3].Text;
                            this.FiltersMenuItem.DropDownItems[num3 + 1].Tag = this.FiltersMenuItem.DropDownItems[num3].Tag;
                        }
                        num3--;
                    }
                }
                this.SetCustomFilter(filtersMenuItemIndex);
                return;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //this.RestoreNodes();
            base.Close();
        }

        private void CancelFilterMenuItem_Click(object sender, EventArgs e)
        {
            string filterString = this.FilterString;
            this.ClearFilter();
            if ((filterString != this.FilterString) && (this._FilterChanged != null))
            {
                this._FilterChanged(this, new EventArgs());
            }
        }

        private void CancelSortMenuItem_Click(object sender, EventArgs e)
        {
            string sortString = this.SortString;
            this.ClearSorting();
            if ((sortString != this.SortString) && (this._SortChanged != null))
            {
                this._SortChanged(this, new EventArgs());
            }
        }

        /*private void CheckList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.NodeCheckChange(this.CheckList.SelectedNode as TripleTreeNode);
            }
        }*/

        /*private void CheckList_MouseEnter(object sender, EventArgs e)
        {
            this.CheckList.Focus();
        }*/

        /*private void CheckList_MouseLeave(object sender, EventArgs e)
        {
            base.Focus();
        }*/

        /*private void CheckList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo info = this.CheckList.HitTest(e.X, e.Y);
            if ((info != null) && (info.Location == TreeViewHitTestLocations.StateImage))
            {
                this.NodeCheckChange(e.Node as TripleTreeNode);
            }
        }*/

        /*private void CheckList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TripleTreeNode node = e.Node as TripleTreeNode;
            this.SetNodesCheckedState(this.CheckList.Nodes, false);
            node.CheckState = CheckState.Unchecked;
            this.NodeCheckChange(node);
            this.okButton_Click(this, new EventArgs());
        }
*/
        public void ClearFilter()
        {
            for (int i = 2; i < (this.FiltersMenuItem.DropDownItems.Count - 1); i++)
            {
                (this.FiltersMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            this.activeFilterType = EDGVFilterMenuFilterType.None;
            //this.SetNodesCheckedState(this.CheckList.Nodes, true);
            //string filterString = this.FilterString;
            this.FilterString = null;
            //this.filterNodes = null;
            this.FiltersMenuItem.Checked = false;
            //this.okButton.Enabled = true;
        }

        private void ClearResizeBox()
        {
            if (this.resizeEndPoint.X != -1)
            {
                Point point = base.PointToScreen(resizeStartPoint);
                Rectangle rectangle = new Rectangle(point.X, point.Y, this.resizeEndPoint.X, this.resizeEndPoint.Y)
                {
                    X = Math.Min(point.X, this.resizeEndPoint.X),
                    Width = Math.Abs((int)(point.X - this.resizeEndPoint.X)),
                    Y = Math.Min(point.Y, this.resizeEndPoint.Y),
                    Height = Math.Abs((int)(point.Y - this.resizeEndPoint.Y))
                };
                ControlPaint.DrawReversibleFrame(rectangle, Color.Black, FrameStyle.Dashed);
                this.resizeEndPoint.X = -1;
            }
        }

        public void ClearSorting()
        {
            string sortString = this.SortString;
            this.SortASCMenuItem.Checked = false;
            this.SortDESCMenuItem.Checked = false;
            this.activeSortType = EDGVFilterMenuSortType.None;
            this.SortString = null;
        }

        /*private string CreateFilterString(IEnumerable<TripleTreeNode> nodes)
        {
            StringBuilder builder = new StringBuilder("");
            string str = (!(this.DataType == typeof(DateTime)) || (this.TimeFilter || !this.DateWithTime)) ? ", " : " OR ";
            if ((nodes != null) && (nodes.Count<TripleTreeNode>() > 0))
            {
                if (this.DataType == typeof(DateTime))
                {
                    using (IEnumerator<TripleTreeNode> enumerator = nodes.GetEnumerator())
                    {
                        TripleTreeNode current;
                        goto TR_0016;
                    TR_000A:
                        if (this.TimeFilter && this.DateWithTime)
                        {
                            builder.Append("'" + ((DateTime)current.Value).ToString("o") + "'" + str);
                        }
                        else if (!this.TimeFilter && this.DateWithTime)
                        {
                            builder.Append("(Convert([{0}], System.String) LIKE '" + ((DateTime)current.Value).ToShortDateString() + "%')" + str);
                        }
                        else
                        {
                            builder.Append("'" + ((DateTime)current.Value).ToShortDateString() + "'" + str);
                        }
                    TR_0016:
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            current = enumerator.Current;
                            if (!current.Checked || (((current.NodeType != TripleTreeNodeType.DayDateTimeNode) || (this.TimeFilter && this.DateWithTime)) && ((current.NodeType != TripleTreeNodeType.MSecDateTimeNode) || (!this.TimeFilter || !this.DateWithTime))))
                            {
                                if ((current.CheckState != CheckState.Unchecked) && (current.Nodes.Count > 0))
                                {
                                    string str2 = this.CreateFilterString(current.Nodes.AsParallel().Cast<TripleTreeNode>().Where<TripleTreeNode>(sn => sn.CheckState != CheckState.Unchecked));
                                    if (str2.Length > 0)
                                    {
                                        builder.Append(str2 + str);
                                    }
                                }
                                continue;
                            }
                            goto TR_000A;
                        }
                        goto TR_0005;
                    }
                }
                if (this.DataType == typeof(bool))
                {
                    using (IEnumerator<TripleTreeNode> enumerator2 = nodes.GetEnumerator())
                    {
                        if (enumerator2.MoveNext())
                        {
                            builder.Append(enumerator2.Current.Value.ToString());
                        }
                    }
                }
                else if ((this.DataType == typeof(int)) || ((this.DataType == typeof(long)) || ((this.DataType == typeof(short)) || ((this.DataType == typeof(uint)) || ((this.DataType == typeof(ulong)) || ((this.DataType == typeof(ushort)) || ((this.DataType == typeof(byte)) || (this.DataType == typeof(sbyte)))))))))
                {
                    foreach (TripleTreeNode node3 in nodes)
                    {
                        builder.Append(node3.Value.ToString() + str);
                    }
                }
                else if ((this.DataType != typeof(float)) && ((this.DataType != typeof(double)) && (this.DataType != typeof(decimal))))
                {
                    foreach (TripleTreeNode node5 in nodes)
                    {
                        builder.Append("'" + this.FormatString(node5.Value.ToString()) + "'" + str);
                    }
                }
                else
                {
                    foreach (TripleTreeNode node4 in nodes)
                    {
                        builder.Append(node4.Value.ToString().Replace(",", ".") + str);
                    }
                }
            }
        TR_0005:
            if ((builder.Length > str.Length) && (this.DataType != typeof(bool)))
            {
                builder.Remove(builder.Length - str.Length, str.Length);
            }
            return builder.ToString();
        }*/

        /*private void DuplicateFilterNodes()
        {
            this.filterNodes = new TripleTreeNode[this.CheckList.Nodes.Count];
            int index = 0;
            foreach (TripleTreeNode node in this.CheckList.Nodes)
            {
                this.filterNodes[index] = node.Clone();
                index++;
            }
        }*/

        /*private void DuplicateNodes()
        {
            this.startingNodes = new TripleTreeNode[this.CheckList.Nodes.Count];
            int index = 0;
            foreach (TripleTreeNode node in this.CheckList.Nodes)
            {
                this.startingNodes[index] = node.Clone();
                index++;
            }
        }*/

        private void FilterContextMenu_Closed(object sender, EventArgs e)
        {
            this.ClearResizeBox();
            //this.startingNodes = null;
        }

        private void FilterContextMenu_LostFocus(object sender, EventArgs e)
        {
            if (!base.ContainsFocus)
            {
                base.Close();
            }
        }

        private void FiltersMenuItem_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(this.FiltersMenuItem.Width - 12, 7, 10, 10);
            ControlPaint.DrawMenuGlyph(e.Graphics, rectangle, MenuGlyph.Arrow, Color.Black, Color.Transparent);
        }

        /*private string FormatString(string Text)
        {
            string str = "";
            string[] source = new string[] { "%", "[", "]", "*", "\"", "`", @"\" };
            for (int i = 0; i < Text.Length; i++)
            {
                string str2 = Text[i].ToString();
                str = !source.Contains<string>(str2) ? (str + str2) : (str + "[" + str2 + "]");
            }
            return str.Replace("'", "''");
        }*/

        /*private TripleTreeNode GetAllsNode()
        {
            TripleTreeNode node = null;
            int num = 0;
            foreach (TripleTreeNode node2 in this.CheckList.Nodes)
            {
                if (node2.NodeType == TripleTreeNodeType.AllsNode)
                {
                    node = node2;
                }
                else if (num <= 2)
                {
                    num++;
                    continue;
                }
                break;
            }
            return node;
        }*/

        /*private ImageList GetCheckImages()
        {
            ImageList list = new ImageList();
            Bitmap image = new Bitmap(0x10, 0x10);
            Bitmap bitmap2 = new Bitmap(0x10, 0x10);
            Bitmap bitmap3 = new Bitmap(0x10, 0x10);
            using (Bitmap bitmap4 = new Bitmap(0x10, 0x10))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap4))
                {
                    CheckBoxRenderer.DrawCheckBox(graphics, new Point(0, 1), CheckBoxState.UncheckedNormal);
                    image = (Bitmap)bitmap4.Clone();
                    CheckBoxRenderer.DrawCheckBox(graphics, new Point(0, 1), CheckBoxState.CheckedNormal);
                    bitmap2 = (Bitmap)bitmap4.Clone();
                    CheckBoxRenderer.DrawCheckBox(graphics, new Point(0, 1), CheckBoxState.MixedNormal);
                    bitmap3 = (Bitmap)bitmap4.Clone();
                }
            }
            list.Images.Add("uncheck", image);
            list.Images.Add("check", bitmap2);
            list.Images.Add("mixed", bitmap3);
            return list;
        }*/

        /*private TripleTreeNode GetNullNode()
        {
            TripleTreeNode node = null;
            int num = 0;
            foreach (TripleTreeNode node2 in this.CheckList.Nodes)
            {
                if (node2.NodeType == TripleTreeNodeType.EmptysNode)
                {
                    node = node2;
                }
                else if (num <= 2)
                {
                    num++;
                    continue;
                }
                break;
            }
            return node;
        }*/

        public static IEnumerable<DataGridViewCell> GetValuesForFilter(DataGridView grid, string columnName)
        {
            return (from r in grid.Rows.Cast<DataGridViewRow>()
                    where !r.IsNewRow
                    select r.Cells[columnName]);
        }

        private void lastfilter1MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            for (int i = 2; i < this.FiltersMenuItem.DropDownItems.Count; i++)
            {
                if ((this.FiltersMenuItem.DropDownItems[i].Text == item.Text) && (this.FiltersMenuItem.DropDownItems[i].Tag.ToString() == item.Tag.ToString()))
                {
                    this.SetCustomFilter(i);
                    return;
                }
            }
        }

        private void lastfilter1MenuItem_TextChanged(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Available = true;
            (sender as ToolStripMenuItem).TextChanged -= new EventHandler(this.lastfilter1MenuItem_TextChanged);
        }

        private void lastfilter1MenuItem_VisibleChanged(object sender, EventArgs e)
        {
            this.toolStripSeparator2MenuItem.Visible = !this.lastfilter1MenuItem.Visible;
            (sender as ToolStripMenuItem).VisibleChanged -= new EventHandler(this.lastfilter1MenuItem_VisibleChanged);
        }

        /*private void NodeCheckChange(TripleTreeNode node)
        {
            node.CheckState = (node.CheckState != CheckState.Checked) ? CheckState.Checked : CheckState.Unchecked;
            if (node.NodeType == TripleTreeNodeType.AllsNode)
            {
                this.SetNodesCheckedState(this.CheckList.Nodes, node.Checked);
                this.okButton.Enabled = node.Checked;
            }
            else
            {
                if (node.Nodes.Count > 0)
                {
                    this.SetNodesCheckedState(node.Nodes, node.Checked);
                }
                this.RefreshNodesState();
            }
        }*/

        private void okButton_Click(object sender, EventArgs e)
        {
            /*TripleTreeNode allsNode = this.GetAllsNode();
            this.FiltersMenuItem.Checked = false;
            if ((allsNode != null) && allsNode.Checked)
            {
                this.CancelFilterMenuItem_Click(null, new EventArgs());
            }
            else
            {
                string filterString = this.FilterString;
                this.FilterString = "";
                this.activeFilterType = EDGVFilterMenuFilterType.CheckList;
                if (this.CheckList.Nodes.Count > 1)
                {
                    allsNode = this.GetNullNode();
                    if ((allsNode != null) && allsNode.Checked)
                    {
                        this.FilterString = "[{0}] IS NULL";
                    }
                    if ((this.CheckList.Nodes.Count > 2) || (allsNode == null))
                    {
                        string str2 = this.CreateFilterString(this.CheckList.Nodes.AsParallel().Cast<TripleTreeNode>().Where<TripleTreeNode>(n => (n.NodeType != TripleTreeNodeType.AllsNode) && ((n.NodeType != TripleTreeNodeType.EmptysNode) && (n.CheckState != CheckState.Unchecked))));
                        if (str2.Length > 0)
                        {
                            if (this.FilterString.Length > 0)
                            {
                                this.FilterString = this.FilterString + " OR ";
                            }
                            this.FilterString = !(this.DataType == typeof(DateTime)) ? (!(this.DataType == typeof(bool)) ? (((this.DataType == typeof(int)) || ((this.DataType == typeof(long)) || ((this.DataType == typeof(short)) || ((this.DataType == typeof(uint)) || ((this.DataType == typeof(ulong)) || ((this.DataType == typeof(ushort)) || ((this.DataType == typeof(byte)) || ((this.DataType == typeof(sbyte)) || (this.DataType == typeof(string)))))))))) ? (this.FilterString + "[{0}] IN (" + str2 + ")") : (this.FilterString + "Convert([{0}],System.String) IN (" + str2 + ")")) : (this.FilterString + "{0}=" + str2)) : ((this.TimeFilter || !this.DateWithTime) ? (this.FilterString + "[{0}] IN (" + str2 + ")") : (this.FilterString + str2));
                        }
                    }
                }
                this.DuplicateFilterNodes();
                if ((filterString != this.FilterString) && (this._FilterChanged != null))
                {
                    this._FilterChanged(this, new EventArgs());
                }
            }*/
            base.Close();
        }

        private void PaintResizeBox(int X, int Y)
        {
            this.ClearResizeBox();
            X += base.Width - this.ResizeBoxControlHost.Width;
            Y += base.Height - this.ResizeBoxControlHost.Height;
            X = Math.Max(X, this.MinimumSize.Width - 1);
            Y = Math.Max(Y, this.MinimumSize.Height - 1);
            Point point = base.PointToScreen(resizeStartPoint);
            Point point2 = base.PointToScreen(new Point(X, Y));
            Rectangle rectangle = new Rectangle
            {
                X = Math.Min(point.X, point2.X),
                Width = Math.Abs((int)(point.X - point2.X)),
                Y = Math.Min(point.Y, point2.Y),
                Height = Math.Abs((int)(point.Y - point2.Y))
            };
            ControlPaint.DrawReversibleFrame(rectangle, Color.Black, FrameStyle.Dashed);
            this.resizeEndPoint.X = point2.X;
            this.resizeEndPoint.Y = point2.Y;
        }

        /*private void RefreshFilterMenu(IEnumerable<DataGridViewCell> vals)
        {
            this.CheckList.BeginUpdate();
            this.CheckList.Nodes.Clear();
            if (vals != null)
            {
                TripleTreeNode node = TripleTreeNode.CreateAllsNode("(Выбрать все)            ", CheckState.Checked);
                node.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
                this.CheckList.Nodes.Add(node);
                if (vals.Count<DataGridViewCell>() > 0)
                {
                    IEnumerable<DataGridViewCell> source = from c in vals
                                                           where (c.Value != null) && (c.Value != DBNull.Value)
                                                           select c;
                    if (vals.Count<DataGridViewCell>() != source.Count<DataGridViewCell>())
                    {
                        TripleTreeNode node2 = TripleTreeNode.CreateEmptysNode(this.RM.GetString("tripletreenode_nullnode_text") + "               ", CheckState.Checked);
                        node2.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
                        this.CheckList.Nodes.Add(node2);
                    }
                    if (!(this.DataType == typeof(DateTime)))
                    {
                        if (this.DataType == typeof(bool))
                        {
                            IEnumerable<DataGridViewCell> enumerable9 = from c in source
                                                                        where (bool)c.Value
                                                                        select c;
                            if (enumerable9.Count<DataGridViewCell>() != source.Count<DataGridViewCell>())
                            {
                                TripleTreeNode node12 = TripleTreeNode.CreateNode(this.RM.GetString("tripletreenode_boolean_false"), false, CheckState.Checked);
                                this.CheckList.Nodes.Add(node12);
                            }
                            if (enumerable9.Count<DataGridViewCell>() > 0)
                            {
                                TripleTreeNode node13 = TripleTreeNode.CreateNode(this.RM.GetString("tripletreenode_boolean_true"), true, CheckState.Checked);
                                this.CheckList.Nodes.Add(node13);
                            }
                        }
                        else
                        {
                            foreach (IGrouping<object, DataGridViewCell> grouping8 in from c in source
                                                                                      group c by c.Value into g
                                                                                      orderby g.Key
                                                                                      select g)
                            {
                                TripleTreeNode node14 = TripleTreeNode.CreateNode(grouping8.First<DataGridViewCell>().FormattedValue.ToString(), grouping8.Key, CheckState.Checked);
                                this.CheckList.Nodes.Add(node14);
                            }
                        }
                    }
                    else
                    {
                        foreach (IGrouping<int, DataGridViewCell> grouping in from year in source
                                                                              group year by ((DateTime)year.Value).Year into y
                                                                              orderby y.Key
                                                                              select y)
                        {
                            TripleTreeNode node3 = TripleTreeNode.CreateYearNode(grouping.Key.ToString(), grouping.Key, CheckState.Checked);
                            this.CheckList.Nodes.Add(node3);
                            foreach (IGrouping<int, DataGridViewCell> grouping2 in from month in grouping
                                                                                   group month by ((DateTime)month.Value).Month into m
                                                                                   orderby m.Key
                                                                                   select m)
                            {
                                TripleTreeNode node4 = node3.CreateChildNode(this.months[grouping2.Key], grouping2.Key);
                                foreach (IGrouping<int, DataGridViewCell> grouping3 in from day in grouping2
                                                                                       group day by ((DateTime)day.Value).Day into d
                                                                                       orderby d.Key
                                                                                       select d)
                                {
                                    TripleTreeNode node5;
                                    if (!this.TimeFilter)
                                    {
                                        node5 = node4.CreateChildNode(grouping3.Key.ToString("D2"), grouping3.First<DataGridViewCell>().Value);
                                        continue;
                                    }
                                    if (!this.DateWithTime)
                                    {
                                        node4.CreateChildNode(grouping3.Key.ToString("D2"), grouping3.First<DataGridViewCell>().Value).CreateChildNode("## " + this.RM.GetString("checknodetree_hour"), null).CreateChildNode("## " + this.RM.GetString("checknodetree_minute"), null).CreateChildNode("## " + this.RM.GetString("checknodetree_second"), null).CreateChildNode("### " + this.RM.GetString("checknodetree_millisecond"), null);
                                        continue;
                                    }
                                    node5 = node4.CreateChildNode(grouping3.Key.ToString("D2"), grouping3.Key);
                                    foreach (IGrouping<int, DataGridViewCell> grouping4 in from hour in grouping3
                                                                                           group hour by ((DateTime)hour.Value).Hour into h
                                                                                           orderby h.Key
                                                                                           select h)
                                    {
                                        TripleTreeNode node9 = node5.CreateChildNode(grouping4.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_hour"), grouping4.Key);
                                        foreach (IGrouping<int, DataGridViewCell> grouping5 in from min in grouping4
                                                                                               group min by ((DateTime)min.Value).Minute into mn
                                                                                               orderby mn.Key
                                                                                               select mn)
                                        {
                                            TripleTreeNode node10 = node9.CreateChildNode(grouping5.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_minute"), grouping5.Key);
                                            foreach (IGrouping<int, DataGridViewCell> grouping6 in from sec in grouping5
                                                                                                   group sec by ((DateTime)sec.Value).Second into s
                                                                                                   orderby s.Key
                                                                                                   select s)
                                            {
                                                TripleTreeNode node11 = node10.CreateChildNode(grouping6.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_second"), grouping6.Key);
                                                foreach (IGrouping<int, DataGridViewCell> grouping7 in from msec in grouping6
                                                                                                       group msec by ((DateTime)msec.Value).Millisecond into ms
                                                                                                       orderby ms.Key
                                                                                                       select ms)
                                                {
                                                    node11.CreateChildNode(grouping7.Key.ToString("D3") + " " + this.RM.GetString("checknodetree_millisecond"), grouping7.First<DataGridViewCell>().Value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.CheckList.EndUpdate();
        }*/

        /*private void RefreshNodesState()
        {
            CheckState state = this.UpdateNodesCheckState(this.CheckList.Nodes);
            this.GetAllsNode().CheckState = state;
            this.okButton.Enabled = state != CheckState.Unchecked;
        }*/

        private void ResizeGrip_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resources.ResizeGrip, 0, 0);
        }

        private void ResizeMenu(int W, int H)
        {
            this.SortASCMenuItem.Width = W - 1;
            this.SortDESCMenuItem.Width = W - 1;
            this.CancelSortMenuItem.Width = W - 1;
            this.CancelFilterMenuItem.Width = W - 1;
            this.SetupFilterMenuItem.Width = W - 1;
            this.FiltersMenuItem.Width = W - 1;
            //this.CheckFilterListControlHost.Size = new Size(W - 0x23, H - 160);
            //this.CheckFilterListPanel.Size = new Size(W - 0x23, H - 160);
            //this.CheckList.Bounds = new Rectangle(4, 4, (W - 0x23) - 8, (H - 160) - 8);
            this.CheckFilterListButtonsControlHost.Size = new Size(W - 0x23, 0x18);
            //this.okButton.Location = new Point((W - 0x23) - 0xa4, 0);
            this.cancelButton.Location = new Point((W - 0x23) - 0x4f, 0);
            this.ResizeBoxControlHost.Margin = new Padding(W - 0x2e, 0, 0, 0);
            base.Size = new Size(W, H);
            //Console.WriteLine($"Base size: {W}/{H}");
            //Console.WriteLine($"Button panel: {CheckFilterListButtonsPanel.Width}/{CheckFilterListButtonsPanel.Height}");
        }

        private void ResizePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ClearResizeBox();
            }
        }

        private void ResizePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (base.Visible && (e.Button == MouseButtons.Left))
            {
                this.PaintResizeBox(e.X, e.Y);
            }
        }

        private void ResizePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.resizeEndPoint.X != -1)
            {
                this.ClearResizeBox();
                if (base.Visible && (e.Button == MouseButtons.Left))
                {
                    int w = Math.Max((e.X + base.Width) - this.ResizeBoxControlHost.Width, this.MinimumSize.Width);
                    this.ResizeMenu(w, Math.Max((e.Y + base.Height) - this.ResizeBoxControlHost.Height, this.MinimumSize.Height));
                }
            }
        }

        /*private void RestoreFilterNodes()
        {
            this.CheckList.Nodes.Clear();
            if (this.filterNodes != null)
            {
                this.CheckList.Nodes.AddRange(this.filterNodes);
            }
        }

        private void RestoreNodes()
        {
            this.CheckList.Nodes.Clear();
            if (this.startingNodes != null)
            {
                this.CheckList.Nodes.AddRange(this.startingNodes);
            }
        }*/

        private void SetCustomFilter(int FiltersMenuItemIndex)
        {
            /*if (this.activeFilterType == EDGVFilterMenuFilterType.CheckList)
            {
                this.SetNodesCheckedState(this.CheckList.Nodes, false);
            }*/
            string str = this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Tag.ToString();
            string text = this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Text;
            if (FiltersMenuItemIndex != 2)
            {
                int num = FiltersMenuItemIndex;
                while (true)
                {
                    if (num <= 2)
                    {
                        this.FiltersMenuItem.DropDownItems[2].Text = text;
                        this.FiltersMenuItem.DropDownItems[2].Tag = str;
                        break;
                    }
                    this.FiltersMenuItem.DropDownItems[num].Text = this.FiltersMenuItem.DropDownItems[num - 1].Text;
                    this.FiltersMenuItem.DropDownItems[num].Tag = this.FiltersMenuItem.DropDownItems[num - 1].Tag;
                    num--;
                }
            }
            for (int i = 3; i < this.FiltersMenuItem.DropDownItems.Count; i++)
            {
                (this.FiltersMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            (this.FiltersMenuItem.DropDownItems[2] as ToolStripMenuItem).Checked = true;
            this.activeFilterType = EDGVFilterMenuFilterType.Custom;
            string filterString = this.FilterString;
            this.FilterString = str;
            //this.SetNodesCheckedState(this.CheckList.Nodes, false);
            //this.DuplicateFilterNodes();
            this.FiltersMenuItem.Checked = true;
            //this.okButton.Enabled = false;
            if ((filterString != this.FilterString) && (this._FilterChanged != null))
            {
                this._FilterChanged(this, new EventArgs());
            }
        }

        public void SetLoadedFilterMode(bool Enabled)
        {
            this.SetupFilterMenuItem.Enabled = !Enabled;
            this.CancelFilterMenuItem.Enabled = Enabled;
            if (!Enabled)
            {
                this.activeFilterType = EDGVFilterMenuFilterType.None;
            }
            else
            {
                this.activeFilterType = EDGVFilterMenuFilterType.Loaded;
                this.sortString = null;
                this.filterString = null;
                //this.filterNodes = null;
                this.FiltersMenuItem.Checked = false;
                for (int i = 2; i < (this.FiltersMenuItem.DropDownItems.Count - 1); i++)
                {
                    (this.FiltersMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
                }
                /*this.CheckList.Nodes.Clear();
                TripleTreeNode node = TripleTreeNode.CreateAllsNode(this.RM.GetString("tripletreenode_allnode_text") + "            ", CheckState.Checked);
                node.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
                node.CheckState = CheckState.Indeterminate;
                this.CheckList.Nodes.Add(node);*/
            }
        }

        /*private void SetNodesCheckedState(TreeNodeCollection nodes, bool isChecked)
        {
            foreach (TripleTreeNode node in nodes)
            {
                node.Checked = isChecked;
                if ((node.Nodes != null) && (node.Nodes.Count > 0))
                {
                    this.SetNodesCheckedState(node.Nodes, isChecked);
                }
            }
        }*/

        private void SetupFilterMenuItem_Click(object sender, EventArgs e)
        {
            SetFilterForm form = new SetFilterForm(this.DataType, this.DateWithTime, this.TimeFilter);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.AddCustomFilter(form.FilterString, form.ViewFilterString);
            }
        }

        /*public void Show(Control control, int x, int y, bool RestoreFilter)
        {
            if (RestoreFilter)
            {
                this.RestoreFilterNodes();
            }
            this.DuplicateNodes();
            base.Show(control, x, y);
        }

        public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
        {
            this.RefreshFilterMenu(vals);
            if (this.activeFilterType == EDGVFilterMenuFilterType.Custom)
            {
                this.SetNodesCheckedState(this.CheckList.Nodes, false);
            }
            this.DuplicateNodes();
            base.Show(control, x, y);
        }*/

        private void SortASCMenuItem_Click(object sender, EventArgs e)
        {
            this.SortASCMenuItem.Checked = true;
            this.SortDESCMenuItem.Checked = false;
            this.activeSortType = EDGVFilterMenuSortType.ASC;
            string sortString = this.SortString;
            this.SortString = "[{0}] ASC";
            if ((sortString != this.SortString) && (this._SortChanged != null))
            {
                this._SortChanged(this, new EventArgs());
            }
        }

        private void SortASCMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Enabled)
            {
                (sender as ToolStripMenuItem).Select();
            }
        }

        private void SortDESCMenuItem_Click(object sender, EventArgs e)
        {
            this.SortASCMenuItem.Checked = false;
            this.SortDESCMenuItem.Checked = true;
            this.activeSortType = EDGVFilterMenuSortType.DESC;
            string sortString = this.SortString;
            this.SortString = "[{0}] DESC";
            if ((sortString != this.SortString) && (this._SortChanged != null))
            {
                this._SortChanged(this, new EventArgs());
            }
        }

        /*private CheckState UpdateNodesCheckState(TreeNodeCollection nodes)
        {
            CheckState @unchecked = CheckState.Unchecked;
            bool flag = true;
            bool flag2 = true;
            foreach (TripleTreeNode node in nodes)
            {
                if (node.NodeType != TripleTreeNodeType.AllsNode)
                {
                    if (node.Nodes.Count > 0)
                    {
                        node.CheckState = this.UpdateNodesCheckState(node.Nodes);
                    }
                    if (flag)
                    {
                        @unchecked = node.CheckState;
                        flag = false;
                    }
                    else if (@unchecked != node.CheckState)
                    {
                        flag2 = false;
                    }
                }
            }
            return (!flag2 ? CheckState.Indeterminate : @unchecked);
        }*/

        public System.Type DataType { get; private set; }

        public bool DateWithTime { get; set; }

        public bool TimeFilter { get; set; }

        public EDGVFilterMenuSortType ActiveSortType
        {
            get
            {
                return this.activeSortType;
            }
        }

        public EDGVFilterMenuFilterType ActiveFilterType
        {
            get
            {
                return this.activeFilterType;
            }
        }

        public string SortString
        {
            get
            {
                return ((this.sortString == null) ? "" : this.sortString);
            }
            private set
            {
                this.CancelSortMenuItem.Enabled = (value != null) && (value.Length > 0);
                this.sortString = value;
            }
        }

        public string FilterString
        {
            get
            {
                return ((this.filterString == null) ? "" : this.filterString);
            }
            private set
            {
                this.CancelFilterMenuItem.Enabled = (value != null) && (value.Length > 0);
                this.filterString = value;
            }
        }
    }
}
