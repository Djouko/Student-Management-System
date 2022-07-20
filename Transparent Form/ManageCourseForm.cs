using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            showData();

        }
        // Show data of the course 
        private void showData()
        {
            //to show course list on datagridview
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Cname.Clear();
            textBox_Chour.Clear();
            textBox_description.Clear();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (textBox_Cname.Text == "" || textBox_Chour.Text == ""|| textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Need Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
                int id = Convert.ToInt32(textBox_id.Text);
                string cName = textBox_Cname.Text;
                int chr = Convert.ToInt32(textBox_Chour.Text);
                string desc = textBox_description.Text;


                if (course.updateCourse(id, cName, chr, desc))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("course update successfuly", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error-Course not Edit", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Need Course Id", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(textBox_id.Text);
                    if (course.deletCourse(id))
                    {
                        showData();
                        button_clear.PerformClick();
                        MessageBox.Show("course Deleted", "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_course.CurrentRow.Cells[0].Value.ToString();
            textBox_Cname.Text = DataGridView_course.CurrentRow.Cells[1].Value.ToString();
            textBox_Chour.Text = DataGridView_course.CurrentRow.Cells[2].Value.ToString();
            textBox_description.Text = DataGridView_course.CurrentRow.Cells[3].Value.ToString();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //To Search course and show on datagridview
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course` WHERE CONCAT(`CourseName`)LIKE '%"+textBox_search.Text+"%'"));
            textBox_search.Clear();
        }
    }
}
