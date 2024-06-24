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
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
            displayCars();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        
        private void displayCars()
        {
            con.Open();
            String Query = "select * from CarTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(CarNumTb.Text == "Car Number" || CarBrandTb.Text == "Car Brand" || CarModelTb.Text == "Car Model" || ColorTb.Text == "Color" || OwnerNameTb.Text == "OwnerName" || CarNumTb.Text == "" || CarBrandTb.Text == "" || CarModelTb.Text == "" || ColorTb.Text == "" || OwnerNameTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }
            
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CarTbl(CNum, CBrand, CModel, CDate, CColor, OwnerName) values(@CN, @CB, @CM, @CD, @CC, @ON)", con);
                    cmd.Parameters.AddWithValue("@CN", CarNumTb.Text);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Registered");
                    con.Close();
                    displayCars();
                }catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        string CarKey = "";
        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarNumTb.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarKey = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarBrandTb.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            CarModelTb.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            CDate.Text = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
            ColorTb.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString();
            OwnerNameTb.Text = CarDGV.SelectedRows[0].Cells[5].Value.ToString();

           /* if(CarNumTb.Text == "")
            {
                Key = 0;
            }

            else
            {
                Key = Convert.ToInt32(CarDGV.SelectedRows[0].Cells[1].Value.ToString());
            }*/
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(CarKey == "")
            {
                MessageBox.Show("Select The Car");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CarTbl where CNum=@CN", con);
                    cmd.Parameters.AddWithValue("@CN", CarKey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Deleted");
                    con.Close();
                    displayCars();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CarKey == "")
            {
                MessageBox.Show("Select The Car");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CarTbl set CBrand=@CB, CModel=@CM, CDate=@CD, CColor=@CC, OwnerName=@ON where CNum=@CN", con);
                    cmd.Parameters.AddWithValue("@CN", CarKey);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Updated");
                    con.Close();
                    displayCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void label18_Click(object sender, EventArgs e)
        {
            DashBoard Obj = new DashBoard();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
           
        }
    }
}
