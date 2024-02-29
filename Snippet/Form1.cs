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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        CLB c1 = new CLB();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select count(AccId) from RegisterNewAccount where AccId = '" + textBox1.Text + "'", c1.con);
                var a = cmd.ExecuteScalar();
                c1.conclose();
                int b = Convert.ToInt32(a.ToString());
                if (b == 0)
                { MessageBox.Show("Account Id Doesn't Exist !"); }
                c1.conopen();
                SqlCommand cmd1 = new SqlCommand("select Password from RegisterNewAccount where AccId = '"+textBox1.Text+"'",c1.con);
                var a1 = cmd1.ExecuteScalar();
                if (a1 != null)
                {
                    string str = a1.ToString();
                    if (str == textBox2.Text)
                    {
                        int abc = Convert.ToInt32(textBox1.Text);
                        UserLogin ul = new UserLogin(abc);
                        ul.Show();
                    }
                    else
                        MessageBox.Show("Password Doesn't Match !");
                }
                
                c1.conclose();
                textBox1.Clear();
                textBox2.Clear();
            }
            else
                MessageBox.Show("Please Fill Up All Details Properly !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterNewAccount rna = new RegisterNewAccount();
            rna.Show();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
        }

        private void Login_Paint_1(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
