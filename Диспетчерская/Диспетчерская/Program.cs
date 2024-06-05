using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диспетчерская
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {

            File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + @"Datas", System.IO.FileAttributes.Hidden);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string nameData = "Data Source = " + AppDomain.CurrentDomain.BaseDirectory + @"Datas\Диспетчерская.db";
            string x = "", y = "";
            string name = Path.GetFileNameWithoutExtension(System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            string[] allFoundFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"Datas", "*.txt", SearchOption.AllDirectories);
            string file = "";
            int q = 0;

            for (int i = 0; i < allFoundFiles.Count(); i++)
            {
                string a = Path.GetFileNameWithoutExtension(allFoundFiles[i]);
                if (a == name)
                {

                    file = a;
                    q = 1;
                    break;
                }
                else
                {
                    q = 0;
                }

            }

            if (q == 0)
            {

                Application.Run(new Vhod());
            }
            else
            {
                string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + file + ".txt", Encoding.UTF8);
                if (lines.Length != 0)
                {
                    string a = "", b = "";
                    if (lines[0] == "Admin" && lines[1] == "12345")
                    {

                        Application.Run(new GlavForm("Admin", "Админ"));
                    }
                    else
                    {
                        a = lines[0].ToString();
                        b = lines[1].ToString();
                        using (SQLiteConnection connection = new SQLiteConnection(nameData, true))
                        {
                            try
                            {
                                connection.Open();
                                string skript = string.Format("SELECT * FROM [Table] WHERE [Users] = '{0}' AND [Password] = '{1}'", a, b);

                                SQLiteCommand sql = new SQLiteCommand(skript, connection);
                                SQLiteDataReader reader = null;
                                reader = sql.ExecuteReader();
                                while (reader.Read())
                                {
                                    x = reader["Names"].ToString();
                                    y = reader["Profill"].ToString();
                                }

                                if (x == null && y == null)
                                {
                                    Application.Run(new Vhod());
                                }
                                else
                                {
                                    Application.Run(new GlavForm(y, x));
                                }

                                reader.Close();
                                string skripts = string.Format("INSERT INTO [{0}] ([Время входа],[Время выхода]) VALUES (@data, @datas)", x);
                                SQLiteCommand command = new SQLiteCommand(skripts, connection);
                                command.Parameters.AddWithValue("@data", DateTime.Now.ToString());
                                command.Parameters.AddWithValue("@datas", '@');


                                command.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Неудалось установить связь с учетной записью\n Программа вернется к окну входа\n Приносим свои извинения", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                File.Create(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + file + ".txt").Close();
                                Application.Run(new Vhod());
                            }
                            finally
                            {


                            }
                        }





                    }

                }
                else 
                {


                    Application.Run(new Vhod());
                }
            }






        }
    }
}
