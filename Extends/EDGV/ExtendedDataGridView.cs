
namespace EDGV
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class ExtendedDataGridView : DataGridView
    {
        private List<string> sortOrder = new List<string>();
        private List<string> filterOrder = new List<string>();
        private List<string> readyToShowFilters = new List<string>();
        private string sortString;
        private string filterString;
        private bool atoGenerateContextFilters = true;
        private bool dateWithTime;
        private bool timeFilter;
        private bool loadedFilter;
        private EventHandler _SortStringChanged; // Изменил SortStringChanged на _SortStringChanged
        private EventHandler _FilterStringChanged; // Изменил FilterStringChanged на _FilterStringChanged

        public event EventHandler FilterStringChanged 
        {
            add
            {
                EventHandler filterStringChanged = this._FilterStringChanged;
                while (true)
                {
                    EventHandler comparand = filterStringChanged;
                    EventHandler handler3 = comparand + value;
                    filterStringChanged = Interlocked.CompareExchange<EventHandler>(ref this._FilterStringChanged, handler3, comparand);
                    if (ReferenceEquals(filterStringChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler filterStringChanged = this._FilterStringChanged;
                while (true)
                {
                    EventHandler comparand = filterStringChanged;
                    EventHandler handler3 = comparand - value;
                    filterStringChanged = Interlocked.CompareExchange<EventHandler>(ref this._FilterStringChanged, handler3, comparand);
                    if (ReferenceEquals(filterStringChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler SortStringChanged 
        {
            add
            {
                EventHandler sortStringChanged = this._SortStringChanged;
                while (true)
                {
                    EventHandler comparand = sortStringChanged;
                    EventHandler handler3 = comparand + value;
                    sortStringChanged = Interlocked.CompareExchange<EventHandler>(ref this._SortStringChanged, handler3, comparand);
                    if (ReferenceEquals(sortStringChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler sortStringChanged = this._SortStringChanged;
                while (true)
                {
                    EventHandler comparand = sortStringChanged;
                    EventHandler handler3 = comparand - value;
                    sortStringChanged = Interlocked.CompareExchange<EventHandler>(ref this._SortStringChanged, handler3, comparand);
                    if (ReferenceEquals(sortStringChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public void ClearFilter(bool FireEvent = false)
        {
            foreach (EDGVColumnHeaderCell cell in this.filterCells)
            {
                cell.ClearFilter();
            }
            this.filterOrder.Clear();
            if (FireEvent)
            {
                this.FilterString = null;
            }
            else
            {
                this.filterString = null;
            }
        }

        public void ClearSort(bool FireEvent = false)
        {
            foreach (EDGVColumnHeaderCell cell in this.filterCells)
            {
                cell.ClearSorting();
            }
            this.sortOrder.Clear();
            if (FireEvent)
            {
                this.SortString = null;
            }
            else
            {
                this.sortString = null;
            }
        }

        private string CreateFilterString()
        {
            StringBuilder builder = new StringBuilder("");
            string str = "";
            foreach (string str2 in this.filterOrder)
            {
                DataGridViewColumn column = base.Columns[str2];
                if (column != null)
                {
                    EDGVColumnHeaderCell headerCell = column.HeaderCell as EDGVColumnHeaderCell;
                    if ((headerCell != null) && (headerCell.FilterEnabled && (headerCell.ActiveFilterType != EDGVFilterMenuFilterType.None)))
                    {
                        builder.AppendFormat(str + "(" + headerCell.FilterString + ")", column.DataPropertyName);
                        str = " AND ";
                    }
                }
            }
            return builder.ToString();
        }

        private string CreateSortString()
        {
            StringBuilder builder = new StringBuilder("");
            string str = "";
            foreach (string str2 in this.sortOrder)
            {
                DataGridViewColumn column = base.Columns[str2];
                if (column != null)
                {
                    EDGVColumnHeaderCell headerCell = column.HeaderCell as EDGVColumnHeaderCell;
                    if ((headerCell != null) && (headerCell.FilterEnabled && (headerCell.ActiveSortType != EDGVFilterMenuSortType.None)))
                    {
                        builder.AppendFormat(str + headerCell.SortString, column.DataPropertyName);
                        str = ", ";
                    }
                }
            }
            return builder.ToString();
        }

        public void DisableFilter(DataGridViewColumn Column)
        {
            if (base.Columns.Contains(Column))
            {
                EDGVColumnHeaderCell headerCell = Column.HeaderCell as EDGVColumnHeaderCell;
                if (headerCell != null)
                {
                    if (!headerCell.FilterEnabled || ((headerCell.SortString.Length <= 0) && (headerCell.FilterString.Length <= 0)))
                    {
                        headerCell.FilterEnabled = false;
                    }
                    else
                    {
                        this.ClearFilter(true);
                        headerCell.FilterEnabled = false;
                    }
                    this.filterOrder.Remove(Column.Name);
                    this.sortOrder.Remove(Column.Name);
                    this.readyToShowFilters.Remove(Column.Name);
                }
                Column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void eFilterChanged(object sender, EDGVFilterEventArgs e)
        {
            if (base.Columns.Contains(e.Column))
            {
                Func<EDGVColumnHeaderCell, bool> predicate = null;
                EDGVFilterMenu FilterMenu = e.FilterMenu;
                DataGridViewColumn column = e.Column;
                this.filterOrder.Remove(column.Name);
                if (FilterMenu.ActiveFilterType != EDGVFilterMenuFilterType.None)
                {
                    this.filterOrder.Add(column.Name);
                }
                this.FilterString = this.CreateFilterString();
                if (this.loadedFilter)
                {
                    this.loadedFilter = false;
                    if (predicate == null)
                    {
                        predicate = f => !ReferenceEquals(f.FilterMenu, FilterMenu);
                    }
                    foreach (EDGVColumnHeaderCell cell in this.filterCells.Where<EDGVColumnHeaderCell>(predicate))
                    {
                        cell.SetLoadedFilterMode(false);
                    }
                }
            }
        }

        private void eFilterPopup(object sender, EDGVFilterEventArgs e)
        {
            if (base.Columns.Contains(e.Column))
            {
                EDGVFilterMenu filterMenu = e.FilterMenu;
                DataGridViewColumn column = e.Column;
                Rectangle rectangle = base.GetCellDisplayRectangle(column.Index, -1, true);
                if (this.readyToShowFilters.Contains(column.Name))
                {
                    filterMenu.Show(this, rectangle.Left, rectangle.Bottom/*, false*/);
                }
                else
                {
                    this.readyToShowFilters.Add(column.Name);
                    if ((this.filterOrder.Count<string>() > 0) && (this.filterOrder.Last<string>() == column.Name))
                    {
                        filterMenu.Show(this, rectangle.Left, rectangle.Bottom/*, true*/);
                    }
                    else
                    {
                        filterMenu.Show(this, rectangle.Left, rectangle.Bottom/*, EDGVFilterMenu.GetValuesForFilter(this, column.Name)*/);
                    }
                }
            }
        }

        public void EnableFilter(DataGridViewColumn Column)
        {
            if (base.Columns.Contains(Column))
            {
                EDGVColumnHeaderCell headerCell = Column.HeaderCell as EDGVColumnHeaderCell;
                if (headerCell != null)
                {
                    this.EnableFilter(Column, headerCell.DateWithTime, headerCell.TimeFilter);
                }
                else
                {
                    this.EnableFilter(Column, this.DateWithTime, this.TimeFilter);
                }
            }
        }

        public void EnableFilter(DataGridViewColumn Column, bool DateWithTime, bool TimeFilter)
        {
            if (base.Columns.Contains(Column))
            {
                EDGVColumnHeaderCell headerCell = Column.HeaderCell as EDGVColumnHeaderCell;
                if (headerCell != null)
                {
                    if ((headerCell.DateWithTime != DateWithTime) || ((headerCell.TimeFilter != TimeFilter) || (!headerCell.FilterEnabled && ((headerCell.FilterString.Length > 0) || (headerCell.SortString.Length > 0)))))
                    {
                        this.ClearFilter(true);
                    }
                    headerCell.DateWithTime = DateWithTime;
                    headerCell.TimeFilter = TimeFilter;
                    headerCell.FilterEnabled = true;
                    this.readyToShowFilters.Remove(Column.Name);
                }
                else
                {
                    Column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    headerCell = new EDGVColumnHeaderCell(Column.HeaderCell, true)
                    {
                        DateWithTime = this.DateWithTime,
                        TimeFilter = this.TimeFilter
                    };
                    headerCell.SortChanged += new EDGVFilterEventHandler(this.eSortChanged);
                    headerCell.FilterChanged += new EDGVFilterEventHandler(this.eFilterChanged);
                    headerCell.FilterPopup += new EDGVFilterEventHandler(this.eFilterPopup);
                    Column.MinimumWidth = headerCell.MinimumSize.Width;
                    if (base.ColumnHeadersHeight < headerCell.MinimumSize.Height)
                    {
                        base.ColumnHeadersHeight = headerCell.MinimumSize.Height;
                    }
                    Column.HeaderCell = headerCell;
                }
                Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void eSortChanged(object sender, EDGVFilterEventArgs e)
        {
            if (base.Columns.Contains(e.Column))
            {
                EDGVFilterMenu filterMenu = e.FilterMenu;
                DataGridViewColumn column = e.Column;
                this.sortOrder.Remove(column.Name);
                if (filterMenu.ActiveSortType != EDGVFilterMenuSortType.None)
                {
                    this.sortOrder.Add(column.Name);
                }
                this.SortString = this.CreateSortString();
            }
        }

        public DataGridViewCell FindCell(string ValueToFind, string ColumnName = null, int RowIndex = 0, int ColumnIndex = 0, bool isWholeWordSearch = true, bool isCaseSensitive = false)
        {
            int index;
            int num2;
            int num3;
            int num4;
            if ((ValueToFind == null) || ((base.RowCount <= 0) || ((base.ColumnCount <= 0) || ((ColumnName != null) && (!base.Columns.Contains(ColumnName) || !base.Columns[ColumnName].Visible)))))
            {
                goto TR_0000;
            }
            else
            {
                RowIndex = Math.Max(0, RowIndex);
                if (!isCaseSensitive)
                {
                    ValueToFind = ValueToFind.ToLower();
                }
                if (ColumnName == null)
                {
                    ColumnIndex = Math.Max(0, ColumnIndex);
                    num3 = RowIndex;
                    while (true)
                    {
                        if (num3 >= base.RowCount)
                        {
                            break;
                        }
                        num4 = ColumnIndex;
                        while (true)
                        {
                            if (num4 < base.ColumnCount)
                            {
                                string str2 = base.Rows[num3].Cells[num4].FormattedValue.ToString();
                                if (!isCaseSensitive)
                                {
                                    str2 = str2.ToLower();
                                }
                                if (isWholeWordSearch || !str2.Contains(ValueToFind))
                                {
                                    if (!str2.Equals(ValueToFind))
                                    {
                                        num4++;
                                    }
                                    else
                                    {
                                        goto TR_000F;
                                    }
                                }
                                else
                                {
                                    goto TR_000F;
                                }
                                continue;
                            }
                            else
                            {
                                ColumnIndex = 0;
                                num3++;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    index = base.Columns[ColumnName].Index;
                    if (ColumnIndex > index)
                    {
                        RowIndex++;
                    }
                    num2 = RowIndex;
                    while (true)
                    {
                        if (num2 >= base.RowCount)
                        {
                            break;
                        }
                        string str = base.Rows[num2].Cells[index].FormattedValue.ToString();
                        if (!isCaseSensitive)
                        {
                            str = str.ToLower();
                        }
                        if (isWholeWordSearch || !str.Contains(ValueToFind))
                        {
                            if (!str.Equals(ValueToFind))
                            {
                                num2++;
                            }
                            else
                            {
                                goto TR_0001;
                            }
                        }
                        else
                        {
                            goto TR_0001;
                        }
                    }
                }
                goto TR_0000;
            }
            goto TR_000F;
        TR_0000:
            return null;
        TR_0001:
            return base.Rows[num2].Cells[index];
        TR_000F:
            return base.Rows[num3].Cells[num4];
        }

        public void LoadFilter(string Filter, string Sorting = null)
        {
            foreach (EDGVColumnHeaderCell cell in this.filterCells)
            {
                cell.SetLoadedFilterMode(true);
            }
            this.filterOrder.Clear();
            this.sortOrder.Clear();
            this.readyToShowFilters.Clear();
            if (Filter != null)
            {
                this.FilterString = Filter;
            }
            if (Sorting != null)
            {
                this.SortString = Sorting;
            }
            this.loadedFilter = true;
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            this.readyToShowFilters.Remove(base.Columns[e.ColumnIndex].Name);
            base.OnCellValueChanged(e);
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            EDGVColumnHeaderCell cell = new EDGVColumnHeaderCell(e.Column.HeaderCell, this.AutoGenerateContextFilters)
            {
                DateWithTime = this.DateWithTime,
                TimeFilter = this.TimeFilter
            };
            cell.SortChanged += new EDGVFilterEventHandler(this.eSortChanged);
            cell.FilterChanged += new EDGVFilterEventHandler(this.eFilterChanged);
            cell.FilterPopup += new EDGVFilterEventHandler(this.eFilterPopup);
            e.Column.MinimumWidth = cell.MinimumSize.Width;
            if (base.ColumnHeadersHeight < cell.MinimumSize.Height)
            {
                base.ColumnHeadersHeight = cell.MinimumSize.Height;
            }
            e.Column.HeaderCell = cell;
            base.OnColumnAdded(e);
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            this.readyToShowFilters.Remove(e.Column.Name);
            this.filterOrder.Remove(e.Column.Name);
            this.sortOrder.Remove(e.Column.Name);
            EDGVColumnHeaderCell headerCell = e.Column.HeaderCell as EDGVColumnHeaderCell;
            if (headerCell != null)
            {
                headerCell.SortChanged -= new EDGVFilterEventHandler(this.eSortChanged);
                headerCell.FilterChanged -= new EDGVFilterEventHandler(this.eFilterChanged);
                headerCell.FilterPopup -= new EDGVFilterEventHandler(this.eFilterPopup);
            }
            base.OnColumnRemoved(e);
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            this.readyToShowFilters.Clear();
            base.OnRowsAdded(e);
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            this.readyToShowFilters.Clear();
            base.OnRowsRemoved(e);
        }

        protected override void OnSorted(EventArgs e)
        {
            this.ClearSort(false);
            base.OnSorted(e);
        }

        public bool AutoGenerateContextFilters
        {
            get
            {
                return this.atoGenerateContextFilters;
            }
            set
            {
                this.atoGenerateContextFilters = value;
            }
        }

        public bool DateWithTime
        {
            get
            {
                return this.dateWithTime;
            }
            set
            {
                this.dateWithTime = value;
            }
        }

        public bool TimeFilter
        {
            get
            {
                return this.timeFilter;
            }
            set
            {
                this.timeFilter = value;
            }
        }

        private IEnumerable<EDGVColumnHeaderCell> filterCells
        {
            get
            {
                return (from c in base.Columns.Cast<DataGridViewColumn>()
                        where (c.HeaderCell != null) && (c.HeaderCell is EDGVColumnHeaderCell)
                        select c.HeaderCell as EDGVColumnHeaderCell);
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
                if (value != this.sortString)
                {
                    this.sortString = value;
                    if (base.SortedColumn != null)
                    {
                        base.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                    if (this._SortStringChanged != null)
                    {
                        this._SortStringChanged(this, new EventArgs());
                    }
                }
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
                if (value != this.filterString)
                {
                    this.filterString = value;
                    if (this._FilterStringChanged != null)
                    {
                        this._FilterStringChanged(this, new EventArgs());
                    }
                }
            }
        }
    }
}
