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

namespace WindowsFormsApp1
{
    public partial class Salary_Component : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader dr;
        String query;
        float tax;

        public Salary_Component()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            guna2DateTimePicker1.Value = DateTime.Today; // Set date pickers to today's date or default value
            guna2DateTimePicker2.Value = DateTime.Today;
            Leaved_per_Year_txtbx1.Value = 0; // Resets the numeric up-down value
            Cycle_Date_Range.Clear();
            Setting_ID_txtbx.Clear();
            Employee_ID_txtbx.Clear();
            Employee_Age_txtbx.Clear();
            Employee_Allowances_txtbx.Clear();
            Employee_name_txtbx.Clear();
            Employee_OT_txtbx.Clear();
            Employee_Salary_txtbx.Clear();
            MaleRadiobutton.Checked = false;
            FemaleRadiobutton.Checked = false;
            txtabsentdays.Clear();
            txtattendance.Clear();
            txtbasepay.Clear();
            txtgrosspay.Clear();
            txtholidaystaken.Clear();
            txtleavestaken.Clear();
            txtnopay.Clear();
            txtothrs.Clear();
            txtovertime.Clear();
            Salary_ID_Txtbx.Clear();
            Payroll_ID.Clear();
            Employee_Address_txtxbx.Clear();
            Taxtxtbx.Clear();
            
        }

        private void main_Menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage F2 = new Homepage();
            F2.ShowDialog();
            F2 = null;
            this.Show();
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
                    Leaved_per_Year_txtbx1.Value = Convert.ToDecimal(dr["Days_Got_Leaved"]);
                    Cycle_Date_Range.Text = dr["Worked_Days"].ToString();

                }
                else
                {
                    // If the setting ID does not exist, clear the other textboxes
                    guna2DateTimePicker1.Value = DateTime.Today;
                    guna2DateTimePicker2.Value = DateTime.Today;
                    Leaved_per_Year_txtbx1.Value = 0;
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

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = guna2DateTimePicker1.Value;
            DateTime endDate = guna2DateTimePicker2.Value;
            TimeSpan gap = endDate - startDate;
            int dateRange = gap.Days;
            Cycle_Date_Range.Text = dateRange.ToString();
        }

        private void Setting_Save_btn1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                query = "insert into Setting_Table (Setting_ID,StartDate,EndDate,Days_Got_Leaved,Worked_Days)values ('" + Setting_ID_txtbx.Text + "','" + guna2DateTimePicker1.Value + "','" + guna2DateTimePicker2.Value + "','" + Leaved_per_Year_txtbx1.Value + "','" + Cycle_Date_Range.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Settings Updated Successfully!!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlsearch;
                sqlsearch = "SELECT * FROM Employee_Information WHERE Employee_ID='" + Employee_ID_txtbx.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlsearch, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Employee_name_txtbx.Text = dr["Employee_Name"].ToString();
                    Employee_Age_txtbx.Text = dr["Employee_Age"].ToString();
                    if (dr["Gender"].ToString() == "M")
                    {
                        MaleRadiobutton.Checked = true;
                        FemaleRadiobutton.Checked = false;
                    }
                    else
                    {
                        MaleRadiobutton.Checked = false;
                        FemaleRadiobutton.Checked = true;
                    }

                    Employee_Address_txtxbx.Text = dr["Employee_Address"].ToString();
                    Employee_Salary_txtbx.Text = dr["Salary_Per_Month"].ToString();
                    Employee_OT_txtbx.Text = dr["Over Time Rate"].ToString();
                    Employee_Allowances_txtbx.Text = dr["Allowances"].ToString();
                    Taxtxtbx.Text = dr["Tax"].ToString();
                }
                else
                {
                    MessageBox.Show("Employee ID is not registered in the Employee database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Checkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the input values from the textboxes
                int absentdays = int.Parse(txtabsentdays.Text);
                int cycledaterange = int.Parse(Cycle_Date_Range.Text);

                // Calculate the overall attendance
                int overallattendance = cycledaterange - absentdays;

                // Display the overall attendance in the txtattendance textbox
                txtattendance.Text = overallattendance.ToString();
            }
            catch (FormatException ex)
            {
                // Show an error message if the input values are not valid numeric values
                MessageBox.Show("Please enter valid numeric values for absent days and cycle date range.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Show a general error message if any other exception occurs
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Calculatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the input values from the textboxes
                float overallattendance = float.Parse(txtattendance.Text);
                float cycledaterange = float.Parse(Cycle_Date_Range.Text);
                float absentdays = float.Parse(txtabsentdays.Text);
                float monthlysal = float.Parse(Employee_Salary_txtbx.Text);
                float allowance = float.Parse(Employee_Allowances_txtbx.Text);
                float otrate = float.Parse(Employee_OT_txtbx.Text);
                float othrs = float.Parse(txtothrs.Text);
                float taxrate = float.Parse(Taxtxtbx.Text);

                // NOPAY CALCULATION 
                float nopay = (monthlysal / cycledaterange) * absentdays;
                txtnopay.Text = nopay.ToString("0.00");

                // BASE PAY CALCULATION
                float overtime = othrs * otrate;
                float basepay = monthlysal + allowance + overtime;
                txtbasepay.Text = basepay.ToString("0.00");
                txtovertime.Text = overtime.ToString("0.00");

                // GROSS PAY CALCULATION
                float tax = basepay * taxrate / 100;
                float grosspay = basepay - (nopay + tax);
                txtgrosspay.Text = grosspay.ToString("0.00");
            }
            catch (FormatException ex)
            {
                // Show an error message if the input values are not valid numeric values
                MessageBox.Show("Please enter a valid value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Show a general error message if any other exception occurs
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;"))
                {
                    con.Open();
                    string query = "INSERT INTO Salary (Salary_ID, Employee_ID, Employee, Payment_ID, Payment_Date, Overall_Attendance, Basepay, No_Pay, Gross_Pay, Tax) " +
                                   "VALUES (@SalaryID, @EmployeeID, @Employee, @PaymentID, @PaymentDate, @OverallAttendance, @Basepay, @NoPay, @GrossPay, @Tax)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SalaryID", Salary_ID_Txtbx.Text);
                        cmd.Parameters.AddWithValue("@EmployeeID", Employee_ID_txtbx.Text);
                        cmd.Parameters.AddWithValue("@Employee", Employee_name_txtbx.Text);
                        cmd.Parameters.AddWithValue("@PaymentID", Payroll_ID.Text);
                        cmd.Parameters.AddWithValue("@PaymentDate", Payout_Day_DateTime.Value);
                        cmd.Parameters.AddWithValue("@OverallAttendance", txtattendance.Text);
                        cmd.Parameters.AddWithValue("@Basepay", txtbasepay.Text);
                        cmd.Parameters.AddWithValue("@NoPay", txtnopay.Text);
                        cmd.Parameters.AddWithValue("@GrossPay", txtgrosspay.Text);
                        cmd.Parameters.AddWithValue("@Tax", Taxtxtbx.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Successfully Payment Record has been Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            
        }

        private void Salary_ID_Txtbx_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open(); // Opens the connection

                    string query = "SELECT COUNT(1) FROM Salary WHERE Salary_ID = @SalaryID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SalaryID", Salary_ID_Txtbx.Text);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("The Salary ID already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Salary_ID_Txtbx.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Checkidbtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open(); // Opens the connection

                    // SQL query to get the maximum Salary_ID and Payment_ID from the Salary table
                    string query = "SELECT ISNULL(MAX(Salary_ID), 0), ISNULL(MAX(Payment_ID), 0) FROM Salary";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int newSalaryID = reader.GetInt32(0) + 1;
                                int newPayrollID = reader.GetInt32(1) + 1;

                                Salary_ID_Txtbx.Text = newSalaryID.ToString();
                                Payroll_ID.Text = newPayrollID.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnalysisBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportView R1 = new ReportView();
            R1.ShowDialog();
            R1 = null;
            this.Show();
        }
    }
      
}
