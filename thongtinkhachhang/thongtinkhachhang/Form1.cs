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
namespace thongtinkhachhang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=thongtinkhachhang;Integrated Security=True;";

        private void btnShow_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string showquery = "SELECT * FROM customer ORDER BY id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(showquery, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvCustomer.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO customer VALUES (@id, @ten, @diachi, @dt, @email)";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@ten", tenkh.Text);
            cmd.Parameters.AddWithValue("@diachi", dc.Text);
            cmd.Parameters.AddWithValue("@dt", dt.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.ExecuteNonQuery();
            btnShow_Click(sender, e);
        }

        private void bntDel_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            int rowIndex = dgvCustomer.SelectedCells[0].RowIndex;
            string ma = dgvCustomer.Rows[rowIndex].Cells[0].Value.ToString();
            string deleteQuery = "DELETE FROM customer WHERE id = @id";
            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
            cmd.Parameters.AddWithValue("@id", ma);
            cmd.ExecuteNonQuery();
            btnShow_Click(sender, e);
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            tenkh.Text = row.Cells[1].Value.ToString();
            dc.Text = row.Cells[2].Value.ToString();
            dt.Text = row.Cells[3].Value.ToString();
            email.Text = row.Cells[4].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = connection.CreateCommand();
            connection.Open();
            cmd.CommandText = "UPDATE customer SET tenkh = @ten, diachi = @dc, dienthoai = @dt, email = @email WHERE id = @id";
            cmd.Parameters.AddWithValue("@ten", tenkh.Text);
            cmd.Parameters.AddWithValue("@dc", dc.Text);
            cmd.Parameters.AddWithValue("@dt", dt.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.ExecuteNonQuery();
            btnShow_Click(sender, e);
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string searchQuery = "SELECT * FROM customer WHERE tenkh LIKE @tenkh"; 
            conn.Open();
            SqlCommand cmd = new SqlCommand(searchQuery, conn);
            cmd.Parameters.AddWithValue("@tenkh", "%"+ search.Text+ "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvCustomer.DataSource = dt;
        }
    }
}
