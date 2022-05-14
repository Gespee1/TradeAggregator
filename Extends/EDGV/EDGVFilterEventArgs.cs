
namespace EDGV
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class EDGVFilterEventArgs : EventArgs
    {
        public EDGVFilterEventArgs(EDGVFilterMenu filterMenu, DataGridViewColumn column)
        {
            this.FilterMenu = filterMenu;
            this.Column = column;
        }

        public EDGVFilterMenu FilterMenu { get; private set; }

        public DataGridViewColumn Column { get; private set; }
    }
}
