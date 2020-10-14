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
    public partial class MainForm : Form
    {
        Query controler;
        public MainForm()
        {
            InitializeComponent();
            controler = new Query(ConnectionString.connstr);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;
            this.ActiveControl = button1;
            this.dataGridView1.DataSource = controler.UpdateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage4;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = controler.UpdateTable();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            controler.Add(textBox1.Text,textBox2.Text,textBox3.Text);
            dataGridView1.DataSource = controler.UpdateTable();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            controler.Delete(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString()));
            dataGridView1.DataSource = controler.UpdateTable();
        }
    }
}
