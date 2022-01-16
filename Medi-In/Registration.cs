using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Medi_In
{
    public partial class Registration : Form
    {
        //string connection
        string path = @"Data Source = MNCCORP\SQLEXPRESS; Initial Catalog = Medi_In; Integrated Security = True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        int select;

        string name;
        string surname;
        string ID;
        string town;
        string email;
        string address;
        string addmitted;
        string health;
        string title;
        string aid;
        string gender;

        string status;
        string language;
        string ethnicity;
        string religion;
        public Registration()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             name = txtName.Text;
             surname = txtSurname.Text;
             //ID = "";
             status = cmbStatus.Text;
             language = cmbLanguage.Text;
             ethnicity = cmbEthnicity.Text;
             religion = cmbReligion.Text;
             town = txtTown.Text;
             email = txtEmail.Text;
             address = txtAddress.Text;
            health = txtHealth.Text;
            addmitted = this.dateTimePicker2.Text;

            //int parsedID;
            //int.TryParse(ID, out parsedID);

            //int len = parsedID.Length;

            Regex ex = new Regex("[1-9]{13}");
            bool isValid = ex.IsMatch(txtID.Text);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid ID");
            }
            else
            {
                Console.WriteLine(ex);
            }

            //MessageBox.Show(name);

           
            if (dateTimePicker2.Text == "" || title == "" || txtName.Text == "" || txtSurname.Text == "" || gender == "" || txtID.Text == "" || cmbStatus.Text == "" || cmbLanguage.Text == "" || cmbEthnicity.Text == "" || cmbReligion.Text == "" || txtTown.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtHealth.Text == "")
            {
                MessageBox.Show("Please fill out the blanks");
            }
            else
            {
                try
                {
                    string title;
                    string aid;
                    string gender;

                    if (chkMr.Checked)
                    {
                        title = "Mr";
                    }
                    else if (chkMrs.Checked)
                    {
                        title = "Mrs";
                    }
                    else
                    {
                        title = "Mss";
                    }

                    if (rbtnYes.Checked)
                    {
                        aid = "Yes";
                    }
                    else
                    {
                        aid = "No";
                    }

                    if (rbtnMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }

                    con.Open();
                    cmd = new SqlCommand("insert into Registration (Addmitted, Title, Names, Surname, Gender, ID_Passport, Marital, Languages, Ethnicity, Religion, Birth_Place, Email, Home_Address, Health, Aid) values ('" + dateTimePicker2.Text + "','" + title + "','" + txtName.Text + "','" + txtSurname.Text + "','" + gender + "','" + txtID.Text + "','" + cmbStatus.Text + "','" + cmbLanguage.Text + "','" + cmbEthnicity.Text + "','" + cmbReligion.Text + "','" + txtTown.Text + "','" + txtEmail.Text + "','" + txtAddress.Text + "','" + txtHealth.Text + "', '" + aid + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Your Data Has Been Saved");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            clear();
            display();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            cmbStatus.Items.Add("Single");
            cmbStatus.Items.Add("Married");
            cmbStatus.Items.Add("Divorced");
            cmbStatus.Items.Add("Widowed");
            cmbStatus.Items.Add("Seperated");
            cmbStatus.Items.Add("N/A");

            cmbLanguage.Items.Add("Afrikaans");
            cmbLanguage.Items.Add("English");
            cmbLanguage.Items.Add("Ndebele");
            cmbLanguage.Items.Add("Pedi");
            cmbLanguage.Items.Add("Sotho");
            cmbLanguage.Items.Add("Swati");
            cmbLanguage.Items.Add("Tsonga");
            cmbLanguage.Items.Add("Tswana");
            cmbLanguage.Items.Add("Venda");
            cmbLanguage.Items.Add("Xhosa");
            cmbLanguage.Items.Add("Zulu");
            cmbLanguage.Items.Add("Other");

            cmbEthnicity.Items.Add("Black");
            cmbEthnicity.Items.Add("Coloured");
            cmbEthnicity.Items.Add("Indian");
            cmbEthnicity.Items.Add("White");
            cmbEthnicity.Items.Add("Other");

            cmbReligion.Items.Add("African Traditional & Diasporic");
            cmbReligion.Items.Add("Agnostic");
            cmbReligion.Items.Add("Anglican");
            cmbReligion.Items.Add("Atheist");
            cmbReligion.Items.Add("Charismatic");
            cmbReligion.Items.Add("Dutch Reformed");
            cmbReligion.Items.Add("Hindu");
            cmbReligion.Items.Add("Islam");
            cmbReligion.Items.Add("Judaism");
            cmbReligion.Items.Add("Methodist");
            cmbReligion.Items.Add("Muslim");
            cmbReligion.Items.Add("Pentecostal");
            cmbReligion.Items.Add("Protestant");
            cmbReligion.Items.Add("Roman Catholic");
            cmbReligion.Items.Add("Zion");
            cmbReligion.Items.Add("Other");
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            status = cmbStatus.SelectedItem.ToString();
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            language = cmbLanguage.SelectedItem.ToString();
        }

        private void cmbEthnicity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ethnicity = cmbEthnicity.SelectedItem.ToString();
        }

        private void cmbReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            religion = cmbReligion.SelectedItem.ToString();
        }
        public void display()
        {
            try
            {
                dt = new DataTable();
                con.Open();
                adpt = new SqlDataAdapter("select * from Registration", con);
                adpt.Fill(dt);
                dtDataGridView.DataSource = dt;
                con.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            /*dt.Columns.Add("Addmission Date");
            dt.Columns.Add("Title");
            dt.Columns.Add("Name(s)");
            dt.Columns.Add("Surname");
            dt.Columns.Add("Gender");
            dt.Columns.Add("ID/Passport");
            dt.Columns.Add("Marital Status");
            dt.Columns.Add("Language");
            dt.Columns.Add("Ethnicity");
            dt.Columns.Add("Religion");
            dt.Columns.Add("Place of Birth");
            dt.Columns.Add("Email");
            dt.Columns.Add("Home Address");

            DataRow dr = dt.NewRow();
            dr[0] = addmitted;
            dr[1] = title;
            dr[2] = name;
            dr[3] = surname;
            dr[4] = gender;
            dr[5] = ID;
            dr[6] = status;
            dr[7] = language;
            dr[8] = ethnicity;
            dr[9] = religion;
            dr[10] = town;
            dr[11] = email;
            dr[12] = address;

            dt.Rows.Add(dr);
            dtDataGridView.DataSource = dt;*/
        }
        public void clear()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtID.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtTown.Text = "";
            txtHealth.Text = "";
            this.dateTimePicker2.Text = "";
            cmbStatus.Text = "";
            cmbLanguage.Text = "";
            cmbEthnicity.Text = "";
            cmbReligion.Text = "";
        }

        private void dtDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //To view the details from the text boxes for Update and Remove button operation
        {
            this.dateTimePicker2.Text = dtDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            //title = dtDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dtDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSurname.Text = dtDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            //gender = dtDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtID.Text = dtDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbStatus.Text = dtDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
            cmbLanguage.Text = dtDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbEthnicity.Text = dtDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
            cmbReligion.Text = dtDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtTown.Text = dtDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtEmail.Text = dtDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtAddress.Text = dtDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtHealth.Text = dtDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();
            //aid = dtDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
