using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestVersionWorkTestMashin.Controller;

namespace TestVersionWorkTestMashin
{
    public partial class Form2 : Form
    {
        Query QrKey = new Query(ConnectionString.connstr);
        Query QrUpd = new Query(ConnectionString.connstr);
                
        public Form2()
        {
            InitializeComponent();
            dataGridView1.DataSource = QrKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE used = false");
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            QrUpd.AddThing(name.Text, tmaterial.Text, mclener.Text, number_ticet.Text, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString());
            QrUpd.UpdateKeyTableByID(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString()));
            dataGridView1.DataSource = QrKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE used = false");
            name.Text = "";
            tmaterial.Text = "";
            mclener.Text = "";
            number_ticet.Text = "";
        }
    }
}
