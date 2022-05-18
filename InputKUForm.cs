using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeAggregator
{
    public partial class InputKUForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private bool _showKU = false, _approved = false;
        private Int64 _KUId, _VendorId;
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
            _KUId = KUId;
            _VendorId = VendorId;
            _showKU = true;
            labelMain.Text = "Изменение коммерческого условия";
            textBoxKUCode.Text = _KUId.ToString();
            buttonCreate.Text = "Изменить";
            buttonCreateNApprove.Text = "Изменить и утвердить";
            textBoxStatus.Visible = true;
            labelStatus.Visible = true;
            buttonCancel.Visible = true;
        }

        // Первоначальная настройка формы при её загрузке
        private void InputKUForm_Load(object sender, EventArgs e)
        {
            _connection.Open();

            // Настройка дат
            dateTimePickerDateFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerDateTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerDateFrom.CustomFormat = " ";
            dateTimePickerDateTo.CustomFormat = " ";

            if (dateTimePickerDateFrom.Format != DateTimePickerFormat.Custom)
            {
                dateTimePickerDateTo.MinDate = DateTime.Today.AddDays(1);
            }
            // doResize();

            if (_showKU)
             showSelectedKU();
        }



            //
            // Первая вкладка
            //

            // Отображение КУ, выбранного из формы списка КУ
            private void showSelectedKU()
            {
                // Загрузка всех параметров КУ
                SqlCommand command = new SqlCommand($"SELECT Period, DateFrom, DateTo, Status, Description, " +
                    $"Tax, [Return], Ofactured, Type " +
                    $"FROM KU, Vendors WHERE KU.VendorId = {_VendorId} AND KU_id = {_KUId}", _connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                
                comboBoxPeriod.SelectedItem = reader[0].ToString();
                dateTimePickerDateFrom.Format = DateTimePickerFormat.Long;
                dateTimePickerDateFrom.Value = Convert.ToDateTime(reader[1]);
                if (reader[2].ToString() != "")
                {
                    dateTimePickerDateTo.Format = DateTimePickerFormat.Long;
                    dateTimePickerDateTo.Value = Convert.ToDateTime(reader[2]);
                }
                textBoxStatus.Text = reader[3].ToString();
                richTextBoxDescription.Text = reader[4].ToString();
                
                if (reader[5].ToString() != "")
                    checkBoxTax.Checked = Convert.ToBoolean(reader[5]);
                if (reader[6].ToString() != "")
                    checkBoxReturn.Checked = Convert.ToBoolean(reader[6]);
                if (reader[7].ToString() != "")
                    checkBoxOfactured.Checked = Convert.ToBoolean(reader[7]);
                
                comboBoxKUType.SelectedItem = reader[8].ToString();
                reader.Close();

                if (textBoxStatus.Text == "Утверждено")
                    _approved = true;

                // Если КУ в статусе "Утверждено"
                if (_approved)
                {
                    buttonCreate.Visible = false;
                    buttonCreateNApprove.Visible = false;
                    buttonClose.Visible = true;
                    buttonUnapprove.Visible = true;
                    comboBoxPeriod.Enabled = false;
                    comboBoxKUType.Enabled = false;
                    dateTimePickerDateFrom.Enabled = false;
                    dateTimePickerDateTo.Enabled = false;
                    richTextBoxDescription.Enabled = false;
                    textBoxStatus.Enabled = false;
                    buttonAddAll.Enabled = false;
                    buttonAddProduct.Enabled = false;
                    buttonAddCategory.Enabled = false;
                    buttonDelete.Enabled = false;
                    toolStripMenuItemAddTerm.Enabled = false;
                    toolStripMenuItemDelTerm.Enabled = false;
                    checkBoxTax.Enabled = false;
                    checkBoxReturn.Enabled = false;
                    checkBoxOfactured.Enabled = false;
                    dataGridViewTerms.Enabled = false;
                    //dataGridViewIncluded.ReadOnly = true;
                    //dataGridViewExcluded.ReadOnly = true;
                }

                //showExInProducts(_KU_id);
               // showTerms(_KU_id);
              
               
            }
        
    }
}
