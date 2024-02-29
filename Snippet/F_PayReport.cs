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
    public partial class F_PayReport : Form
    {
        public F_PayReport()
        {
            InitializeComponent();
        }

        public void fillparty()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'By' ", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                By.DataSource = dt;
                By.DisplayMember = "PartyName";
                By.ValueMember = "Party_Id";

                SqlCommand cmd1 = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'Party' or Party_Id = '1'", c1.con);
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

        CLB c1 = new CLB();
        string qry;

        private void button1_Click(object sender, EventArgs e)
        {
            //group by Party.PartyName,Payment.Byy
            //string a = By.SelectedValue.ToString();

            if (By.Text.ToString() != "ALL" && Party.Text.ToString() != "ALL")
            {
                qry = "SELECT Payment.Byy,Party.PartyName,Payment.PayDt as PayDate,CONVERT(int,Round(Payment.Amount,0)) as Amount,Payment.Remark,Payment.RegDt as Date FROM Payment INNER JOIN Party ON Payment.Party = Party.Party_Id where PayDt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Byy='" + By.SelectedValue + "' and Party='" + Party.SelectedValue + "'";
            }
            else if(By.Text.ToString() == "ALL" && Party.Text.ToString() == "ALL")
            {
                qry = "SELECT Payment.Byy,Party.PartyName,Payment.PayDt as PayDate,CONVERT(int,Round(Payment.Amount,0)) as Amount,Payment.Remark,Payment.RegDt as Date FROM Payment INNER JOIN Party ON Payment.Party = Party.Party_Id where PayDt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "'" ;
            }
            else if (By.Text.ToString() == "ALL" && Party.Text.ToString() != "ALL")
            {
                qry = "SELECT Payment.Byy,Party.PartyName,Payment.PayDt as PayDate,CONVERT(int,Round(Payment.Amount,0)) as Amount,Payment.Remark,Payment.RegDt as Date FROM Payment INNER JOIN Party ON Payment.Party = Party.Party_Id where PayDt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Party='" + Party.SelectedValue + "'";
            }
            else if (By.Text.ToString() != "ALL" && Party.Text.ToString() == "ALL")
            {
                qry = "SELECT Payment.Byy,Party.PartyName,Payment.PayDt as PayDate,CONVERT(int,Round(Payment.Amount,0)) as Amount,Payment.Remark,Payment.RegDt as Date FROM Payment INNER JOIN Party ON Payment.Party = Party.Party_Id where PayDt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Byy='" + By.SelectedValue + "'";
            }
            c1.getData(dataGridView1, qry);
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 115;
            dataGridView1.Columns[3].Width = 115;
            dataGridView1.Columns[4].Width = 155;
            dataGridView1.Columns[5].Width = 115;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                int ab = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                //  MessageBox.Show(ab.ToString());
                var name = c1.execScalar("Select Partyname From Party where Party_id='" + ab + "'");
                string sname = Convert.ToString(name);
                dataGridView1.Rows[i].Cells[0].Value = sname;
            }

            decimal TotAmt=0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                TotAmt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Amount"].Value);

            }
            txttotamt.Text = TotAmt.ToString();
        }

        F_Dash fd = new F_Dash();

        private void F_PayReport_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            fillparty();
        }

        private void dt1_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
