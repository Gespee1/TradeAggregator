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
    public partial class CommOffers : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userID;

        public CommOffers()
        {
            InitializeComponent();
        }
        public CommOffers(Int64 userId)
        {
            _userID = userId;
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
            /*SqlCommand command = new SqlCommand($"select co.RecId as 'Номер КП', " +
                $"co.OrderId as 'Номер заказа', co.Date as 'Дата', net.UrasticName as 'Сеть', " +
                $"vend.UrasticName as 'Поставщик', 'Создано' as 'Статус', co.NetworkReadFlag " +
                $"from CommercialOffers co " +
                $"left join Profiles net on net.RecID = co.NetworkId " +
                $"left join Profiles vend on vend.RecID = co.VendorId", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["NetworkReadFlag"].Visible = false;

            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if(Convert.ToBoolean(dataGridView1.Rows[i].Cells["NetworkReadFlag"].Value) == false)
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            }*/

            SqlCommand command = new SqlCommand($"select cl.L4_name as 'Категория', p.Name as 'Продукт', col.Qty as 'Количество' " +
                $"from CommercialOfferLines col " +
                $"join Classifier cl on cl.ID = col.ClassifierId " +
                $"join Products p on p.ProductID = col.ProductId " +
                $"where col.CommercialOfferId = 2", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn());
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].HeaderText = "Выбран";
        }






        private void CommOffers_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
