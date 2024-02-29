using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public partial class Dashboard : Form
    {
        public Dashboard(string s,string st,int i)
        {
            InitializeComponent();
            userrole.Text = s;
            accid.Text = i.ToString();
            username.Text = st;
            if (s == "Admin")
            {
                btnrnu.Visible = true;
                btnaddcom.Visible = true;
            }
            else
            {
                btnrnu.Visible = false;
                btnaddcom.Visible = false;
            }
        }
        CLB c1 = new CLB();
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnrnu_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(accid.Text);
            RegisterNewUser rnu = new RegisterNewUser(i);
            rnu.Show();
        }

        private void Dashboard_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle BaseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            Brush Gradient_Brush = new LinearGradientBrush(BaseRectangle, Color.Cyan, Color.White, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = Convert.ToInt32(accid.Text);
            ForgotPassword fp = new ForgotPassword(j);
            fp.Show();
        }
    }
}
