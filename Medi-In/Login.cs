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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = MNCCORP\SQLEXPRESS; Initial Catalog = cmblogin; Integrated Security = True");
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cmbUser.Items.Add("Admin");
            cmbUser.Items.Add("Doctor");
            cmbUser.Items.Add("Nurse");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUser.Text == "" && txtuser.Text == "" && txtPass.Text == "")
                {
                    MessageBox.Show("Please select the correct Usertype with Username and Password!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source = MNCCORP\SQLEXPRESS; Initial Catalog = cmblogin; Integrated Security = True");
                    SqlCommand cmd = new SqlCommand("select * from Login where Usertype='"+cmbUser.Text+"' and Username='"+txtuser.Text+"' and Password='"+txtPass.Text+"'", con);
                    cmd.Parameters.Add("@Type", cmbUser.Text);
                    cmd.Parameters.Add("@User", txtuser.Text);
                    cmd.Parameters.Add("@Pass", txtPass.Text);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);

                    string cmbItemValue = cmbUser.SelectedItem.ToString();
                    int count = ds.Tables[0].Rows.Count;

                    if (count > 0)
                    {
                        MessageBox.Show("You have successfuly logged in");
                        Form1 Reg = new Form1();
                        Reg.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please kindly check your credantials!");
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
