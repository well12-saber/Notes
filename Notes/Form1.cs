using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes
{
   
    public partial class Form : System.Windows.Forms.Form
    {
        Connection con = new Connection();
        NoteTable table;

        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListTitle = new ArrayList();
        private static ArrayList ListMessage = new ArrayList();
        public Form()
            {
                InitializeComponent();
            }

        private void Form_Load(object sender, EventArgs e)
        {
            table = new NoteTable();
            dataTable.DataSource = table;

            dataTable = NoteTable.Adjust(dataTable);

            GetData();
            updateDatagrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textTitle.Clear();
            textMessage.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "insert into note values " +
                String.Format("(default,'{0}','{1}');", textTitle.Text, textMessage.Text);

            MySqlDataReader saver = getQuery(query);

            table.Add(textTitle.Text, textMessage.Text);

            con.Close();
            textTitle.Clear();
            textMessage.Clear();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (dataTable.CurrentCell == null)
                return;
            int idx = dataTable.CurrentCell.RowIndex;

            textTitle.Text = table.getTitle(idx);
            textMessage.Text = table.getMessage(idx);           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataTable.CurrentCell == null)
                return;
            int idx = dataTable.CurrentCell.RowIndex;

            string query = "delete from note where " +
                String.Format("title='{0}' AND message='{1}';", table.getTitle(idx), table.getMessage(idx));

            MySqlDataReader deleter = getQuery(query);

            con.Close();
            table.Delete(idx);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            GetData();            
            updateDatagrid();                       
        }

        private MySqlDataReader getQuery(string query) 
        {
            MySqlDataReader res=null;
            try
            {
                con.Open();                
                res = con.ExecuteReader(query);                                
            }            
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            return res;
        }

        private void GetData()
        {            
            string query = "select id,title,message from note";

            MySqlDataReader row=getQuery(query);

            ListID = new ArrayList();
            ListTitle = new ArrayList();
            ListMessage = new ArrayList();

            if (row != null && row.HasRows)
            {                
                while (row.Read())
                {
                    ListID.Add(row["id"].ToString());
                    ListTitle.Add(row["title"].ToString());
                    ListMessage.Add(row["message"].ToString());
                }
            }            
            con.Close();
        }

        private void updateDatagrid()
        {
            table.Clear();
            for (int i = 0; i < ListID.Count; i++)
            {                
                table.Add(ListTitle[i].ToString(), ListMessage[i].ToString());
            }
        }

    }   
}
