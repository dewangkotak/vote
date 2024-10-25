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
    public partial class dashboard : Form
    {
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;
        private int RoundId;
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }
        private int userId;
        private string username;
        public dashboard(int userId,string full_name,int RoundId)
        {
            InitializeComponent();
            username = full_name; // Store the passed username
            this.userId = userId;
            this.RoundId = RoundId;
            // Display the username in a label (assuming you have a label named labelUsername)
            name.Text = "Hello, " + full_name;
        
        }

        public dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            home h = new home();
            homepanel(h);
        }
        private void homepanel(home h)
        {
            // Clear existing controls in the panel
            panel2.Controls.Clear();

            // Set the form's TopLevel property to false
            h.TopLevel = false;
            h.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            h.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel2.Controls.Add(h);

            // Show the form
            h.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            about a = new about();
            aboutpanel(a);
        }
        private void aboutpanel(about a)
        {
            // Clear existing controls in the panel
            panel2.Controls.Clear();

            // Set the form's TopLevel property to false
            a.TopLevel = false;
            a.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            a.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel2.Controls.Add(a);

            // Show the form
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contact c = new contact();
            contactpanel(c);
        }
        private void contactpanel (contact c)
        {
            // Clear existing controls in the panel
            panel2.Controls.Clear();

            // Set the form's TopLevel property to false
            c.TopLevel = false;
            c.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            c.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel2.Controls.Add(c);

            // Show the form
            c.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vote v = new vote(userId,RoundId);
            votepanel(v);
        }
        private void votepanel (vote v)
        {
            // Clear existing controls in the panel
            panel2.Controls.Clear();

            // Set the form's TopLevel property to false
            v.TopLevel = false;
            v.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            v.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel2.Controls.Add(v);

            // Show the form
            v.Show();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            home h = new home();
            homepanel(h);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
