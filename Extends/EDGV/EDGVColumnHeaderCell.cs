

namespace EDGV
{
    using EDGV.Properties;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class EDGVColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        private Image filterImage = Resources.AddFilter;
        private Size filterButtonImageSize = new Size(0x10, 0x10);
        private bool filterButtonPressed;
        private bool filterButtonOver;
        private Rectangle filterButtonOffsetBounds = Rectangle.Empty;
        private Rectangle filterButtonImageBounds = Rectangle.Empty;
        private Padding filterButtonMargin = new Padding(3, 4, 3, 4);
        private bool filterEnabled;
        private EDGVFilterEventHandler _FilterPopup;
        private EDGVFilterEventHandler _SortChanged;
        private EDGVFilterEventHandler _FilterChanged;

        public event EDGVFilterEventHandler FilterChanged
        {
            add
            {
                EDGVFilterEventHandler filterChanged = this._FilterChanged;
                while (true)
                {
                    EDGVFilterEventHandler comparand = filterChanged;
                    EDGVFilterEventHandler handler3 = comparand + value;
                    filterChanged = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._FilterChanged, handler3, comparand);
                    if (ReferenceEquals(filterChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EDGVFilterEventHandler filterChanged = this._FilterChanged;
                while (true)
                {
                    EDGVFilterEventHandler comparand = filterChanged;
                    EDGVFilterEventHandler handler3 = comparand - value;
                    filterChanged = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._FilterChanged, handler3, comparand);
                    if (ReferenceEquals(filterChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EDGVFilterEventHandler FilterPopup
        {
            add
            {
                EDGVFilterEventHandler filterPopup = this._FilterPopup;
                while (true)
                {
                    EDGVFilterEventHandler comparand = filterPopup;
                    EDGVFilterEventHandler handler3 = comparand + value;
                    filterPopup = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._FilterPopup, handler3, comparand);
                    if (ReferenceEquals(filterPopup, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EDGVFilterEventHandler filterPopup = this._FilterPopup;
                while (true)
                {
                    EDGVFilterEventHandler comparand = filterPopup;
                    EDGVFilterEventHandler handler3 = comparand - value;
                    filterPopup = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._FilterPopup, handler3, comparand);
                    if (ReferenceEquals(filterPopup, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EDGVFilterEventHandler SortChanged
        {
            add
            {
                EDGVFilterEventHandler sortChanged = this._SortChanged;
                while (true)
                {
                    EDGVFilterEventHandler comparand = sortChanged;
                    EDGVFilterEventHandler handler3 = comparand + value;
                    sortChanged = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._SortChanged, handler3, comparand);
                    if (ReferenceEquals(sortChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EDGVFilterEventHandler sortChanged = this._SortChanged;
                while (true)
                {
                    EDGVFilterEventHandler comparand = sortChanged;
                    EDGVFilterEventHandler handler3 = comparand - value;
                    sortChanged = Interlocked.CompareExchange<EDGVFilterEventHandler>(ref this._SortChanged, handler3, comparand);
                    if (ReferenceEquals(sortChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public EDGVColumnHeaderCell(DataGridViewColumnHeaderCell oldCell, bool FilterEnabled = false)
        {
            base.Tag = oldCell.Tag;
            base.ErrorText = oldCell.ErrorText;
            base.ToolTipText = oldCell.ToolTipText;
            base.Value = oldCell.Value;
            this.ValueType = oldCell.ValueType;
            this.ContextMenuStrip = oldCell.ContextMenuStrip;
            base.Style = oldCell.Style;
            this.filterEnabled = FilterEnabled;
            EDGVColumnHeaderCell cell = oldCell as EDGVColumnHeaderCell;
            if ((cell == null) || (cell.FilterMenu == null))
            {
                this.FilterMenu = new EDGVFilterMenu(oldCell.OwningColumn.ValueType);
                this.FilterMenu.FilterChanged += new EventHandler(this.FilterMenu_FilterChanged);
                this.FilterMenu.SortChanged += new EventHandler(this.FilterMenu_SortChanged);
            }
            else
            {
                this.FilterMenu = cell.FilterMenu;
                this.filterImage = cell.filterImage;
                this.filterButtonPressed = cell.filterButtonPressed;
                this.filterButtonOver = cell.filterButtonOver;
                this.filterButtonOffsetBounds = cell.filterButtonOffsetBounds;
                this.filterButtonImageBounds = cell.filterButtonImageBounds;
                this.FilterMenu.FilterChanged += new EventHandler(this.FilterMenu_FilterChanged);
                this.FilterMenu.SortChanged += new EventHandler(this.FilterMenu_SortChanged);
            }
        }

        internal void ClearFilter()
        {
            if ((this.FilterMenu != null) && this.FilterEnabled)
            {
                this.FilterMenu.ClearFilter();
                this.RefreshImage();
                this.RepaintCell();
            }
        }

        internal void ClearSorting()
        {
            if ((this.FilterMenu != null) && this.FilterEnabled)
            {
                this.FilterMenu.ClearSorting();
                this.RefreshImage();
                this.RepaintCell();
            }
        }

        public override object Clone()
        {
            return new EDGVColumnHeaderCell(this, this.FilterEnabled);
        }

        private void FilterMenu_FilterChanged(object sender, EventArgs e)
        {
            this.RefreshImage();
            this.RepaintCell();
            if (this.FilterEnabled && (this._FilterChanged != null))
            {
                this._FilterChanged(this, new EDGVFilterEventArgs(this.FilterMenu, base.OwningColumn));
            }
        }

        private void FilterMenu_SortChanged(object sender, EventArgs e)
        {
            this.RefreshImage();
            this.RepaintCell();
            if (this.FilterEnabled && (this._SortChanged != null))
            {
                this._SortChanged(this, new EDGVFilterEventArgs(this.FilterMenu, base.OwningColumn));
            }
        }

        ~EDGVColumnHeaderCell()
        {
            if (this.FilterMenu != null)
            {
                this.FilterMenu.FilterChanged -= new EventHandler(this.FilterMenu_FilterChanged);
                this.FilterMenu.SortChanged -= new EventHandler(this.FilterMenu_SortChanged);
            }
        }

        private Rectangle GetFilterBounds(bool withOffset = true)
        {
            Rectangle rectangle = base.DataGridView.GetCellDisplayRectangle(base.ColumnIndex, -1, false);
            return new Rectangle(new Point(((withOffset ? rectangle.Right : base.OwningColumn.Width) - this.filterButtonImageSize.Width) - this.filterButtonMargin.Right, ((withOffset ? rectangle.Bottom : rectangle.Height) - this.filterButtonImageSize.Height) - this.filterButtonMargin.Bottom), this.filterButtonImageSize);
        }

        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (!this.FilterEnabled)
            {
                base.OnMouseDown(e);
            }
            else if (this.filterButtonImageBounds.Contains(e.X, e.Y) && ((e.Button == MouseButtons.Left) && !this.filterButtonPressed))
            {
                this.filterButtonPressed = true;
                this.filterButtonOver = true;
                this.RepaintCell();
            }
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            if (this.FilterEnabled && this.filterButtonOver)
            {
                this.filterButtonOver = false;
                this.RepaintCell();
            }
            base.OnMouseLeave(rowIndex);
        }

        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            if (this.FilterEnabled)
            {
                if (this.filterButtonImageBounds.Contains(e.X, e.Y) && !this.filterButtonOver)
                {
                    this.filterButtonOver = true;
                    this.RepaintCell();
                }
                else if (!this.filterButtonImageBounds.Contains(e.X, e.Y) && this.filterButtonOver)
                {
                    this.filterButtonOver = false;
                    this.RepaintCell();
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            if (!this.FilterEnabled)
            {
                base.OnMouseUp(e);
            }
            else if ((e.Button == MouseButtons.Left) && this.filterButtonPressed)
            {
                this.filterButtonPressed = false;
                this.filterButtonOver = false;
                this.RepaintCell();
                if (this.filterButtonImageBounds.Contains(e.X, e.Y) && (this._FilterPopup != null))
                {
                    this._FilterPopup(this, new EDGVFilterEventArgs(this.FilterMenu, base.OwningColumn));
                }
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (this.FilterEnabled && (base.SortGlyphDirection != SortOrder.None))
            {
                base.SortGlyphDirection = SortOrder.None;
            }
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            if (this.FilterEnabled && paintParts.HasFlag(DataGridViewPaintParts.ContentBackground))
            {
                this.filterButtonOffsetBounds = this.GetFilterBounds(true);
                this.filterButtonImageBounds = this.GetFilterBounds(false);
                Rectangle filterButtonOffsetBounds = this.filterButtonOffsetBounds;
                if (clipBounds.IntersectsWith(filterButtonOffsetBounds))
                {
                    ControlPaint.DrawBorder(graphics, filterButtonOffsetBounds, Color.Gray, ButtonBorderStyle.Solid);
                    filterButtonOffsetBounds.Inflate(-1, -1);
                    using (Brush brush = new SolidBrush(this.filterButtonOver ? Color.LightGray : Color.White))
                    {
                        graphics.FillRectangle(brush, filterButtonOffsetBounds);
                    }
                    graphics.DrawImage(this.filterImage, filterButtonOffsetBounds);
                }
            }
        }

        private void RefreshImage()
        {
            if (this.ActiveFilterType == EDGVFilterMenuFilterType.Loaded)
            {
                this.filterImage = Resources.SavedFilter;
            }
            else if (this.ActiveFilterType == EDGVFilterMenuFilterType.None)
            {
                if (this.ActiveSortType == EDGVFilterMenuSortType.None)
                {
                    this.filterImage = Resources.AddFilter;
                }
                else if (this.ActiveSortType == EDGVFilterMenuSortType.ASC)
                {
                    this.filterImage = Resources.ASC;
                }
                else
                {
                    this.filterImage = Resources.DESC;
                }
            }
            else if (this.ActiveSortType == EDGVFilterMenuSortType.None)
            {
                this.filterImage = Resources.Filter;
            }
            else if (this.ActiveSortType == EDGVFilterMenuSortType.ASC)
            {
                this.filterImage = Resources.FilterASC;
            }
            else
            {
                this.filterImage = Resources.FilterDESC;
            }
        }

        private void RepaintCell()
        {
            if (this.Displayed && (base.DataGridView != null))
            {
                base.DataGridView.InvalidateCell(this);
            }
        }

        public void SetLoadedFilterMode(bool Enabled)
        {
            this.FilterMenu.SetLoadedFilterMode(Enabled);
            this.RefreshImage();
            this.RepaintCell();
        }

        public EDGVFilterMenu FilterMenu { get; private set; }

        public Size MinimumSize
        {
            get
            {
                return new Size((this.filterButtonImageSize.Width + this.filterButtonMargin.Left) + this.filterButtonMargin.Right, (this.filterButtonImageSize.Height + this.filterButtonMargin.Bottom) + this.filterButtonMargin.Top);
            }
        }

        public EDGVFilterMenuSortType ActiveSortType
        {
            get
            {
                return (((this.FilterMenu == null) || !this.FilterEnabled) ? EDGVFilterMenuSortType.None : this.FilterMenu.ActiveSortType);
            }
        }

        public EDGVFilterMenuFilterType ActiveFilterType
        {
            get
            {
                return (((this.FilterMenu == null) || !this.FilterEnabled) ? EDGVFilterMenuFilterType.None : this.FilterMenu.ActiveFilterType);
            }
        }

        public string SortString
        {
            get
            {
                return (((this.FilterMenu == null) || !this.FilterEnabled) ? "" : this.FilterMenu.SortString);
            }
        }

        public string FilterString
        {
            get
            {
                return (((this.FilterMenu == null) || !this.FilterEnabled) ? "" : this.FilterMenu.FilterString);
            }
        }

        public bool FilterEnabled
        {
            get
            {
                return this.filterEnabled;
            }
            set
            {
                if (!value)
                {
                    this.filterButtonPressed = false;
                    this.filterButtonOver = false;
                }
                if (value != this.filterEnabled)
                {
                    this.filterEnabled = value;
                    bool flag = false;
                    if (this.FilterMenu.FilterString.Length > 0)
                    {
                        this.FilterMenu_FilterChanged(this, new EventArgs());
                        flag = true;
                    }
                    if (this.FilterMenu.SortString.Length > 0)
                    {
                        this.FilterMenu_SortChanged(this, new EventArgs());
                        flag = true;
                    }
                    if (!flag)
                    {
                        this.RepaintCell();
                    }
                }
            }
        }

        public bool DateWithTime
        {
            get
            {
                return this.FilterMenu.DateWithTime;
            }
            set
            {
                this.FilterMenu.DateWithTime = value;
            }
        }

        public bool TimeFilter
        {
            get
            {
                return this.FilterMenu.TimeFilter;
            }
            set
            {
                this.FilterMenu.TimeFilter = value;
            }
        }
    }
}
