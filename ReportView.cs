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
using DGVPrinterHelper;

namespace WindowsFormsApp1
{
    public partial class ReportView : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-AQFHGCG\\SQLEXPRESS;Initial Catalog=\"Griffindo_Toys 2.0\";Integrated Security=True;");

        public ReportView()
        {
            InitializeComponent();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary_Component F6 = new Salary_Component();
            F6.ShowDialog();
            F6 = null;
            this.Show();
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            string sqlfill;
            sqlfill = "Select * from Salary ";
            SqlDataAdapter da = new SqlDataAdapter(sqlfill, con);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }

        private void Print_Btn_Click(object sender, EventArgs e)
        {
            // Create a DGVPrinter object to handle printing
            DGVPrinter printer = new DGVPrinter();

            // Set printer properties
            printer.Title = "Salary Report"; // Title to be printed at the top
            printer.SubTitle = "Generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true; // Include page numbers
            printer.ShowTotalPageNumber = true; // Show the total number of pages
            printer.PageNumberInHeader = false; // Show page numbers in the footer
            printer.PorportionalColumns = true; // Adjust columns to fit the page
            printer.HeaderCellAlignment = StringAlignment.Near; // Align the header
            printer.Footer = "Griffindo Toys"; // Footer text
            printer.FooterSpacing = 15; // Space between footer and content

            // Print the contents of the DataGridView (guna2DataGridView1 in this case)
            printer.PrintDataGridView(guna2DataGridView1);
        }
    }
    
}
