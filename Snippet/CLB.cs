using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Snippet
{
    public class CLB
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SnippetString"].ConnectionString);

        public void conopen()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void conclose()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        //  txtAchName.Text = c1.getSingleString("name", "investApplication", "AccNo", cbaccno.Text);
        //     int a = c1.CountRow("investApplication", "AccNo", cbaccno.ToString());

        public string getSingleString(string res, string tbl, string concol, string conval)
        {
            string b = "";
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select " + res + " from " + tbl + " where " + concol + " = '" + conval + "' ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();
                b = Convert.ToString(g);
                conclose();
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;
        }
        public string getSingleString2(string res, string tbl, string comcol, string comval, string comcol2, string comval2)
        {
            string b = "";
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select " + res + " from " + tbl + " where " + comcol + " = '" + comval + "' and " + comcol2 + " = '" + comval2 + "' ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();
                b = Convert.ToString(g);
                conclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;


        }

        public void enter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.Escape)
            {
                //  SendKeys.Send(keys.Shift + Key.Tab);
                SendKeys.Send("+{TAB}");
            }
        }

        public int CountRow(string tbl, string concol, string conval)
        {
            int b = 0;
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select count(*) from " + tbl + " where " + concol + " = '" + conval + "' ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();
                b = Convert.ToInt32(g);
                conclose();
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;


        }

        public int CountRow2(string tbl, string comcol, string comval, string comcol2, string comval2)
        {
            int b = 0;
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select count(*) from " + tbl + " where " + comcol + " = '" + comval + "' and " + comcol2 + "='" + comval2 + "' ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();
                b = Convert.ToInt32(g);
                conclose();
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;


        }

        public void OnlyDec(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public int MaxRow(string concol1, string tbl, string concol2, string conval)
        {
            int b = 0;
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select max(" + concol1 + ") from " + tbl + " where " + concol2 + " = '" + conval + "' ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();

                if (g.ToString() == "")
                {
                    g = 0;
                }
                b = Convert.ToInt32(g);
                conclose();
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;


        }

        public int MaxRowNoWhere(string concol1, string tbl)
        {
            int b = 0;
            try
            {
                conopen();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select max(" + concol1 + ") from " + tbl + " ";

                cmd.ExecuteNonQuery();
                var g = cmd.ExecuteScalar();

                if (g.ToString() == "")
                {
                    g = 0;
                }
                b = Convert.ToInt32(g);
                conclose();
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return b;


        }

        //public DataTable getDataTable(int NoOfVal,string[] ak,string tbl )
        //{
        //    string s = null;
        //    int count = ak.Length;

        //    for (int i =0; i<count;i++)
        //    {
        //        string ss += ak[i];

        //    }

        //        conopen();
        //    SqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select "+s + " "

        //    cmd.ExecuteNonQuery();
        //    var g = cmd.ExecuteScalar();
        //    int b = Convert.ToInt32(g);
        //    conclose();
        //    return b;
        //}



        public void closeForm()
        {
            try
            {
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "F_Dash" && Application.OpenForms[i].Name != "UserLogin" && Application.OpenForms[i].Name != "Login" && Application.OpenForms[i].Name != "Corebitform")
                    {

                        Application.OpenForms[i].Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public int exec(string qry)
        {
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                conopen();
                i = cmd.ExecuteNonQuery();
                conclose();
                return i;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            return i;
        }

        public string execScalar(string qry)
        {
            string s = "";
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                conopen();
                var i = cmd.ExecuteScalar();
                s = Convert.ToString(i.ToString());
                conclose();
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return s;
        }

        public void Paint(PaintEventArgs e, int x, int y)
        {
            // Change 300 to this.Width,this.Height;
            Rectangle BaseRectangle = new Rectangle(0, 0, x, y);
            Brush Gradient_Brush = new LinearGradientBrush(BaseRectangle, Color.White, Color.Cyan, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle);
        }
        public void DateCustomFormat(DateTimePicker dtp)
        {
            DateTime dt = Convert.ToDateTime(dtp.Value);
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;
            dtp.Text = (year.ToString() + "-" + month.ToString() + "-" + day.ToString());
        }

        public DataTable getData(DataGridView d, string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
                d.DataSource = dt;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return dt;
        }


        /*
         * 
         * 
     [CId] INT NULL, 
    [UId] INT NULL, 
    [AId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_PartyMaster_Column] UNIQUE NONCLUSTERED ([PartyName] ASC), 
    CONSTRAINT [FK_PartyMaster_ToCompany] FOREIGN KEY ([CId]) REFERENCES [CompanyMaster]([ComId]), 
    CONSTRAINT [FK_PartyMaster_ToRegisterNewUser] FOREIGN KEY ([UId]) REFERENCES [RegisterNewUser]([UserId]), 
    CONSTRAINT [FK_PartyMaster_ToRegisterNewAccount] FOREIGN KEY ([AId]) REFERENCES [RegisterNewAccount]([AccId])
         * */

    }
}
