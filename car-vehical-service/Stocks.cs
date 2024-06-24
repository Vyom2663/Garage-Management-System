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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
            displayStock();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        private void displayStock()
        {
            con.Open();
            String Query = "select * from StockTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PartsDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "Part Name" || PQtyTb.Text == "Quantity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StockTbl(PartName,PartQty,PartPrice) values(@PN,@PQ,@PP)", con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Parts Registered");
                    con.Close();
                    displayStock();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void PQtyTb_TextChanged(object sender, EventArgs e)
        {

        }

        int Key = 0;
        private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PQtyTb.Text = PartsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PPriceTb.Text = PartsDGV.SelectedRows[0].Cells[3].Value.ToString();


            if (PNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Part");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from StockTbl where PartId=@PId", con);
                    cmd.Parameters.AddWithValue("@Pid", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part Deleted");
                    con.Close();
                    displayStock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "Part Name" || PQtyTb.Text == "Quantity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update StockTbl set PartName=@PN,PartQty=@PQ,PartPrice=@PP where PartId=@PId", con);
                    cmd.Parameters.AddWithValue("@PId", Key);
                    cmd.Parameters.AddWithValue("@EN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@EA", PPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part Updated");
                    con.Close();
                    displayStock();
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

        private void label19_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}