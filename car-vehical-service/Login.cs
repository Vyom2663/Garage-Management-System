using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace car_vehical_service
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        public static string Username = "";
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
            }
            else
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmpTbl where EmpName='" + UnameTb.Text + "' and EmpPass='" + PasswordTb.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString() == "1")
                {
                    Username = UnameTb.Text;
                    Billing Obj = new Billing();
                    Obj.Show();
                    this.Hide();
                    con.Close();
                }
                
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
                con.Close();
            }
        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
