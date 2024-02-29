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
    public partial class F_Process : Form
    {
        public F_Process()
        {
            InitializeComponent();
        }
        string qry = "";
        string msg = "";
        CLB c1 = new CLB();
        F_Dash fd = new F_Dash();
        private void button1_Click(object sender, EventArgs e)
        {
            if (PName .Text != "")
            {
                //  c1.DateCustomFormat(PJoinDt);
                try
                {
                    qry = "INSERT INTO  Process  (ProcessName , RegUser,Todt) VALUES('" + PName .Text + "','" + fd.Usr.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  ) ";
                    int i = c1.exec(qry);
                    msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                    c1.getData(dgv, "select Process_Id as ID , ProcessName as Process from Process ");
                   // getdata();
                    MessageBox.Show(msg);
                    PName.Clear();
                    PName.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please Fill Required Details !!");
                PName.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (PId.Text != "" && PName .Text != "")
            {
                //  c1.DateCustomFormat(PJoinDt);
                if (MessageBox.Show("Do You Really Want to Update This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        qry = "UPDATE Process SET ProcessName ='" + PName.Text + "' WHERE (Process_Id ='" + PId.Text + "')";
                        int i = c1.exec(qry);
                        msg = i != 0 ? "Successfull" : "There is a wrong value you enter !!!";
                        c1.getData(dgv, "select Process_Id as ID , ProcessName as Process from Process ");
                        PId.Clear();
                        PName.ResetText();
                       
                        PName.Focus();
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
                PName.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PName .Text != "" && PId .Text != "")
            {
                if (MessageBox.Show("Do You Really Want to Delete This ??", "YES OR NO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        qry = "delete from process where Process_id = '" + PId.Text + "'";
                        int i = c1.exec(qry);
                        msg = i != 0 ? "Successfull" : "Error !!!";
                        c1.getData(dgv, "select Process_Id as ID , ProcessName as Process from Process ");
                        PId.Clear();
                        PName.ResetText();
                        PName.Focus();
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
                PName.Focus();
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgv.Rows[e.RowIndex];
                    PId.Text = row.Cells[0].Value.ToString();
                    PName .Text = row.Cells[1].Value.ToString();
                    PName.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        F_Dash fd1 = new F_Dash();

        private void F_Process_Load(object sender, EventArgs e)
        {
            this.Location = fd1.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            c1.getData(dgv, "select Process_Id as ID , ProcessName as Process from Process ORDER BY ID DESC");
            dgv.Columns[0].Width = 300;
            dgv.Columns[1].Width = 480;
        }

        private void F_Process_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
