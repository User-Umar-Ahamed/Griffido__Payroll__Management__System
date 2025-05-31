using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Setting_Component : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;
        String query;

        public Setting_Component()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            guna2DateTimePicker1.Value = DateTime.Today; // Set date pickers to today's date or default value
            guna2DateTimePicker2.Value = DateTime.Today;
            Leaves_per_Year_txtbx1.Clear();
            Cycle_Date_Range.Clear();
            Setting_ID_txtbx.Clear();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                query = "insert into Setting_Table (Setting_ID,StartDate,EndDate,Days_Got_Leaved,Worked_Days)values ('" + Setting_ID_txtbx.Text + "','" + guna2DateTimePicker1.Value + "','" + guna2DateTimePicker2.Value + "','" + Leaves_per_Year_txtbx1.Text + "','" + Cycle_Date_Range.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Settings Updated Successfully!!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void Main_Menubtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage F2 = new Homepage();
            F2.ShowDialog();
            F2 = null;
            this.Show();
        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = guna2DateTimePicker1.Value;
            DateTime endDate = guna2DateTimePicker2.Value;
            TimeSpan gap = endDate - startDate;
            int dateRange = gap.Days;
            Cycle_Date_Range.Text = dateRange.ToString();
        }

        private void Setting_ID_txtbx_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string settingID = Setting_ID_txtbx.Text;

                con.Open();

                // Query to check if the setting ID exists in the database
                string sqlQuery = "SELECT * FROM Setting_Table WHERE Setting_ID = @SettingID";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@SettingID", settingID);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    // If the setting ID exists, populate the other textboxes
                    guna2DateTimePicker1.Value = Convert.ToDateTime(dr["StartDate"]);
                    guna2DateTimePicker2.Value = Convert.ToDateTime(dr["EndDate"]);
                    Leaves_per_Year_txtbx1.Text = dr["Days_Got_Leaved"].ToString();
                    Cycle_Date_Range.Text = dr["Worked_Days"].ToString();

                }
                else
                {
                    // If the setting ID does not exist, clear the other textboxes
                    guna2DateTimePicker1.Value = DateTime.Today;
                    guna2DateTimePicker2.Value = DateTime.Today;
                    Leaves_per_Year_txtbx1.Clear();
                    Cycle_Date_Range.Clear();

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
    
}
