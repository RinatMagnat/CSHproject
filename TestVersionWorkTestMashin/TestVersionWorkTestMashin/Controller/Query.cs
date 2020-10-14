using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace TestVersionWorkTestMashin.Controller
{
    class Query
    {
        OleDbConnection  connection;
        OleDbCommand     command;
        OleDbDataAdapter dataAdapter;
        DataTable        bufferTable;
    public Query(string conn)
        {
            connection = new OleDbConnection(conn);
            bufferTable = new DataTable();
        }
        public DataTable UpdateTable() {
            connection.Open();
            dataAdapter = new OleDbDataAdapter("Select id as ID, key1 as key_first,key2 as key_second,key3 as key_three From Key_table", connection);
            bufferTable.Clear();
            dataAdapter.Fill(bufferTable);
            connection.Close();
            return bufferTable;
        }

        public DataTable EditUpdateTable(string str)
        {
            connection.Open();
            dataAdapter = new OleDbDataAdapter(str, connection);
            bufferTable.Clear();
            dataAdapter.Fill(bufferTable);
            connection.Close();
            return bufferTable;
        }

        public int getQuery()
        {
            connection.Open();
            command = new OleDbCommand("SELECT COUNT(*) FROM Key_table",connection);
            int number = (int)command.ExecuteScalar();
            connection.Close();
            return number;
        }

        public void Add(string key1,string key2,string key3)
        {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO Key_table(Key1,Key2,Key3) VALUES (@key1, @key2, @key3)", connection);
            command.Parameters.AddWithValue("key1",key1);
            command.Parameters.AddWithValue("key2", key2);
            command.Parameters.AddWithValue("key3", key3);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(int id)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM Key_table WHERE ID ={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
