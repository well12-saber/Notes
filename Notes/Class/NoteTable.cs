using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Notes
{
    class NoteTable : DataTable
    {
        public NoteTable()
        {
            this.Columns.Add("Title", typeof(String));
            this.Columns.Add("Message", typeof(String));            
        }        

        public static DataGridView Adjust(DataGridView dataTable)
        {
            dataTable.Columns["Message"].Visible = false;//hiding certain columns
                                                         //, don't like it so placed it here
            dataTable.Columns["Title"].Width = 190;      //adjusting width to my app

            return dataTable;
        }

        public void Add(string Title,string Message)
        {
            this.Rows.Add(Title, Message);
        }

        public string getTitle(int idx)
        {
            return this.Rows[idx].ItemArray[0].ToString();
        }

        public string getMessage(int idx)
        {
            return this.Rows[idx].ItemArray[1].ToString();
        }

        public void Delete(int idx)
        {
            this.Rows[idx].Delete();
        }
    }
}
