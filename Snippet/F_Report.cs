using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public partial class F_Report : Form
    {
        public F_Report()
        {
            InitializeComponent();
        }

        CLB c1 = new CLB();

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

                SqlCommand cmd1 = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'Party' or Party_Id='1'", c1.con);
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

        string qry = "";

        private void button1_Click(object sender, EventArgs e)
        {

            if (By.Text.ToString() != "ALL" && Party.Text.ToString() != "ALL")
            {
                qry = "SELECT Party.PartyName,Process.ProcessName,sum(JC.Pcs) as Pcs,sum(JC.wgt) as Weight,JC.Rate,CONVERT(int,Round(sum(JC.Pcs * JC.Rate),0)) as Amount FROM  JC INNER JOIN Process ON JC.Process = Process.Process_Id INNER JOIN Party ON JC.Party = Party.Party_Id where Dt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Byy='" + By.SelectedValue + "' and Party='" + Party.SelectedValue + "' and JC.Return1='Yes' group by Party.PartyName,JC.Byy,Process.ProcessName,JC.Rate";
            }
            else if(By.Text.ToString() == "ALL" && Party.Text.ToString() == "ALL")
            {
                qry = "SELECT Party.PartyName,Process.ProcessName,sum(JC.Pcs) as Pcs,sum(JC.wgt) as Weight,JC.Rate,CONVERT(int,Round(sum(JC.Pcs * JC.Rate),0)) as Amount FROM  JC INNER JOIN Process ON JC.Process = Process.Process_Id INNER JOIN Party ON JC.Party = Party.Party_Id where Dt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and JC.Return1='Yes' group by Party.PartyName,JC.Byy,Process.ProcessName,JC.Rate";
            }
            else if (By.Text.ToString() == "ALL" && Party.Text.ToString() != "ALL")
            {
                qry = "SELECT Party.PartyName,Process.ProcessName,sum(JC.Pcs) as Pcs,sum(JC.wgt) as Weight,JC.Rate,CONVERT(int,Round(sum(JC.Pcs * JC.Rate),0)) as Amount FROM  JC INNER JOIN Process ON JC.Process = Process.Process_Id INNER JOIN Party ON JC.Party = Party.Party_Id where Dt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Party='" + Party.SelectedValue + "' and JC.Return1='Yes' group by Party.PartyName,JC.Byy,Process.ProcessName,JC.Rate";
            }
            else if (By.Text.ToString() != "ALL" && Party.Text.ToString() == "ALL")
            {
                qry = "SELECT Party.PartyName,Process.ProcessName,sum(JC.Pcs) as Pcs,sum(JC.wgt) as Weight,JC.Rate,CONVERT(int,Round(sum(JC.Pcs * JC.Rate),0)) as Amount FROM  JC INNER JOIN Process ON JC.Process = Process.Process_Id INNER JOIN Party ON JC.Party = Party.Party_Id where Dt between '" + dt1.Value.ToString("yyyy-MM-dd") + "' and '" + dt2.Value.ToString("yyyy-MM-dd") + "' and Byy='" + By.SelectedValue + "' and JC.Return1='Yes' group by Party.PartyName,JC.Byy,Process.ProcessName,JC.Rate";
            }
            c1.getData(dataGridView1,qry);
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 110;
            dataGridView1.Columns[4].Width = 0;
            dataGridView1.Columns[5].Width = 110;
            decimal TotPcs = 0, TotWgt = 0, payable = 0, TotAmt = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                TotPcs += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value);
                TotWgt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Weight"].Value);
                TotAmt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Amount"].Value);

            }

            TotWgt = Math.Round(TotWgt,2);
           // TotPcs = Math.Round(TotPcs, 0);
            TotAmt = Math.Round(TotAmt, 0);

            txttotpcs.Text = TotPcs.ToString();
            txttotwgt.Text = TotWgt.ToString();
            txttotamt.Text = TotAmt.ToString();
        }

        F_Dash fd = new F_Dash();

        private void F_Report_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            fillparty();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdc = new PrintDocument();
            pd.Document = pdc;
            pdc.PrintPage += new PrintPageEventHandler(pdc_PrintPage);
            DialogResult result = pd.ShowDialog();

            if (result == DialogResult.OK)
            {
                pdc.Print();
            }
        }

        private void pdc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics gs = e.Graphics;
            Font ft = new Font("Courier New",12);
            float fth = ft.GetHeight();
            int sx = 10;
            int sy = 10;
            int offset = 40;
            StringFormat sft = new StringFormat();
            sft.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            gs.DrawString("Omkar Sarin", new Font("Courier New bold", 20), new SolidBrush(Color.Black), sx + 350, sy);
            gs.DrawString("From:"+dt1.Value.ToString("dd-MM-yyyy")+"   To:"+dt2.Value.ToString("dd-MM-yyyy") +"       By:"+By.Text.ToString()+ "   Party:" + Party.Text.ToString(), new Font("Courier New", 12),new SolidBrush(Color.Black),sx,sy + 60);
            gs.DrawString("Name                Type       Pcs         Weight         Rate        Amount", new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 100);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int a = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                gs.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 100 + offset);
                gs.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 200, sy + 100 + offset);
                gs.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 350, sy + 100 + offset,sft);
                gs.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 510, sy + 100 + offset,sft);
                gs.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 600, sy + 100 + offset);
                gs.DrawString(a.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 790, sy + 100 + offset,sft);

                offset = offset + (int)fth + 5;
            }
            gs.DrawString("_____________________________________________________________________________", new Font("Courier New", 12), new SolidBrush(Color.Black), sx , sy + 80 + offset);
            gs.DrawString("Total:-                    "+ txttotpcs.Text +"            "+ txttotwgt.Text +"                         "+txttotamt.Text, new Font("Courier New", 12), new SolidBrush(Color.Black), 55, sy + 100 +offset);





        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

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
