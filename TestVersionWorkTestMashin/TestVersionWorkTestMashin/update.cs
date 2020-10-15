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
    
    public partial class update : Form
    {
        public string title;
        public string nametable;
        public string clean;
        public string material;
        public string mclean;
        public string key_table;
        Query query;

        public update()
        {
            InitializeComponent();
            query = new Query(ConnectionString.connstr);
        }

        private void update_Load(object sender, EventArgs e)
        {
            this.Text ="Изменить "+ title;
            this.name.Text = nametable;
            this.mclener.Text = clean;
            this.tmaterial.Text = material;
            this.number_ticet.Text = mclean;
            dataGridView1.DataSource = query.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE id =" + this.key_table);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
