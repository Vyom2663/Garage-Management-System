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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            displayEmp();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        private void displayEmp()
        {
            con.Open();
            String Query = "select * from EmpTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {

            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPassTb.Text == "Employee Password" | empGenCb.SelectedIndex == -1 || EmpPassTb.Text == "" || EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmpTbl(EmpName, EmpAdd, EmpPass, EmpGen) values(@EN, @EA, @EP, @EG)", con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPassTb.Text);
                    cmd.Parameters.AddWithValue("@EG", empGenCb.Text);
       
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Registered");
                    con.Close();
                    displayEmp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            empGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPassTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Employee");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmpTbl where EmpId=@EId", con);
                    cmd.Parameters.AddWithValue("@Eid", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted");
                    con.Close();
                    displayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPassTb.Text == "Employee Password" || empGenCb.SelectedIndex == -1 || EmpPassTb.Text == "" || EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update EmpTbl set EmpName=@EN,EmpGen=@EG,EmpAdd=@EA,EmpPass=@EP where EmpId=@EId", con);
                    cmd.Parameters.AddWithValue("@EId", Key);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", empGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated");
                    con.Close();
                    displayEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }    
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

        private void label18_Click(object sender, EventArgs e)
        {
            DashBoard Obj = new DashBoard();
            Obj.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }	
}
