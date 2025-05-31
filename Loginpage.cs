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
    public partial class Loginpage : Form
    {
        public Loginpage()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Usernametxtbx.Text == "Admin" && Passwordtxtbx.Text == "Admin123")
            {
                MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                Homepage f2 = new Homepage();
                // f2.ShowDialog(); Using this code blocks the Form2 opening and showed an Exception Eror
                f2.FormClosed += (s, args) => this.Show();
                f2.Show();


            }
            else
            {
                MessageBox.Show("Invalid Login Credentials. Please check username or password and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Usernametxtbx.Clear();
                Passwordtxtbx.Clear();
                Usernametxtbx.Focus();
            }
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            if (Passwordtxtbx.PasswordChar == '*')
            {
                Passwordtxtbx.PasswordChar = '\0';
            }
            else
            {
                Passwordtxtbx.PasswordChar = '*';
            }
        }
    }
}
