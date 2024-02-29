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
    public partial class F_Payment : Form
    {
        public F_Payment()
        {
            InitializeComponent();
        }

        CLB c1 = new CLB();

        string qry = "";

        public void fillparty()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'By' and Party_Id != '1'", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                By.DataSource = dt;
                By.DisplayMember = "PartyName";
                By.ValueMember = "Party_Id";

                SqlCommand cmd1 = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'Party'", c1.con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                Party.DataSource = dt1;
                Party.DisplayMember = "PartyName";
                Party.ValueMember = "Party_Id";

                c1.conclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        F_Dash fd = new F_Dash();

        private void F_Payment_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            dispdata();
            fillparty();
        }

        public void dispdata()
        {
            qry = "SELECT Payment.Id, Payment.PayDt, Payment.Byy, Party.PartyName, Payment.Amount, Payment.Remark, Payment.RegDt FROM Party INNER JOIN Payment ON Party.Party_Id = Payment.Party  order by id desc";
            c1.getData(dataGridView1, qry);
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 135;
            dataGridView1.Columns[4].Width = 105;
            dataGridView1.Columns[5].Width = 135;
            dataGridView1.Columns[6].Width = 105;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                int ab = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                //  MessageBox.Show(ab.ToString());
                var name = c1.execScalar("Select Partyname From Party where Party_id='" + ab + "'");
                string sname = Convert.ToString(name);
                dataGridView1.Rows[i].Cells[2].Value = sname;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(Paydt.Value.ToString("yyyy-MM-dd"));
            qry = "insert into Payment(PayDt,Byy,Party,Amount,Remark,RegDt) values('" + Paydt.Value.ToString("yyyy-MM-dd") + "','" + By.SelectedValue + "','" + Party.SelectedValue + "','" + Amt.Text + "','" + Remark.Text + "','" + dt.Value.ToString("yyyy-MM-dd") + "')";
            int a = c1.exec(qry);
            if (a==1)
            {
                MessageBox.Show("Paid Sucessfully");
                dispdata();
            }else
            {
                MessageBox.Show("Paid Unsucessfull !!!");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    textBox1.Text = row.Cells[0].Value.ToString();
                    Paydt.Text = row.Cells[1].Value.ToString();
                    By.Text = row.Cells[2].Value.ToString();
                    Party.Text = row.Cells[3].Value.ToString();
                    Amt.Text = row.Cells[4].Value.ToString();
                    Remark.Text = row.Cells[5].Value.ToString();
                    dt.Text = row.Cells[6].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Update This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    qry = "update Payment set Byy='" + By.SelectedValue + "',Party='" + Party.SelectedValue + "',PayDt='" + Paydt.Value.ToString("yyyy-MM-dd") + "',Amount='" + Amt.Text + "',Remark='" + Remark.Text + "',RegDt='" + dt.Value.ToString("yyyy-MM-dd") + "' where id='" + textBox1.Text + "'";
                    int a = c1.exec(qry);
                    if (a == 1)
                    {
                        MessageBox.Show("Payment Updated Successfull", "Success");
                        dispdata();
                    }
                    else
                    {
                        MessageBox.Show("Payment is not Updated !!!", "Error !!!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select proper detail !!!","Error !!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    qry = "Delete from Payment where id='" + textBox1.Text + "'";
                    int a = c1.exec(qry);
                    if (a == 1)
                    {
                        MessageBox.Show("Payment Deleted Successfully", "Success");
                        dispdata();
                    }
                    else
                    {
                        MessageBox.Show("Payment is not Deleted !!!", "Error !!!");
                    }
                }
            }else
            {
                MessageBox.Show("Please select proper detail !!!", "Error !!!");
            }
        }

        private void F_Payment_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
