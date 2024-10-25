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
    public partial class home : Form
    {
        SqlConnection con;//used for connection
        SqlCommand cmd;//insert update dlt
        SqlDataAdapter da;
        DataSet ds;
        DataGridViewCellEventArgs es;

        String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harmi Raiyani\source\repos\finalvote\finalvote\vote.mdf;Integrated Security=True";
        String i, d;
        int id;
        int userId, RoundId;
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }
        public home()
        {
            InitializeComponent();
            
        }
      
    }
}
