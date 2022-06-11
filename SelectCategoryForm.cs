using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace TradeAggregator
{
    public partial class SelectCategoryForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private List<string> _CategoryId;
        public SelectCategoryForm()
        {
            InitializeComponent();
        }

        public SelectCategoryForm(ref List<string> category_id)
        {
            _CategoryId = category_id;
            InitializeComponent();
        }

        //формирование дерева
        private void FormSelectCategory_Load(object sender, EventArgs e)
        {


            _connection.Open();

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

            doResize();
        }

        //запись в форму списка КУ
        private void btnChoiseCategory_Click(object sender, EventArgs e)
        {
            _CategoryId.Add(treeViewCategory.SelectedNode.Name);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        // Закрытие соединения с БД
        private void SelectCategoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }



        // UI
        //
        // Вызов метода изменения размера формы
        private void SelectCategoryForm_Resize(object sender, EventArgs e)
        {
            doResize();
        }

        // Изменение размера формы
        private void doResize()
        {
            panel1.Width = Convert.ToInt32(ClientSize.Width - 101);
        }

    }
}
