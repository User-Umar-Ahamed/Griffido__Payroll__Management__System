using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Employee_info_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Info_Page F3 = new Employee_Info_Page();
            F3.ShowDialog();
            F3 = null;
            this.Show();
        }

        private void Employee_Reg_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Registration_Page F4 = new Employee_Registration_Page();
            F4.ShowDialog();
            F4 = null;
            this.Show();
        }

        private void Salary_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary_Component F6 = new Salary_Component();
            F6.ShowDialog();
            F6 = null;
            this.Show();
        }

        private void Setting_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Setting_Component F5 = new Setting_Component();
            F5.ShowDialog();
            F5 = null;
            this.Show();
        }
    }
}
