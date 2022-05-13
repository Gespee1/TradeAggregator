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
    public partial class Assortment : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userId;
        bool _typeIndex;

        //мб добавить в конструктор еще vendor_id открываемого поставщика, если будет общая форма просмотра ассортимента, надо будет обсудить
        public Assortment(Int64 userId, bool typeIndex)
        {
            _userId = userId;
            _typeIndex = typeIndex;
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
           if (!_typeIndex)
           {
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
