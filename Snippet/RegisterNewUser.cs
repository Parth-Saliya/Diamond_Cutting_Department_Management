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
    public partial class RegisterNewUser : Form
    {
        public RegisterNewUser(int i)
        {
            InitializeComponent();
            textBox3.Text = i.ToString();
        }
        CLB c1 = new CLB();
        private void RegisterNewUser_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                try
                {
                    c1.conopen();
                    SqlCommand cmd = new SqlCommand("insert into RegisterNewUser(AccId,UserName,Role,Password) values('" + textBox3.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox6.Text + "')", c1.con);
                    cmd.ExecuteNonQuery();
                    c1.conclose();
                    Clear();
                }
                catch(SqlException)
                {
                    MessageBox.Show("This Entry Already Exist. Duplicate Entry Not Allowed !");
                }
            }
            else
                MessageBox.Show("Please Fill Up All Details Properly !");
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.TextLength < 6)
                MessageBox.Show("Your Password Is Too Short! It Should Contain Atleast 6 Character.");
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                MessageBox.Show("Your Password Doesn't Match !");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void RegisterNewUser_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e, this.Width, this.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
