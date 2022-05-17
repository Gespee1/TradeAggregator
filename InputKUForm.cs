using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeAggregator
{
    public partial class InputKUForm : Form
    {
        private bool _showKU = false;
        private Int64 _KU_id, _VendorId;
        //
        // Конструкторы
        //
        public InputKUForm()
        {
            InitializeComponent();
        }
        // Конструктор для изменения выбранного КУ в форме списка КУ
        public InputKUForm(Int64 KUId, Int64 VendorId)
        {
            InitializeComponent();
            _KU_id = KUId;
            _VendorId = VendorId;
            _showKU = true;

          /*  textBoxKUCode.Text = _KU_id.ToString();
            buttonCreate.Text = "Изменить";
            buttonCreateNApprove.Text = "Изменить и утвердить";
            comboBoxVendor.Enabled = false;
            textBoxStatus.Visible = true;
            labelStatus.Visible = true;
            buttonCancel.Visible = true;*/
        }
    }
}
