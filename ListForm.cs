using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace TradeAggregator
{
    public partial class ListForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userId, _extraId;
        private Int32 _flag;
        private string _extraString;
        bool _typeIndex;

       //конструктор принимающий значения
        public ListForm(Int64 userId, bool typeIndex, Int32 flag)
        {
            _userId = userId;
            _typeIndex = typeIndex;
            _flag = flag;
            InitializeComponent();
        }

        // Загрузка формы
        private void AssortmentForm_Load(object sender, EventArgs e)
        {
            _connection.Open();
            loadData();
        }

        private void loadData()
        {
            SqlCommand command;
            SqlDataAdapter adapt;
            DataTable dt;

            buttonAllProd.Visible = false;
            switch (_typeIndex)
            {
                // Пользователь: поставщик
                case false:

                    switch (_flag)
                    {
                        //Ассортимент
                        case 0:
                            this.Text = "Ассортимент";
                            labelHeader.Text = "Ассортимент";
                            buttonAllProd.Visible = true;
                            buttonImport.Visible = true;

                            command = new SqlCommand($"Select Products.ProductID as 'Код продукта', ClassifierID as 'Код классификатора', " +
                                $"Name as 'Наименование', Price as 'Цена, руб.', Qty as 'Количество, шт.', Brand as 'Брэнд', Producer as 'Производитель' " +
                                $"from Assortment, Products LEFT JOIN BrandProducer ON Products.BrandProdID = BrandProducer.ID where VendorID = {_userId} and Products.ProductID = Assortment.ProductID", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;

                        //Список КУ
                        case 1:
                            this.Text = "Список коммерческих условий";
                            labelHeader.Text = "Список коммерческих условий";
                            buttonAdd.Visible = true;
                            buttonChange.Visible = true;
                            buttonDelete.Visible = true;

                            command = new SqlCommand($"SELECT RecId As 'Код КУ', DateFrom As 'Дата начала', DateTo As 'Дата конца', Period As 'Период', Status As 'Статус'" +
                                $" FROM KU WHERE VendorId =  {_userId} ", _connection);

                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);
                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;
                    }

                    break;

                // Пользователь: Торговая сеть
                case true:
                    switch (_flag)
                    {
                        // Список поставщиков
                        case 0:
                            this.Text = "Список поставщиков";
                            labelHeader.Text = "Список поставщиков";
                            buttonAllProd.Visible = false;
                            buttonImport.Visible = false;

                            command = new SqlCommand($"SELECT TOP (1000) [Vendor_id] as 'Номер поставщика', [Name] as 'Наименование', " +
                                $"[Urastic_name] as 'Юридическое наименование', [INN\\KPP] as 'ИНН\\КПП', " +
                                $"[Director_name] as 'Директор', [Urastic_address] as 'Юр. адрес', [Account] as 'Счет', [Bank_name] as 'Банк', " +
                                $"[Bank_bik] as 'БИК банка', [Corr_account] as 'Корр. счет' " +
                                $"FROM [DataBaseKU].[dbo].[Vendors]", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            dataGridView1.ReadOnly = true;
                            for(int i = 0; i< dataGridView1.ColumnCount; i++)
                                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                            break;
                        // Ассортимент поставщика
                        case 1:
                            this.Text = $"Ассортимент поставщика {_extraString}";
                            labelHeader.Text = $"Ассортимент поставщика {_extraString}";
                            buttonAllProd.Visible = false;
                            buttonImport.Visible = false;

                            command = new SqlCommand($"select ... where vendorId = {_extraId}", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            dataGridView1.ReadOnly = true;
                            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                            break;
                    }
                    break;


            }
          

        }

        // Двойной клик по строке гриды
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (_typeIndex)
            {
                // Пользователь: Поставщик
                case false:
                    switch (_flag)
                    {
                        //Ассортимент
                        case 0:
                            
                            break;

                        //Список КУ
                        case 1:
                            
                            break;
                    }

                    break;

                // Пользователь: Торговая сеть
                case true:
                    switch (_flag)
                    {
                        // Список поставщиков
                        case 0:
                            _flag = 1;
                            _extraId = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Номер поставщика"].Value);
                            _extraString = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Наименование"].Value.ToString();
                            loadData();
                            break;
                    }
                    break;


            }
        }


        // Добавление КУ
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form FormInputKU = new InputKUForm(_userId);

            FormInputKU.ShowDialog();

            if (FormInputKU.DialogResult == DialogResult.OK || FormInputKU.DialogResult == DialogResult.Cancel)
                loadData();
        }

        // Изменение выбранного КУ
        private void buttonChange_Click(object sender, EventArgs e)
        {
            Form FormInputKu = new InputKUForm(Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код КУ"].Value), _userId);
            FormInputKu.ShowDialog();

            if (FormInputKu.DialogResult == DialogResult.OK)
                loadData();
        }

        // Удаление выбранного КУ
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentRow.Index];

            result = MessageBox.Show("Вы уверены, что хотите удалить информацию о коммерческих условиях с поставщиком " +
                row.Cells["Наименование поставщика"].Value.ToString() + " ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            // Удаление привязанных к КУ значений в других таблицах
            SqlCommand command = new SqlCommand($"DELETE FROM Included_products WHERE KU_id = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM Excluded_products WHERE KU_id = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM KU_graph WHERE KUId = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM Terms WHERE KUId = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();

            // Удаление самого КУ
            command = new SqlCommand("DELETE FROM KU WHERE RecId = " + row.Cells["Код КУ"].Value.ToString(), _connection);
            command.ExecuteNonQuery();

            loadData();

        }

        // Закрытие формы
        private void Assortment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
