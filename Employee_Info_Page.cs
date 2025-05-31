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
    public partial class Employee_Info_Page : Form
    {
        SqlConnection con = new SqlConnection ("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");
        public Employee_Info_Page()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            Employee_ID_txtbx.Clear();
            Employee_name_txtbx.Clear();
            Employee_Age_txtbx.Clear();
            Employee_Address_txtxbx.Clear();
            MaleRadiobutton.Checked = false;
            FemaleRadiobutton.Checked = false;
            Employee_Salary_txtbx.Clear();
            Employee_OT_txtbx.Clear();
            Employee_Allowances_txtbx.Clear();
            Taxtxtbx.Clear();
        }
        private void Update()
        {
            try
            {
                string gen;
                if (MaleRadiobutton.Checked == true)
                    gen = "M";
                else
                    gen = "F";
                string sqlUpdate;
                sqlUpdate = "UPDATE Employee_Information SET Employee_Name='" + Employee_name_txtbx.Text + "', Employee_Age='" + Employee_Age_txtbx.Text + "', Gender='" + gen + "', Employee_Address='" + Employee_Address_txtxbx.Text + "', Salary_Per_Month='" + Employee_Salary_txtbx.Text + "', [Over Time Rate]='" + Employee_OT_txtbx.Text + "', Allowances='" + Employee_Allowances_txtbx.Text + "', Tax='" + Taxtxtbx.Text + "' WHERE Employee_ID='" + Employee_ID_txtbx.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Updated");
                Clear();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }


        private void Delete()
        {
            try
            {
                string employeeID = Employee_ID_txtbx.Text;
                string sqlDeleteSalary = "DELETE FROM Salary WHERE Employee_ID = @EmployeeID";
                string sqlDeleteEmployee = "DELETE FROM Employee_Information WHERE Employee_ID = @EmployeeID";

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;"))
                {
                    con.Open();

                    // Deletes the related salary records first
                    SqlCommand deleteSalaryCmd = new SqlCommand(sqlDeleteSalary, con);
                    deleteSalaryCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    deleteSalaryCmd.ExecuteNonQuery();

                    // Now it deletes the employee record
                    SqlCommand deleteEmployeeCmd = new SqlCommand(sqlDeleteEmployee, con);
                    deleteEmployeeCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    int rowsAffected = deleteEmployeeCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee record deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Employee ID not found in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the employee record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Search()
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



        private void Searchbtn_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Main_Menu_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage F2 = new Homepage();
            F2.ShowDialog();
            F2 = null;
            this.Show();
        }

        private void View_All_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Database_Viewer F6 = new Employee_Database_Viewer();
            F6.ShowDialog();
            F6 = null;
            this.Show();
        }
    }
}
