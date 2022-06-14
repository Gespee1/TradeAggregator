using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace TradeAggregator
{
    public partial class ListForm : Form
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AggregatorDataBase"].ConnectionString);
        private Int64 _userId, _extraId;
        private Int32 _flag;
        private string _extraString;
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
            SqlCommand command;
            SqlDataAdapter adapt;
            DataTable dt;

            buttonAllProd.Visible = false;
            switch (_typeIndex)
            {
                // Пользователь: поставщик
                case false:

                    switch (_flag)
                    {
                        //Ассортимент
                        case 0:
                            this.Text = "Ассортимент";
                            labelHeader.Text = "Ассортимент";
                            buttonAllProd.Visible = true;
                            buttonImport.Visible = true;

                            command = new SqlCommand($"Select Products.ProductID as 'Код продукта', ClassifierID as 'Код классификатора', " +
                                $"Products.Name as 'Наименование', Price as 'Цена, руб.', Qty as 'Количество, шт.' " +
                                $"from Assortment, Products, Profiles, Users " +
                                $"where VendorID = Profiles.RecID And Profiles.RecID = Users.ProfileID AND Users.RecID = {_userId} and Products.ProductID = Assortment.ProductID", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;

                        //Список КУ
                        case 1:
                            this.Text = "Список коммерческих условий";
                            labelHeader.Text = "Список коммерческих условий";
                            buttonAdd.Visible = true;
                            buttonChange.Visible = true;
                            buttonDelete.Visible = true;
                            menuStrip1.Visible = true;

                            command = new SqlCommand($"SELECT RecId As 'Код КУ', DateFrom As 'Дата начала', DateTo As 'Дата конца', Period As 'Период', Status As 'Статус'" +
                                $" FROM KU WHERE VendorId =  {_userId} ", _connection);

                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);
                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;

                        //Список поступивших КП
                        case 2:
                            buttonAccept.Visible = false;
                            buttonDecline.Visible = false;
                            buttonBack.Visible = false;
                            this.Text = "Поступившие коммерческие предложения";
                            labelHeader.Text = "Поступившие коммерческие предложения";

                            command = new SqlCommand($"SELECT CommercialOffers.RecId As 'Код КП', Date As 'Дата', Name As 'Торговая компания', Status As 'Статус'" +
                                $" FROM CommercialOffers, Profiles WHERE VendorId =  {_userId} AND Profiles.RecId = NetworkId", _connection);

                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);
                            adapt.Fill(dt);                        
                            dataGridView1.DataSource = dt;

                           /* for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                string status = Convert.ToString(dataGridView1.Rows[i].Cells[3]);

                                status = status.Replace("0", "Не просмотрено");
                                dataGridView1.Rows[i].Cells[3].Value = status;
                            }*/
                            break;

                            //Список поступивших КП строки
                             case 3:
                            buttonBack.Visible = true;
                            buttonAccept.Visible = true;
                            buttonDecline.Visible = true;
                            this.Text = "Товары коммерческого предложения";
                                 labelHeader.Text = "Товары коммерческого предложения";

                                 command = new SqlCommand($"SELECT  CommercialOfferLines.ProductId As 'Код товара',  CommercialOfferLines.ClassifierId As 'Код классификатора', " +
                                     $"Products.Name As 'Наименование товара', Qty As 'Количество, шт.'" +
                                     $" FROM  CommercialOfferLines, Products WHERE CommercialOfferId = '{_extraId}'" +
                                     $" AND Products.ProductID = CommercialOfferLines.ProductId", _connection);

                                 dt = new DataTable();
                                 adapt = new SqlDataAdapter(command);
                                 adapt.Fill(dt);
                                 dataGridView1.DataSource = dt;
                                 break;

                            //Договоры
                            case 4:
                            this.Text = "Заключенные договоры";
                            labelHeader.Text = "Заключенные договоры";

                            command = new SqlCommand($"SELECT  Contracts.RecId As 'Код договора',  Contracts.CommercialOfferId As 'Код коммерческого предложения', " +
                                $" Contracts.Date As 'Дата' FROM  CommercialOffers, Contracts WHERE Contracts.CommercialOfferId = CommercialOffers.RecId AND VendorId = '{_userId}'", _connection);

                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);
                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;
                    }

                    break;

                // Пользователь: Торговая сеть
                case true:
                    switch (_flag)
                    {
                        // Список поставщиков
                        case 0:
                            this.Text = "Список поставщиков";
                            labelHeader.Text = "Список поставщиков";
                            buttonAllProd.Visible = false;
                            buttonImport.Visible = false;
                            buttonBack.Visible = false;

                            command = new SqlCommand($"SELECT p.[RecID] as 'Номер поставщика'" +
                                $", p.[Name] as 'Наименование'" +
                                $", p.[UrasticName] as 'Юр. наименование'" +
                                $", p.[INN\\KPP] as 'ИНН\\КПП'" +
                                $", p.[DirectorName] as 'Директор'" +
                                $", p.[UrasticAddress] as 'Юр. адрес'" +
                                $", p.[Account] as 'Банковский счет'" +
                                $", p.[BankName] as 'Наименование банка'" +
                                $", p.[BankBik] as 'БИК банка'" +
                                $", p.[CorrAccount] as 'Корр. счет'" +
                                $",case when(rp.[Name] is null) then '-' " +
                                $"else rp.[Name] end as 'Ответств. лицо' " +
                                $"FROM[Aggregator].[dbo].[Users] u " +
                                $"join[Aggregator].[dbo].[Profiles] p on p.RecID = u.ProfileId " +
                                $"left join[Aggregator].[dbo].[ResponsiblePersons] rp on rp.RecID = p.RespPerson " +
                                $"where u.Type = 0", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            dataGridView1.ReadOnly = true;
                            for(int i = 0; i< dataGridView1.ColumnCount; i++)
                                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                            ////////////////////        Автоматическое заполнение ассоритмента одного поставщика
                            /*Random rnd = new Random();
                            command = new SqlCommand($"SELECT top(600) [ProductID] " +
                                $"FROM[Aggregator].[dbo].[Products] p " +
                                $"where p.ClassifierID = 5637274411 " +
                                $"order by p.ProductID", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);
                            adapt.Fill(dt);
                            for(int i = 0; i < dt.Rows.Count; i++)
                            {
                                command = new SqlCommand($"insert into Assortment values (36, {dt.Rows[i][0]}, " +
                                    $"{Math.Round(rnd.Next(20, 500) + rnd.NextDouble(), 2).ToString().Replace(',', '.')}, " +
                                    $"{rnd.Next(10, 100)})", _connection);
                                command.ExecuteNonQuery();
                                Console.WriteLine($"{(i+1)*100/dt.Rows.Count}%");
                            }*/
                            /////////////////////////////

                            break;
                        // Ассортимент поставщика
                        case 1:
                            this.Text = $"Ассортимент поставщика {_extraString}";
                            labelHeader.Text = $"Ассортимент поставщика {_extraString}";
                            buttonAllProd.Visible = false;
                            buttonImport.Visible = false;
                            buttonBack.Visible = true;

                            command = new SqlCommand($"SELECT p.[ProductID] as 'Код продукта'" +
                                $", p.[Name] as 'Название'" +
                                $", case when(c.[L4] is null) then '-' " +
                                $"else c.[L4] end as 'Код группы товаров'" +
                                $", case when(c.[L4_name] is null) then '-' " +
                                $"else c.[L4_name] end as 'Наименование группы товаров'" +
                                $", case when(bp.[Brand] is null) then '-' " +
                                $"else bp.[Brand] end as 'Торговая марка'" +
                                $",case when(bp.[Producer] is null) then '-' " +
                                $"else bp.[Producer] end as 'Производитель'" +
                                $",a.[Price] as 'Цена, руб.'" +
                                $",a.[Qty] as 'Количество' " +
                                $"FROM[Aggregator].[dbo].[Assortment] a " +
                                $"left join[Aggregator].[dbo].[Products] p on p.ProductID = a.ProductID " +
                                $"left join[Aggregator].[dbo].[Classifier] c on c.ForeignID = p.ClassifierID " +
                                $"left join[Aggregator].[dbo].[BrandProducer] bp on bp.ForeignID = p.BrandProdID " +
                                $"where a.VendorID = {_extraId}", _connection);
                            dt = new DataTable();
                            adapt = new SqlDataAdapter(command);

                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                            dataGridView1.ReadOnly = true;
                            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                            break;
                    }
                    break;


            }
          

        }

        // Двойной клик по строке гриды
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (_typeIndex)
            {
                // Пользователь: Поставщик
                case false:
                    switch (_flag)
                    {
                        //Ассортимент
                        case 0:
                            
                            break;

                        //Список КУ
                        case 1:
                            
                            break;

                        //Поступившие КП
                        case 2:
                            _extraId = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код КП"].Value);
                            _flag = 3;
                            loadData();
                            break;
                    }

                    break;

                // Пользователь: Торговая сеть
                case true:
                    switch (_flag)
                    {
                        // Список поставщиков
                        case 0:
                            _flag = 1;
                            _extraId = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Номер поставщика"].Value);
                            _extraString = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Юр. наименование"].Value.ToString();
                            loadData();
                            break;

                         
                    }
                    break;


            }
        }

        // Кнопка возврата к списку поставщиков
        private void buttonBack_Click(object sender, EventArgs e)
        {
            switch (_flag)
            {
                // Список поставщиков
                case 1:
                    _flag = 0;
                    break;

                case 3:
                    _flag = 2;
                    break;
            }
            
            loadData();
        }


        // Добавление КУ
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form FormInputKU = new InputKUForm(_userId);

            FormInputKU.ShowDialog();

            if (FormInputKU.DialogResult == DialogResult.OK || FormInputKU.DialogResult == DialogResult.Cancel)
                loadData();
        }

        // Изменение выбранного КУ
        private void buttonChange_Click(object sender, EventArgs e)
        {
            Form FormInputKu = new InputKUForm(Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Код КУ"].Value), _userId);
            FormInputKu.ShowDialog();

            if (FormInputKu.DialogResult == DialogResult.OK)
                loadData();
        }

        // Удаление выбранного КУ
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentRow.Index];

            result = MessageBox.Show("Вы уверены, что хотите удалить информацию о коммерческих условиях с поставщиком " +
                row.Cells["Наименование поставщика"].Value.ToString() + " ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            // Удаление привязанных к КУ значений в других таблицах
            SqlCommand command = new SqlCommand($"DELETE FROM Included_products WHERE KU_id = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM Excluded_products WHERE KU_id = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM KU_graph WHERE KUId = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();
            command = new SqlCommand($"DELETE FROM Terms WHERE KUId = {row.Cells["Код КУ"].Value}", _connection);
            command.ExecuteNonQuery();

            // Удаление самого КУ
            command = new SqlCommand("DELETE FROM KU WHERE RecId = " + row.Cells["Код КУ"].Value.ToString(), _connection);
            command.ExecuteNonQuery();

            loadData();

        }

        // Закрытие формы
        private void Assortment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Close();
        }
    }
}
