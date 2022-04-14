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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            moneyComing();
            Order();
           // Payment();
        }

        private static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=localhost; Integrated Security=SSPI; Initial Catalog=shop;");
        }

        SqlConnection connect = GetConnection();
        SqlDataAdapter adapter;

        public void moneyComing ()
        {
            connect.Open();
            DataTable dataTable = new DataTable();
            adapter = new SqlDataAdapter("select coming_number as Номер, coming_date as Дата, coming_sum as Сумма, balance as Остаток from money_coming;", connect);
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connect.Close();
        }

        public void Order()
        {
            connect.Open();
            DataTable dataTable = new DataTable();
            adapter = new SqlDataAdapter("select order_number as Номер, order_date as Дата, order_sum as Сумма, order_payment as Сумма_Оплаты from orders;", connect);
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connect.Close();
        }

        public void Payment()
        {
            connect.Open();
            DataTable dataTable = new DataTable();
            adapter = new SqlDataAdapter("ALTER TRIGGER upd_coming ON money_coming INSTEAD OF UPDATE AS BEGIN DECLARE @ID1 INT; SET @ID1 = "+textBox1.Text+ "; DECLARE @ID2 INT; SET @ID2 = " + textBox2.Text + "; END;", connect);
            adapter = new SqlDataAdapter("UPDATE money_coming SET balance = 0;", connect);
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connect.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Order();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Payment();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            moneyComing();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
    }
}
