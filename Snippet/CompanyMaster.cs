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
    public partial class CompanyMaster : Form
    {
        string st = "";int a = 0;
        public CompanyMaster(int i,string s)
        {
            InitializeComponent();
            st = s;a = i;
        }
        CLB c1 = new CLB();
        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            Disp();
        }

        private void CompanyMaster_Paint(object sender, PaintEventArgs e)
        {
            c1.Paint(e,this.Width,this.Height);
            //Rectangle BaseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            //Brush Gradient_Brush = new LinearGradientBrush(BaseRectangle, Color.White, Color.Cyan, LinearGradientMode.Vertical);
            //e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle);
        }

        private void CompanyMaster_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comname.Text != "" && ownername.Text != "" && mono1.Text != "")
            {


                c1.conopen();
                SqlCommand cmd = new SqlCommand("insert into CompanyMaster(ComName,OwnerName,MobileNo1,MobileNo2,Address,Tagline,BankName,AccNo,IFSCCode,PANNo,GSTNo,AccId,UserName) values('" +
                    comname.Text + "','" + ownername.Text + "','" + mono1.Text + "','" + mono2.Text + "','" + address.Text + "','" + tagline.Text + "','" + bankname.Text + "','" + accno.Text
                    + "','" + ifsccode.Text + "','" + panno.Text + "','" + gstno.Text + "','"+a+"','"+st+"')", c1.con);
                cmd.ExecuteNonQuery();
                c1.conclose();
                MessageBox.Show("Company Added Successfully !");
                Clear();
                Disp();
            }
            else
                MessageBox.Show("Please Fill Up All Details Properly !");
        }

        public void Disp()
        {
            c1.conopen();
            SqlCommand cmd = new SqlCommand("select * from CompanyMaster where AccId = '"+a+"'",c1.con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            c1.conclose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                comname.Text = row.Cells[1].Value.ToString();
                ownername.Text = row.Cells[2].Value.ToString();
                mono1.Text = row.Cells[3].Value.ToString();
                mono2.Text = row.Cells[4].Value.ToString();
                address.Text = row.Cells[5].Value.ToString();
                tagline.Text = row.Cells[6].Value.ToString();
                bankname.Text = row.Cells[7].Value.ToString();
                accno.Text = row.Cells[8].Value.ToString();
                ifsccode.Text = row.Cells[9].Value.ToString();
                panno.Text = row.Cells[10].Value.ToString();
                gstno.Text = row.Cells[11].Value.ToString();
                comid.Text = row.Cells[0].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comname.Text != "" && ownername.Text != "" && mono1.Text != "")
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("update CompanyMaster set ComName = '" + comname.Text + "',OwnerName = '" + ownername.Text + "',MobileNo1 = '" + mono1.Text + "', MobileNo2 = '"
                    + mono2.Text + "', Address = '" + address.Text + "', Tagline = '" + tagline.Text + "', Bankname = '" + bankname.Text + "', AccNo = '" + accno.Text + "', IFSCCode = '" +
                    ifsccode.Text + "',PANNo = '" + panno.Text + "',GSTNo = '" + gstno.Text + "',AccId = '"+a+"',UserName = '"+st+"' where ComId = '"+comid.Text+"'", c1.con);
                cmd.ExecuteNonQuery();
                c1.conclose();
                MessageBox.Show("Updated Successfully !");
                Clear();
                Disp();
            }
            else
                MessageBox.Show("Please Fill Up All Details Properly !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            c1.conopen();
            SqlCommand cmd = new SqlCommand("delete from CompanyMaster where ComId = '"+comid.Text+"'", c1.con);
            cmd.ExecuteNonQuery();
            c1.conclose();
            MessageBox.Show("Deleted Succesfully !");
            Clear();
            Disp();
        }
    }
}
