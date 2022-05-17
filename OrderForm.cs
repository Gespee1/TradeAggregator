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
            addCategory();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addCategory();
        }
        // Добавление категории
        private void addCategory()
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = treeViewCategory.SelectedNode.Name;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = treeViewCategory.SelectedNode.Text;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = 1;
            //Console.WriteLine($"Tag: {treeViewCategory.SelectedNode.Name}, Text: {treeViewCategory.SelectedNode.Text}");
        }
        // Удаление категории
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        // Создание заказа
        private void button1_Click(object sender, EventArgs e)
        {
            Int64 orderRecId, classifierId;

            SqlCommand command = new SqlCommand($"insert into Orders values " +
                $"({_userID}, '{dateTimePicker1.Value.ToShortDateString()}')", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("SELECT SCOPE_IDENTITY()", _connection); // Получение только что добавленного ид
            orderRecId = Convert.ToInt64(command.ExecuteScalar());
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                command = new SqlCommand($"select ID FROM Classifier WHERE L1 = '{dataGridView1.Rows[i].Cells[0].Value}' OR " +
                    $"L2 = '{dataGridView1.Rows[i].Cells[0].Value}' OR L3 = '{dataGridView1.Rows[i].Cells[0].Value}' OR " +
                    $"L4 = '{dataGridView1.Rows[i].Cells[0].Value}'", _connection);
                classifierId = Convert.ToInt64(command.ExecuteScalar());

                command = new SqlCommand($"insert into OrderLines values ({orderRecId}, {classifierId}, " +
                    $"{dataGridView1.Rows[i].Cells[2].Value})", _connection);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Ваш заказ принят, ожидайте коммерческие предложения.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }

        
    }
}
