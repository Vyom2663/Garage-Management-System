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

namespace car_vehical_service
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            CountCars();
            CountEmployees();
            CountSpares();
            SumAmount();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        private void CountCars()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CarTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CarLbl.Text = dt.Rows[0][0].ToString();
        }

        private void CountEmployees()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmpTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmpLbl.Text = dt.Rows[0][0].ToString();

        }

        private void CountSpares()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from StockTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SpareLbl.Text = dt.Rows[0][0].ToString();

        }

        private void SumAmount()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(TotFees) from BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AmountLbl.Text = dt.Rows[0][0].ToString();

        }

        private void label17_Click(object sender, EventArgs e)
        {
            Cars Obj = new Cars();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
