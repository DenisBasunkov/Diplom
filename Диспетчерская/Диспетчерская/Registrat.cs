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
    public partial class Registrat : Form
    {
        public Registrat()
        {
            InitializeComponent();
        }

        Vhod Form1 = new Vhod();

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int slosh;

        private void Pas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Pas.Text.Length < 5 && Pas.Text.Length > 0)
            {
                bunifuProgressBar1.ProgressColor = Color.Red;
                bunifuProgressBar1.Value = 25;

                slosh = 1;
            }
            else if (Pas.Text.Length >= 5 && Pas.Text.Length < 10)
            {
                bunifuProgressBar1.ProgressColor = Color.Orange;
                bunifuProgressBar1.Value = 50;
                slosh = 2;
            }
            else if (Pas.Text.Length >= 10)
            {
                bunifuProgressBar1.ProgressColor = Color.Green;
                bunifuProgressBar1.Value = 100;
                slosh = 3;
            }
            else if (Pas.Text.Length <= 0)
            {
                bunifuProgressBar1.ProgressColor = Color.Red;
                bunifuProgressBar1.Value = 0;
                slosh = 0;

            }
        }

       

        async void table(string name)
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                await sql.OpenAsync();
                string skript = string.Format("CREATE TABLE [{0}]([Время входа] String(50) NULL,[Время выхода] String(50) NULL)", name);
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                await command.ExecuteNonQueryAsync();


                string skripts = string.Format("CREATE TABLE [{0}Ист]([Файл] String (50) NULL,[Номер заявки] String(50) NULL,[Время работы] String(50) NULL);", name);
                SQLiteCommand commands = new SQLiteCommand(skripts, sql);
                await commands.ExecuteNonQueryAsync();



            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "");
            }
            finally
            {
                sql.Close();
            }
        }

        private void Registrat_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (slosh > 1)
            {


                if (Pas.Text == PasDuo.Text)
                {

                    SQLiteConnection connection = new SQLiteConnection(Form1.nameData, true);

                    await connection.OpenAsync();
                    string skript = "INSERT INTO [Table] (Users,Password,Names,Profill,Dostup) VALUES (@Users,@Password,@Names,@Profill,@Dostup)";

                    try
                    {

                        SQLiteCommand command = new SQLiteCommand(skript, connection);
                        command.Parameters.AddWithValue("@Users", Log.Text);
                        command.Parameters.AddWithValue("@Password", Pas.Text);
                        command.Parameters.AddWithValue("@Names", Names.Text);
                        command.Parameters.AddWithValue("@Profill", Prof.Text);
                        command.Parameters.AddWithValue("@Dostup", "Ожидание");
                        await command.ExecuteNonQueryAsync();
                        table(Names.Text);
                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        MessageBox.Show(@"Ваша акаунт зарегистрирован и находится в режиме «ожидания». Обратитесь к администратору для получения доступа к программе", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        connection.Close();
                        this.Close();

                    }

                }
                else
                {
                    MetroMessageBox.Show(this, "Пароли не совпадают.\n Проверьте, совпадают ли они", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            else
            {
                MetroMessageBox.Show(this, "Пароль слишком короткий", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
