using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диспетчерская
{
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();
        }

        private async void Administrator_Load(object sender, EventArgs e)
        {

            metroTabControl1.SelectTab(0);
            Vhod form1 = new Vhod();

            SQLiteConnection sql = new SQLiteConnection(@"Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db", true);
            
            SQLiteCommand command = new SQLiteCommand("SELECT name FROM (SELECT * FROM sqlite_master UNION ALL SELECT * FROM sqlite_temp_master) WHERE type='table' ORDER BY name;", sql);

            try
            {
                listBox1.Items.Clear();
                await sql.OpenAsync();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {


            }


            string customers = "SELECT * FROM [Table]";


            
            try
            {
                DataSet dsq = new DataSet();

                SQLiteDataAdapter das = new SQLiteDataAdapter(customers, sql);
                das.Fill(dsq, "Customers");

                bunifuCustomDataGrid1.AutoGenerateColumns = true;
                bunifuCustomDataGrid1.DataSource = dsq;
                
                bunifuCustomDataGrid1.DataMember = "Customers";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            sql.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vhod form1 = new Vhod();

            

            string customers = "SELECT * FROM [" + listBox1.SelectedItem.ToString() + "]";

            SQLiteConnection con = new SQLiteConnection("Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db", true);

            try
            {
                DataSet ds = new DataSet();

                SQLiteDataAdapter da = new SQLiteDataAdapter(customers, con);
                da.Fill(ds, "Customers");

                AdTable.AutoGenerateColumns = true;
                AdTable.DataSource = ds;

                AdTable.DataMember = "Customers";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string q;
        private void bunifuCustomDataGrid1_Click(object sender, EventArgs e)
        {
            try
            {
                q = bunifuCustomDataGrid1.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            {

            }
        }

        private void bunifuCustomDataGrid1_DoubleClick(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.RowCount == 0)
            {


            }
            else
            {
                if (!bunifuCustomDataGrid1.CurrentRow.Cells[0].Value.ToString().Equals(""))
                {
                    MessageBox.Show(bunifuCustomDataGrid1.CurrentRow.Cells[3].Value.ToString());
                    q = bunifuCustomDataGrid1.CurrentRow.Cells[3].Value.ToString();
                    Polzov polzov = new Polzov(q, this);
                    polzov.ShowDialog();
                }

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Registrat registrat = new Registrat();
            registrat.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Данное действие удалит все данные об этом пользователе.\nЖелаете продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                q = bunifuCustomDataGrid1.CurrentRow.Cells[3].Value.ToString();
                string customers = string.Format("DELETE FROM [Table] WHERE [Names] = '{0}'", q);

                SQLiteConnection con = new SQLiteConnection("Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db", true);

                try
                {
                    con.Open();

                    SQLiteCommand sQ = new SQLiteCommand(customers, con);
                    sQ.ExecuteNonQuery();


                    SQLiteCommand sqQ = new SQLiteCommand("DROP TABLE IF EXISTS " + q, con);
                    sqQ.ExecuteNonQuery();

                    SQLiteCommand swQ = new SQLiteCommand("DROP TABLE IF EXISTS " + q + "Ист", con);
                    swQ.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Administrator_Load(null, null);
                }
            }

        }

        private async void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Vhod form1 = new Vhod();

            SQLiteConnection sql = new SQLiteConnection(@"Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db", true);

            SQLiteCommand command = new SQLiteCommand("SELECT name FROM (SELECT * FROM sqlite_master UNION ALL SELECT * FROM sqlite_temp_master) WHERE type='table' ORDER BY name;", sql);

            try
            {
                listBox1.Items.Clear();
                await sql.OpenAsync();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {


            }


            string customers = "SELECT * FROM [Table]";



            try
            {
                DataSet dsq = new DataSet();

                SQLiteDataAdapter das = new SQLiteDataAdapter(customers, sql);
                das.Fill(dsq, "Customers");

                bunifuCustomDataGrid1.AutoGenerateColumns = true;
                bunifuCustomDataGrid1.DataSource = dsq;

                bunifuCustomDataGrid1.DataMember = "Customers";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }

            sql.Close();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            VodOtch otch = new VodOtch();
            otch.Show();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            KanalOtch otch = new KanalOtch();
            otch.Show();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            ObrajhOtch otch = new ObrajhOtch();
            otch.Show();
        }
    }
}
