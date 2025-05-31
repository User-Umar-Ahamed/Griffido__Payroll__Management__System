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
    public partial class Employee_Database_Viewer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");

        public Employee_Database_Viewer()
        {
            InitializeComponent();
        }

        private void fillGrid()
        {
            string sqlfill;
            sqlfill = "Select * from Employee_Information";
            SqlDataAdapter da = new SqlDataAdapter(sqlfill, con);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }

        private void Employee_Database_Viewer_Load(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void Main_Menubtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Info_Page F3 = new Employee_Info_Page();
            F3.ShowDialog();
            F3 = null;
            this.Show();
        }
    }
}
