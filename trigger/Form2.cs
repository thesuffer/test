using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trigger
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Payment();
        }
        private static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=localhost; Integrated Security=SSPI; Initial Catalog=shop;");
        }

        SqlConnection connect = GetConnection();
        SqlDataAdapter adapter;

        public void Payment()
        {
            connect.Open();
            DataTable dataTable = new DataTable();
            adapter = new SqlDataAdapter("select order_info as Заказ, coming_info as Приход_денег, payment_sum as Выделенная_сумма from payment;", connect);
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connect.Close();
        }

        public void Insert()
        {
            int number = 3;
            number++;
            connect.Open();
            DataTable dataTable = new DataTable();
            adapter = new SqlDataAdapter("ALTER TRIGGER payment_ins2 ON payment FOR INSERT AS BEGIN DECLARE @SUM INT; SET @SUM = " + textBox3.Text + "; DECLARE @NUMBER varchar(4); SET @NUMBER = 'N00" + number + "'; END;", connect);
            adapter = new SqlDataAdapter("INSERT INTO payment (order_info, coming_info, payment_sum) VALUES("+textBox1.Text+", "+textBox2.Text+", "+textBox3.Text+");", connect);
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Insert();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
