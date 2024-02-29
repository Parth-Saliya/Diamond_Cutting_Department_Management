using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword(int i)
        {
            InitializeComponent();
            accid.Text = i.ToString();
        }
        CLB c1 = new CLB();
        private void ForgotPassword_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {   
                c1.conopen();
                MessageBox.Show(accid.ToString());
                SqlCommand cmd1 = new SqlCommand("select Password from RegisterNewUser where UserName = '" + textBox1.Text + "'and AccId = '" + accid.Text + "'", c1.con);
                var b = cmd1.ExecuteScalar();
                //MessageBox.Show(b.ToString());
                if (b.ToString() == textBox2.Text)
                {
                    if (textBox2.Text == textBox3.Text)
                        MessageBox.Show("Current And New Password Must Be Different !");
                    SqlCommand cmd = new SqlCommand("update RegisterNewUser set Password='" + textBox3.Text + "' where UserName = '" + textBox1.Text + "'and AccId = '" + accid.Text + "'", c1.con);
                    cmd.ExecuteNonQuery();
                }
                else
                    MessageBox.Show("Password Doesn't Match The Username !");
                c1.conclose();
            }
            else
                MessageBox.Show("Please Fill Up All Details Properly !");
        }
    }
}
