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

namespace Medi_In
{
    public partial class Addmission : Form
    {
        string path = @"Data Source = MNCCORP\SQLEXPRESS; Initial Catalog = Medi_In; Integrated Security = True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        public Addmission()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            adpt = new SqlDataAdapter("select * from Addmission where Names like '%" + txtSearch.Text + "%'", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dtDataGridView.DataSource = dt;
            con.Close();
        }

        public void display()
        {
            try
            {
                dt = new DataTable();
                con.Open();
                adpt = new SqlDataAdapter("select * from Addmission", con);
                adpt.Fill(dt);
                dtDataGridView.DataSource = dt;
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }

    }
}
