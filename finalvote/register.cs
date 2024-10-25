using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace finalvote
{
    public partial class register : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;

        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        public register()
        {
            InitializeComponent();
            
            textBox8.UseSystemPasswordChar = true;
            textBox6.UseSystemPasswordChar = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox6.UseSystemPasswordChar = true; // Hide password
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox6.UseSystemPasswordChar = false; // Show password

        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
          
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            textBox8.UseSystemPasswordChar = false;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            textBox8.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox8.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("DOB cannot be empty.");
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Father name cannot be empty.");
                return;
            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Email cannot be empty.");
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Mobile No cannot be empty.");
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Password cannot be empty.");
                return;
            }
            
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Adhar No cannot be empty.");
                return;
            }
            
            if (textBox6.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
                return;
            }
            if (string.IsNullOrEmpty(textBox8.Text) || textBox8.Text.Length != 12 || !long.TryParse(textBox8.Text, out _))
            {
                MessageBox.Show("Aadhar number must be exactly 12 digits and numeric.");
                return;
            }
            connection();

            try
            {
                cmd = new SqlCommand("insert into users(full_name,dob,father_name,email,mobile_no,password,adhar_no) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox8.Text + "')", con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Register Successfully!");
                this.Hide();
                login l = new login();
                l.Show();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
