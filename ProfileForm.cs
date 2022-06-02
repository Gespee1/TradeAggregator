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
        private Int64 _userId, _profileId;
        private bool _canChange, _changeStatus = false;
        public string NewRespPersonName;

        public ProfileForm()
        {
            InitializeComponent();
        }

        // Конструктор с параметрами
        public ProfileForm(Int64 userId, Int64 profileId = 0, bool canChange = false)
        {
            _userId = userId;
            _profileId = profileId;
            _canChange = canChange;
            InitializeComponent();
        }
        // Загрузка формы
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            _connection.Open();
            dataLoad();
            setEnabled();
            if (!_canChange) // Выключение кнопок при запрете редактирования
            {
                button1.Enabled = false;
                buttonAddRespPerson.Enabled = false;
            }
        }

        // Выбор режима работы формы
        private void setEnabled()
        {
            if(!_changeStatus)
            {
                comboBoxRespPerson.Enabled = false;
                textBoxName.Enabled = false;
                textBoxUrName.Enabled = false;
                textBoxINN.Enabled = false;
                textBoxKPP.Enabled = false;
                textBoxDirector.Enabled = false;
                textBoxUrAddress.Enabled = false;
                textBoxBankAccount.Enabled = false;
                textBoxBankName.Enabled = false;
                textBoxBankBIK.Enabled = false;
                textBoxCorrAccount.Enabled = false;
            }
            else
            {
                comboBoxRespPerson.Enabled = true;
                textBoxName.Enabled = true;
                textBoxUrName.Enabled = true;
                textBoxINN.Enabled = true;
                textBoxKPP.Enabled = true;
                textBoxDirector.Enabled = true;
                textBoxUrAddress.Enabled = true;
                textBoxBankAccount.Enabled = true;
                textBoxBankName.Enabled = true;
                textBoxBankBIK.Enabled = true;
                textBoxCorrAccount.Enabled = true;
            }
        }

        // Загрузка данных
        private void dataLoad()
        {
            int newProfileRecId;
            SqlCommand command;
            SqlDataReader reader;
            string innKpp;

            loadRespPersons();

            if (_profileId != 0)
            {
                command = new SqlCommand($"select rp.Name, p.[Name], p.[UrasticName], p.[INN\\KPP], p.[DirectorName], p.[UrasticAddress], p.[Account]" +
                    $", p.[BankName], p.[BankBik], p.[CorrAccount] from Profiles p " +
                    $"left join ResponsiblePersons rp on p.RespPerson = rp.RecID " +
                    $"where p.RecID = {_profileId}", _connection);
                reader = command.ExecuteReader();
                reader.Read();
                textBoxCode.Text = _profileId.ToString();
                comboBoxRespPerson.SelectedItem = reader[0].ToString();
                textBoxName.Text = reader[1].ToString();
                textBoxUrName.Text = reader[2].ToString();
                innKpp = reader[3].ToString();
                if(innKpp.Contains("/"))
                {
                    textBoxINN.Text = innKpp.Split('/')[0];
                    textBoxKPP.Text = innKpp.Split('/')[1];
                }
                textBoxDirector.Text = reader[4].ToString();
                textBoxUrAddress.Text = reader[5].ToString();
                textBoxBankAccount.Text = reader[6].ToString();
                textBoxBankName.Text = reader[7].ToString();
                textBoxBankBIK.Text = reader[8].ToString();
                textBoxCorrAccount.Text = reader[9].ToString();
                reader.Close();
            }
            else
            {
                command = new SqlCommand($"insert into Profiles values (0, '', '', '', '', '', '', '', '', '')", _connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("SELECT SCOPE_IDENTITY()", _connection); // Получение только что добавленного ид
                newProfileRecId = Convert.ToInt32(command.ExecuteScalar());
                command = new SqlCommand($"update Users set ProfileId = {newProfileRecId} where RecID = {_userId}", _connection);
                command.ExecuteNonQuery();
                _profileId = newProfileRecId;
            }
        }
        // Загрузка списка ответственных лиц
        private void loadRespPersons()
        {
            SqlCommand command = new SqlCommand($"select Name from ResponsiblePersons", _connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBoxRespPerson.Items.Add(reader[0].ToString());
            reader.Close();
        }


        // Кнопка переключения режима редактирования и сохранения
        private void button1_Click(object sender, EventArgs e)
        {
            if (_changeStatus)
            {
                _changeStatus = false;
                setEnabled();
            }
            else
            {
                _changeStatus = true;
                setEnabled();
            }
        }

        
        //
        // Запись в БД измененных данных
        //
        // Ответственное лицо
        private void comboBoxRespPerson_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set RespPerson = (select RecID from ResponsiblePersons where Name = " +
                $"'{comboBoxRespPerson.SelectedItem}') where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Наименоваине
        private void textBoxName_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set Name = '{textBoxName.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Юр. наименоваине
        private void textBoxUrName_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set UrasticName = '{textBoxUrName.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // ИНН/КПП
        private void textBoxINN_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set INN\\KPP = '{textBoxINN.Text + '/' + textBoxKPP.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // ИНН/КПП
        private void textBoxKPP_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set INN\\KPP = '{textBoxINN.Text + '/' + textBoxKPP.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // ФИО директора
        private void textBoxDirector_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set DirectorName = '{textBoxDirector.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Юр. адрес
        private void textBoxUrAddress_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set UrasticAddress = '{textBoxUrAddress.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Наим. банка
        private void textBoxBankName_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set BankName = '{textBoxBankName.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // БИК
        private void textBoxBankBIK_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set BankBik = '{textBoxBankBIK.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Банковский счет
        private void textBoxBankAccount_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set Account = '{textBoxBankAccount.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }
        // Корр. счет
        private void textBoxCorrAccount_Leave(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"update Profiles set CorrAccount = '{textBoxCorrAccount.Text}' where RecID = {_profileId}", _connection);
            command.ExecuteNonQuery();
        }


        // Открытие формы создания отв. лица
        private void buttonAddRespPerson_Click(object sender, EventArgs e)
        {
            Form addRP = new AddRespPersonForm(this);
            DialogResult dr = addRP.ShowDialog();
            if(dr == DialogResult.OK)
            {
                loadRespPersons();
                Console.WriteLine(NewRespPersonName);
                comboBoxRespPerson.SelectedItem = NewRespPersonName;
            }
        }

        // Закрытие формы
        private void ProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
