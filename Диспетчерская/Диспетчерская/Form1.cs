using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диспетчерская
{
    public partial class Vhod : Form
    {
        public Vhod()
        {
            InitializeComponent();
        }

        public string nameData = @"Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db";


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            File.Create(AppDomain.CurrentDomain.BaseDirectory + @"Datas\lok.txt").Close();
            Environment.Exit(0);
        }

        string names;

        private async void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (LogT.Text.Equals("") || PasT.Text.Equals(""))
            {
                MetroMessageBox.Show(this, "Задано пустое поле", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if ((LogT.Text.Equals("")))
                {
                    LogT.Focus();
                    n = 0;
                }
                else if (PasT.Text.Equals(""))
                {
                    PasT.Focus();
                    n = 1;
                }
                
            }
            else if (LogT.Text.Equals("Admin") && PasT.Text.Equals("12345"))
            {
                string name = Path.GetFileNameWithoutExtension(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                
                string[] d = { LogT.Text, PasT.Text };
                for (int i = 0; i < 2; i++)
                {
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + name + ".txt", d[i] + Environment.NewLine);
                }

                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
                SQLiteConnection sql = new SQLiteConnection(nameData, true);
                sql.Open();
                SQLiteCommand commands = new SQLiteCommand("INSERT INTO [Datas] (User) VALUES (@data)", sql);
                commands.Parameters.AddWithValue("@data", "Админ");
                commands.ExecuteNonQuery();
                GlavForm form = new GlavForm("Admin", "Админ");
                form.Show();
                Hide();
            }
            else
            {
                string polz = "";
                string dost = "";
                using (SQLiteConnection sql = new SQLiteConnection(nameData, true))
                {
                    await sql.OpenAsync();
                    SQLiteDataReader reader = null;
                    try
                    {
                        string skript = string.Format("SELECT [Users],[Password],[Profill],[Names],[Dostup] FROM [Table] WHERE [Users]='{0}' AND [Password]='{1}';", LogT.Text, PasT.Text);
                        using (SQLiteCommand command = new SQLiteCommand(skript, sql))
                        {
                            reader = command.ExecuteReader();

                            reader.Read();
                            names = reader["Names"].ToString();
                            polz = reader["Names"].ToString();
                            dost = reader["Dostup"].ToString();
                        }




                    }
                    catch
                    {
                        MetroMessageBox.Show(this, "Пользователь не опознан", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        n = 0;
                    }
                    finally
                    {
                        if (dost == "Ожидание")
                        {

                            MetroMessageBox.Show(this, "Данный пользователь находится в рассмотрении", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else if (dost == "Запрещено")
                        {

                            MetroMessageBox.Show(this, "Данному пользователю запрещен доступ.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                        }
                        else if (dost == "Разрешен")
                        {


                            try
                            {
                                string skript = string.Format("INSERT INTO [{0}] ([Время входа],[Время выхода]) VALUES (@data, @datas)", polz);
                                using (SQLiteCommand command = new SQLiteCommand(skript, sql))
                                {
                                    command.Parameters.AddWithValue("@data", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@datas", "@");


                                    command.ExecuteNonQuery();



                                }

                                string name = Path.GetFileNameWithoutExtension(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                                string f = "";
                                string[] d = { reader["Users"].ToString(), reader["Password"].ToString() };
                                for (int i = 0; i < 2; i++)
                                {
                                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + name + ".txt", d[i] + Environment.NewLine);
                                }
                                SQLiteCommand commands = new SQLiteCommand("INSERT INTO [Datas] (User) VALUES (@data)", sql);
                                commands.Parameters.AddWithValue("@data",names);
                                commands.ExecuteNonQuery();

                                GlavForm form = new GlavForm(reader["Profill"].ToString(), reader["Names"].ToString());
                                form.Show();
                                this.Hide();
                            }
                            catch
                            {
                                MetroMessageBox.Show(this, "Такого пользователья не существует.\n Проверьте правильность написанного.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                               
                               
                            }



                            reader.Close();
                            sql.Close();

                        }

                       
                    }
                }
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));

            }

           


        }
       
        private void Vhod_Load(object sender, EventArgs e)
        {
            LogT.Select();
            
            
        }

        private void Registration_Click(object sender, EventArgs e)
        {
            Registrat registrat = new Registrat();
            
            registrat.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName == "en")
            {

                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
                label3.Text = "РУС";

            }
            else if (InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName == "ru")
            {

                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
                label3.Text = "ENG";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName == "en")
            {

                label3.Text = "ENG";


            }
            else if (InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName == "ru")
            {

                label3.Text = "РУС";

            }
        }
        int n = 0;
        private void Vhod_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.Enter)
            {
                
                bunifuFlatButton1_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }
    }
}
