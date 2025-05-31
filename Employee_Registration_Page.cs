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
    public partial class Employee_Registration_Page : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");

        public Employee_Registration_Page()
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
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string gen;
                if (MaleRadiobutton.Checked)
                    gen = "M";
                else
                    gen = "F";

                // Check if the Employee ID already exists
                string sqlCheckExistence = "SELECT COUNT(*) FROM Employee_Information WHERE Employee_ID = @EmployeeID";
                SqlCommand cmdCheckExistence = new SqlCommand(sqlCheckExistence, con);
                cmdCheckExistence.Parameters.AddWithValue("@EmployeeID", Employee_ID_txtbx.Text);
                con.Open();
                int count = (int)cmdCheckExistence.ExecuteScalar();
                con.Close();

                if (count > 0)
                {
                    MessageBox.Show("Employee ID already exists in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                string sqlInsert = "INSERT INTO Employee_Information(Employee_ID, Employee_Name, Employee_Age, Gender, Employee_Address, Salary_Per_Month, [Over Time Rate], Allowances,Tax) " +
                                   "VALUES ('" + Employee_ID_txtbx.Text + "','" + Employee_name_txtbx.Text + "','" + Employee_Age_txtbx.Text + "','" + gen + "','" + Employee_Address_txtxbx.Text + "','" +
                                   Employee_Salary_txtbx.Text + "','" + Employee_OT_txtbx.Text + "','" + Employee_Allowances_txtbx.Text + "','" + Taxtxtbx.Text + "')";

                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee inserted successfully");
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

        private void Main_Menubtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage F2 = new Homepage();
            F2.ShowDialog();
            F2 = null;
            this.Show();
        }
    }
        
}
