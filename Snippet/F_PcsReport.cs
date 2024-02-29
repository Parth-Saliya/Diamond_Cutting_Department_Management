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
    public partial class F_PcsReport : Form
    {
        public F_PcsReport()
        {
            InitializeComponent();
        }

        public void fillparty()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'By' and PartyName !='ALL'", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                By.DataSource = dt;
                By.DisplayMember = "PartyName";
                By.ValueMember = "Party_Id";

                SqlCommand cmd1 = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'Party' ", c1.con);
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
        string qry = "";

        private void button1_Click(object sender, EventArgs e)
        {
            qry = "SELECT JC.Dt as Date, Process.ProcessName, JC.Pcs, JC.wgt as Weight, JC.Return1 as ReturnStatus FROM  JC INNER JOIN  Process ON JC.Process = Process.Process_Id where Byy = '"+ By.SelectedValue +"' and Party = '"+ Party.SelectedValue +"'";
            c1.getData(dataGridView1,qry);
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 120;
            decimal TotPcs = 0, TotWgt = 0, payable = 0, TotAmt = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                TotPcs += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value);
                TotWgt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Weight"].Value);
              //  TotAmt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Amount"].Value);

            }

            TotWgt = Math.Round(TotWgt, 2);
           // TotPcs = Math.Round(TotPcs, 0);
           // TotAmt = Math.Round(TotAmt, 0);

            txttotpcs.Text = TotPcs.ToString();
            txttotwgt.Text = TotWgt.ToString();
            nang.Text = dataGridView1.Rows.Count.ToString();
            //   txttotamt.Text = TotAmt.ToString();
        }

        F_Dash fd = new F_Dash();

        private void F_PcsReport_Load(object sender, EventArgs e)
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
            Font ft = new Font("Courier New", 12);
            float fth = ft.GetHeight();
            int sx = 10;
            int sy = 10;
            int offset = 40;
            StringFormat sft = new StringFormat();
            sft.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            gs.DrawString("Omkar Sarin", new Font("Courier New bold", 20), new SolidBrush(Color.Black), sx + 350, sy);
            gs.DrawString("From:" + dt1.Value.ToString("dd-MM-yyyy") + "   To:" + dt2.Value.ToString("dd-MM-yyyy") + "       By:" + By.Text.ToString() + "   Party:" + Party.Text.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 60);
            gs.DrawString("Date                Process            Pcs           Weight      Return Status", new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 100);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DateTime a = Convert.ToDateTime(dataGridView1.Rows[i].Cells[0].Value.ToString());
                gs.DrawString(a.ToString("dd-MM-yyyy"), new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 100 + offset);
                gs.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 210, sy + 100 + offset);
                gs.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 430, sy + 100 + offset, sft);
                gs.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 600, sy + 100 + offset, sft);
                gs.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Courier New", 12), new SolidBrush(Color.Black), sx + 760, sy + 100 + offset, sft);
                offset = offset + (int)fth + 5;
            }
            gs.DrawString("________________________________________________________________________________", new Font("Courier New", 12), new SolidBrush(Color.Black), sx, sy + 80 + offset);
            gs.DrawString(" Total:-    "+nang.Text+"                         " + txttotpcs.Text + "            " + txttotwgt.Text + " " , new Font("Courier New", 12), new SolidBrush(Color.Black), 5, sy + 100 + offset);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
