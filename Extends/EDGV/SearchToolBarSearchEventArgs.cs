
namespace EDGV
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SearchToolBarSearchEventArgs : EventArgs
    {
        public SearchToolBarSearchEventArgs(string Value, DataGridViewColumn Column, bool Case, bool Whole, bool fromBegin)
        {
            this.ValueToSearch = Value;
            this.ColumnToSearch = Column;
            this.CaseSensitive = Case;
            this.WholeWord = Whole;
            this.FromBegin = fromBegin;
        }

        public string ValueToSearch { get; private set; }

        public DataGridViewColumn ColumnToSearch { get; private set; }

        public bool CaseSensitive { get; private set; }

        public bool WholeWord { get; private set; }

        public bool FromBegin { get; private set; }
    }
}
