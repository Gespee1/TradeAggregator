using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TradeAggregator
{
    public partial class GraphForm : Form
    {

        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userId;
        private DataGridViewSelectedRowCollection dgvSelectedRows;
        private bool byDate = false, _asked = false, _typeIndex;
        private string VendorName;
        private string EntitiesName;
        private string DocNum;
        private string DocDate;
        private Stopwatch _timer = new Stopwatch();

        string user = Environment.MachineName;

        public GraphForm(Int64 userId, bool typeIndex)
        {
            _userId = userId;
            _typeIndex = typeIndex;
            InitializeComponent();
        }

        // Загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            _connection.Open();


            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = " ";
            dateTimePickerTo.CustomFormat = " ";
            if (dateTimePickerFrom.Format != DateTimePickerFormat.Custom)
            {
                dateTimePickerTo.MinDate = DateTime.Today.AddDays(1);
            }
            ShowGraph();
            doResize();
        }

        //Загрузка графика из БД
        private void ShowGraph()
        {
            DataTable graphs = new DataTable();
            List<int> RowIndexes = new List<int>();
            SqlCommand command = new SqlCommand($"SELECT * FROM KU_graph where VendorId = {_userId}  ", _connection);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(graphs);

            // Запись выделенных строк
            if (dataGridViewGraph.RowCount > 0)
                for (int i = 0; i < dataGridViewGraph.SelectedRows.Count; i++)
                    RowIndexes.Add(dataGridViewGraph.SelectedRows[i].Index);

            dataGridViewGraph.Rows.Clear();
            for (int i = 0; i < graphs.Rows.Count; i++)
            {
                dataGridViewGraph.Rows.Add();
                dataGridViewGraph.Rows[i].Cells["Graph_Id"].Value = graphs.Rows[i][0];
                dataGridViewGraph.Rows[i].Cells["KU_id"].Value = graphs.Rows[i][1];
                dataGridViewGraph.Rows[i].Cells["Vendor_Id"].Value = graphs.Rows[i][2];
                dataGridViewGraph.Rows[i].Cells["Buyer_Id"].Value = graphs.Rows[i][3];
                SqlCommand command1 = new SqlCommand($"SELECT Name FROM Profiles where Profiles.RecId = {graphs.Rows[i][3]} ", _connection);
                dataGridViewGraph.Rows[i].Cells["Period"].Value = graphs.Rows[i][4];
                dataGridViewGraph.Rows[i].Cells["Date_from"].Value = Convert.ToDateTime(graphs.Rows[i][5]).ToShortDateString();
                dataGridViewGraph.Rows[i].Cells["Date_to"].Value = Convert.ToDateTime(graphs.Rows[i][6]).ToShortDateString();
                dataGridViewGraph.Rows[i].Cells["Date_calc"].Value = Convert.ToDateTime(graphs.Rows[i][7]).ToShortDateString();
                dataGridViewGraph.Rows[i].Cells["GraphStatus"].Value = graphs.Rows[i][8];
                dataGridViewGraph.Rows[i].Cells["GraphSumN"].Value = graphs.Rows[i][9];
                dataGridViewGraph.Rows[i].Cells["GraphSumP"].Value = graphs.Rows[i][10];
                if (graphs.Rows[i][11].ToString() != "")
                    dataGridViewGraph.Rows[i].Cells["Percent"].Value = Convert.ToDouble(graphs.Rows[i][11]) / 11;

                dataGridViewGraph.Rows[i].Cells["Turnover"].Value = graphs.Rows[i][12];
            }

            // Отображение ранее выделенных строк
            if (RowIndexes.Count > 0 && RowIndexes[0] < dataGridViewGraph.RowCount - 1)
            {
                dataGridViewGraph.CurrentCell = dataGridViewGraph.Rows[RowIndexes[0]].Cells[0];
                for (int i = 0; i < RowIndexes.Count; i++)
                    dataGridViewGraph.Rows[RowIndexes[i]].Cells[0].Selected = true;
            }
        }

             

        // Изменение размеров формы
        private void KUGraphForm_Resize(object sender, EventArgs e)
        {
            doResize();
        }

        // Изменение размеров панели с гридой
        private void doResize()
        {
            panel1.Height = buttonCalcBonus.Location.Y - menuStrip1.Height;
        }

        // Закрытие соединения с БД
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
