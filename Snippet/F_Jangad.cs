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
    public partial class F_Jangad : Form
    {
        public F_Jangad()
        {
            InitializeComponent();
        }
        CLB c1 = new CLB();
        string qry1 = "";

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

        public void fillparty1()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select ProcessName,Process_Id from Process", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Process.DataSource = dt;
                Process.DisplayMember = "ProcessName";
                Process.ValueMember = "Process_Id";
                c1.conclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        F_Dash fd = new F_Dash();

        private void F_Jangad_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;
            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            fillparty();
            fillparty1();
        }
        
        public void maintain()
        {
            qry1 = "update JC set byy = '"+ By.SelectedValue +"',Party='"+ Party.SelectedValue +"',dt='"+ dt.Value.ToString("yyyy-MM-dd") +"' where jNo='"+ JNo.Text +"'";
            c1.exec(qry1);
        }

        public void fillDgv()
        {
            string qry = "SELECT JC.id,Process.ProcessName, JC.Pcs, JC.wgt,JC.Rate FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where JC.JNo = '"+JNo .Text +"' ORDER BY Id DESC";

            c1.getData(dataGridView1,qry);
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[3].Width = 170;
            dataGridView1.Columns[4].Width = 0;
           
            decimal Totpcs = 0, Totwgt = 0, nang1 = 0, Tot = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                Totpcs += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value);
                Totwgt += Convert.ToDecimal(dataGridView1.Rows[i].Cells["wgt"].Value);
               // Tot += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value) * Convert.ToDecimal(dataGridView1.Rows[i].Cells["Rate"].Value);

            }

            txttotpcs.Text = Totpcs.ToString();
            txttotwgt.Text = Totwgt.ToString();
            nang.Text = dataGridView1.Rows.Count.ToString();
        }

        private void JNo_Leave(object sender, EventArgs e)
        {
            if (JNo.Text != "")
            {
                try
                {
                    string a = c1.execScalar("select distinct Party from JC where JNo='" + JNo.Text + "'");
                    //   MessageBox.Show(a);
                    Party.SelectedValue = a;
                    string b = c1.execScalar("select distinct byy from JC where JNo='" + JNo.Text + "'");
                    By.SelectedValue = b;
                    string ab = c1.execScalar("select distinct Dt from JC where JNo='" + JNo.Text + "'");
                    dt.Value = Convert.ToDateTime(ab.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            
            fillDgv();
        }

        public void GenerateNewJaNo()
        {
            var ab = dataGridView1.Rows.Count.ToString();
            int nums = int.Parse(ab);
            //       MessageBox.Show(nums.ToString());  
            if (nums == 0)
            {
                qry1 = "select max(JNo) from JC";

                var x = c1.execScalar(qry1);
                if (x.ToString() == "")
                {
                    JNo .Text = "1";
                }
                else
                {
                    int bn = Convert.ToInt32(x);
                    bn++;
                    JNo.Text = Convert.ToString(bn);
                }
            }
        }

        public void closeApp()
        {
            int a = Convert.ToInt32(dt.Value.Year);
            int b = Convert.ToInt32(dt.Value.Month);
            if (a > 2019 || (b >= 6 && a == 2019))
            {
                // F_Dash fd = new F_Dash();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closeApp();


            var A = c1.execScalar("select rate from rate where party_Id = '" + Party.SelectedValue + "' and Process_Id = '" + Process.SelectedValue + "'");
            Rate.Text = Convert.ToString(A);
            if (Rate .Text != "" && By .Text != "" && Party .Text != "" && Process.Text != "" && Pcs.Text != "" && Wgt .Text != "")
            {
                GenerateNewJaNo();
                qry1 = "insert into Jc (JNo,Byy,Party,Dt,Process,pcs,Wgt,Rate,Edt) values('"+ JNo.Text +"','"+ By.SelectedValue +"','"+ Party .SelectedValue + "','"+ dt.Value.ToString("yyyy-MM-dd") + "','"+ Process .SelectedValue + "','"+ Pcs .Text +"','"+ Wgt .Text +"','"+ Rate .Text +"','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                c1.exec(qry1);
                maintain();
                fillDgv();
                MessageBox.Show("Lot Added In Jangad");
                Pcs.Clear();
                Wgt.Clear();
                Process.ResetText();
                Process.Focus();
            } 
            else
            {
                MessageBox.Show("Please Fill Required Details");
            }
        }

        private void Process_Leave(object sender, EventArgs e)
        {
            var  A = c1.execScalar("select rate from rate where party_Id = '"+ Party.SelectedValue +"' and Process_Id = '"+ Process.SelectedValue +"'");
            Rate.Text = Convert.ToString(A);
        }

        private void JNo_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            closeApp();
            try
            {
                if (textBox1.Text != "")
                {
                    var A = c1.execScalar("select rate from rate where party_Id = '" + Party.SelectedValue + "' and Process_Id = '" + Process.SelectedValue + "'");
                    Rate.Text = Convert.ToString(A);

                    if (MessageBox.Show("Do You Really Want to Update This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        qry1 = "update JC set Pcs='" + Pcs.Text + "',wgt='" + Wgt.Text + "',Rate='" + Rate.Text + "',Process='" + Process.SelectedValue + "' where id='" + textBox1.Text + "'";
                        int a = c1.exec(qry1);

                        if (a == 1)
                        {
                            MessageBox.Show("Data updated Sucessfull");
                            maintain();
                            fillDgv();
                            cleardata();
                        }
                        else
                        {
                            MessageBox.Show("You entered wrong value !!!");
                        }
                    }
                }else
                {
                    MessageBox.Show("Please select proper detail !!!");
                    By.Focus();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void cleardata()
        {
            textBox1.Clear();
            Rate.Clear();
            Wgt.Clear();
            Pcs.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        qry1 = "Delete from JC where id='" + textBox1.Text + "'";
                        int a = c1.exec(qry1);

                        if (a == 1)
                        {
                            MessageBox.Show("Data Delete Sucessfull");
                            
                            fillDgv();
                            cleardata();
                        }
                        else
                        {
                            MessageBox.Show("You entered wrong value !!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select proper detail !!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    Process.Text = row.Cells[1].Value.ToString();
                    Pcs.Text = row.Cells[2].Value.ToString();
                    Wgt.Text = row.Cells[3].Value.ToString();
                    Rate.Text = row.Cells[4].Value.ToString();
                    By.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void JNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void Pcs_Enter(object sender, EventArgs e)
        {
            var A = c1.execScalar("select rate from rate where party_Id = '" + Party.SelectedValue + "' and Process_Id = '" + Process.SelectedValue + "'");
            Rate.Text = Convert.ToString(A);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (JNo.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string a = c1.execScalar("select count(*) from JC where JNo = '" + JNo.Text + "'");
                    if (a.ToString() != "0")
                    {
                        qry1 = "delete from JC where JNo = '" + JNo.Text + "'";
                        c1.exec(qry1);
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid jangad number !!!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter valid jangad number !!!");
            }

        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
