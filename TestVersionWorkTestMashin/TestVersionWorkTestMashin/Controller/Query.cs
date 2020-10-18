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
        public void AddThing(string name, string tipmaterial, string metClean, string nTicket, string k_t ="0") {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO Thing(name, material, mclean, ticet, key_table) VALUES (@name, @tipmaterial, @metClean, @nTicket, @k_t)",connection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("tipmaterial", tipmaterial);
            command.Parameters.AddWithValue("metClean", metClean);
            command.Parameters.AddWithValue("nTicket", nTicket);
            command.Parameters.AddWithValue("k_t", k_t);
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
        public void DeleteThing(int id)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM Thing WHERE ID ={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteThingByUseID(int id)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM Thing WHERE key_table ={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateKeyTableByID(int Id) {
            connection.Open();
            command = new OleDbCommand($"UPDATE Key_table set used = true WHERE ID ={Id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateThing(string name, string material, string mcclean, string tict, string id) {
            connection.Open();
            command = new OleDbCommand($"UPDATE Thing set name ='{name}', material ='{material}', mclean ='{mcclean}', ticet ='{tict}' WHERE ID={id} ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void CreateDateT(string timeT) {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO DateT(TimeT) VALUES (@timeT)", connection);
            command.Parameters.AddWithValue("timeT", timeT);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteDateT(string ID) {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM DateT WHERE Id ={ID}", connection);
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
