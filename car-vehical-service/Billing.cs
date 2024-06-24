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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            GetCars();            
            displayStock();
            EmpNameLbl.Text = Login.Username;
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-56INE733;Initial Catalog=master;Integrated Security=True");
        private void GetCars()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CarTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(string));
            dt.Load(rdr);
            CarNumCb.ValueMember = "CNum";
            CarNumCb.DataSource = dt;
            con.Close();
        }

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

        private void UpdateQty()
        {
            int newQty = Convert.ToInt32(QtyTb.Text);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update StockTbl set PartQty=@PQ where PartId=@PId", con);
                cmd.Parameters.AddWithValue("@PId", Key);
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.ExecuteNonQuery();
                con.Close();
                displayStock();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int n = 0, num;
        int tot = 0, GrdTot = 0;
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0 || QtyTb.Text == "Quantity" || QtyTb.Text == "")
            {
                MessageBox.Show("Select spare parts to Add");
            }
            else if (Convert.ToInt32(QtyTb.Text) > Qty)
            {
                MessageBox.Show("No Enough In Stock");
            }
            else
            {
                num = Convert.ToInt32(QtyTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ChangedPartsDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PartName;
                tot = num * Price;
                newRow.Cells[2].Value = num;
                newRow.Cells[3].Value = Price;
                newRow.Cells[4].Value = tot;
                ChangedPartsDGV.Rows.Add(newRow);
                n++;

                GrdTot = GrdTot + tot;
                PartFeeLbl.Text = "Rs" + GrdTot;
                UpdateQty();
                QtyTb.Text = "";
            }
        }

        int Qty = 0, Price = 0;

        string PartName = "";
        private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PartName = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Qty = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[2].Value.ToString());
            Price = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[3].Value.ToString());

            if (PartName == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        int Key;

        private void label19_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        int Tf = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MFeesTb.Text == "Mechanics Cost" || MFeesTb.Text == "")
            {
                MessageBox.Show("Enter Valid Amount");
            }

            else if (PartFeeLbl.Text == "Part Cost")
            {
                Tf = GrdTot + Convert.ToInt32(MFeesTb.Text);
                TotalFeesLbl.Text = "Rs" + Convert.ToString(MFeesTb.Text);
            }

            else
            {
                Tf = GrdTot + Convert.ToInt32(MFeesTb.Text);
                TotalFeesLbl.Text = "Rs" + Convert.ToString(GrdTot + Convert.ToInt32(MFeesTb.Text));

            }
        }

        private void SaveBillBtn_Click(object sender, EventArgs e)
        {
            if (CarNumCb.SelectedIndex == -1 || TotalFeesLbl.Text == "Total Cost")
            {
                MessageBox.Show("Missing Data");
            }

            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(CarNum, BDate, MechFees, PartFees, TotFees, EmpName) values(@CN, @BD, @MF, @PF, @TF, @EN)", con);
                    cmd.Parameters.AddWithValue("@CN", CarNumCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MF", MFeesTb.Text);
                    cmd.Parameters.AddWithValue("@PF", GrdTot);
                    cmd.Parameters.AddWithValue("@TF", Tf);
                    cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
        
