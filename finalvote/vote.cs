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
    
    public partial class vote : Form
    {
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;

        DataTable candidateTable;
        int currentIndex = 0; // Track the current index for pagination
        const int CandidatesPerPage = 3;
        private int userId;
        private int RoundId;
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }
        public vote(int userId,int RoundId)
        {
            InitializeComponent();
            this.userId = userId;
            this.RoundId = RoundId;
        }

        public vote()
        {
            InitializeComponent();
        }

        private void DisplayCandidates(int startIndex)
        {
            ClearCandidateDetails(); // Clear previous candidate details
            SetCandidateVisibility(false, false, false); // Hide all sections initially

            for (int i = 0; i < CandidatesPerPage; i++)
            {
                int index = startIndex + i;
                if (index < candidateTable.Rows.Count)
                {
                    DataRow row = candidateTable.Rows[index];

                    // Assign candidate ID to radio button's Tag property
                    switch (i)
                    {
                        case 0:
                            SetCandidateVisibility(true, false, false);
                            DisplayCandidateDetails(row, label1, label2, label3, label4, label5, label6, label7, radioButton1, pictureBox1);
                            radioButton1.Tag = row["candidate_id"]; // Store ID in Tag
                            break;
                        case 1:
                            SetCandidateVisibility(true, true, false);
                            DisplayCandidateDetails(row, label9, label10, label11, label12, label13, label14, label15, radioButton2, pictureBox2);
                            radioButton2.Tag = row["candidate_id"]; // Store ID in Tag
                            break;
                        case 2:
                            SetCandidateVisibility(true, true, true);
                            DisplayCandidateDetails(row, label17, label18, label19, label20, label21, label22, label23, radioButton3, pictureBox3);
                            radioButton3.Tag = row["candidate_id"]; // Store ID in Tag
                            break;
                    }
                }
            }
        }

        // Helper function to set visibility of candidate sections
        private void SetCandidateVisibility(bool section1, bool section2, bool section3)
        {
            // Control visibility of the first section
            label1.Visible = label2.Visible = label3.Visible = label4.Visible =
            label5.Visible = label6.Visible = label7.Visible =
            radioButton1.Visible = pictureBox1.Visible = section1;

            // Control visibility of the second section
            label9.Visible = label10.Visible = label11.Visible = label12.Visible =
            label13.Visible = label14.Visible = label15.Visible = 
            radioButton2.Visible = pictureBox2.Visible = section2;

            // Control visibility of the third section
            label17.Visible = label18.Visible = label19.Visible = label20.Visible =
            label21.Visible = label22.Visible = label23.Visible = 
            radioButton3.Visible = pictureBox3.Visible = section3;
        }
        private bool IsVotingRoundActive()
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SELECT IsActive FROM VotingRound WHERE RoundId = @RoundId", con);
                cmd.Parameters.AddWithValue("@RoundId", RoundId);

                object result = cmd.ExecuteScalar();

                // Debugging line
                
                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking voting round status: " + ex.Message);
                return false;
            }
            finally
            {
                con?.Close();
            }
        }

        private void FetchCandidateInformation()
        {
            try
            {
                if (!IsVotingRoundActive())
                {
                    MessageBox.Show("The current voting round is not active. No candidates will be displayed.");
                    return;
                }
                connection();
                cmd = new SqlCommand("SELECT * FROM candidates", con);
                da = new SqlDataAdapter(cmd);
                candidateTable = new DataTable();
                da.Fill(candidateTable);

                if (candidateTable.Rows.Count > 0)
                {
                    DisplayCandidates(0); // Display first set of candidates
                }
                else
                {
                    MessageBox.Show("No candidates found for this round!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching candidates: " + ex.Message);
            }
            finally
            {
                con?.Close();
            }
        }

        private void ClearCandidateDetails()
        {
            // Clear labels
            label1.Text = label2.Text = label3.Text = label4.Text = label5.Text = label6.Text = label7.Text = "";
            label9.Text = label10.Text = label11.Text = label12.Text = label13.Text = label14.Text = label15.Text= "";
            label17.Text = label18.Text = label19.Text = label20.Text = label21.Text = label22.Text = label23.Text = "";

            // Clear checkboxes
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = false;

            // Clear images
            pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = null;
        }

        // Display candidate information on the labels and picture box
        private void DisplayCandidateDetails(DataRow row, Label idLabel, Label nameLabel, Label ageLabel,
     Label partyLabel, Label networthLabel, Label educationLabel, Label contactLabel,
     RadioButton radioButton, PictureBox pictureBox)
        {
            // Assign values to the labels
            idLabel.Text = "ID: " + row["candidate_id"].ToString();
            nameLabel.Text = "Name: " + row["c_name"].ToString();
            ageLabel.Text = "Age: " + row["c_age"].ToString();
            partyLabel.Text = "Party: " + row["c_party"].ToString();
            networthLabel.Text = "Networth: " + row["c_networth"].ToString();
            educationLabel.Text = "Education: " + row["c_education"].ToString();

            // Ensure contact is displayed last
            contactLabel.Text = "Contact: " + row["c_contact"].ToString();

            // Load candidate's image if available
            string imagePath = row["c_image"].ToString();
            if (File.Exists(imagePath))
            {
                pictureBox.Image = Image.FromFile(imagePath);
            }
            else
            {
                pictureBox.Image = null;
                MessageBox.Show("Image not found for: " + row["c_name"].ToString());
            }

            // Set radio button label to allow user to select the candidate for voting
            radioButton.Text = "Vote";
        }




        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex + CandidatesPerPage < candidateTable.Rows.Count)
            {
                currentIndex += CandidatesPerPage;
                DisplayCandidates(currentIndex);
            }
            else
            {
                MessageBox.Show("This is the last page of candidates.");
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex -= CandidatesPerPage;
                DisplayCandidates(currentIndex);
            }
            else
            {
                MessageBox.Show("This is the first page of candidates.");
            }
        }

        private void SaveVoteToDatabase(int candidateId, int userId, int roundId) // Add roundId as parameter
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(s))
                {
                    string query = "INSERT INTO Votes (candidate_id, user_id, RoundId, vote_time) VALUES (@CandidateId, @UserId, @RoundId, GETDATE())"; // Include RoundId

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RoundId", RoundId); // Set RoundId parameter

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving vote: " + ex.Message);
            }
        }
        private bool HasUserVoted(int userId, int roundId)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Votes WHERE user_id = @UserId AND round_id = @RoundId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@RoundId", roundId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Return true if user has voted
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking voting status: " + ex.Message);
                return false;
            }
            finally
            {
                con?.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int selectedCandidateId = 0;

            // Check which radio button is selected
            if (radioButton1.Checked && radioButton1.Tag != null)
            {
                selectedCandidateId = (int)radioButton1.Tag;
            }
            else if (radioButton2.Checked && radioButton2.Tag != null)
            {
                selectedCandidateId = (int)radioButton2.Tag;
            }
            else if (radioButton3.Checked && radioButton3.Tag != null)
            {
                selectedCandidateId = (int)radioButton3.Tag;
            }

            // Ensure a candidate is selected
            if (selectedCandidateId != 0)
            {
                if (HasUserVoted(userId, RoundId))
                {
                    MessageBox.Show("You have already voted in this round.");
                }
                else
                {
                    SaveVoteToDatabase(selectedCandidateId, userId, RoundId); // Pass RoundId here
                    MessageBox.Show("Your vote has been recorded.");
                }
            }
            else
            {
                MessageBox.Show("Please select a candidate.");
            }

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void vote_Load(object sender, EventArgs e)
        {
           FetchCandidateInformation();
        }
    }
}
