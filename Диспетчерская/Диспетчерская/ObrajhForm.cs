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
    public partial class ObrajhForm : Form
    {
        
        int tipes = 0;
        string nomera = "";
        string names = "";
        public ObrajhForm(int tipe, string nomer,string name)
        {
            InitializeComponent();

            tipes = tipe;
            if (tipe == 1)
            {

                nomera = nomer;
                label5.Text = nomera;
                names = name;
            }
            else if (tipe == 0)
            {
                nomera = nomers();
                label5.Text = nomera;
                names = name;
            }


        }

        void readу()
        {
            string skript = "SELECT * FROM [Datas]";
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            sql.Open();
            SQLiteCommand command = new SQLiteCommand(skript, sql);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader["User"].ToString() != names)
                {
                    if (reader["tab"].ToString() == nomera)
                    {
                        DialogResult dialog = MetroMessageBox.Show(this, "Данная заявка используется другим пользователем", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            toolStrip1.Enabled = false;
                            
                            panel2.Enabled = false;
                            checkBox1.Enabled = false;
                            break;
                        }
                        else if (dialog == DialogResult.No)
                        {
                            this.Close();
                        }


                    }
                }
                else if (reader["User"].ToString() == names)
                {
                    string s = $"UPDATE [Datas] SET [tab] = '{nomera}'  Where [User] = '{names}';";
                    SQLiteCommand commands = new SQLiteCommand(s, sql);
                    commands.ExecuteNonQuery();
                }


            }

        }

        private void maskedTextBox1_DoubleClick(object sender, EventArgs e)
        {
            DatTime form = new DatTime("Obrajh", 0, 0, this, maskedTextBox1.Text,"text",0);



            form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            form.ShowDialog();
        }

        private void ObrajhForm_Load(object sender, EventArgs e)
        {
            string[] yl = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"Datas\Улицы.txt", Encoding.GetEncoding(1251));

            for (int i = 0; i < yl.Count(); i++)
            {
                Ylicha.Items.Add(yl[i]);
            }


            if (tipes == 0)
            {

                Stats.ForeColor = Color.DarkGreen;
                Stats.Text = "новая";
                ReadGlav.Enabled = false;

            }
            else if (tipes == 1)
            {

                Dannie();
                SaveGlav.Enabled = false;
                ReadGlav.Enabled = true;

               

            }
            if (Stats.Text == "новая")
            {

                Stats.ForeColor = Color.DarkGreen;

            }
            else if (Stats.Text == "новая (ред.)")
            {

                Stats.ForeColor = Color.DarkGreen;

            }
            else if (Stats.Text == "закрыта")
            {
                Stats.ForeColor = Color.DarkRed;

            }

            readу();
        }

        async void Dannie()
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                string Adres = Ylicha.Text + "-" + Dom.Text;

                await sql.OpenAsync();

                string skript = string.Format("SELECT * FROM [Обращения] WHERE [Номер] LIKE '{0}'", nomera);
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Raion.Text = reader["Район"].ToString();
                    string[] Adress = null;
                    string nom = reader["Адрес"].ToString();
                    Adress = nom.Split('-');
                    Ylicha.Text = Adress[0];
                    Dom.Text = Adress[1];
                    Zaiavit.Text = reader["Заявитель"].ToString();
                    Opisanie.Text = reader["Текст обращения"].ToString();
                    maskedTextBox1.Text = reader["Дата ввода"].ToString();
                    DopInfo.Text = reader["Доп.инфо"].ToString();
                    Meri.Text = reader["Принятые меры"].ToString();
                    VidObr.Text = reader["Вид обращения"].ToString();
                    Peredano.Text = reader["Передано"].ToString();
                    maskedTextBox2.Text = reader["Закрытие"].ToString();

                    Stats.Text = reader["Статус"].ToString();
                    if (Stats.Text == "")
                    {
                        checkBox1.Checked = true ;
                    }else if (Stats.Text =="" || Stats.Text == "")
                    {
                        checkBox1.Checked = false;
                    }

                    

                }



            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }


        }


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            sql.Open();
            string s = $"UPDATE [Datas] SET [tab] = ''  Where [User] = '{names}';";
            SQLiteCommand commands = new SQLiteCommand(s, sql);

            commands.ExecuteNonQuery();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Stats.Text = "закрыта";
                Stats.ForeColor = Color.DarkRed;
                groupBox2.Enabled = true;
            }
            else if(!checkBox1.Checked)
            {
              

                if (Meri.Text != "" || DopInfo.Text != "")
                {
                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.DarkOrange;
                    groupBox2.Enabled = false;
                }
                else
                {
                    Stats.Text = "новая (ред.)";
                    Stats.ForeColor = Color.DarkGreen;
                    groupBox2.Enabled = false;
                }
                
               
            }
        }


        Vhod Form1 = new Vhod();

        string nomers()
        {
            string ret = "";

            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);

            int n = 0;

            SQLiteCommand sqlc = new SQLiteCommand("SELECT * FROM [Обращения] WHERE id=(SELECT MAX(id) FROM [Обращения])", sql);

            try
            {
                sql.Open();
                SQLiteDataReader reader = sqlc.ExecuteReader();
                reader.Read();

                string no = reader["Номер"].ToString();
                n = Convert.ToInt16(no.Remove(0, 2));
                n = n + 1;
                ret = "O-" + n.ToString();
                if (reader.Read())
                    throw new Exception("too many rows has been readed!");

                reader.Close();

            }
            catch
            {

                ret = "O-1";
            }
            finally
            {

            }
            sql.Close();


            return ret;
        }

        private void SaveGlav_Click(object sender, EventArgs e)
        {
            Glav();
            SaveGlav.Enabled = false;
            ReadGlav.Enabled = true;
        }

        async void Glav()
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                string Adres = Ylicha.Text + "-" + Dom.Text;
                await sql.OpenAsync();

                string skript = "INSERT INTO [Обращения] ([Номер],[Вид обращения],[Дата ввода],[Район],[Адрес]," +
                    "[Заявитель],[Текст обращения],[Передано],[Статус],[Принятые меры],[Доп.инфо]) " +
                    "VALUES (@Н,@ВО,@ДВ,@Р,@А,@З,@ТО,@П,@С,@ПМ,@ДИ);";
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@Н", nomera);
                command.Parameters.AddWithValue("@ВО", VidObr.Text);
                command.Parameters.AddWithValue("@ДВ", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@Р", Raion.Text);
                command.Parameters.AddWithValue("@А", Adres);
                command.Parameters.AddWithValue("@З", Zaiavit.Text);
                command.Parameters.AddWithValue("@ТО", Opisanie.Text);
                command.Parameters.AddWithValue("@П", Peredano.Text);
                command.Parameters.AddWithValue("@С", Stats.Text);
                command.Parameters.AddWithValue("@ПМ", Meri.Text);
                command.Parameters.AddWithValue("@ДИ", DopInfo.Text);
                


                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private async void ReadGlav_Click(object sender, EventArgs e)
        {
            string Adres = Ylicha.Text + "-" + Dom.Text;
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                await sql.OpenAsync();



                string skript = string.Format("UPDATE [Обращения] SET " +
                    "[Номер] = @Н,[Вид обращения] = @ВО,[Дата ввода] = @ДВ,[Район] = @Р,[Адрес] = @А," +
                    "[Заявитель] = @З,[Текст обращения] = @ТО,[Передано] = @П,[Статус] = @С,[Принятые меры] = @ПМ,[Доп.инфо] = @ДИ,[Закрытие] = @Зк WHERE [Номер] = '{0}'", nomera);

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                command.Parameters.AddWithValue("@Н", nomera);
                command.Parameters.AddWithValue("@ВО", VidObr.Text);
                command.Parameters.AddWithValue("@ДВ", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@Р", Raion.Text);
                command.Parameters.AddWithValue("@А", Adres);
                command.Parameters.AddWithValue("@З", Zaiavit.Text);
                command.Parameters.AddWithValue("@ТО", Opisanie.Text);
                command.Parameters.AddWithValue("@П", Peredano.Text);

                if (checkBox1.Checked)
                {
                    command.Parameters.AddWithValue("@С", Stats.Text);
                    Stats.ForeColor = Color.DarkRed;
                    command.Parameters.AddWithValue("@Зк", maskedTextBox2.Text);
                    command.Parameters.AddWithValue("@ПМ", Meri.Text);
                    command.Parameters.AddWithValue("@ДИ", DopInfo.Text);
                }
                else
                {
                    
                    command.Parameters.AddWithValue("@Зк", maskedTextBox2.Text);
                    if (Meri.Text != "" || DopInfo.Text != "")
                    {
                        Stats.Text = "в работе";
                        Stats.ForeColor = Color.DarkOrange;
                        command.Parameters.AddWithValue("@Зк", "");
                        command.Parameters.AddWithValue("@С", Stats.Text);
                        command.Parameters.AddWithValue("@ПМ", Meri.Text);
                        command.Parameters.AddWithValue("@ДИ", DopInfo.Text);
                    }
                    else
                    {
                        Stats.Text = "новая (ред.)";
                        Stats.ForeColor = Color.DarkGreen;
                        command.Parameters.AddWithValue("@Зк", "");
                        command.Parameters.AddWithValue("@С", Stats.Text);
                        command.Parameters.AddWithValue("@ПМ", Meri.Text);
                        command.Parameters.AddWithValue("@ДИ", DopInfo.Text);
                    }
                }

                

                




                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void maskedTextBox2_DoubleClick(object sender, EventArgs e)
        {
            DatTime form = new DatTime("Obrajh", 0, 0, this, maskedTextBox2.Text, "text", 1);



            form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (SaveGlav.Enabled == true)
            {
                nomera = nomers();
                label5.Text = nomera;
               
            }
            else
            {
                label5.Text = nomera;
               
            }
        }
    }
}
