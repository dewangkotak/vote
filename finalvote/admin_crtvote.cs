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
    public partial class admin_crtvote : Form
    {
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
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
        

        public admin_crtvote()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           

        }
        private void clear()
        {
            textBox1.Clear();
            checkBox1.Checked = false;

            // Reset DateTimePickers to the current date and time
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }
        void gridFill()
        {
            try
            {
                connection(); // Open the database connection

                // SQL query with calculated status
                string query = @"
                SELECT RoundId, RoundName, StartDate, EndDate, IsActive, 
                CASE 
                    WHEN GETDATE() < StartDate THEN 'Upcoming'
                    WHEN GETDATE() BETWEEN StartDate AND EndDate THEN 'Ongoing'
                    ELSE 'Completed'
                END AS Status 
                FROM VotingRound;";

                // Initialize SqlDataAdapter with query and connection
                da = new SqlDataAdapter(query, con);

                // Create a DataTable to hold the query result
                DataTable dt = new DataTable();

                // Fill the DataTable with data
                da.Fill(dt);

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt;

                // Close the connection after use
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Handle any errors
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Add")
            {
                try
                {
                    connection();
                    
                    cmd = new SqlCommand("insert into VotingRound(RoundName,StartDate,EndDate,IsActive) values('" + textBox1.Text + "', @StartDate, @EndDate, @IsActive )", con);
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value); // Correctly use .Value
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value); // Correctly use .Value
                    cmd.Parameters.AddWithValue("@IsActive", checkBox1.Checked); // Use .Checked for CheckBox

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" New Round Add Succesfully" );
                    clear();
                    gridFill();

                }
                catch (Exception res)
                {
                    MessageBox.Show(" " + res);
                }
            }
            else
            {
                try
                {
                    connection();
                    id = Convert.ToInt16(dataGridView1.Rows[es.RowIndex].Cells["RoundId"].Value);
                    cmd = new SqlCommand("Update VotingRound set RoundName='"+textBox1.Text+ "',StartDate = @StartDate, EndDate = @EndDate, IsActive = @IsActive where RoundId='" + id+"'", con);
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value); // DateTime values directly
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);   // DateTime values directly
                    cmd.Parameters.AddWithValue("@IsActive", checkBox1.Checked);
                    cmd.ExecuteNonQuery();
                    clear();
                   gridFill();
                }
                catch (Exception ree)
               {
                  MessageBox.Show(" " + ree);
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                button2.Text = "Update";
                es = e;
                connection();
                id = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells["RoundID"].Value);


                textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells["RoundName"].Value).ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["StartDate"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["EndDate"].Value);

                // Handle the CheckBox value. It assumes IsActive is stored as a boolean.
                checkBox1.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["IsActive"].Value);


            }
            else
            {
                connection();
                id = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells["Emp_id"].Value);
                cmd = new SqlCommand("delete from VotingRound where RoundId = '" + id + "'", con);
                cmd.ExecuteNonQuery();
                gridFill();
            }
        }

        private void admin_crtvote_Load(object sender, EventArgs e)
        {
            gridFill();
        }

        
    }
}
