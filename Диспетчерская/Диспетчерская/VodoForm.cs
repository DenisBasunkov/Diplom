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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диспетчерская
{
    public partial class VodoForm : Form
    {

        
        string status = "";
        int tipes = 0;
        string nomera = "";
        string names = "";

        public VodoForm(int tipe, string nomer, string name)
        {
            InitializeComponent();

           
            tipes = tipe;
            if (tipe == 1)
            {
                names = name;
                nomera = nomer;
                label5.Text = nomera;
                Nomer.Text = nomera;
                
            }
            else if (tipe == 0)
            {
                names = name;
                nomera = nomers();
                label5.Text = nomera;
                Nomer.Text = nomera;
            }

            
           


            Rabota.Columns[0].DefaultCellStyle.Format = "M";
            Otvetstv.Text = names;
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
                            toolStrip2.Enabled = false;
                            panel2.Enabled = false;
                            Main.Enabled = false;

                            for (int i = 0; i < Obsled.ColumnCount; i++)
                            {
                                Obsled.Rows[0].Cells[i].ReadOnly = true;
                            }
                            for (int i = 0; i < Rabota.ColumnCount; i++)
                            {
                                Rabota.Rows[0].Cells[i].ReadOnly = true;
                            }
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

        Vhod Form1 = new Vhod();

        string nomers()
        {
            string ret = "";

            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);

            int n = 0;

            SQLiteCommand sqlc = new SQLiteCommand("SELECT * FROM [Водопровод] WHERE id=(SELECT MAX(id) FROM [Водопровод])", sql);

            try
            {
                sql.Open();
                SQLiteDataReader reader = sqlc.ExecuteReader();
                reader.Read();

                string no = reader["Номер"].ToString();
                n = Convert.ToInt16(no.Remove(0, 2));
                n = n + 1;
                ret = "V-" + n.ToString();
                if (reader.Read())
                    throw new Exception("too many rows has been readed!");
                
                reader.Close();

            }
            catch
            {
               
                ret = "V-1";
            }
            finally
            {

            }
            sql.Close();


            return ret;
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

        private void VodoForm_Load(object sender, EventArgs e)
        {
            string[] yl = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"Datas\Улицы.txt", Encoding.GetEncoding(1251));

            for (int i = 0; i< yl.Count();i++)
            {
                Ylicha.Items.Add(yl[i]);
            }
            
            
            
            metroTabControl1.SelectTab(0);
            if (tipes == 0)
            {
               
                Stats.ForeColor = Color.DarkGreen;
                Stats.Text = "новая";
                ReadGlav.Enabled = false;

            }
            else if (tipes == 1)
            {
               
                Dannie();
                TabObr();
                TabRab();
                SaveGlav.Enabled = false;
                ReadGlav.Enabled = true;

                if (Obsled.Rows.Count != 0)
                {
                    RedaktObr.Enabled = true;
                    DeleteObr.Enabled = true;
                }
                else
                {
                    RedaktObr.Enabled = false;
                    DeleteObr.Enabled = false;
                }



                if (Rabota.Rows.Count != 0)
                {
                    ReadRab.Enabled = true;
                    DeleteRab.Enabled = true;
                }
                else
                {
                    ReadRab.Enabled = false;
                    DeleteRab.Enabled = false;
                }

            }
            if (Stats.Text == "новая")
            {

                Stats.ForeColor = Color.DarkGreen;

            }
            else if (Stats.Text == "новая (ред.)")
            {

                Stats.ForeColor = Color.DarkGreen;

            }
            else if (Stats.Text == "обследована")
            {

                Stats.ForeColor = Color.DarkOrange;

            }
            else if (Stats.Text == "в работе")
            {

                Stats.ForeColor = Color.OrangeRed;

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

                string skript = string.Format("SELECT * FROM [Водопровод] WHERE [Номер] LIKE '{0}'", nomera);
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
                    Opisanie.Text = reader["Описание"].ToString();
                    VidYtechki.Text = reader["Вид утечки"].ToString();

                   maskedTextBox1.Text = reader["Дата поступления"].ToString();
                    
                    Stats.Text = reader["Статус"].ToString();
                    VidZaiavki.Text = reader["Вид заявки"].ToString();
                    Prinadlejn.Text = reader["Принадлежность"].ToString();
                    Otvetstv.Text = reader["Ответственный"].ToString();

                    maskedTextBox2.Text = reader["Закрытие"].ToString();
                   
                    
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
        async void TabObr()
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            string skript = string.Format("SELECT * FROM [ВодОбращ] WHERE [№ заявки] = '{0}'", nomera);

            try
            {
                await sql.OpenAsync();

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int q = Obsled.Rows.Add();
                    Obsled.Rows[q].Cells[0].Value = reader["№ обращения"].ToString();
                    Obsled.Rows[q].Cells[1].Value = reader["Диаметр"].ToString();
                    Obsled.Rows[q].Cells[2].Value = reader["Материал"].ToString();
                    Obsled.Rows[q].Cells[3].Value = reader["Вид повреждения"].ToString();
                    Obsled.Rows[q].Cells[4].Value = reader["Описание утечки"].ToString();
                    Obsled.Rows[q].Cells[5].Value = reader["Дата обследования"].ToString();
                    Obsled.Rows[q].Cells[6].Value = reader["Обследовал"].ToString();
                }
                reader.Close();
                sql.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

            }
        }

        async void TabRab()
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            string skript = string.Format("SELECT * FROM [ВодоРабота] WHERE [№ заявки] = '{0}'", nomera);

            try
            {
                await sql.OpenAsync();

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int q = Rabota.Rows.Add();
                    Rabota.Rows[q].Cells[0].Value = reader["№ работы"].ToString();
                    Rabota.Rows[q].Cells[1].Value = reader["Время начала"].ToString();
                    

                    if (reader["Разрытие"].ToString()== "разрытие")
                    {
                        Rabota.Rows[q].Cells[2].Value = true;
                        Rabota.Rows[q].Cells[3].Value = reader["Размер латки"].ToString();
                        Rabota.Rows[q].Cells[3].ReadOnly = false;

                    }
                    else if (reader["Разрытие"].ToString() == "Без разрытия")
                    {
                        Rabota.Rows[q].Cells[2].Value = false;
                        Rabota.Rows[q].Cells[3].ReadOnly = true;
                    }
                    

                    

                    if (reader["Отключения"].ToString() == "С отключением")
                    {
                        Rabota.Rows[q].Cells[4].Value = true;
                        Rabota.Rows[q].Cells[3].ReadOnly = false;
                        Rabota.Rows[q].Cells[5].Value = reader["Длина отключаемого участка"].ToString();
                    }
                    else if (reader["Отключения"].ToString() == "Без отключения")
                    {
                        Rabota.Rows[q].Cells[4].Value  = false;
                        Rabota.Rows[q].Cells[3].ReadOnly = true;
                    }
                    

                    
                    Rabota.Rows[q].Cells[6].Value = reader["Ответственный за проведение работ"].ToString();
                    Rabota.Rows[q].Cells[7].Value = reader["Описание работ"].ToString();
                    Rabota.Rows[q].Cells[8].Value = reader["Материал"].ToString();
                    Rabota.Rows[q].Cells[9].Value = reader["Диаметр"].ToString();
                    Rabota.Rows[q].Cells[10].Value = reader["Время завершения работ"].ToString();

                }
                reader.Close();
                sql.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            finally
            {

            }
        }
        
       
        private void Rabota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        int nomeRab;
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            nomeRab = Rabota.Rows.Add();
            Rabota.Rows[nomeRab].Cells[0].Value = nomerrab();
            SaveRab.Enabled = true;
            DobavRab.Enabled = false;
            ReadRab.Enabled = false;
            DeleteRab.Enabled = false;
            OtmenaRab.Enabled = true;
        }

        string nomerrab()
        {
            string q = "";

            try
            {
                string s = Rabota.Rows[nomeRab - 1].Cells[0].Value.ToString();
                int i = Convert.ToInt32(s.Remove(0, 3));
                q = "VR-" + (i + 1);
            }
            catch
            {
                q = "VR-1";
            }

            return q;
        }

        int nomeObsl;
        private void DobavObs_Click(object sender, EventArgs e)
        {
            nomeObsl = Obsled.Rows.Add();
            Obsled.Rows[nomeObsl].Cells[0].Value = nomerObsl();
            SaveObr.Enabled = true;
            DobavObs.Enabled = false;
            RedaktObr.Enabled = false;
            DeleteObr.Enabled = false;
            OtmenObs.Enabled = true;
        }

        string nomerObsl()
        {
            string nom = "";
            try
            {
                string s = Obsled.Rows[nomeObsl - 1].Cells[0].Value.ToString();
                int i = Convert.ToInt32(s.Remove(0, 3));
                nom = "VO-" + (i + 1);
            }
            catch
            {
                nom = "VO-1";
            }


            return nom;
        }

        private void SaveGlav_Click(object sender, EventArgs e)
        {
            SaveGlav.Enabled = false;
            ReadGlav.Enabled = true;
            Glav();
        }

        async void Glav()
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                string Adres = Ylicha.Text + "-" + Dom.Text;

                await sql.OpenAsync();

                string skript = "INSERT INTO [Водопровод] ([Номер],[Район],[Адрес],[Заявитель]," +
                    "[Описание],[Вид утечки],[Дата поступления],[Статус],[Вид заявки]," +
                    "[Принадлежность],[Ответственный]) " +
                    "VALUES (@Н,@Р,@А,@З,@О,@ВУ,@ДП,@С,@ВЗ,@П,@От);";
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@Н", nomera);
                command.Parameters.AddWithValue("@Р", Raion.Text);
                command.Parameters.AddWithValue("@А", Adres);
                command.Parameters.AddWithValue("@З", Zaiavit.Text);
                command.Parameters.AddWithValue("@О", Opisanie.Text);
                command.Parameters.AddWithValue("@ВУ", VidYtechki.Text);
                command.Parameters.AddWithValue("@ДП", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@С", Stats.Text);
                command.Parameters.AddWithValue("@ВЗ", VidZaiavki.Text);
                command.Parameters.AddWithValue("@П", Prinadlejn.Text);
                command.Parameters.AddWithValue("@От", Otvetstv.Text);

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



                string skript = string.Format("UPDATE [Водопровод] SET " +
                    "[Район] = @Р, [Адрес] = @А, " +
                    "[Заявитель] = @З, [Описание] = @О, [Вид утечки] = @ВУ, " +
                    "[Дата поступления] = @ДП, [Статус] = @С, " +
                    "[Вид заявки] = @ВЗ, [Принадлежность] = @П," +
                    "[Ответственный] = @От, " +
                    "[Закрытие] = @Зк WHERE [Номер] = '{0}'", nomera);

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                command.Parameters.AddWithValue("@Р", Raion.Text);
                command.Parameters.AddWithValue("@А", Adres);
                command.Parameters.AddWithValue("@З", Zaiavit.Text);
                command.Parameters.AddWithValue("@О", Opisanie.Text);
                command.Parameters.AddWithValue("@ВУ", VidYtechki.Text);
                command.Parameters.AddWithValue("@ДП", maskedTextBox1.Text);

                command.Parameters.AddWithValue("@ВЗ", VidZaiavki.Text);
                command.Parameters.AddWithValue("@П", Prinadlejn.Text);
                command.Parameters.AddWithValue("@От", Otvetstv.Text);

                if (checkBox1.Checked)
                {
                    Stats.ForeColor = Color.DarkRed;
                    Stats.Text = "закрыта";
                    command.Parameters.AddWithValue("@Зк", maskedTextBox2.Text);
                    command.Parameters.AddWithValue("@С", Stats.Text);
                }
                else
                {
                    if (Obsled.Rows.Count != 0)
                    {
                        if (Rabota.Rows.Count != 0)
                        {
                            Stats.ForeColor = Color.OrangeRed;
                            Stats.Text = "в работе";
                            command.Parameters.AddWithValue("@Зк", null);
                            command.Parameters.AddWithValue("@С", Stats.Text);
                        }
                        else
                        {
                            Stats.ForeColor = Color.DarkOrange;
                            Stats.Text = "обследована";
                            command.Parameters.AddWithValue("@Зк", null);
                            command.Parameters.AddWithValue("@С", Stats.Text);
                        }
                    }
                    else
                    {
                        if (Rabota.Rows.Count != 0)
                        {
                            Stats.ForeColor = Color.OrangeRed;
                            Stats.Text = "в работе";
                            command.Parameters.AddWithValue("@Зк", null);
                            command.Parameters.AddWithValue("@С", Stats.Text);
                        }
                        else
                        {
                            Stats.ForeColor = Color.DarkGreen;
                            Stats.Text = "новая (ред.)";
                            command.Parameters.AddWithValue("@Зк", null);
                            command.Parameters.AddWithValue("@С", Stats.Text);
                        }
                        
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

        private async void SaveRab_Click(object sender, EventArgs e)
        {
            SaveRab.Enabled = false;

            bool bol = (bool)(Rabota.Rows[nomeRab].Cells[4]).EditedFormattedValue;
            bool b = (bool)(Rabota.Rows[nomeRab].Cells[2]).EditedFormattedValue;

            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                await sql.OpenAsync();

                string skript = "INSERT INTO [ВодоРабота] ([№ заявки],[№ работы],[Время начала]," +
                    "[Разрытие],[Размер латки],[Отключения],[Длина отключаемого участка]," +
                    "[Ответственный за проведение работ],[Описание работ],[Материал],[Диаметр]," +
                    "[Время завершения работ]) VALUES (@№З,@№Р,@ВН,@Р,@РЛ,@О,@ДОУ,@ОПР,@ОР,@М,@Д,@ВЗР);";
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@№З", nomera);
                command.Parameters.AddWithValue("@№Р", Rabota.Rows[nomeRab].Cells[0].Value);
                command.Parameters.AddWithValue("@ВН", Rabota.Rows[nomeRab].Cells[1].Value);
               

                if (b == true)
                {
                    command.Parameters.AddWithValue("@Р", "разрытие");
                }
                else if (b == false)
                {
                    command.Parameters.AddWithValue("@Р", "Без разрытия");
                }
                

                command.Parameters.AddWithValue("@РЛ", Rabota.Rows[nomeRab].Cells[3].Value);

                if (bol == true)
                {
                    command.Parameters.AddWithValue("@О", "С отключением");
                }
                else if (bol == false)
                {
                    command.Parameters.AddWithValue("@О", "Без отключения");
                }

                command.Parameters.AddWithValue("@ДОУ", Rabota.Rows[nomeRab].Cells[5].Value);
                command.Parameters.AddWithValue("@ОПР", Rabota.Rows[nomeRab].Cells[6].Value);
                command.Parameters.AddWithValue("@ОР", Rabota.Rows[nomeRab].Cells[7].Value);
                command.Parameters.AddWithValue("@М", Rabota.Rows[nomeRab].Cells[8].Value);
                command.Parameters.AddWithValue("@Д", Rabota.Rows[nomeRab].Cells[9].Value);
                command.Parameters.AddWithValue("@ВЗР", Rabota.Rows[nomeRab].Cells[10].Value);
                if (status == "закрыта")
                {
                    status = "закрыта";
                }
                else
                {
                    status = "в работе";
                }

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '{0}'", sql);

                if (Stats.Text == "новая")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.DarkGreen;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "новая (ред.)")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.DarkGreen;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "обследована")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.DarkOrange;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "в работе")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.OrangeRed;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "закрыта")
                {
                    Stats.Text = "закрыта";
                    Stats.ForeColor = Color.Red;
                    comman.Parameters.AddWithValue("@С", Stats.Text);

                }
            }

            DobavRab.Enabled = true;
            ReadRab.Enabled = true;
            DeleteRab.Enabled = true;
            OtmenaRab.Enabled = true;
        }

        private async void SaveObr_Click(object sender, EventArgs e)
        {

            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                await sql.OpenAsync();

                string skript = "INSERT INTO [ВодОбращ] ([№ заявки],[№ обращения],[Диаметр],[Материал]," +
                    "[Вид повреждения],[Описание утечки],[Дата обследования],[Обследовал]) VALUES (@№З,@№Об,@Д,@М,@ВП,@ОУ,@ДО,@О);";
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@№З", nomera);
                command.Parameters.AddWithValue("@№Об", Obsled.Rows[nomeObsl].Cells[0].Value);
                command.Parameters.AddWithValue("@Д", Obsled.Rows[nomeObsl].Cells[1].Value);
                command.Parameters.AddWithValue("@М", Obsled.Rows[nomeObsl].Cells[2].Value);
                command.Parameters.AddWithValue("@ВП", Obsled.Rows[nomeObsl].Cells[3].Value);
                command.Parameters.AddWithValue("@ОУ", Obsled.Rows[nomeObsl].Cells[4].Value);
                command.Parameters.AddWithValue("@ДО", Obsled.Rows[nomeObsl].Cells[5].Value);
                command.Parameters.AddWithValue("@О", Obsled.Rows[nomeObsl].Cells[6].Value);


                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '"+nomera+"'", sql);

                if (Stats.Text == "новая")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "новая (ред.)")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "обследована")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "в работе")
                {
                    Stats.ForeColor = Color.OrangeRed;
                    Stats.Text = "в работе";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "закрыта")
                {
                    Stats.ForeColor = Color.Red;
                    Stats.Text = "закрыта";
                    comman.Parameters.AddWithValue("@С", Stats.Text);

                }

                comman.ExecuteNonQuery();
            }
            SaveObr.Enabled = false;
            RedaktObr.Enabled = true;
            DeleteObr.Enabled = true;
            DobavObs.Enabled = true;
            OtmenObs.Enabled = true;
        }

        private async void RedaktObr_Click(object sender, EventArgs e)
        {
            
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            await sql.OpenAsync();
            try
            {
                

                string skript = string.Format("UPDATE [ВодОбращ] SET [№ обращения] = @№Об,[Диаметр] = @Д,[Материал] = @М," +
                    "[Вид повреждения] = @ВП,[Описание утечки] = @ОУ,[Дата обследования] = @ДО ,[Обследовал] = @О  WHERE [№ заявки] = '{0}' AND [№ обращения] = '{1}';", nomera,nom);
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@№З", nomera);
                command.Parameters.AddWithValue("@№Об", Obsled.Rows[nomes].Cells[0].Value);
                command.Parameters.AddWithValue("@Д", Obsled.Rows[nomes].Cells[1].Value);
                command.Parameters.AddWithValue("@М", Obsled.Rows[nomes].Cells[2].Value);
                command.Parameters.AddWithValue("@ВП", Obsled.Rows[nomes].Cells[3].Value);
                command.Parameters.AddWithValue("@ОУ", Obsled.Rows[nomes].Cells[4].Value);
                command.Parameters.AddWithValue("@ДО", Obsled.Rows[nome].Cells[5].Value);
                command.Parameters.AddWithValue("@О", Obsled.Rows[nome].Cells[6].Value);


                await command.ExecuteNonQueryAsync();
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                if (Stats.Text == "новая")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "новая (ред.)")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "обследована")
                {
                    Stats.ForeColor = Color.DarkOrange;
                    Stats.Text = "обследована";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "в работе")
                {
                    Stats.ForeColor = Color.OrangeRed;
                    Stats.Text = "в работе";
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "закрыта")
                {
                    Stats.ForeColor = Color.Red;
                    Stats.Text = "закрыта";
                    comman.Parameters.AddWithValue("@С", Stats.Text);

                }
                comman.ExecuteNonQuery();
                ReadGlav_Click(null, null);
            }
        }

        private async void DeleteObr_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            await sql.OpenAsync();

            try
            {
                string row = Obsled.CurrentRow.Cells[0].Value.ToString();   
                string skript = string.Format("DELETE FROM [ВодОбращ] WHERE [№ обращения] = '{1}' AND [№ заявки] = '{0}'", nomera, row);

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                await command.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                if (Obsled.Rows.Count != 0)
                {
                   
                    try
                    {
                        Obsled.Rows.Remove(Obsled.CurrentRow);
                        if (Obsled.Rows.Count == 0)
                        {

                            if (Stats.Text == "обследована")
                            {

                                Stats.Text = "новая (ред.)";

                            }

                            SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                            comman.Parameters.AddWithValue("@С", Stats.Text);

                            await comman.ExecuteNonQueryAsync();

                        }
                        else
                        {

                        }


                        if (Rabota.Rows.Count == 0)
                        {

                            if (Stats.Text == "в работе")
                            {

                                Stats.Text = "обследована";

                            }

                            SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                            comman.Parameters.AddWithValue("@С", Stats.Text);

                            await comman.ExecuteNonQueryAsync();

                        }
                        else
                        {

                        }

                        if (Stats.Text == "новая")
                        {

                            Stats.ForeColor = Color.DarkGreen;

                        }
                        else if (Stats.Text == "новая (ред.)")
                        {

                            Stats.ForeColor = Color.DarkGreen;

                        }
                        else if (Stats.Text == "обследована")
                        {

                            Stats.ForeColor = Color.GreenYellow;

                        }
                        else if (Stats.Text == "в работе")
                        {

                            Stats.ForeColor = Color.DarkOrange;

                        }
                        else if (Stats.Text == "закрыта")
                        {
                            Stats.ForeColor = Color.DarkRed;

                        }


                    }
                    catch(Exception ex)
                    {
                        MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

                
            }

            if (Obsled.Rows.Count != 0)
            {

                RedaktObr.Enabled = true;
                DeleteObr.Enabled = true;

            }
            else if(Obsled.Rows.Count == 0)
            {

                RedaktObr.Enabled = false;
                DeleteObr.Enabled = false;

            }
            ReadGlav_Click(null, null);

        }

        int nomerOb;
        private void Obsled_Click(object sender, EventArgs e)
        {
            try
            {
                nomerOb = Obsled.CurrentRow.Index;
            }
            catch
            {

            }

        }

        private async void ReadRab_Click(object sender, EventArgs e)
        {
           
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            try
            {
                await sql.OpenAsync();

                string skript = string.Format("UPDATE [ВодоРабота] SET [№ работы]=@№Р ,[Время начала]=@ВН," +
                    "[Разрытие]=@Р,[Размер латки]=@РЛ,[Отключения]=@О,[Длина отключаемого участка]=@ДОУ," +
                    "[Ответственный за проведение работ]=@ОПР,[Описание работ]=@ОР,[Материал]=@М,[Диаметр]=@Д," +
                    "[Время завершения работ]=@ВЗР WHERE [№ заявки] = '{0}' AND [№ работы] = '{1}';", nomera,nom);
                SQLiteCommand command = new SQLiteCommand(skript, sql);
                command.Parameters.AddWithValue("@№Р", Rabota.Rows[nome].Cells[0].Value);
                command.Parameters.AddWithValue("@ВН", Rabota.Rows[nome].Cells[1].Value);
                
                if (Convert.ToBoolean(Rabota.CurrentRow.Cells[2].EditedFormattedValue) == true)
                {
                    command.Parameters.AddWithValue("@Р", "разрытие");
                }
                else if (Convert.ToBoolean(Rabota.CurrentRow.Cells[2].EditedFormattedValue)==false)
                {
                    command.Parameters.AddWithValue("@Р", "Без разрытия");
                }
                command.Parameters.AddWithValue("@РЛ", Rabota.Rows[nome].Cells[3].Value.ToString());
                if (Convert.ToBoolean(Rabota.CurrentRow.Cells[4].EditedFormattedValue) == true)
                {
                    command.Parameters.AddWithValue("@О", "С отключением");
                }
                else if (Convert.ToBoolean(Rabota.CurrentRow.Cells[4].EditedFormattedValue) == false)
                {
                    command.Parameters.AddWithValue("@О", "Без отключения");
                }
                command.Parameters.AddWithValue("@ДОУ", Rabota.Rows[nome].Cells[5].Value);
                command.Parameters.AddWithValue("@ОПР", Rabota.Rows[nome].Cells[6].Value);
                command.Parameters.AddWithValue("@ОР", Rabota.Rows[nome].Cells[7].Value);
                command.Parameters.AddWithValue("@М", Rabota.Rows[nome].Cells[8].Value);
                command.Parameters.AddWithValue("@Д", Rabota.Rows[nome].Cells[9].Value);
                command.Parameters.AddWithValue("@ВЗР", Rabota.Rows[nome].Cells[10].Value);


                await command.ExecuteNonQueryAsync();
            }
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            finally
            {
                SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                if (Stats.Text == "новая")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.OrangeRed;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "новая (ред.)")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.OrangeRed;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "обследована")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.OrangeRed;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "в работе")
                {

                    Stats.Text = "в работе";
                    Stats.ForeColor = Color.OrangeRed;
                    comman.Parameters.AddWithValue("@С", Stats.Text);
                }
                else if (Stats.Text == "закрыта")
                {
                    Stats.Text = "закрыта";
                    Stats.ForeColor = Color.Red;
                    comman.Parameters.AddWithValue("@С", Stats.Text);

                }

                await comman.ExecuteNonQueryAsync();
                ReadGlav_Click(null, null);
            }
        }

        private void Obsled_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            object value = Obsled.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (!((DataGridViewComboBoxColumn)Obsled.Columns[e.ColumnIndex]).Items.Contains(value))
            {
                ((DataGridViewComboBoxColumn)Obsled.Columns[e.ColumnIndex]).Items.Add(value);
                e.ThrowException = false;



            }
        }

       
        private void Rabota_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool bol = (bool)(Rabota.Rows[e.RowIndex].Cells[4]).EditedFormattedValue;
                bool b = (bool)(Rabota.Rows[e.RowIndex].Cells[2]).EditedFormattedValue;
                if (Convert.ToBoolean(Rabota.Rows[e.RowIndex].Cells[2].EditedFormattedValue) == true)
                {

                    Rabota.Rows[e.RowIndex].Cells[3].ReadOnly = false;

                }
                else if (Convert.ToBoolean(Rabota.Rows[e.RowIndex].Cells[2].EditedFormattedValue) == false)
                {
                    Rabota.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                    Rabota.Rows[e.RowIndex].Cells[3].Value = "";
                }

                if (bol == true)
                {

                    Rabota.Rows[e.RowIndex].Cells[5].ReadOnly = false;

                }
                else if (bol == false)
                {
                    Rabota.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    Rabota.Rows[e.RowIndex].Cells[5].Value = "";
                }
            }
            catch
            {

            }
            
        }

        private async void DeleteRab_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(Form1.nameData, true);
            await sql.OpenAsync();

            try
            {
                string row = Rabota.CurrentRow.Cells[0].Value.ToString();
                string skript = string.Format("DELETE FROM [ВодоРабота] WHERE [№ работы] = '{1}' AND [№ заявки] = '{0}'", nomera, row);

                SQLiteCommand command = new SQLiteCommand(skript, sql);

                await command.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                if (Rabota.Rows.Count != 0)
                {
                    Rabota.Rows.Remove(Rabota.CurrentRow);
                    if (Rabota.Rows.Count == 0)
                    {

                        if (Stats.Text == "в работе")
                        {
                            Stats.ForeColor = Color.DarkSeaGreen;
                            Stats.Text = "обследована";

                        }

                        SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                        comman.Parameters.AddWithValue("@С", Stats.Text);

                        await comman.ExecuteNonQueryAsync();

                    }
                    else
                    {

                    }


                    if (Obsled.Rows.Count == 0)
                    {

                        if (Stats.Text == "обследована")
                        {
                            Stats.ForeColor = Color.DarkGreen;
                            Stats.Text = "новая (ред.)";

                        }

                        SQLiteCommand comman = new SQLiteCommand("UPDATE[Водопровод] SET [Статус] = @С WHERE [Номер] = '" + nomera + "'", sql);

                        comman.Parameters.AddWithValue("@С", Stats.Text);

                        await comman.ExecuteNonQueryAsync();

                    }
                    else
                    {

                    }

                    if (Stats.Text == "новая")
                    {

                        Stats.ForeColor = Color.DarkGreen;

                    }
                    else if (Stats.Text == "новая (ред.)")
                    {

                        Stats.ForeColor = Color.DarkSeaGreen;

                    }
                    else if (Stats.Text == "обследована")
                    {

                        Stats.ForeColor = Color.GreenYellow;

                    }
                    else if (Stats.Text == "в работе")
                    {

                        Stats.ForeColor = Color.DarkOrange;

                    }
                    else if (Stats.Text == "закрыта")
                    {
                        Stats.ForeColor = Color.DarkRed;

                    }
                }
              
                
            }

            if (Rabota.Rows.Count != 0)
            {
                ReadRab.Enabled = true;
                DeleteRab.Enabled = true;
            }
            else if(Rabota.Rows.Count == 0)
            {
                ReadRab.Enabled = false;
                DeleteRab.Enabled = false;
            }

        }

        private void Rabota_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Rabota.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Cell_KeyPress);
            }
            else if (Rabota.CurrentCell.ColumnIndex == 5)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Cell_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= (Cell_KeyPress);
            }
            
        }

        private void Cell_KeyPress(object Sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.KeyChar = Convert.ToChar("\0");
        }

        private void Rabota_DoubleClick(object sender, EventArgs e)
        {
            int y = Rabota.CurrentRow.Index;
            if (Rabota.CurrentCell.ColumnIndex == Rabota.CurrentRow.Cells[1].ColumnIndex)
            {
                int q = Rabota.Columns[1].Index;
                string ssd = "";
                if (Rabota.CurrentRow.Cells[1].Value != null)
                {
                    ssd = Rabota.CurrentRow.Cells[1].Value.ToString();
                }

                DatTime form = new DatTime("Vodo", y, q, this, ssd, "data", 0);

                

                form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

                form.ShowDialog();
            }
            if (Rabota.CurrentCell.ColumnIndex == Rabota.CurrentRow.Cells[10].ColumnIndex)
            {
                int q = Rabota.Columns[10].Index;
                string ssd = "";
                if (Rabota.CurrentRow.Cells[10].Value != null)
                {
                    ssd = Rabota.CurrentRow.Cells[10].Value.ToString();
                }

                DatTime form = new DatTime("Vodo", y, q, this, ssd, "data", 0);



                form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

                form.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
            }
        }

        private void Obsled_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Obsled.CurrentCell.ColumnIndex == 1)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Cell_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= (Cell_KeyPress);
            }
        }
        int nome = 0;
        string nom = "";
        int nomes = 0;
        private void Rabota_Click(object sender, EventArgs e)
        {
            try
            {
                nom = Rabota.CurrentRow.Cells[0].Value.ToString();
                nome = Rabota.CurrentRow.Index;
            }
            catch
            {

            }
            
        }

        
        private void Obsled_Click_1(object sender, EventArgs e)
        {
            try
            {
                nom = Obsled.CurrentRow.Cells[0].Value.ToString();
                nomes = Obsled.CurrentRow.Index;
            }
            catch
            {
                
            }
            
        }

        private void maskedTextBox2_DoubleClick(object sender, EventArgs e)
        {
            DatTime form = new DatTime("Vodo", 0, 0, this, maskedTextBox2.Text, "text", 1);



            form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            form.ShowDialog();
        }

        private void maskedTextBox1_DoubleClick(object sender, EventArgs e)
        {
            DatTime form = new DatTime("Vodo", 0, 0, this, maskedTextBox1.Text, "text", 0);



            form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SaveGlav.Enabled == true)
            {
                nomera = nomers();
                label5.Text = nomera;
                Nomer.Text = nomera;
            }
            else
            {
                label5.Text = nomera;
                Nomer.Text = nomera;
            }
            
        }

        private void Obsled_DoubleClick(object sender, EventArgs e)
        {
            int y = Obsled.CurrentRow.Index;
            if (Obsled.CurrentCell.ColumnIndex == Obsled.CurrentRow.Cells[5].ColumnIndex)
            {
                int q = Obsled.Columns[5].Index;
                string ssd = "";
                if (Obsled.CurrentRow.Cells[5].Value != null)
                {
                    ssd = Obsled.CurrentRow.Cells[5].Value.ToString();
                }

                DatTime form = new DatTime("Vodo", y, q, this, ssd, "data", 1);



                form.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

                form.ShowDialog();
            }
        }

        private void OtmenObs_Click(object sender, EventArgs e)
        {
            Obsled.Rows.Remove(Obsled.Rows[nomeObsl]);
            OtmenObs.Enabled = false;
            SaveObr.Enabled = false;
            RedaktObr.Enabled = true;
            DeleteObr.Enabled = true;
            DobavObs.Enabled = true;
            
        }

        private void OtmenaRab_Click(object sender, EventArgs e)
        {
            Rabota.Rows.Remove(Rabota.Rows[nomeRab]);
            OtmenaRab.Enabled = false;
            SaveRab.Enabled = false;
            ReadRab.Enabled = true;
            DeleteRab.Enabled = true;
            DobavRab.Enabled = true;
        }

        private void Rabota_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            object value = Rabota.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (!((DataGridViewComboBoxColumn)Rabota.Columns[e.ColumnIndex]).Items.Contains(value))
            {
                ((DataGridViewComboBoxColumn)Rabota.Columns[e.ColumnIndex]).Items.Add(value);
                e.ThrowException = false;



            }
        }
    }
}
