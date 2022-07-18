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
using System.Configuration;


namespace ADO.Net
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            //step1
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //step2 write a query that needs to be fire
                string qry = "insert into employeedetails values(@emp_name,@emp_salary)";
                //step 3 assign qry to the command
                cmd = new SqlCommand(qry, con);
                // step 4  assign parameter value
                cmd.Parameters.AddWithValue("@name", textName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(textSalary.Text));
                // step 5 open the connection to fire query
                con.Open();
                // step 6 fire the query
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success ! Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchById_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from employeedetails where emp_id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textName.Text = dr["emp_name"].ToString();// Name , Salary are the col names
                        textSalary.Text = dr["emp_salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                //step2 write a query that needs to be fire
                string qry = "update employeedetails set emp_name=@name, emp_salary=@salary where emp_id=@id;";
                //step 3 assign qry to the command
                cmd = new SqlCommand(qry, con);
                // step 4  assign parameter value
                cmd.Parameters.AddWithValue("@name", textName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(textSalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                // step 5 open the connection to fire query
                con.Open();
                // step 6 fire the query
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Success ! Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

       

    }
}
