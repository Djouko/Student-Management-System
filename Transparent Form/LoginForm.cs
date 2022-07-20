using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Transparent_Form
{
    public partial class LoginForm : Form
    {
        StudentClass student = new StudentClass();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Red;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Transparent;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (textBox_usrname.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("Need login data", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string uname = textBox_usrname.Text;
                string pass = textBox_password.Text;
                DataTable table = student.getList(new MySqlCommand("SELECT * FROM `user` WHERE `username`= '" + uname + "' AND `password`='" + pass + "'"));
                if (table.Rows.Count > 0)
                {

                    MainForm main = new MainForm();
                    this.Hide();
                    main.Show();

                }
                else
                {
                    MessageBox.Show("Your username and password are not exists", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   

        }
    }
}
