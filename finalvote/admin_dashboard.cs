using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalvote
{
    public partial class admin_dashboard : Form
    {
        public admin_dashboard()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin_home ah = new admin_home();
            homepanel(ah);
        }
        private void homepanel(admin_home ah)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            ah.TopLevel = false;
            ah.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            ah.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(ah);

            // Show the form
            ah.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin_userdetail au = new admin_userdetail();
            userpanel(au);
        }
        private void userpanel (admin_userdetail au)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            au.TopLevel = false;
            au.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            au.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(au);

            // Show the form
            au.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin_candidate ac = new admin_candidate();
            candidatepanel(ac);
        }
        private void candidatepanel(admin_candidate ac)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            ac.TopLevel = false;
            ac.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            ac.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(ac);

            // Show the form
            ac.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            admin_crtvote acv = new admin_crtvote();
            createvotepanel(acv);
        }
        private void createvotepanel(admin_crtvote acv)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            acv.TopLevel = false;
            acv.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            acv.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(acv);

            // Show the form
            acv.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin_voteresult avr = new admin_voteresult();
            voteresultpanel(avr);
        }
        private void voteresultpanel(admin_voteresult avr)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            avr.TopLevel = false;
            avr.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            avr.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(avr);

            // Show the form
            avr.Show();
        }

        private void admin_dashboard_Load(object sender, EventArgs e)
        {
            admin_home ah = new admin_home();
            homepanel(ah);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            admin_crtvote acv = new admin_crtvote();
            votecrt(acv);
        }
        private void votecrt(admin_crtvote acv)
        {
            // Clear existing controls in the panel
            panel3.Controls.Clear();

            // Set the form's TopLevel property to false
            acv.TopLevel = false;
            acv.FormBorderStyle = FormBorderStyle.None;

            // Set the form's Dock property to fill the panel
            acv.Dock = DockStyle.Fill;

            // Add the form to the panel's controls
            panel3.Controls.Add(acv);

            // Show the form
            acv.Show();
        }
    }
}
