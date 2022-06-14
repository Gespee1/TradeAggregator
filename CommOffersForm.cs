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
    public partial class CommOffersForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userID, _extraID;
        private bool _typeIdx, _listMode;

        public CommOffersForm()
        {
            InitializeComponent();
        }
        public CommOffersForm(Int64 userId, bool type)
        {
            _userID = userId;
            _typeIdx = type;
            _listMode = true;
            InitializeComponent();
        }
        private void CommOffers_Load(object sender, EventArgs e)
        {
            _connection.Open();
            loadData();
        }

        // Загрузка данных
        private void loadData()
        {
            DataTable dt = new DataTable();

            // Преднастройка грид
            if (_listMode) // Форма списка
            {
                buttonReturn.Visible = false;
                splitContainer1.Panel2MinSize = 0;
                splitContainer1.SplitterDistance = splitContainer1.Size.Width;
                splitContainer1.IsSplitterFixed = true;
            }
            else
            {
                buttonReturn.Visible = true;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.SplitterDistance = Convert.ToInt32(splitContainer1.Size.Width / 2);
                splitContainer1.Panel2MinSize = 25;
            }

            if (!_typeIdx) // Поставщик
            {
                if(_listMode) // Форма списка
                {

                }
                else
                {

                }
            }
            else // Торговая компания
            {
                if (_listMode) // Форма списка
                {
                    generateComOffs();

                    SqlCommand command = new SqlCommand($"select co.RecId as 'Номер КП', " +
                        $"co.OrderId as 'Номер заказа', co.Date as 'Дата', net.UrasticName as 'Сеть', " +
                        $"case when co.Status = 0 then 'Создано' " +
                        $"when co.Status = 1 then 'Утверджено ТК' " +
                        $"when co.Status = 2 then 'Утверджено' " +
                        $"when co.Status = -1 then 'Отклонено поставщиком' " +
                        $"end as 'Статус', co.NetworkReadFlag " +
                        $"from CommercialOffers co " +
                        $"left join Users u on u.RecID = {_userID}" +
                        $"left join Profiles net on net.RecID = u.ProfileId", _connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                    dataGridViewVendors.Columns.Clear();
                    dataGridViewVendors.DataSource = dt;
                    dataGridViewVendors.Columns["NetworkReadFlag"].Visible = false;

                    for (int i = 0; i < dataGridViewVendors.ColumnCount; i++)
                        dataGridViewVendors.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                    for (int i = 0; i < dataGridViewVendors.RowCount; i++)
                    {
                        if (Convert.ToBoolean(dataGridViewVendors.Rows[i].Cells["NetworkReadFlag"].Value) == false)
                            dataGridViewVendors.Rows[i].DefaultCellStyle.Font = new Font(dataGridViewVendors.Font, FontStyle.Bold);
                    }
                }
                else // Документная форма
                {
                    SqlCommand command = new SqlCommand($"select cov.RecId, p.Name, cov.Selected " +
                        $"from CommercialOfferVendors cov " +
                        $"left join Profiles p on p.RecID = cov.VendorId " +
                        $"where cov.CommercialOfferId = {_extraID}", _connection);
                    dataGridViewVendors.DataSource = null;
                    dataGridViewVendors.Columns.Clear();
                    dataGridViewVendors.Columns.Add(new DataGridViewTextBoxColumn() { Name = "VendCode", HeaderText = "Код поставщика" });
                    dataGridViewVendors.Columns.Add(new DataGridViewTextBoxColumn() { Name = "VendName", HeaderText = "Поставщик" });
                    dataGridViewVendors.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Select", HeaderText = "Выбор" });
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        dataGridViewVendors.Rows.Add();
                        dataGridViewVendors.Rows[dataGridViewVendors.RowCount - 1].Cells[0].Value = reader[0].ToString();
                        dataGridViewVendors.Rows[dataGridViewVendors.RowCount - 1].Cells[1].Value = reader[1].ToString();
                        dataGridViewVendors.Rows[dataGridViewVendors.RowCount - 1].Cells[2].Value = Convert.ToBoolean(reader[2]);
                    }
                    reader.Close();

                    command = new SqlCommand($"select col.ProductId, p.Name, col.Qty, a.Price, col.Selected " +
                        $"from CommercialOfferLines col " +
                        $"left join Products p on p.ProductID = col.ProductId " +
                        $"left join CommercialOfferVendors cov on cov.RecId = col.ComOffVendorId " +
                        $"left join Assortment a on a.VendorID = cov.VendorId and a.ProductID = col.ProductId " +
                        $"where col.CommercialOfferId = {_extraID} and col.ComOffVendorId = {dataGridViewVendors.Rows[0].Cells[0].Value}", _connection);
                    dataGridViewProducts.DataSource = null;
                    dataGridViewProducts.Columns.Clear();
                    dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductCode", HeaderText = "Код продукта" });
                    dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductName", HeaderText = "Продукт" });
                    dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Qty", HeaderText = "Количество" });
                    dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Price", HeaderText = "Стоимость" });
                    dataGridViewProducts.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Select", HeaderText = "Выбор" });
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridViewProducts.Rows.Add();
                        dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[0].Value = reader[0].ToString();
                        dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[1].Value = reader[1].ToString();
                        dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[2].Value = reader[2].ToString();
                        dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[3].Value = reader[3].ToString();
                        dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[4].Value = Convert.ToBoolean(reader[4]);
                    }
                    reader.Close();

                }
            }

        }

        // Одиночный клик по гриде поставщиков
        private void dataGridViewVendors_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!_listMode)
            {
                SqlCommand command = new SqlCommand($"select col.ProductId, p.Name, col.Qty, a.Price, col.Selected " +
                        $"from CommercialOfferLines col " +
                        $"left join Products p on p.ProductID = col.ProductId " +
                        $"left join CommercialOfferVendors cov on cov.RecId = col.ComOffVendorId " +
                        $"left join Assortment a on a.VendorID = cov.VendorId and a.ProductID = col.ProductId " +
                        $"where col.CommercialOfferId = {_extraID} and col.ComOffVendorId = " +
                        $"{dataGridViewVendors.Rows[dataGridViewVendors.CurrentRow.Index].Cells[0].Value}", _connection);
                dataGridViewProducts.Rows.Clear();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataGridViewProducts.Rows.Add();
                    dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[0].Value = reader[0].ToString();
                    dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[1].Value = reader[1].ToString();
                    dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[2].Value = reader[2].ToString();
                    dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[3].Value = reader[3].ToString();
                    dataGridViewProducts.Rows[dataGridViewProducts.RowCount - 1].Cells[4].Value = Convert.ToBoolean(reader[4]);
                }
                reader.Close();
            }
        }
        // Двойной клик по гриде
        private void dataGridViewVendors_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(_listMode)
                openCommOff();
        }
        // Клик по кнопке "Открыть" на менюстрипе
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_listMode)
                openCommOff();
        }
        // Открытие КП
        private void openCommOff()
        {
            SqlCommand command;

            if (!_typeIdx) // Поставщик
            {
                if (_listMode) // Форма списка
                {

                }
            }
            else // Торговая компания
            {
                if (_listMode) // Форма списка
                {
                    _listMode = false;
                    _extraID = Convert.ToInt64(dataGridViewVendors.Rows[dataGridViewVendors.CurrentRow.Index].Cells["Номер КП"].Value);

                    // Выставление флага прочитанности
                    if(Convert.ToBoolean(dataGridViewVendors.Rows[dataGridViewVendors.CurrentRow.Index].Cells["NetworkReadFlag"].Value) == false)
                    {
                        command = new SqlCommand($"update CommercialOffers set NetworkReadFlag = 1 where RecId = {_extraID}", _connection);
                        command.ExecuteNonQuery();
                    }

                    loadData();
                }
            }
        }

        
        // Создание КП по заказам
        private void generateComOffs()
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            Int64 VendorId = 0, ComOfferRecId = 0, ComOfferVendRecId = 0;
            int ComOffVendsCount = 0, qty = 0;

            command = new SqlCommand($"SELECT o.RecId, o.NetworkId, a.VendorID, o.Date, ol.ProductId, a.Price, " +
                $"ol.Qty as 'OrderQTY', a.Qty as 'VendorQTY' FROM Orders o " +
                $"left join OrderLines ol on ol.OrderId = o.RecId " +
                $"left join Assortment a on a.ProductID = ol.ProductId " +
                $"where o.Status = 0 order by o.RecId, a.VendorID, ol.ProductId", _connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            if(dt.Rows.Count > 0) // Создание нового КП
            {
                command = new SqlCommand($"insert into CommercialOffers values ({dt.Rows[0][0]}, '{dt.Rows[0][3]}', " +
                        $"{dt.Rows[0][1]}, 0, 0, 0, '')", _connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("SELECT SCOPE_IDENTITY()", _connection); // Получение только что добавленного ид
                ComOfferRecId = Convert.ToInt64(command.ExecuteScalar());
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(VendorId != Convert.ToInt64(dt.Rows[i][2])) // Создание нового поставщика в КП
                {
                    VendorId = Convert.ToInt64(dt.Rows[i][2]);
                    command = new SqlCommand($"insert into CommercialOfferVendors values ({ComOfferRecId}, {VendorId}, 0)", _connection);
                    command.ExecuteNonQuery();
                    command = new SqlCommand("SELECT SCOPE_IDENTITY()", _connection); // Получение только что добавленного ид
                    ComOfferVendRecId = Convert.ToInt64(command.ExecuteScalar());

                    ComOffVendsCount++;
                }
                // Заполнение КП
                qty = Convert.ToInt32(dt.Rows[i][6]) < Convert.ToInt32(dt.Rows[i][7]) ? Convert.ToInt32(dt.Rows[i][6]) : Convert.ToInt32(dt.Rows[i][7]);
                command = new SqlCommand($"insert into CommercialOfferLines values ({ComOfferRecId}, {ComOfferVendRecId}, {dt.Rows[i][4]}, " +
                    $"{qty}, 1)", _connection);
                command.ExecuteNonQuery();

                if (ComOffVendsCount == 5)
                    break;
            }
            if(dt.Rows.Count > 0)
            {
                command = new SqlCommand($"update Orders set Status = 1 where RecId = {dt.Rows[0][0]}", _connection);
                command.ExecuteNonQuery();
            }
            
        }

        // Возврат к списку КП
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            _listMode = true;
            loadData();
        }



        private void CommOffers_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
