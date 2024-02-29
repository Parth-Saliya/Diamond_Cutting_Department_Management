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
    public partial class F_Rate : Form
    {
        public F_Rate()
        {
            InitializeComponent();
        }

        CLB c1 = new CLB();
        string qry = "";
        string msg = "";
        F_Dash  fd = new F_Dash();
        string gqry = "SELECT Rate.Id as Id, Party.PartyName as Party, Process.ProcessName as Process, Rate.Rate, Rate.Todt as Date FROM Party INNER JOIN  Rate ON Party.Party_Id = Rate.Party_Id INNER JOIN Process ON Rate.Process_Id = Process.Process_Id ORDER BY ID DESC";

        public void fillparty()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select PartyName,Party_Id from Party where Dep='Party'", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                PartyName.DataSource = dt;
                PartyName.DisplayMember = "PartyName";
                PartyName.ValueMember = "Party_Id";
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

                ProcessName .DataSource = dt;
                ProcessName.DisplayMember = "ProcessName";
                ProcessName.ValueMember = "Process_Id";
                c1.conclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        F_Dash fd1 = new F_Dash();

        private void F_Rate_Load(object sender, EventArgs e)
        {
            this.Location = fd1.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            fillparty();
            fillparty1();
            c1.getData(dgv, gqry);
            dgv.Columns[0].Width = 100;
            dgv.Columns[1].Width = 210;
            dgv.Columns[2].Width = 155;
            dgv.Columns[3].Width = 0;
            dgv.Columns[4].Width = 155;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PartyName .Text != "" && ProcessName .Text != "" && Rate .Text != "")
            {
                //  c1.DateCustomFormat(PJoinDt);
                try
                {
                    qry = "INSERT INTO  Rate  (Party_Id ,Process_Id,Rate,RegUser,Todt) VALUES('" + PartyName.SelectedValue + "','" + ProcessName.SelectedValue + "','" + Rate.Text + "','" + fd.Usr.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  ) ";
                    var sqry = c1.execScalar("Select count(*) from Rate where Party_Id = '" + PartyName.SelectedValue + "' and Process_Id = '" + ProcessName.SelectedValue + "'");

                    if (sqry.ToString() == "0")
                    {
                        int i = c1.exec(qry);
                        //  MessageBox.Show("ok");
                        msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                        //      c1.getData(dgv, "select Party_Id as ID , ProcessName as Process from Process ");
                        // getdata();

                        c1.getData(dgv, gqry);
                        dgv.Columns[3].Width = 0;
                        PartyName.ResetText();
                        ProcessName.ResetText();
                        Rate.ResetText();
                        PartyName.Focus();
                        MessageBox.Show(msg);
                    }
                    else
                    {

                        MessageBox.Show("You Already Enter This Process Rate For This Party !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please Fill Required Details !!");
                PartyName.Focus();
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                    RId .Text = row.Cells[0].Value.ToString();
                    PartyName.Text = row.Cells[1].Value.ToString();
                    ProcessName.Text = row.Cells[2].Value.ToString();
                    Rate.Text = row.Cells[3].Value.ToString();
                    PJoinDt.Text = row.Cells[4].Value.ToString();
                    PartyName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             if (RId.Text != "" && PartyName.Text != "" && ProcessName .Text != "" && Rate.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Update This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //  c1.DateCustomFormat(PJoinDt);

                    try
                    {
                        qry = "UPDATE Rate SET Party_Id ='" + PartyName.SelectedValue + "', Process_Id ='" + ProcessName.SelectedValue + "', Rate ='" + Rate.Text + "' , Todt = '" + PJoinDt.Value.ToString("yyyy-MM-dd") + "' WHERE (Id ='" + RId.Text + "')";
                        var sqry = c1.execScalar("Select count(*) from Rate where id != '"+ RId.Text +"' and Party_Id = '" + PartyName.SelectedValue + "' and Process_Id = '" + ProcessName.SelectedValue + "'");

                        if (sqry.ToString() == "0")
                        {
                            int i = c1.exec(qry);
                            msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                            c1.getData(dgv, gqry);
                            PartyName.ResetText();
                            ProcessName.ResetText();
                            Rate.ResetText();
                            RId.ResetText();
                            PartyName.Focus();
                            MessageBox.Show(msg);
                        }
                        else
                        {
                            MessageBox.Show("You Already Enter This Process Rate For This Party !!!");
                            PartyName.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill Required Details !!");
                PartyName.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RId.Text != "" && PartyName.Text != "" && ProcessName.Text != "" && Rate.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //  c1.DateCustomFormat(PJoinDt);

                    try
                    {
                        qry = "DELETE from Rate WHERE (Id ='" + RId.Text + "')";
                        int i = c1.exec(qry);
                        msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                        c1.getData(dgv, gqry);
                        PartyName.ResetText();
                        ProcessName.ResetText();
                        Rate.ResetText();
                        RId.ResetText();
                        PartyName.Focus();
                        MessageBox.Show(msg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill Required Details !!");
                PartyName.Focus();
            }
        }

        private void RId_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
