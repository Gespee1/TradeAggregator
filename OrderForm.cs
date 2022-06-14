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
    public partial class OrderForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userID;

        public OrderForm()
        {
            InitializeComponent();
        }

        public OrderForm(Int64 userID)
        {
            _userID = userID;
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            _connection.Open();
            dateTimePicker1.Value = DateTime.Now;
            categoriesLoad();
        }

        // Загрузка категорий
        private void categoriesLoad()
        {
            SqlCommand command = new SqlCommand("SELECT L1, L1_name, L2, L2_name, L3, L3_name, L4, L4_name FROM Classifier ORDER BY L4", _connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);


            int L1, L2, L3;
            L1 = L2 = L3 = -1;
            string prevNode1, prevNode2, prevNode3;
            prevNode1 = prevNode2 = prevNode3 = "";

            treeViewCategory.BeginUpdate();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() != prevNode1)
                {
                    prevNode1 = dt.Rows[i][0].ToString();
                    treeViewCategory.Nodes.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                    L1++;
                    L2 = -1;
                    L3 = -1;
                }
                if (dt.Rows[i][2].ToString() != prevNode2)
                {
                    prevNode2 = dt.Rows[i][2].ToString();
                    treeViewCategory.Nodes[L1].Nodes.Add(dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                    L2++;
                    L3 = -1;
                }
                if (dt.Rows[i][4].ToString() != prevNode3)
                {
                    prevNode3 = dt.Rows[i][4].ToString();
                    treeViewCategory.Nodes[L1].Nodes[L2].Nodes.Add(dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                    L3++;
                }
                treeViewCategory.Nodes[L1].Nodes[L2].Nodes[L3].Nodes.Add(dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString());
            }
            treeViewCategory.EndUpdate();

        }

        // Выбор категории
        private void treeViewCategory_DoubleClick(object sender, EventArgs e)
        {
            addProductsFromCategory();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addProductsFromCategory();
        }
        // Добавление товаров из категории
        private void addProductsFromCategory()
        {
            DataTable dt = new DataTable();
            SqlCommand command;
            SqlDataAdapter adapt;

            // Проверка, что выбрана категория последнего уровня
            if(treeViewCategory.SelectedNode.Level != 3)
            {
                MessageBox.Show("Пожалуйста, выберите категорию самого последнего уровня детализации.\n\n" +
                    "Это необходимо для того, чтобы избежать огромного количества оторажаемых товаров.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Удаление не отмеченных галочкой товаров
            deleteUnselectedRows();

            // Поиск товаров по выбранной категории
            command = new SqlCommand($"select p.ProductID, p.Name " +
                $"from Classifier c " +
                $"join Products p on p.ClassifierID = c.ForeignID " +
                $"where c.L1 = '{treeViewCategory.SelectedNode.Name}' or " +
                $"c.L2 = '{treeViewCategory.SelectedNode.Name}' or " +
                $"c.L3 = '{treeViewCategory.SelectedNode.Name}' or " +
                $"c.L4 = '{treeViewCategory.SelectedNode.Name}'", _connection);
            adapt = new SqlDataAdapter(command);
            adapt.Fill(dt);
            // Добавление товаров из новой категории
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(!isItAlreadyInserted(dt.Rows[i][0].ToString()))
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["ProductCode"].Value = dt.Rows[i][0];
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["Product"].Value = dt.Rows[i][1];
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["Qty"].Value = 1;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["AddFlag"].Value = false;
                }
            }
            //Console.WriteLine($"Tag: {treeViewCategory.SelectedNode.Name}, Text: {treeViewCategory.SelectedNode.Text}"); // Код категории + Наименование категории
        }
        // Проверка, не добавлен ли уже такой товар
        private bool isItAlreadyInserted(string productID)
        {
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["ProductCode"].Value.ToString() == productID)
                    return true;
            }
            return false;
        }
        // Удаление товаров
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }
        // Удаление невыбранных галочкой товаров
        private void убратьНевыбранныеТоварыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            deleteUnselectedRows();
        }
        // Метод удаления невыбранных галочкой товаров
        private void deleteUnselectedRows()
        {
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["AddFlag"].Value) != true)
                    dataGridView1.Rows.RemoveAt(i);
        }

        // Создание заказа
        private void button1_Click(object sender, EventArgs e)
        {
            Int64 orderRecId;

            dataGridView1.EndEdit();
            deleteUnselectedRows();

            SqlCommand command = new SqlCommand($"insert into Orders values " +
                $"({_userID}, '{dateTimePicker1.Value.ToShortDateString()}')", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("SELECT SCOPE_IDENTITY()", _connection); // Получение только что добавленного ид
            orderRecId = Convert.ToInt64(command.ExecuteScalar());
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                command = new SqlCommand($"insert into OrderLines values ({orderRecId}, {dataGridView1.Rows[i].Cells["ProductCode"].Value}, " +
                    $"{dataGridView1.Rows[i].Cells["Qty"].Value}, 0)", _connection);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Ваш заказ принят, ожидайте коммерческих предложений.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        // Изменение значения ячеек гриды
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка правильности ввода кол-ва
            if(e.ColumnIndex == 2 && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Qty"].Value) < 0)
            {
                MessageBox.Show("Количество товара в заказе не может быть меньше 0!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.Rows[e.RowIndex].Cells["Qty"].Value = 1;
            }

            // Автопроставление галочки, если изменилось кол-во
            if (e.ColumnIndex == 2 && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Qty"].Value) != 1)
                dataGridView1.Rows[e.RowIndex].Cells["AddFlag"].Value = true;
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }

        
    }
}
