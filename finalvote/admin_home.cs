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
    public partial class admin_home : Form
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
        public admin_home()
        {
            InitializeComponent();
        }
        void ShowLoginCountAndCandidates()
        {
            try
            {
                connection();

                // Query to get the count of distinct users
                string loginQuery = "SELECT COUNT(DISTINCT user_id) AS LoginCount FROM users";
                cmd = new SqlCommand(loginQuery, con);
                int loginCount = (int)cmd.ExecuteScalar();
                label1.Text = "Number of users logged in: " + loginCount.ToString();

                // Query to fetch all candidate names
                string candidateCountQuery = "SELECT COUNT(*) AS TotalCandidates FROM candidates";
                cmd = new SqlCommand(candidateCountQuery, con);
                int totalCandidates = (int)cmd.ExecuteScalar();
                label2.Text = "Total Candidates: " + totalCandidates.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void admin_home_Load(object sender, EventArgs e)
        {
            ShowLoginCountAndCandidates();
        }
    }
}
