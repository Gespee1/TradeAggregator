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
    public partial class AddRespPersonForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private ProfileForm _parent;

        public AddRespPersonForm()
        {
            InitializeComponent();
        }
        public AddRespPersonForm(ProfileForm parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        // Создание новой записи
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            _connection.Open();

            SqlCommand command = new SqlCommand($"insert into ResponsiblePersons values ('{textBoxName.Text}', '{textBoxNumber.Text}', '{textBoxEmail.Text}', " +
                $"'{textBoxPost.Text}')", _connection);
            command.ExecuteNonQuery();
            _parent.NewRespPersonName = textBoxName.Text;
            this.DialogResult = DialogResult.OK;

            _connection.Close();
            this.Close();
        }
    }
}
