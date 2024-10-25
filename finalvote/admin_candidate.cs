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
    public partial class admin_candidate : Form
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
        void uploading()
        {
            openFileDialog1.ShowDialog();
            try
            {
                i = openFileDialog1.FileName.ToString();
                pictureBox1.Load(i);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e);
            }
        }

        public admin_candidate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Add")
            {
                try
                {
                    connection();
                    //uploading();

                    d = Application.StartupPath + "\\images\\" + openFileDialog1.SafeFileName.ToString();

                    System.IO.File.Copy(i, d);

                    cmd = new SqlCommand("insert into candidates(c_name,c_age,c_networth,c_education,c_party,c_contact,c_image) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + d + "')", con);

                    cmd.ExecuteNonQuery();
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
                    id = Convert.ToInt16(dataGridView2.Rows[es.RowIndex].Cells["candidate_id"].Value);
                    cmd = new SqlCommand("Update candidates set c_name='" + textBox2.Text + "'," +
    "c_age='" + textBox3.Text + "'," +
    "c_networth='" + textBox4.Text + "'," + 
    "c_education='" + textBox5.Text + "'," +
    "c_party='" + textBox6.Text + "'," +
    "c_contact='" + textBox7.Text + "' where candidate_id='" + id + "'", con);

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

        private void button1_Click(object sender, EventArgs e)
        {
            uploading();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            gridFill();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            clear();
        }

        private void dataGridView2_CellClick_2(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Update")
                {
                    button2.Text = "Update";
                    es = e;
                    connection();
                    id = Convert.ToInt16(dataGridView2.Rows[e.RowIndex].Cells["candidate_id"].Value);


                    
                    textBox2.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_name"].Value).ToString();
                    textBox3.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_age"].Value).ToString();
                    textBox4.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_networth"].Value).ToString();
                    textBox5.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_education"].Value).ToString();
                    textBox6.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_party"].Value).ToString();
                    textBox7.Text = (dataGridView2.Rows[e.RowIndex].Cells["c_contact"].Value).ToString();

                    
                }
                else
                {
                    connection();
                    id = Convert.ToInt16(dataGridView2.Rows[e.RowIndex].Cells["candidate_id"].Value);
                    cmd = new SqlCommand("delete from candidates where candidate_id='" + id + "'", con);
                    cmd.ExecuteNonQuery();
                    gridFill();
                }
            }
            catch (Exception res)
            {
                MessageBox.Show(" " + res);
            }
        }

        void gridFill()
        {
            connection();
            da = new SqlDataAdapter("select candidate_id,c_name,c_age,c_networth,c_education,c_party,c_contact,c_image from candidates", con);

            DataTable dt = new DataTable(); 
            da.Fill(dt);

            dt.Columns.Add("image", Type.GetType("System.Byte[]"));

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataRow drow in dt.Rows)
            {
                drow["image"] = File.ReadAllBytes(drow["c_image"].ToString());
            }

            dataGridView2.DataSource = dt;

        }
    }
}
