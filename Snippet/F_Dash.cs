using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public partial class F_Dash : Form
    {
        public F_Dash()
        {
            InitializeComponent();
            panel2.Top = button1.Top;
            panel2.Height = button1.Height;
        }

        CLB c1 = new CLB();

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Top = button1.Top;
            panel2.Height = button1.Height;
            button1.BackColor = Color.Aqua;
            btncolor("button1");
            F_Jangad fj = new F_Jangad();       
            c1.closeForm();
            fj.MdiParent = this;
            fj.Show();
            
        }

        public void btncolor(string s)
        {
            if (s.ToString() != "button1")
            {
                button1.BackColor = Color.Silver;
            }
            if (s.ToString() != "button3")
            {
                button3.BackColor = Color.Silver;
            }
            if (s.ToString() != "button4")
            {
                button4.BackColor = Color.Silver;
            }
            if (s.ToString() != "button5")
            {
                button5.BackColor = Color.Silver;
            }
            if (s.ToString() != "button6")
            {
                button6.BackColor = Color.Silver;
            }
            if (s.ToString() != "button7")
            {
                button7.BackColor = Color.Silver;
            }
            if (s.ToString() != "button2")
            {
                button2.BackColor = Color.Silver;
            }
            if (s.ToString() != "button8")
            {
                button8.BackColor = Color.Silver;
            }
            if (s.ToString() != "button9")
            {
                button9.BackColor = Color.Silver;
            }
            if (s.ToString() != "button12")
            {
                button12.BackColor = Color.Silver;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Top = button4.Top;
            panel2.Height = button4.Height;
            button4.BackColor = Color.Aqua;
            btncolor("button4");
            F_Process fpro = new F_Process();
            c1.closeForm();
            fpro.MdiParent = this;
            fpro.Show();
            fpro.Location = panel1.Location;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Top = button5.Top;
            panel2.Height = button5.Height;
            button5.BackColor = Color.Aqua;
            btncolor("button5");
            F_Return fr = new F_Return();
            c1.closeForm();
            fr.MdiParent = this;
            fr.Show();
            fr.Location = panel1.Location;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Top = button6.Top;
            panel2.Height = button6.Height;
            button6.BackColor = Color.Aqua;
            btncolor("button6");
            F_Rate fra = new F_Rate();
            c1.closeForm();
            fra.MdiParent = this;
            fra.Show();
            fra.Location = panel1.Location;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Top = button3.Top;
            panel2.Height = button3.Height;
            button3.BackColor = Color.Aqua;
            btncolor("button3");
            F_Party fp = new F_Party();
            c1.closeForm();
            fp.MdiParent = this;
            fp.Show();
            fp.Location = panel1.Location;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Top = button7.Top;
            panel2.Height = button7.Height;
            button7.BackColor = Color.Aqua;
            btncolor("button7");
            F_Report fr = new F_Report();
            c1.closeForm();
            fr.MdiParent = this;
            fr.Show();
            fr.Location = panel1.Location;
        }

        public void countpapcs()
        {
            c1.getData(dataGridView1, "SELECT Party.PartyName,sum(JC.Pcs) as Pcs FROM JC INNER JOIN Party ON JC.Party = Party.Party_Id  where JC.Return1 is NULL group by Party.PartyName ");
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 100;
        }

        private void F_Dash_Load(object sender, EventArgs e)
        {
            string dt = sysDt.Value.ToString("dd-MM-yyyy");
            int a = Convert.ToInt32(DateTime.Now.Year.ToString());
            int b = Convert.ToInt32(DateTime.Now.Month.ToString());
            if (a > 2019 || (b >= 6 && a == 2019))
            {
                this.Close();
            }

            Animation.AnimateWindow(this.Handle, 500, Animation.VER_Negative);
            countpapcs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Top = button2.Top;
            panel2.Height = button2.Height;
            button2.BackColor = Color.Aqua;
            btncolor("button2");
            F_Payment fp = new F_Payment();
            c1.closeForm();
            fp.MdiParent = this;
            fp.Show();
            fp.Location = panel1.Location;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Top = button8.Top;
            panel2.Height = button8.Height;
            button8.BackColor = Color.Aqua;
            btncolor("button8");
            F_PayReport fpr = new F_PayReport();
            c1.closeForm();
            fpr.MdiParent = this;
            fpr.Show();
            
            fpr.Location = panel1.Location;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Top = button9.Top;
            panel2.Height = button9.Height;
            button9.BackColor = Color.Aqua;
            btncolor("button9");
            F_SummaryReport fsr = new F_SummaryReport();
            c1.closeForm();
            fsr.MdiParent = this;
            fsr.Show();
            fsr.Location = panel1.Location;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string qry = "BACKUP DATABASE SarinJob To DISK = 'E:BACKUPDATA\\SarinJob-"+DateTime.Now.Ticks.ToString()+".bak'";
            int a = c1.exec(qry);
            MessageBox.Show("Data Backup Take Sucessfully");
            
        
        }

        private void button11_Click(object sender, EventArgs e)
        {
            countpapcs();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Top = button12.Top;
            panel2.Height = button12.Height;
            button12.BackColor = Color.Aqua;
            btncolor("button12");
            F_PcsReport fpr = new F_PcsReport();
            c1.closeForm();
            fpr.MdiParent = this;
            fpr.Show();
            fpr.Location = panel1.Location;
        }
    }
}
