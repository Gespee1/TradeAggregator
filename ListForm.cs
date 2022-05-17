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
        private Int64 _userId;
        private Int32 _flag;
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
            buttonAllProd.Visible = false;

            //Поставщик просматривает свой ассортимент
           if (!_typeIndex && _flag == 0)
           {
                labelHeader.Text = "Ассортимент";
                buttonAllProd.Visible = true;
           }
        
           else
           {
                buttonAllProd.Visible = false;
           }
        }

        // Закрытие формы
        private void Assortment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
