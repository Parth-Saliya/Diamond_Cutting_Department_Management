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
    public partial class F_Party : Form
    {
        public F_Party()
        {
            InitializeComponent();
        }
        string qry="";
        string msg = "";
        CLB c1 = new CLB();
        F_Dash fd = new F_Dash();
       
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

  

        private void F_Party_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            getdata();
        }

        string dep = "";


        private void button1_Click(object sender, EventArgs e)
        {
            if(PartyName.Text != "")
            {
                if (rbby.Checked == true)
                {
                    dep = "By";
                }
                else
                {
                    dep = "Party";
                }
                //  c1.DateCustomFormat(PJoinDt);
                try
                {
                    qry = "INSERT INTO  Party  (PartyName, PartyNumber,PartyAdd , PartyJoinDate, RegUser,todt,Dep) VALUES('" + PartyName.Text + "','" + PContNo.Text + "','"+txtpartyadd .Text+"', '" + PJoinDt.Value.ToString("yyyy-MM-dd") + "','" + fd.Usr.Text + "', '"+ DateTime.Now.ToString("yyyy-MM-dd") +"', '"+ dep +"') ";
                    int i = c1.exec(qry);
                    msg=  i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                    getdata();
                    clear();
                    MessageBox.Show(msg);
                    PartyName.Focus();
                  
                    
                }
                catch(Exception ex)
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
        public void clear()
        {
            PartyName.Clear();
            PId.Clear();
            PJoinDt.ResetText();
            PContNo.ResetText();
            txtpartyadd.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (PartyName.Text != "" && PId.Text != "")
            {

                if (MessageBox.Show("Do You Really Want to Update This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //  c1.DateCustomFormat(PJoinDt);
                    if (rbby.Checked == true)
                    {
                        dep = "By";
                    }
                    else
                    {
                        dep = "Party";
                    }

                    try
                    {
                        qry = "UPDATE Party SET PartyName ='" + PartyName.Text + "', PartyNumber ='" + PContNo.Text + "', PartyAdd ='" + txtpartyadd.Text + "', Dep = '"+ dep +"' WHERE (Party_Id ='" + PId.Text + "')";
                        int i = c1.exec(qry);
                        msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                        getdata();
                        clear();
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
                MessageBox.Show("Please Select Proper Details !!");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            //    PId.Text = row.Cells[1].Value.ToString();
            //ownername.Text = row.Cells[2].Value.ToString();
            //mono1.Text = row.Cells[3].Value.ToString();
            //mono2.Text = row.Cells[4].Value.ToString();
            //  c1.celldouble(4,e,dataGridView1, ""+PId.Name+",1,"+PartyName.Name+",2,"+PContNo.Name+",3,"+txtpartyadd.Name+",4");
        }

        public void getdata()
        {
            c1.conopen();
            SqlCommand cmd = new SqlCommand("SELECT  Party_Id AS Id,   PartyName AS Name, PartyNumber AS [Contact No], PartyAdd AS Address, PartyJoinDate as Date,Dep as Department FROM Party where PartyName != 'ALL' ORDER BY Id DESC", c1.con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgv.DataSource = dt;
            dgv.Columns[0].Width = 53;
            dgv.Columns[1].Width = 170;
            dgv.Columns[2].Width = 160;
            dgv.Columns[3].Width = 140;
            dgv.Columns[4].Width = 120;
            dgv.Columns[5].Width = 120;
            c1.conclose();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (PartyName.Text != "" && PId.Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        qry = "delete from party where Party_id = '" + PId.Text + "'";
                        int i = c1.exec(qry);
                        msg = i != 0 ? "Successfull" : "Error !!!";
                        getdata();
                        clear();
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
                MessageBox.Show("Please Select Proper Details !!");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                    PId.Text = row.Cells[0].Value.ToString();
                    PartyName.Text = row.Cells[1].Value.ToString();
                    PContNo.Text = row.Cells[2].Value.ToString();
                    txtpartyadd.Text = row.Cells[3].Value.ToString();
                   
                }
                PartyName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PId_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void rbby_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void rbby_KeyDown_1(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
