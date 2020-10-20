using System;
using System.IO;
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
        Query controlerThing;
        Query controlerThingKey;
        Query controlerWish;
        Query ConTT;
        string dateCh ="";
        string metodCh="";
        string materialCh="";
        public MainForm()
        {
            InitializeComponent();
            controler = new Query(ConnectionString.connstr);
            controlerThing = new Query(ConnectionString.connstr);
            controlerThingKey = new Query(ConnectionString.connstr);
            controlerWish = new Query(ConnectionString.connstr);
            ConTT =new Query(ConnectionString.connstr);
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
            this.label6.Text = "Количество ключей " + controler.getQuery();
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].HeaderText = "Ключ RFID_1";
            this.dataGridView1.Columns[2].HeaderText = "Ключ RFID_2";
            this.dataGridView1.Columns[3].HeaderText = "Ключ RFID_3";
            this.dataGridView3.DataSource = controlerThing.EditUpdateTable("SELECT *  FROM Thing");
            this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID");
            this.dataGridView2.Columns[0].Visible = false;
            this.dataGridView2.Columns[1].HeaderText = "Дата";
            this.dataGridView2.Columns[2].HeaderText = "Наименование";
            this.dataGridView2.Columns[3].HeaderText = "Материал";
            this.dataGridView2.Columns[4].HeaderText = "Метод чистки";
            this.dataGridView2.Columns[5].HeaderText = "Билет на чистку";
            #region Заголовочные поля таблицы 3
            this.dataGridView3.Columns[1].HeaderText = "Имя";
            this.dataGridView3.Columns[2].HeaderText = "Материал";
            this.dataGridView3.Columns[3].HeaderText = "Метод чистки";
            this.dataGridView3.Columns[4].HeaderText = "Номер талона";
            #endregion
            this.dataGridView3.Columns[0].Visible = false;
            this.dataGridView3.Columns[5].Visible = false;
            this.dataGridView3.Rows[0].Cells[0].Selected = true;
            //this.label8.Text = dataGridView3.CurrentRow.Cells[5].Value.ToString();
            this.dataGridKeyThing.DataSource = controlerThingKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3" +
                " as key_three FROM Key_table WHERE id =" + dataGridView3.Rows[0].Cells[5].Value.ToString());
            this.dataGridKeyThing.Columns[0].Visible = false;
            #region Загаловки ключей в таблице
            this.dataGridKeyThing.Columns[1].HeaderText = "Ключ RFID_1";
            this.dataGridKeyThing.Columns[2].HeaderText = "Ключ RFID_2";
            this.dataGridKeyThing.Columns[3].HeaderText = "Ключ RFID_3";
            #endregion 
            this.dataGridKeyThing.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridKeyThing.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.comboBox1.Items.Add("");
            for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(dataGridView2.Rows[i].Cells[4].Value.ToString());
            }
            this.comboBox2.Items.Add("");
            for (int i = 0; i < this.dataGridView2.Rows.Count; i++) {
                this.comboBox2.Items.Add(dataGridView2.Rows[i].Cells[3].Value.ToString());
            }
            label12.Text = "Количество записей в таблице " + this.dataGridView2.Rows.Count;
            this.comboBox1.SelectedIndex = 0; this.comboBox2.SelectedIndex = 0;
          
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
            this.label6.Text = "Количество ключей " + controler.getQuery();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            controler.Add(textBox1.Text,textBox2.Text,textBox3.Text);
            dataGridView1.DataSource = controler.UpdateTable();
            label6.Text = "Количество ключей " + controler.getQuery();
            dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.Rows.Count - 1];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int position = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString());
            controler.DeleteThingByUseID(position);
            controler.Delete(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString()));
            this.dataGridView3.DataSource = controlerThing.EditUpdateTable("SELECT  *  FROM Thing");
            dataGridView1.DataSource = controler.UpdateTable();
            this.label6.Text = "Количество ключей " + controler.getQuery();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                this.dataGridView3.DataSource = controlerThing.EditUpdateTable("SELECT name as 'Наименование', material as 'Материал', mclean as 'Метод чистки', ticet as 'Билет на чистку' FROM Thing");
                this.dataGridView3.CurrentCell = dataGridView3[1, dataGridView3.Rows.Count - 1];
                this.dataGridKeyThing.DataSource = controlerThingKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE id =" + dataGridView3.CurrentRow.Cells[5].Value.ToString());
            }
        }

        private void dataGridKeyThing_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
         
        }

        private void dataGridKeyThing_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            this.dataGridKeyThing.DataSource = controlerThingKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE id =" + dataGridView3.CurrentRow.Cells[5].Value.ToString());
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            this.dataGridKeyThing.DataSource = controlerThingKey.EditUpdateTable("SELECT id as ID, key1 as key_first,key2 as key_second,key3 as key_three FROM Key_table WHERE id =" + dataGridView3.CurrentRow.Cells[5].Value.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            controler.Delete(int.Parse(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["key_table"].Value.ToString()));
            controler.DeleteThing(int.Parse(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["ID"].Value.ToString()));
            this.dataGridView3.DataSource = controlerThing.EditUpdateTable("SELECT name as 'Наименование', material as 'Материал', mclean as 'Метод чистки', ticet as 'Билет на чистку' FROM Thing");
            this.dataGridView1.DataSource = controler.UpdateTable();

        }

        private void button12_Click(object sender, EventArgs e)
        {
          string st = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["name"].Value.ToString();
          string material = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["material"].Value.ToString();
          string clean = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["mclean"].Value.ToString();
          string mclean = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["ticet"].Value.ToString();
          string key_table = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["key_table"].Value.ToString();
          string ID = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["ID"].Value.ToString();
            update up = new update();
            up.title = st;
            up.nametable = st;
            up.clean = clean;
            up.material = material;
            up.mclean = mclean;
            up.key_table = key_table;
            up.id = ID;
            if (up.ShowDialog(this) == DialogResult.OK) {

                this.dataGridView3.DataSource = controlerThing.EditUpdateTable("SELECT *  FROM Thing");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Addbydate faddbydate = new Addbydate();
            if (faddbydate.ShowDialog(this) == DialogResult.OK) {
                this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.ConTT.DeleteTimeThink(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["ID"].Value.ToString());
            this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.label9.Enabled = true;
                this.label10.Enabled = true;
                this.label11.Enabled = true;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.textBox4.Enabled = true;
            }
            else {
                this.label9.Enabled = false;
                this.label10.Enabled = false;
                this.label11.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.textBox4.Enabled = false;
                this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID");
                this.dataGridView2.Columns[0].Visible = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        if (this.textBox4.Text != "") { this.dateCh = " AND DateT.TimeT like '%" + this.textBox4.Text+"%'"; } else { this.dateCh = ""; }
        if (this.comboBox1.SelectedItem.ToString() != "") { this.metodCh = " AND Thing.mclean like('%" + this.comboBox1.SelectedItem.ToString()+"%') "; } else { this.metodCh = "";}
        if (this.comboBox2.SelectedItem.ToString() != "") { this.materialCh = " AND Thing.material like('%" + this.comboBox2.SelectedItem.ToString() + "%')"; } else { this.materialCh = "";}
            this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID " + this.dateCh + this.metodCh + this.materialCh);
            label12.Text = "Количество записей в таблице " + this.dataGridView2.Rows.Count;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.SelectedIndex = 0;
            if (this.textBox4.Text != "") { this.dateCh = " AND DateT.TimeT like '%" + this.textBox4.Text + "%'"; } else { this.dateCh = ""; }
            if (this.comboBox1.SelectedItem.ToString() != "") { this.metodCh = " AND Thing.mclean like('%" + this.comboBox1.SelectedItem.ToString() + "%') "; } else { this.metodCh = ""; }
            if (this.comboBox2.SelectedItem.ToString() != "") { this.materialCh = " AND Thing.material like('%" + this.comboBox2.SelectedItem.ToString() + "%')"; } else { this.materialCh = ""; }
            this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID " + this.dateCh + this.metodCh + this.materialCh);
            this.dataGridView2.Columns[0].Visible = false;
            label12.Text = "Количество записей в таблице " + this.dataGridView2.Rows.Count;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.textBox4.Text != "") { this.dateCh = " AND DateT.TimeT like '%" + this.textBox4.Text + "%'"; } else { this.dateCh = ""; }
            if (this.comboBox1.SelectedItem.ToString() != "") { this.metodCh = " AND Thing.mclean like('%" + this.comboBox1.SelectedItem.ToString() + "%') "; } else { this.metodCh = ""; }
            if (this.comboBox2.SelectedItem.ToString() != "") { this.materialCh = " AND Thing.material like('%" + this.comboBox2.SelectedItem.ToString() + "%')"; } else { this.materialCh = ""; }
            this.dataGridView2.DataSource = controlerWish.EditUpdateTable(@"SELECT TimeThink.ID, DateT.TimeT as 'Время', Thing.name as 'Наименование', Thing.material as 'Материал', 
                                                                           Thing.mclean as 'Метод чистки', Thing.ticet as 'Билет на чистку'
                                                                           FROM DateT,Thing,TimeThink WHERE DateT.ID = TimeThink.DateT 
                                                                           AND  TimeThink.Thing = Thing.ID " + this.dateCh + this.metodCh + this.materialCh);
            this.dataGridView2.Columns[0].Visible = false;
            label12.Text = "Количество записей в таблице " + this.dataGridView2.Rows.Count;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\tamplete\\";

            #region filewrite
            try
            {
                
                StreamWriter sw = new StreamWriter(path +"Simple.txt");
                for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                {
                    sw.WriteLine(dataGridView2.Rows[i].Cells[1].Value.ToString()+" "+ dataGridView2.Rows[i].Cells[2].Value.ToString()+" "+ dataGridView2.Rows[i].Cells[3].Value.ToString() +
                        " "+dataGridView2.Rows[i].Cells[4].Value.ToString());
                    
                }



                
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
               System.Diagnostics.Process.Start(path + "Simple.txt");
            }
            #endregion

        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region filewriteExcel
            string path = Application.StartupPath + "\\tamplete\\";
            try
            {

                StreamWriter sw = new StreamWriter(path + "Simple.txt",false,Encoding.UTF8);
                for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                {
                    sw.WriteLine(dataGridView2.Rows[i].Cells[1].Value.ToString() + " " + dataGridView2.Rows[i].Cells[2].Value.ToString() + " " + dataGridView2.Rows[i].Cells[3].Value.ToString() +
                        " " + dataGridView2.Rows[i].Cells[4].Value.ToString());

                }




                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                System.Diagnostics.Process.Start(path + "Simple.xlsm");
            }
            #endregion
        }
    }
}
