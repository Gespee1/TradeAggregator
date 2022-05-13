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
    public partial class AccountForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userID;
        private bool _typeIndex;

        public AccountForm()
        {
            InitializeComponent();
        }
        // Конструктор с параметрами
        public AccountForm(Int64 userId)
        {
            _userID = userId;
            InitializeComponent();
        }
        // Загрузка формы
        private void AccountForm_Load(object sender, EventArgs e)
        {
            _connection.Open();
            loadData();
        }

        // Загрузка данных о пользователе
        private void loadData()
        {
            SqlCommand command = new SqlCommand($"select Type, Login from Users where RecID = {_userID}", _connection);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                _typeIndex = Convert.ToBoolean(reader[0]);
                labelMain.Text = $"Вы авторизовались как {reader[1]}";
                reader.Close();

                if(_typeIndex) // 0 - поставщик | 1 - торговая сеть
                {
                    panelNetwork.Visible = true;
                    panelVendor.Visible = false;
                }
                else
                {
                    panelNetwork.Visible = false;
                    panelVendor.Visible = true;
                }
            }
        }


        //
        // Обработчики нажатий на кнопки и объединяющие методы
        //
        private void buttonProfile_Click(object sender, EventArgs e)
        {
            profileOpen();
        }
        private void buttonProfileVend_Click(object sender, EventArgs e)
        {
            profileOpen();
        }

        private void profileOpen()
        {
            string profileId;
            Form profileForm;

            SqlCommand command = new SqlCommand($"select ProfileId from Users where RecID = {_userID}", _connection);
            profileId = command.ExecuteScalar().ToString();
            if (profileId != "")
                profileForm = new ProfileForm(_userID, Convert.ToInt64(profileId), true);
            else
                profileForm = new ProfileForm(_userID, 0, true);
            profileForm.ShowDialog();
        }

        private void buttonAssortment_Click(object sender, EventArgs e)
        {
            Form assortment;
            assortment = new Assortment(_userID, _typeIndex);
            assortment.ShowDialog();

        }








        // Закрытие формы
        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
               
    }
}
