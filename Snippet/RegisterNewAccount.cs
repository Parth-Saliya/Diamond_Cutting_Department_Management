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
    public partial class RegisterNewAccount : Form
    {
        public RegisterNewAccount()
        {
            InitializeComponent();
        }
        CLB c1 = new CLB();
        private void textBox4_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            c1.OnlyDec(e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if(textBox6.TextLength <6 )
            {
                MessageBox.Show("Your Password Is Too Short! It Should Contain Atleast 6 Character.");
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if(textBox6.Text != textBox5.Text)
            {
                MessageBox.Show("Your Password Doesn't Match !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("insert into RegisterNewAccount(AccId,Name,MobileNo,EmailId,Password) values('"+textBox1.Text+"','"+textBox2.Text
                    +"','"+textBox4.Text+"','"+textBox3.Text+"','"+textBox6.Text+"')",c1.con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("insert into RegisterNewUser(AccId,UserName,Role,Password) values('"+textBox1.Text+"','"+textBox2.Text
                    +"','Admin','"+textBox6.Text+"')",c1.con);
                cmd1.ExecuteNonQuery();
                c1.conclose();
                MessageBox.Show("Registered Successfully !");
                Clear();
            }
            else
            {
                MessageBox.Show("Please Fill Up All Details Properly !");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            c1.conopen();
            SqlCommand cmd2 = new SqlCommand("select max(AccId) from RegisterNewAccount", c1.con);
            var var = cmd2.ExecuteScalar();
            int a = Convert.ToInt32(var.ToString());
            c1.conclose();
            if (a == 0)
                a = 1001;
            else
                a++;
            textBox1.Text = a.ToString();
            c1.conclose();
        }

        public void Clear()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is ComboBox)
                        (control as ComboBox).ResetText();
                    else if (control is RichTextBox)
                        (control as RichTextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
        public void AllKeyDown(object sender, KeyPressEventArgs e)
        {
            
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
              
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void RegisterNewAccount_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterNewAccount_Load(object sender, EventArgs e)
        {

        }
    }
}
