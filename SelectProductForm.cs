using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace TradeAggregator
{
    public partial class SelectProductForm : Form
    {
        
        private Int64 VendorId;
        private List<Int64> ProdId;
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        bool add = false;
        public SelectProductForm()
        {
            InitializeComponent();
        }

        public SelectProductForm(Int64 id, ref List<Int64> prodId)
        {
            VendorId = id;
            ProdId = prodId;
            InitializeComponent();
        }

        // Подключение к БД
        private void SelectForm_Load(object sender, EventArgs e)
        {

            _connection.Open();
            
        }

        // Отображение данных формы
        private void ShowProducts()
        {
            // Отображение имени поставщика в шапке
            SqlCommand command = new SqlCommand($"SELECT Name FROM Profiles, Users WHERE Profiles.RecID = Users.ProfileId AND Users.RecID = {VendorId}", _connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            labelMain.Text = $"Товары поставщика {reader[0]}";
            reader.Close();

            command = new SqlCommand($"SELECT Products.Name AS 'Наименование', ClassifierID AS 'Ид классификатора', BrandProdID AS 'Номер производителя и бренда' " +
                $"FROM Users, Profiles, Assortment, Products " +
                $"WHERE Assortment.ProductID = Products.ProductID AND Assortment.VendorId = Profiles.RecID AND Profiles.RecID = Users.ProfileId AND Users.RecID = {VendorId}", _connection);

            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = command;
            adapt.Fill(dt);
            advancedDataGridViewProducts.DataSource = dt;
            for (int i = 0; i < advancedDataGridViewProducts.ColumnCount; i++)
            {
                advancedDataGridViewProducts.Columns[i].ReadOnly = true;
            }
            if (add == false)
            {
                advancedDataGridViewProducts.Columns.AddRange(new DataGridViewCheckBoxColumn());
                advancedDataGridViewProducts.Columns[advancedDataGridViewProducts.ColumnCount - 1].HeaderText = "Выбрать";
                advancedDataGridViewProducts.Columns[advancedDataGridViewProducts.ColumnCount - 1].Name = "checkBoxes";
                add = true;
            }
           
            showRowCount();
            doResize();
        }

        

        // Показать общее кол-во отображенных строк
        private void showRowCount()
        {
            labelShownProducts.Text = "Отображено товаров: " + advancedDataGridViewProducts.RowCount;
        }

        // Добавление выбранных товаров
        private void button1_Click(object sender, EventArgs e)
        {
            bool emptyFlag = true;
            for (int i = 0; i < advancedDataGridViewProducts.RowCount; i++)
            {
                if (Convert.ToBoolean(advancedDataGridViewProducts.Rows[i].Cells["checkBoxes"].Value) == true)
                {
                    SqlCommand command = new SqlCommand($"SELECT ProductID FROM Products WHERE Name = '{advancedDataGridViewProducts.Rows[i].Cells["Наименование"].Value}'", _connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    ProdId.Add(Convert.ToInt64(reader[0]));
                    reader.Close();
                    emptyFlag = false;
                }
            }
            if (emptyFlag)
                MessageBox.Show("Не выбрано ни одного товара для добавления!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        // Кнопка отмены
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Закрытие подключения к БД
        private void SelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }

        private void SelectProductForm_Resize(object sender, EventArgs e)
        {
            doResize();
        }

        // Изменение размера формы
        private void doResize()
        {
            panel2.Height = buttonAddSelected.Location.Y - panel1.Height;
        }

        // Обновление данных формы при переходе
        private void SelectProductForm_Activated(object sender, EventArgs e)
        {
            ShowProducts();
        }
    }
}
