using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TradeAggregator
{
    public partial class InputKUForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private bool _showKU = false, _wasChanged = false, _approved = false, _allowFormClose = false;
        private Int64 _KUId, _VendorId;
        private List<Int64> _ProdIds = new List<Int64>();
        private List<string> _CategoryID = new List<string>();
        private DataTable _BrandProd = new DataTable();
        //
        // Конструкторы
        //
        public InputKUForm(Int64 VendorId)
        {
            InitializeComponent();
            _VendorId = VendorId;
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

            loadProducerBrand();
            // doResize();

            if (_showKU)
             showSelectedKU();
            else
            {
                hideTabPageServices();                
            }
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
                    $"FROM KU WHERE VendorId = {_VendorId} AND RecId = {_KUId}", _connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                
                comboBoxPeriod.SelectedIndex = Convert.ToInt32(reader[0]);
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
                    dataGridViewSale.Enabled = false;
                //dataGridViewIncluded.ReadOnly = true;
                // dataGridViewExcluded.ReadOnly = true;
            }

                hideTabPageServices();
                showExInProducts(_KUId);
                showTerms(_KUId);
                
                             
            }

        // Добавление/изменение КУ в БД
        private void addOrUpdateKU(string status, bool addOrUpdate = true) // addOrUpdate: true -> add, false -> update
        {
            // Проверка на неповторность временного периода
            List<DateTime> dateFrom = new List<DateTime>(), dateTo = new List<DateTime>();
            DateTime currDateFrom = dateTimePickerDateFrom.Value, currDateTo = dateTimePickerDateTo.Value;

            SqlCommand command =
                addOrUpdate == true ? new SqlCommand($"SELECT DateFrom, DateTo FROM KU WHERE VendorId = {_VendorId}", _connection)
                : new SqlCommand($"SELECT DateFrom, DateTo FROM KU WHERE VendorId = " +
                $"{_VendorId} AND RecId != {_KUId}", _connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dateFrom.Add(Convert.ToDateTime(reader[0]));
                dateTo.Add(Convert.ToDateTime(reader[1]));
            }
            reader.Close();

            // цикл с проверкой
            for (int i = 0; i < dateFrom.Count; i++)
            {
                if (dateFrom[i] < currDateTo && currDateFrom < dateTo[i])
                {
                    MessageBox.Show($"В базе данных уже содержится информация о коммерческих условиях в обозначенный период с " +
                        $"{currDateFrom.ToShortDateString()} по {currDateTo.ToShortDateString()}", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string date_to;
            // Создание КУ
            if (dateTimePickerDateTo.Format == DateTimePickerFormat.Custom)
            {
                date_to = "NULL";
            }
            else
            {
                date_to = "'" + Convert.ToString(dateTimePickerDateTo.Value) + "'";
            }

            command = addOrUpdate == true ? new SqlCommand(
                $"INSERT INTO KU (VendorId, Period, DateFrom, DateTo, Status, Description, Tax, [Return], Ofactured,  Type)" +
                $" VALUES ('{_VendorId}','{comboBoxPeriod.SelectedIndex}', '{dateTimePickerDateFrom.Value.ToShortDateString()}', " +
                $"{date_to}, '{status}', '{richTextBoxDescription.Text}', '{checkBoxTax.Checked}', '{checkBoxReturn.Checked}', " +
                $"'{checkBoxOfactured.Checked}', '{comboBoxKUType.SelectedItem}')", _connection)
                : new SqlCommand( // Изменение КУ
                $"UPDATE KU SET Period = '{comboBoxPeriod.SelectedIndex}', DateFrom = '{dateTimePickerDateFrom.Value.ToShortDateString()}', " +
                $"DateTo = '{dateTimePickerDateTo.Value.ToShortDateString()}', Status = '{status}', Description = '{richTextBoxDescription.Text}', " +
                $"Tax = '{checkBoxTax.Checked}', [Return] = '{checkBoxReturn.Checked}', Ofactured = '{checkBoxOfactured.Checked}', " +
                $"Type = '{comboBoxKUType.SelectedItem}' WHERE RecId = {_KUId}", _connection);
            command.ExecuteNonQuery();

            if (addOrUpdate == true)
            {
                // Получение номера только что созданного КУ
                command = new SqlCommand($"SELECT RecId FROM KU WHERE VendorId = {_VendorId} AND DateFrom = " +
                    $"'{dateTimePickerDateFrom.Value.ToShortDateString()}' AND DateTo = '{dateTimePickerDateTo.Value.ToShortDateString()}'", _connection);
                _KUId = Convert.ToInt64(command.ExecuteScalar());
            }
            else
            {
                if (comboBoxKUType.SelectedIndex == 0)
                {
                    //Удаление условий бонуса в БД для последующей перезаписи
                    command = new SqlCommand($"DELETE FROM Terms WHERE KUId = '{_KUId}'", _connection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    //Удаление условий акции в БД для последующей перезаписи
                    command = new SqlCommand($"DELETE FROM Sales WHERE KUId = '{_KUId}'", _connection);
                    command.ExecuteNonQuery();
                }
            }
            if (comboBoxKUType.SelectedIndex == 0)
            {
                //Запись условий бонуса в БД
                for (int i = 0; i < dataGridViewTerms.RowCount; i++)
                {
                    command = new SqlCommand(
                        $"INSERT INTO Terms (KUId, Fixed, Criteria, [Percent/Amount], Total) VALUES ('{_KUId}', '{dataGridViewTerms.Rows[i].Cells["FixSum"].Value}'," +
                        $" '{dataGridViewTerms.Rows[i].Cells["Criterion"].Value}', '{dataGridViewTerms.Rows[i].Cells["PercentSum"].Value}', " +
                        $"'{dataGridViewTerms.Rows[i].Cells["Total"].Value}')", _connection);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                //Запись условий акции в БД
                for (int i = 0; i < dataGridViewSale.RowCount; i++)
                {
                    command = new SqlCommand(
                        $"INSERT INTO Sales (KUId, SalePercent) VALUES ('{_KUId}', '{dataGridViewSale.Rows[i].Cells["SalePercent"].Value}')", _connection);
                    command.ExecuteNonQuery();
                }
            }
            // МЕТОД ДЛЯ СОХРАНЕНИЯ iN/EX
            AddInExBD();

            if (addOrUpdate == true)
            {
                comboBoxPeriod.SelectedIndex = -1;
                comboBoxKUType.SelectedIndex = -1;
                richTextBoxDescription.Text = "";
                checkBoxTax.Checked = Convert.ToBoolean(0);
                checkBoxReturn.Checked = Convert.ToBoolean(0);
                checkBoxOfactured.Checked = Convert.ToBoolean(0);
                textBoxStatus.Text = "";
                dateTimePickerDateFrom.Format = DateTimePickerFormat.Custom;
                dateTimePickerDateTo.Format = DateTimePickerFormat.Custom;
                dataGridViewTerms.Rows.Clear();
                dataGridViewSale.Rows.Clear();
                dataGridViewIncluded.Rows.Clear();
                dataGridViewExcluded.Rows.Clear();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Изменение минимальной даты окончания, в зависимости от выбранной даты начала
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerDateTo.MinDate = dateTimePickerDateFrom.Value.AddDays(1);
            dateTimePickerDateFrom.Format = DateTimePickerFormat.Long;
            _wasChanged = true;
        }
        // Изменение формата календаря даты окончания КУ
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerDateTo.Format = DateTimePickerFormat.Long;
            _wasChanged = true;
        }
       
        // Изменение типа КУ
        private void comboBoxKUType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideTabPageServices();
        }

        //
        // Вторая вкладка
        //
        // Отображение условий ретро
        private void showTerms(Int64 KUId)
        {
            dataGridViewTerms.Rows.Clear();
            dataGridViewSale.Rows.Clear();
            if (comboBoxKUType.SelectedIndex == 0)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Terms WHERE KUId = {KUId}", _connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataGridViewTerms.Rows.Add();
                    (dataGridViewTerms.Rows[dataGridViewTerms.RowCount - 1].Cells["FixSum"] as DataGridViewCheckBoxCell).Value = reader[2];
                    dataGridViewTerms.Rows[dataGridViewTerms.RowCount - 1].Cells["Criterion"].Value = reader[3];
                    dataGridViewTerms.Rows[dataGridViewTerms.RowCount - 1].Cells["PercentSum"].Value = reader[4];
                    dataGridViewTerms.Rows[dataGridViewTerms.RowCount - 1].Cells["Total"].Value = reader[5];

                }
                reader.Close();
            }
            else
            {
                SqlCommand command = new SqlCommand($"SELECT SalePercent FROM Sales WHERE KUId = {KUId}", _connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataGridViewSale.Rows.Add();
                    dataGridViewSale.Rows[dataGridViewSale.RowCount - 1].Cells["SalePercent"].Value = reader[0];
                }
                reader.Close();
            }
            
            
        }

        //
        // Таблицы вкл. и искл. товаров
        //
        // Очистка таблиц вкл и искл товаров + доб. строки по умолчанию
        private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_showKU == false)
            {
                dataGridViewIncluded.Rows.Clear();
                dataGridViewExcluded.Rows.Clear();

                //Добавление условия "Все" при создании КУ
                dataGridViewIncluded.Rows.Add();
                dataGridViewIncluded.Rows[0].Cells["TypeP"].Value = "Все";

                
            }
        }

        // Загрузка производителя и марки в combobox'ы в таблицах искл и вкл товаров
        private void loadProducerBrand()
        {
            DataGridViewComboBoxColumn combo1 = dataGridViewIncluded.Columns["ProducerP"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn combo2 = dataGridViewIncluded.Columns["BrandP"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn combo3 = dataGridViewExcluded.Columns["ProducerM"] as DataGridViewComboBoxColumn;
            DataGridViewComboBoxColumn combo4 = dataGridViewExcluded.Columns["BrandM"] as DataGridViewComboBoxColumn;
            combo1.Items.Clear();
            combo2.Items.Clear();
            combo3.Items.Clear();
            combo4.Items.Clear();

            SqlCommand comm = new SqlCommand("SELECT DISTINCT ID, Producer, Brand FROM BrandProducer ORDER BY Producer", _connection);
            SqlDataAdapter adapt = new SqlDataAdapter(comm);

            adapt.Fill(_BrandProd);

            combo1.DataSource = _BrandProd;
            combo1.DisplayMember = "Producer";
            combo1.ValueMember = "Producer";
            combo2.DataSource = _BrandProd;
            combo2.DisplayMember = "Brand";
            combo2.ValueMember = "Brand";
            combo3.DataSource = _BrandProd;
            combo3.DisplayMember = "Producer";
            combo3.ValueMember = "Producer";
            combo4.DataSource = _BrandProd;
            combo4.DisplayMember = "Brand";
            combo4.ValueMember = "Brand";
        }

        // Отображение добавленных и исключенных из расчета продуктов
        private void showExInProducts(Int64 KUId) // was: 470-532, mow: 470-524
        {
            int idx = 0;
            DataGridView dgv;
            SqlCommand command;
            SqlDataReader reader;

            while (idx < 2)
            {
                if (idx == 0)
                {
                    dgv = dataGridViewIncluded;
                    command = new SqlCommand($"SELECT * FROM Included_products WHERE KU_id = {KUId}", _connection);
                }
                else
                {
                    dgv = dataGridViewExcluded;
                    command = new SqlCommand($"SELECT * FROM Excluded_products WHERE KU_id = {KUId}", _connection);
                }
                dgv.Rows.Clear();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dgv.Rows.Add();
                    dgv.Rows[dgv.RowCount - 1].Cells[0].Value = reader[0];
                    dgv.Rows[dgv.RowCount - 1].Cells[1].Value = reader[1];
                    dgv.Rows[dgv.RowCount - 1].Cells[2].Value = reader[2];
                    dgv.Rows[dgv.RowCount - 1].Cells[3].Value = reader[3];
                    dgv.Rows[dgv.RowCount - 1].Cells[4].Value = reader[4];
                    if (reader[5].ToString() != "")
                    {
                        for (int i = 0; i < _BrandProd.Rows.Count; i++)
                        {
                            if ((Int64)reader[5] == (Int64)_BrandProd.Rows[i][0])
                            {
                                (dgv.Rows[dgv.RowCount - 1].Cells[5] as DataGridViewComboBoxCell).Value = _BrandProd.Rows[i][1].ToString();
                                (dgv.Rows[dgv.RowCount - 1].Cells[6] as DataGridViewComboBoxCell).Value = _BrandProd.Rows[i][2].ToString();
                                break;
                            }
                        }
                    }

                    // Запрет выбора произв и торг марки для товаров
                    if (dgv.Rows[dgv.RowCount - 1].Cells[2].Value.ToString() == "Товары")
                    {
                        dgv.Rows[dgv.RowCount - 1].Cells[5].ReadOnly = true;
                        dgv.Rows[dgv.RowCount - 1].Cells[6].ReadOnly = true;
                    }
                }
                reader.Close();
                idx++;
            }
        }

        // Добавление строк в таблицы включения и исключения
        private void addLine(string type)
        {

            //Int64 KU_id = _KU_id;
            Int16 tabPageId = Convert.ToInt16(tabControlInEx.SelectedIndex);
            SqlCommand command;
            switch (type)
            {
                case "Все":
                    if (tabPageId == 0)
                    {
                        for (int i = 0; i < dataGridViewIncluded.RowCount; i++)
                        {
                            if (dataGridViewIncluded.Rows[i].Cells["TypeP"].Value.ToString() == "Все")
                            {
                                MessageBox.Show("Данное условие уже добавлено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        dataGridViewIncluded.Rows.Add();
                        dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["TypeP"].Value = "Все";

                    }
                    else
                    {
                        DialogResult result = DialogResult.Yes;
                        // Проверка, есть ли условие "все" в условиях на добавление
                        for (int i = 0; i < dataGridViewIncluded.RowCount; i++)
                        {
                            if (dataGridViewIncluded.Rows[i].Cells["TypeP"].Value.ToString() == "Все" && (dataGridViewIncluded.Rows[i].Cells["ProducerP"] as DataGridViewComboBoxCell).Value is null && (dataGridViewIncluded.Rows[i].Cells["BrandP"] as DataGridViewComboBoxCell).Value is null)
                                result = MessageBox.Show("В условиях на добавление есть условие 'Все', если добавить условие 'Все' для исключения, ни один товар не будет выбран.\nВы уверены, что хотите добавить это условие?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }
                        if (result == DialogResult.Yes)
                        {
                            for (int i = 0; i < dataGridViewExcluded.RowCount; i++)
                            {
                                if (dataGridViewExcluded.Rows[i].Cells["TypeM"].Value.ToString() == "Все")
                                {
                                    MessageBox.Show("Данное условие уже добавлено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            dataGridViewExcluded.Rows.Add();
                            dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["TypeM"].Value = "Все";

                        }
                    }

                    break;
                case "Категория":
                    if (tabPageId == 0)
                    {
                        dataGridViewIncluded.Rows.Add();
                        dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["TypeP"].Value = type;
                        dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["Attribute1P"].Value = _CategoryID[0];
                        dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["Attribute2P"].Value = findCategoryNameById(_CategoryID[0]);

                    }
                    else
                    {
                        dataGridViewExcluded.Rows.Add();
                        dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["TypeM"].Value = type;
                        dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["Attribute1M"].Value = _CategoryID[0];
                        dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["Attribute2M"].Value = findCategoryNameById(_CategoryID[0]);

                    }

                    break;
                case "Товары":
                    for (int i = 0; i < _ProdIds.Count; i++)
                    {
                        string _ProdName;
                        if (tabPageId == 0)
                        {
                            dataGridViewIncluded.Rows.Add();
                            dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["TypeP"].Value = type;
                            dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["Attribute1P"].Value = _ProdIds[i];
                            command = new SqlCommand($"SELECT Name FROM Products WHERE ProductID = {_ProdIds[i]}", _connection);
                            _ProdName = command.ExecuteScalar().ToString();
                            dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["Attribute2P"].Value = _ProdName;

                            if (dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["TypeP"].Value.ToString() == "Товары")
                            {
                                dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["ProducerP"].ReadOnly = true;
                                dataGridViewIncluded.Rows[dataGridViewIncluded.RowCount - 1].Cells["BrandP"].ReadOnly = true;
                            }

                        }
                        else
                        {
                            dataGridViewExcluded.Rows.Add();
                            dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["TypeM"].Value = type;
                            dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["Attribute1M"].Value = _ProdIds[i];
                            command = new SqlCommand($"SELECT Name FROM Products WHERE ProductID = {_ProdIds[i]}", _connection);
                            _ProdName = command.ExecuteScalar().ToString();
                            dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["Attribute2M"].Value = _ProdName;

                            if (dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["TypeM"].Value.ToString() == "Товары")
                            {
                                dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["ProducerM"].ReadOnly = true;
                                dataGridViewExcluded.Rows[dataGridViewExcluded.RowCount - 1].Cells["BrandM"].ReadOnly = true;
                            }

                        }

                    }

                    break;
            }

        }

        // Запись в бд для in/ex через создание ку
        private void AddInExBD() // was: 619-738, now: 619-693
        {
            DataGridView dgv = dataGridViewIncluded;
            string table = "Included_products", type = "TypeP", producer = "ProducerP", attr = "Attribute1P", attr2 = "Attribute2P";
            SqlCommand command;
            int counter = 0;

            while (counter < 2)
            {
                if (buttonCreate.Text.ToString() == "Изменить")
                {
                    command = new SqlCommand($"DELETE FROM {table} WHERE KU_id = '{_KUId}'", _connection);
                    command.ExecuteNonQuery();
                }
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    switch (dgv.Rows[i].Cells[type].Value.ToString())
                    {
                        case "Все":
                            command = new SqlCommand($"INSERT INTO {table} (KU_id, Type) VALUES ({_KUId}, '{dgv.Rows[i].Cells[type].Value}')", _connection);
                            command.ExecuteNonQuery();

                            //производитель и марка
                            if (!((dgv.Rows[i].Cells[producer] as DataGridViewComboBoxCell).Value is null))
                            {
                                command = new SqlCommand($"UPDATE {table} SET BrandProdID = " +
                                    $"'{findBrandProdId(dgv.Rows[i].Cells[producer].Value.ToString(), 1)}' WHERE KU_id = '{_KUId}' AND Type = " +
                                    $"'{dgv.Rows[i].Cells[type].Value}'", _connection);
                                command.ExecuteNonQuery();
                            }
                            break;

                        case "Категория":
                            command = new SqlCommand($"INSERT INTO {table} (KU_id, Type, Attribute_1, Attribute_2) VALUES ({_KUId}, " +
                                $"'{dgv.Rows[i].Cells[type].Value}', '{dgv.Rows[i].Cells[attr].Value}', '{dgv.Rows[i].Cells[attr2].Value}')", _connection);
                            command.ExecuteNonQuery();

                            //производитель и марка
                            if (!((dgv.Rows[i].Cells[producer] as DataGridViewComboBoxCell).Value is null))
                            {
                                command = new SqlCommand($"UPDATE {table} SET BrandProdID = " +
                                    $"'{findBrandProdId(dgv.Rows[i].Cells[producer].Value.ToString(), 1)}' WHERE KU_id = '{_KUId}' AND Type = " +
                                    $"'{dgv.Rows[i].Cells[type].Value}' AND Attribute_1 = '{dgv.Rows[i].Cells[attr].Value}' " +
                                    $"AND Attribute_2 = '{dgv.Rows[i].Cells[attr2].Value}'", _connection);
                                command.ExecuteNonQuery();
                            }
                            break;

                        case "Товары":
                            command = new SqlCommand($"INSERT INTO {table} (KU_id, Type, Attribute_1, Attribute_2) VALUES ({_KUId}, " +
                                $"'{dgv.Rows[i].Cells[type].Value}', '{dgv.Rows[i].Cells[attr].Value}', '{dgv.Rows[i].Cells[attr2].Value}')", _connection);
                            command.ExecuteNonQuery();

                            //производитель и марка
                            if (!((dgv.Rows[i].Cells[producer] as DataGridViewComboBoxCell).Value is null))
                            {
                                command = new SqlCommand($"UPDATE {table} SET BrandProdID = " +
                                    $"'{findBrandProdId(dgv.Rows[i].Cells[producer].Value.ToString(), 1)}' WHERE KU_id = '{_KUId}' AND Type = " +
                                    $"'{dgv.Rows[i].Cells[type].Value}' AND Attribute_1 = '{dgv.Rows[i].Cells[attr].Value}' " +
                                    $"AND Attribute_2 = '{dgv.Rows[i].Cells[attr2].Value}'", _connection);
                                command.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                dgv = dataGridViewExcluded;
                table = "Excluded_products";
                type = "TypeM";
                producer = "ProducerM";
                attr = "Attribute1M";
                attr2 = "Attribute2M";
                counter++;
            }
        }

        // Изменение значения ячеек производителя и торг. марки
        private void dataGridViewInEx_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = tabControlInEx.SelectedIndex == 0 ? dataGridViewIncluded : dataGridViewExcluded;
            string producer = tabControlInEx.SelectedIndex == 0 ? "ProducerP" : "ProducerM";
            string brand = tabControlInEx.SelectedIndex == 0 ? "BrandP" : "BrandM";

            if (dgv.CurrentCell.OwningColumn.Name == producer || dgv.CurrentCell.OwningColumn.Name == brand) // Если произошло изменение в целевых столбцах
            {
                if (dgv.CurrentCell.OwningColumn.Name == producer)
                    dgv.Rows[dgv.CurrentRow.Index].Cells[dgv.Columns[brand].Index].Value = findBrandProdByThemselfs($"{dgv.CurrentCell.Value}");
                else
                    dgv.Rows[dgv.CurrentRow.Index].Cells[dgv.Columns[producer].Index].Value = findBrandProdByThemselfs($"{dgv.CurrentCell.Value}", 1);
            }
        }

        // Удаление данных из комбобоксов в таблицах вкл/искл товаров
        private void InputKUForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridView dgv;

            // Отслеживание нажатия на delete и backspace
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                if (tabControlInEx.SelectedIndex == 0)
                    dgv = dataGridViewIncluded;
                else
                    dgv = dataGridViewExcluded;

                if (dgv.Focused)
                {
                    if (dgv.RowCount > 0 && dgv.CurrentCell.ColumnIndex > dgv.ColumnCount - 3) // Проверка наличия строк в таблице и фокуса на целевых столбцах
                    {
                        (dgv.Rows[dgv.CurrentRow.Index].Cells[dgv.ColumnCount - 2] as DataGridViewComboBoxCell).Value = "";
                        (dgv.Rows[dgv.CurrentRow.Index].Cells[dgv.ColumnCount - 1] as DataGridViewComboBoxCell).Value = "";
                    }


                }
            }
        }

        //
        // Кнопки
        //
        // Добавление или изменение данных о КУ
        private void create_button_Click(object sender, EventArgs e)
        {
            if (!nullCheck())
                return;
            _allowFormClose = true;

            // Добавление или изменение информаци о коммерческих условиях
            if (buttonCreate.Text == "Создать")
            {
                addOrUpdateKU("Создано");
            }
            else
            {
                addOrUpdateKU("Создано", false);
            }
        }

        // Нажатие на кнопку создания(изменения) и утверждения
        private void createNapprove_button_Click(object sender, EventArgs e)
        {
            DialogResult result;

            result = MessageBox.Show($"Вы уверены, что хотите {buttonCreateNApprove.Text} выбранное коммерческое условие?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (!nullCheck())
                    return;
                _allowFormClose = true;

                if (buttonCreateNApprove.Text == "Создать и утвердить")
                {
                    // Создание и утверждение
                    addOrUpdateKU("Утверждено");
                }
                else
                {
                    // Изменение и утверждение
                    addOrUpdateKU("Утверждено", false);
                }
            }
        }

        // Нажатие на кнопку закрытия КУ (перевод в статус "Закрыто")
        private void close_button_Click(object sender, EventArgs e)
        {
            DialogResult result;

            result = MessageBox.Show("Вы уверены, что хотите изменить статус КУ на 'Закрыто' ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _allowFormClose = true;

                SqlCommand command = new SqlCommand($"UPDATE KU SET Status = 'Закрыто' WHERE RecId = {_KUId}", _connection);
                command.ExecuteNonQuery();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        // Нажатие на кнопку отмены при изменении КУ
        private void cancel_button_Click(object sender, EventArgs e)
        {
            _allowFormClose = true;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Кнопка сброса статуса КУ
        private void buttonUnapprove_Click(object sender, EventArgs e)
        {
            DialogResult result;
            DataTable tb = new DataTable();
            tb.Columns.Add("Graph_id", typeof(int));

            result = MessageBox.Show("Вы уверены, что хотите отменить утверждение КУ и перевести его в статус 'Создано' ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _allowFormClose = true;

                SqlCommand command = new SqlCommand($"UPDATE KU SET Status = 'Создано' WHERE RecId = {_KUId}", _connection);
                command.ExecuteNonQuery();
                //Удаление расчета из графика и товаров из InEx 
                SqlCommand command2 = new SqlCommand($"SELECT RecId FROM KU_graph WHERE KUId = {_KUId}", _connection);
                SqlDataAdapter adapt1 = new SqlDataAdapter(command2);
                adapt1.Fill(tb);


                for (int i = 0; i < tb.Rows.Count; i++)
                {

                    SqlCommand command3 = new SqlCommand($"DELETE FROM Included_products_list WHERE Graph_id = {tb.Rows[i]["Graph_id"]}", _connection);
                    command3.ExecuteNonQuery();
                    SqlCommand command4 = new SqlCommand($"DELETE FROM Excluded_products_list WHERE Graph_id = {tb.Rows[i]["Graph_id"]}", _connection);
                    command4.ExecuteNonQuery();

                }

                SqlCommand command5 = new SqlCommand($"DELETE FROM KU_graph WHERE KUId = {_KUId}", _connection);
                command5.ExecuteNonQuery();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Кнопка "Добавить все" в таблицы вкл. и искл. товаров
        private void button4_Click(object sender, EventArgs e)
        {
            addLine("Все");
        }

        // Открытие формы выбора категории
        private void btnSelectCategory_Click(object sender, EventArgs e)
        {
            _CategoryID.Clear();

            Form selectCategoryForm = new SelectCategoryForm(ref _CategoryID);
            selectCategoryForm.ShowDialog();

            if (selectCategoryForm.DialogResult == DialogResult.OK)
                addLine("Категория");

        }

        // Открытие формы выбора продуктов
        private void button5_Click(object sender, EventArgs e)
        {
                        
            if (_VendorId != 0)
            {
                int selectedVendorId = Convert.ToInt32(_VendorId);
                _ProdIds.Clear();

                Form SelectForm = new SelectProductForm(selectedVendorId, ref _ProdIds);
                SelectForm.ShowDialog();

                // Добавление строк с товарами 
                if (SelectForm.DialogResult == DialogResult.OK)
                    addLine("Товары");
            }
            else
            {
                MessageBox.Show("Выберите поставщика", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Кнопка удаления строки в таблицах вкл/иск товаров
        private void button7_Click(object sender, EventArgs e) // was: 606-640, now: 606-624
        {
            DialogResult result;
            DataGridView dgv = tabControlInEx.SelectedIndex == 0 ? dataGridViewIncluded : dataGridViewExcluded;
            string type = tabControlInEx.SelectedIndex == 0 ? "TypeP" : "TypeM";


            if (dgv.RowCount < 1)
            {
                MessageBox.Show("Нечего удалять", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            result = MessageBox.Show($"Вы уверены, что хотите удалить условие '{dgv.Rows[dgv.CurrentRow.Index].Cells[type].Value}' ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
        }

        //
        // Условия бонуса
        //
        // Добавление строки
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridViewTerms.Rows.Add();
        }
        //Удаление строки
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridViewTerms.RowCount > 0)
                dataGridViewTerms.Rows.RemoveAt(dataGridViewTerms.CurrentRow.Index);
            else
                MessageBox.Show("Отсутствуют строки для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        // Условия бонуса
        //
        // Добавление строки
        private void toolStripMenuSqleItem1_Click(object sender, EventArgs e)
        {
            if (dataGridViewSale.RowCount < 1)
                dataGridViewSale.Rows.Add();
            else
                MessageBox.Show("Можно добавить только одну акцию!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //Удаление строки
        private void toolStripMenuSqleItem2_Click(object sender, EventArgs e)
        {
            if (dataGridViewSale.RowCount > 0)
                dataGridViewSale.Rows.RemoveAt(dataGridViewSale.CurrentRow.Index);
            else
                MessageBox.Show("Отсутствуют строки для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        // Остальные методы
        //
        // Проверка ввода обязательных данных
        private bool nullCheck()
        {
            
            if (comboBoxKUType.Text == "") // Тип КУ
            {
                MessageBox.Show("Тип коммерческого условия не выбран!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxPeriod.SelectedIndex == -1) // Период
            {
                MessageBox.Show("Тип периода коммерческого условия не выбран!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dateTimePickerDateFrom.Format == DateTimePickerFormat.Custom) // Дата начала действия КУ
            {
                MessageBox.Show("Дата начала действия коммерческого условия не введена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxKUType.SelectedIndex == 0 && dataGridViewTerms.Rows.Count == 0) // Таблица условий бонуса
            {
                MessageBox.Show("Условия бонуса не добавлены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxKUType.SelectedIndex == 0 && dataGridViewTerms.Rows.Count != 0) // Таблица условий бонуса
            {

                for (int i = 0; i < dataGridViewTerms.Rows.Count; i++)
                {


                    if (dataGridViewTerms.Rows[i].Cells["Criterion"].Value is null
                        || dataGridViewTerms.Rows[i].Cells["PercentSum"].Value is null)
                    {
                        MessageBox.Show("Условия бонуса не введены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    Int64 Sum = Convert.ToInt64(dataGridViewTerms.Rows[i].Cells["PercentSum"].Value);
                    bool Fix = Convert.ToBoolean(dataGridViewTerms.Rows[i].Cells["FixSum"].Value);
                    if (Fix == false && Sum > 100)
                    {
                        MessageBox.Show("Вы ввели слишком большой процент вознаграждения, проверьте условия!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (comboBoxKUType.SelectedIndex == 1 && dataGridViewSale.Rows.Count == 0) // Таблица условий бонуса
            {
                MessageBox.Show("Условия акции не добавлены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxKUType.SelectedIndex == 1 && dataGridViewSale.Rows.Count != 0) // Таблица условий бонуса
            {

                for (int i = 0; i <= dataGridViewTerms.Rows.Count; i++)
                {


                    if (dataGridViewSale.Rows[i].Cells["SalePercent"].Value is null)
                    {
                        MessageBox.Show("Условия акции не введены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    Int64 Sum = Convert.ToInt64(dataGridViewSale.Rows[i].Cells["SalePercent"].Value);
                   
                    if (Sum > 100)
                    {
                        MessageBox.Show("Вы ввели слишком большой процент скидки, проверьте условия!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;

        }

       
        
        // Поиск названия категории
        private string findCategoryNameById(string id)
        {
            SqlCommand command = new SqlCommand($"SELECT L1, L1_name, L2, L2_name, L3, L3_name, L4, L4_name FROM Classifier " +
                $"WHERE L1 = '{id}' OR L2 = '{id}' OR L3 = '{id}' OR L4 = '{id}'", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i <= 6; i += 2)
            {
                if (dt.Rows[0][i].ToString() == id)
                {
                    return dt.Rows[0][i + 1].ToString();
                }
            }

            return "NULL";
        }

        // Поиск ID пары производитель-торговая марка
        private long findBrandProdId(string brandProdValue, int brandOrProd = 0) // brandOrProd = 0 - поиск по бренду, ...= 1 - поиск по производителю
        {
            for (int i = 0; i < _BrandProd.Rows.Count; i++)
            {
                if (brandOrProd == 0 && _BrandProd.Rows[i]["Brand"].ToString() == brandProdValue)
                    return Convert.ToInt64(_BrandProd.Rows[i]["ID"]);
                else if (brandOrProd == 1 && _BrandProd.Rows[i]["Producer"].ToString() == brandProdValue)
                    return Convert.ToInt64(_BrandProd.Rows[i]["ID"]);
            }
            return 0;
        }

        // Поиск произв. или торг. марки по паре
        private string findBrandProdByThemselfs(string brandProdValue, int brandOrProd = 0) // brandOrProd = 0 - поиск по бренду, ...= 1 - поиск по производителю
        {
            for (int i = 0; i < _BrandProd.Rows.Count; i++)
            {
                if (brandOrProd == 1 && _BrandProd.Rows[i]["Brand"].ToString() == brandProdValue)
                    return _BrandProd.Rows[i]["Producer"].ToString();
                else if (brandOrProd == 0 && _BrandProd.Rows[i]["Producer"].ToString() == brandProdValue)
                    return _BrandProd.Rows[i]["Brand"].ToString();
            }
            return "";
        }

       
        // Изменение размеров формы
        private void InputKUForm_Resize(object sender, EventArgs e)
        {
            doResize();
        }

        // Настройки адаптивного UI формы
        private void doResize()
        {
            labelMain.Location = new System.Drawing.Point(Convert.ToInt32((panel1.Width - labelMain.Width) / 2), labelMain.Location.Y);

            panel2.Height = Height - 400;
            TabPage activePage = tabControlMain.SelectedTab;
            int distance = Convert.ToInt32((activePage.Width - groupBoxDescription.Width - groupBoxSettings.Width - groupBoxPeriod.Width) / 5);
            int distanceY = Convert.ToInt32((activePage.Height - groupBoxDescription.Height) / 2);

            groupBoxDescription.Location = new System.Drawing.Point(distance, distanceY);
           
            groupBoxSettings.Location = new System.Drawing.Point(groupBoxDescription.Location.X + groupBoxDescription.Width + distance, distanceY);
            groupBoxPeriod.Location = new System.Drawing.Point(groupBoxSettings.Location.X + groupBoxSettings.Width + distance, distanceY);

            panel3.Height = panel4.Location.Y - panel3.Location.Y;
            tabControlInEx.Height = panel3.Height;

            dataGridViewTerms.Height = activePage.Height - 12;
        }

        //
        // Методы отслеживания изменения данных формы
        //
        // Отслеживание изм. значений в комбобоксах
        private void control_TextChanged(object sender, EventArgs e)
        {
           
                _wasChanged = true;
        }

        // Отслеживание изм. значений в чекбоксах
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
           
                _wasChanged = true;
        }
        // Отслеживание изм. значений ячеек и строк в гридах
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
                _wasChanged = true;

        }
        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
                _wasChanged = true;
        }
        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
                _wasChanged = true;
        }

        // Скрытие/отображение вкладки "Скидка"
        private void hideTabPageServices()
        {
            if (comboBoxKUType.SelectedIndex == 1)
            {
                tabPageSale.Parent = tabControlMain;
                tabPageBonus.Parent = null;
            }
            //else
            if (comboBoxKUType.SelectedIndex == 0)
            {
                tabPageBonus.Parent = tabControlMain;
                tabPageSale.Parent = null;
            }
            if (comboBoxKUType.SelectedIndex == -1)
            {
                tabPageBonus.Parent = null;
                tabPageSale.Parent = null;
            }
        }
        // Закрытие формы
        private void InputKUForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_wasChanged) // Если не было произведено изменений
                _connection.Close();
            else if (_allowFormClose) // Если производится "санкционированный" выход
                _connection.Close();
            else if (MessageBox.Show("При закрытии все несохраненные данные будут утеряны.\n\nВы уверены, что хотите закрыть окно?",
                "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
