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
    public partial class Polzov : Form
    {
        private readonly Administrator vodo;
        string nomer;
        public Polzov(string nom, Administrator ad)
        {
            InitializeComponent();
            nomer = nom;
            vodo = ad;
        }

        string name;
        private void Polzov_Load(object sender, EventArgs e)
        {
            string skript = string.Format("SELECT * FROM [Table] WHERE [Names] = '{0}';", nomer);

            SQLiteConnection sql = new SQLiteConnection(form1.nameData, true);

            SQLiteCommand command = new SQLiteCommand(skript, sql);
            SQLiteDataReader reader = null;
            try
            {
                sql.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Login.Text = (string)reader["Users"];
                    Password.Text = (string)reader["Password"];
                    Names.Text = (string)reader["Names"];
                    Profill.Text = (string)reader["Profill"];
                    Dost.Text = (string)reader["Dostup"];
                    name = (string)reader["Names"];
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                sql.Close();
            }
        }

        Vhod form1 = new Vhod();
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(form1.nameData, true);

            try
            {
                sql.Open();


                string skript = string.Format("UPDATE [Table] SET [Users] = @Users, [Password] = @Password, [Names] = @Names, [Profill] = @Profill, [Dostup] = @Dostup WHERE [Names] = '{0}'", nomer);
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@Users", Login.Text);
                command.Parameters.AddWithValue("@Password", Password.Text);
                command.Parameters.AddWithValue("@Names", Names.Text);
                command.Parameters.AddWithValue("@Profill", Profill.Text);
                command.Parameters.AddWithValue("@Dostup", Dost.Text);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                try
                {
                    string skript = string.Format($"ALTER TABLE {name} RENAME TO {Names.Text};");

                    SQLiteCommand command = new SQLiteCommand(skript, sql);
                    command.ExecuteNonQuery();

                    string skripts = string.Format($"ALTER TABLE {name}Ист RENAME TO {Names.Text}Ист;");
                    SQLiteCommand commands = new SQLiteCommand(skripts, sql);
                    commands.ExecuteNonQuery();
                }
                catch
                {
                   
                }
                finally
                {

                }


                this.Close();
            }

        }

        private void Polzov_FormClosing(object sender, FormClosingEventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db", true);




            string customers = "SELECT * FROM [Table]";



            try
            {
                DataSet dsq = new DataSet();

                SQLiteDataAdapter das = new SQLiteDataAdapter(customers, sql);
                das.Fill(dsq, "Customers");

                vodo.bunifuCustomDataGrid1.AutoGenerateColumns = true;
                vodo.bunifuCustomDataGrid1.DataSource = dsq;

                vodo.bunifuCustomDataGrid1.DataMember = "Customers";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                vodo.bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            for (int i = 0; i < vodo.bunifuCustomDataGrid1.RowCount; i++)
            {

                //vodo.bunifuCustomDataGrid1.Rows[i].Selected = false;
                try
                {
                    if (vodo.bunifuCustomDataGrid1.Rows[i].Cells[3].Value != null)
                        if (vodo.bunifuCustomDataGrid1.Rows[i].Cells[3].Value.ToString().Contains(nomer))
                        {
                            vodo.bunifuCustomDataGrid1.Rows[i].Selected = true;

                            vodo.bunifuCustomDataGrid1.CurrentCell = vodo.bunifuCustomDataGrid1[3, i];


                            break;
                        }
                }
                catch
                {

                    vodo.bunifuCustomDataGrid1.Rows[i].Selected = true;

                    vodo.bunifuCustomDataGrid1.CurrentCell = vodo.bunifuCustomDataGrid1[3, 0];


                    break;

                }

            }
            sql.Close();
        }
    }
}
