using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TourfirmApplication.Support
{
    internal class DataBaseConnetion
    {
        SqlConnection con = new SqlConnection(@"Data Sourse=YOGGSARONSPC;Initial Catalog=TourfirmApplication; Integrated Security=True;");

        public void OpenConnection()
        { 
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
