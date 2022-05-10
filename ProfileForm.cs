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
    public partial class ProfileForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userId;
        private bool _canChange;

        public ProfileForm()
        {
            InitializeComponent();
        }

        // Конструктор с параметрами
        public ProfileForm(Int64 userId = 0, bool canChange = false)
        {
            _userId = userId;
            _canChange = canChange;
            InitializeComponent();
        }
        // Загрузка формы
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            _connection.Open();
            dataLoad();
        }
        // Загрузка данных
        private void dataLoad()
        {
            DataTable data = new DataTable();
            SqlCommand command;

            if (_userId != 0)
            {
                command = new SqlCommand($"select * from Profiles " +
                    $"where RecId = (select ProfileId from Users where RecId = {_userId})", _connection);
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                adapt.Fill(data);
            }
            else
            {
                command = new SqlCommand($"insert into Profiles values (0, '', '', '', '', '', '', '', '', '',)", _connection);
                command.ExecuteNonQuery();
            }
        }




        // Закрытие формы
        private void ProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
