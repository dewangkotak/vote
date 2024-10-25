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
    public partial class admin_voteresult : Form
    {

        public admin_voteresult()
        {
            InitializeComponent();

        }
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;
        int userId, RoundId;

        private void admin_voteresult_Load(object sender, EventArgs e)
        {
            LoadRoundNames();
            DisplayTableHeaders();
        }

        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }
        private void LoadRoundNames()
        {
            try
            {
                using (con = new SqlConnection(s))
                {
                    con.Open();
                    string query = "SELECT DISTINCT RoundName FROM VotingRound";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    comboBoxRound.Items.Clear();
                    comboBoxRound.Items.Add("Select Round Name"); // Default placeholder

                    while (reader.Read())
                    {
                        comboBoxRound.Items.Add(reader["RoundName"].ToString());
                    }

                    comboBoxRound.SelectedIndex = 0; // Set "Select Round Name" as the default
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading round names: " + ex.Message);
            }
        }

        private void comboBoxRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRound.SelectedIndex > 0) // Skip the first item ("Select Round Name")
            {
                LoadVoteStatistics(comboBoxRound.SelectedItem.ToString());
            }
            else
            {
                // Clear the table if the user selects the default item
                DisplayTableHeaders();
            }
        }

        private void DisplayTableHeaders()
        {
            // Clear previous rows and set the table size to just the header row
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.ColumnCount = 5;

            // Add header row
            AddStyledLabel("Candidate Id", 0, 0, true);
            AddStyledLabel("Candidate Name", 1, 0, true);
            AddStyledLabel("Round Name", 2, 0, true);
            AddStyledLabel("Total Vote", 3, 0, true);
            AddStyledLabel("Percentage", 4, 0, true);
        }
        private void LoadVoteStatistics(string selectedRound)
        {
            try
            {
                using (con = new SqlConnection(s))
                {
                    con.Open();

                    // Adjust query to filter by selected Round Name if not "All"
                    string query = @"
                        SELECT c.candidate_id, c.c_name, r.RoundName, COUNT(v.vote_id) AS VoteCount
                        FROM candidates c
                        JOIN Votes v ON c.candidate_id = v.candidate_id
                        JOIN VotingRound r ON v.RoundId = r.RoundId
                        WHERE (@RoundName = 'All' OR r.RoundName = @RoundName)
                        GROUP BY c.candidate_id, c.c_name, r.RoundName;
                    ";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RoundName", selectedRound);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int totalVotes = dt.AsEnumerable().Sum(row => row.Field<int>("VoteCount"));

                    // Clear previous rows in TableLayoutPanel
                    tableLayoutPanel1.Controls.Clear();
                    tableLayoutPanel1.RowCount = dt.Rows.Count + 1;
                    tableLayoutPanel1.ColumnCount = 5;

                    // Add header row
                    AddStyledLabel("Candidate Id", 0, 0, true);
                    AddStyledLabel("Candidate Name", 1, 0, true);
                    AddStyledLabel("Round Name", 2, 0, true);
                    AddStyledLabel("Total Vote", 3, 0, true);
                    AddStyledLabel("Percentage", 4, 0, true);

                    // Populate TableLayoutPanel with data
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        string candidateId = row["candidate_id"].ToString();
                        string candidateName = row["c_name"].ToString();
                        string roundName = row["RoundName"].ToString();
                        int voteCount = Convert.ToInt32(row["VoteCount"]);
                        double percentage = totalVotes > 0 ? (voteCount / (double)totalVotes) * 100 : 0;

                        AddStyledLabel(candidateId, 0, i + 1);
                        AddStyledLabel(candidateName, 1, i + 1);
                        AddStyledLabel(roundName, 2, i + 1);
                        AddStyledLabel(voteCount.ToString(), 3, i + 1);
                        AddStyledLabel(percentage.ToString("F2") + " %", 4, i + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vote statistics: " + ex.Message);
            }
        }
        // Helper method to add styled labels with borders and font settings
        private void AddStyledLabel(string text, int column, int row, bool isHeader = false)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", isHeader ? 12 : 10, isHeader ? FontStyle.Bold : FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(5),
                AutoSize = true,
                BackColor = isHeader ? Color.LightGray : Color.White
            };

            tableLayoutPanel1.Controls.Add(label, column, row);
        }


    }
}
