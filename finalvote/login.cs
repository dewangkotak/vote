using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace finalvote
{
    public partial class login : Form
    {
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;
        int userId,RoundId;
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            try
            {
                using (con = new SqlConnection(s))
                {
                    con.Open();
                    string query = "SELECT user_id,full_name FROM users WHERE email = @Email AND password = @Password";
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox6.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = Convert.ToInt32(reader["user_id"]);
                                string username = reader["full_name"].ToString();

                                RoundId = GetActiveRoundId();

                                MessageBox.Show("Login Successful!");

                                // Pass the username to the dashboard form.
                                

                                dashboard d = new dashboard(userId,username,RoundId);
                                this.Hide();
                                d.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private int GetActiveRoundId()
        {
            try
            {
                connection();
                string query = "SELECT RoundId FROM VotingRound WHERE IsActive = 1"; // Assuming IsActive is used to denote active rounds
                using (cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0; // or some default value indicating no active round
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving active RoundId: " + ex.Message);
                return 0; // Default to 0 in case of error
            }
            finally
            {
                con?.Close();
            }
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
