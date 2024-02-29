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
    public partial class F_Return : Form
    {
        public F_Return()
        {
            InitializeComponent();
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        CLB c1 = new CLB();

        public void fillDgv()
        {
            if (nre.Checked == true)
            {
                string qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where JC.Return1 is NULL and Status ='0' and Byy='" + comboBox1.SelectedValue + "' and Party='" + comboBox2.SelectedValue + "' ";

                c1.getData(dataGridView1, qry);
                dataGridView1.Columns[0].Width = 0;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 90;
                dataGridView1.Columns[4].Width = 90;
                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value);
                }
                textBox2.Text = Totpcs.ToString();
                nang1.Text = dataGridView1.Rows.Count.ToString();
            }
            if (Re.Checked == true)
            {
                string qry1 = "Update jc set status='1' where JC.Return1 ='Yes' and ReturnDt='"+ dt.Value.ToString("yyyy-MM-dd") +"'";
                c1.exec(qry1);
                string qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where JC.Return1 ='Yes' and ReturnDt='"+ dt.Value.ToString("yyyy-MM-dd") +"' and Byy='" + comboBox1.SelectedValue + "' and Party='" + comboBox2.SelectedValue + "' ";

                c1.getData(dataGridView2, qry);
                dataGridView2.Columns[0].Width = 0;
                dataGridView2.Columns[1].Width = 100;
                dataGridView2.Columns[2].Width = 100;
                dataGridView2.Columns[3].Width = 90;
                dataGridView2.Columns[4].Width = 90;
                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView2.Rows[i].Cells["Pcs"].Value);
                }
                textBox1.Text = Totpcs.ToString();
                nang.Text = dataGridView2.Rows.Count.ToString();
            }

        }

        public void fillparty()
        {
            try
            {
                c1.conopen();
                SqlCommand cmd = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'By' and Party_Id != '1' ", c1.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "PartyName";
                comboBox1.ValueMember = "Party_Id";

                SqlCommand cmd1 = new SqlCommand("select PartyName,Party_Id from Party where Dep = 'Party'", c1.con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                comboBox2.DataSource = dt1;
                comboBox2.DisplayMember = "PartyName";
                comboBox2.ValueMember = "Party_Id";

                c1.conclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        F_Dash fd = new F_Dash();

        private void F_Return_Load(object sender, EventArgs e)
        {
            this.Location = fd.panel1.Location;

            Animation.AnimateWindow(this.Handle, 500, Animation.StartFromLeftBottom);
            qry = "update JC set Status='0'";
            c1.exec(qry);
            fillparty();
        }

        

        string abc = "";
        string qry = "";

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (nre.Checked == true)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    abc = row.Cells[0].Value.ToString();
                    qry = "update JC set Status='1' where id='" + abc + "' ";
                    c1.exec(qry);
                }

                fillDgv();

                qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where Status = '1' and JC.Return1 is NULL";
                c1.getData(dataGridView2, qry);
                dataGridView2.Columns[0].Width = 0;
                dataGridView2.Columns[1].Width = 100;
                dataGridView2.Columns[2].Width = 100;
                dataGridView2.Columns[3].Width = 90;
                dataGridView2.Columns[4].Width = 90;

                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView2.Rows[i].Cells["Pcs"].Value);
                }
                textBox1.Text = Totpcs.ToString();
                nang.Text = dataGridView2.Rows.Count.ToString();
            }
            if(Re.Checked == true)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    abc = row.Cells[0].Value.ToString();
                    qry = "update JC set Status='1' where id='" + abc + "' ";
                    c1.exec(qry);
                }

                fillDgv11();

                qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where Status = '1' and JC.Return1 ='Yes'";
                c1.getData(dataGridView2, qry);
                dataGridView2.Columns[0].Width = 0;
                dataGridView2.Columns[1].Width = 100;
                dataGridView2.Columns[2].Width = 100;
                dataGridView2.Columns[3].Width = 90;
                dataGridView2.Columns[4].Width = 90;

                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView2.Rows[i].Cells["Pcs"].Value);
                }
                textBox1.Text = Totpcs.ToString();
                nang.Text = dataGridView2.Rows.Count.ToString();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (nre.Checked == true)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                    abc = row.Cells[0].Value.ToString();
                    qry = "update JC set Status='0' where id='" + abc + "' ";
                    c1.exec(qry);
                }

                fillDgv();

                qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where Status = '1' and JC.Return1 is NULL";
                c1.getData(dataGridView2, qry);
                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView2.Rows[i].Cells["Pcs"].Value);
                }
                textBox1.Text = Totpcs.ToString();
                nang.Text = dataGridView2.Rows.Count.ToString();
            }
            if(Re.Checked == true)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                    abc = row.Cells[0].Value.ToString();
                    qry = "update JC set Status='0' where id='" + abc + "' ";
                    c1.exec(qry);
                }

                fillDgv11();

                qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where JC.Return1 ='Yes' and Status = '1'";
                c1.getData(dataGridView2, qry);
                decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    Totpcs += Convert.ToDecimal(dataGridView2.Rows[i].Cells["Pcs"].Value);
                }
                textBox1.Text = Totpcs.ToString();
                nang.Text = dataGridView2.Rows.Count.ToString();
            }
        }
        public void fillDgv11()
        {
            string qry = "SELECT JC.id,JC.Dt,Process.ProcessName as Process, JC.Pcs, JC.wgt FROM JC INNER JOIN  Process ON JC.Process = Process.Process_Id where JC.Return1 ='Yes' and Status ='0' and Byy='" + comboBox1.SelectedValue + "' and Party='" + comboBox2.SelectedValue + "' ";

            c1.getData(dataGridView1, qry);
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            decimal Totpcs = 0, Totwgt = 0, payable = 0, Tot = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                Totpcs += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Pcs"].Value);
            }
            textBox2.Text = Totpcs.ToString();
            nang1.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fillDgv();
        }


        int JNo;

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateNewJaNo();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
               
                int ab = Convert.ToInt32(dataGridView2.Rows[i].Cells["id"].Value);
               // MessageBox.Show(ab.ToString());
                qry = "update JC set Return1='Yes',ReturnDt='" + dt.Value.ToString("yyyy-MM-dd") +"',ReJaNo='"+ JNo +"' where id='" + ab + "' ";
                c1.exec(qry);
            }
            MessageBox.Show("Return Sucessfully");
            dataGridView2.DataSource = null;
            
            textBox1.Clear();
            nang.Clear();
        }

        public void GenerateNewJaNo()
        {
            var ab = dataGridView2.Rows.Count.ToString();
            int nums = int.Parse(ab);
            //       MessageBox.Show(nums.ToString());  
            if (nums != 0)
            {
                qry = "select max(ReJaNo) from JC";

                var x = c1.execScalar(qry);
                if (x.ToString() == "")
                {
                    JNo = 1;
                }
                else
                {
                    int bn = Convert.ToInt32(x);
                    bn++;
                    JNo = bn;
                }

            }
        }

        private void F_Return_Leave(object sender, EventArgs e)
        {
            
        }

        private void dt_KeyDown(object sender, KeyEventArgs e)
        {
            c1.enter(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Re_Click(object sender, EventArgs e)
        {
            if(Re.Checked == true)
            {
                button3.Visible = true;
                button1.Visible = false;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                textBox2.Clear();
                textBox1.Clear();
                nang.Clear();
                nang1.Clear();
            }
            if(nre.Checked == true)
            {
                button3.Visible = false;
                button1.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                textBox2.Clear();
                textBox1.Clear();
                nang.Clear();
                nang1.Clear();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                int ab = Convert.ToInt32(dataGridView1.Rows[i].Cells["id"].Value);
                // MessageBox.Show(ab.ToString());
                qry = "update JC set Return1 = NULL,ReturnDt = NULL,ReJaNo = NULL where id='" + ab + "' ";
                c1.exec(qry);
            }
            MessageBox.Show("Return Back Sucessfully");
            dataGridView1.DataSource = null;
            textBox2.Clear();
            nang1.Clear();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
