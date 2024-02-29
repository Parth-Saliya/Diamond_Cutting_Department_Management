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
    public partial class UserLogin : Form
    {
        public UserLogin(int i)
        {
            InitializeComponent();
            textBox3.Text = i.ToString();
        }
        CLB c1 = new CLB();
        
        private void UserLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string c = "";
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select UserName from RegisterNewUser where AccId = '"+textBox3.Text+"'", c1.con);
                var a = cmd.ExecuteScalar();
                if (a == null)
                    MessageBox.Show("User Name Doesn't Exist !");
                SqlCommand cmd1 = new SqlCommand("select Password from RegisterNewUser where UserName = '"+textBox1.Text+"' and AccId = '"+textBox3.Text+"'",c1.con);
                var a1 = cmd1.ExecuteScalar();
                string str = a1.ToString();
                if (str != textBox2.Text)
                    MessageBox.Show("Password Doesn't Match !");
                else
                {
                    c1.conopen();
                    SqlCommand cmd2 = new SqlCommand("select Role from RegisterNewUser where UserName = '"+textBox1.Text+"' and AccId = '"+textBox3.Text+"'", c1.con);
                    var b = cmd2.ExecuteScalar();
                    c1.conclose();
                    int i = Convert.ToInt32(textBox3.Text);
                    Dashboard d = new Dashboard(b.ToString(),textBox1.Text,i);
                    d.Show();
                }
                c1.conclose();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void UserLogin_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e, this.Width, this.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
