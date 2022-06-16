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
        private Int32 _buttonFlag; //флаг, фиксирующий то, какую кнопку нажали

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

        // Открытие формы профиля
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

        //открытие формы Ассортимента
        private void buttonAssortment_Click(object sender, EventArgs e)
        {
            _buttonFlag = 0;
            Form assortment;
            assortment = new ListForm(_userID, _typeIndex, _buttonFlag);
            assortment.ShowDialog();

        }

        // Открытие формы создания заказа
        private void buttonOrder_Click(object sender, EventArgs e)
        {
            Form OrderForm = new OrderForm(_userID);
            OrderForm.ShowDialog();
        }
        //открытие формы КУ
        private void buttonKU_Click(object sender, EventArgs e)
        {
            _buttonFlag = 1;
            Form KU;
            KU = new ListForm(_userID, _typeIndex, _buttonFlag);
            KU.ShowDialog();

        }
        // Открытие формы списка поставщиков
        private void buttonVendors_Click(object sender, EventArgs e)
        {
            Form KU;
            KU = new ListForm(_userID, _typeIndex, 0);
            KU.ShowDialog();
        }
        // Открытие формы КП
        private void buttonKP_Click(object sender, EventArgs e)
        {
            Form commOffers = new CommOffersForm(_userID, _typeIndex);
            commOffers.ShowDialog();
        }

        //Открытие формы полученных КП
        private void buttonReceivedKP_Click(object sender, EventArgs e)
        {
            //_buttonFlag = 2;
            //Form receivedComOffers = new ListForm(_userID, _typeIndex, _buttonFlag);
            //receivedComOffers.ShowDialog();
            Form commOffers = new CommOffersForm(_userID, _typeIndex);
            commOffers.ShowDialog();
        }

        //Открытие формы списка Договоров поставщик
        private void buttonCont_Click(object sender, EventArgs e)
        {
            _buttonFlag = 4;
            Form Contracts = new ListForm(_userID, _typeIndex, _buttonFlag);
            Contracts.ShowDialog();
        }

        //Открытие формы списка Договоров тс
        private void buttonContracts_Click(object sender, EventArgs e)
        {
            _buttonFlag = 2;
            Form Contracts = new ListForm(_userID, _typeIndex, _buttonFlag);
            Contracts.ShowDialog();
        }

        //Открытие формы фин. графиков
        private void buttonGraph_Click(object sender, EventArgs e)
        {
            _buttonFlag = 5;
            Form Graph = new GraphForm(_userID, _typeIndex);
            Graph.ShowDialog();

        }

        // Закрытие формы
        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }

        
    }
}
