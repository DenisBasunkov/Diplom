using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Диспетчерская
{
    public partial class GlavForm : Form
    {
        string profName, name;

        private void GlavForm_Load(object sender, EventArgs e)
        {
            

            NameTxt.Text = name;
            ProfTxt.Text = profName;
            NameTxt.Focus();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            File.Create(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + name + ".txt").Close();
            tim();
            Environment.Exit(0);
        }

        Vhod form = new Vhod();
        async void tim()
        {

            if (name == "Админ")
            {
                SQLiteConnection sql = new SQLiteConnection(form.nameData);

                await sql.OpenAsync();
                SQLiteCommand commands = new SQLiteCommand($"DELETE FROM [Datas] WHERE User = '{name}'", sql);
                commands.ExecuteNonQuery();
            }
            else
            {
                SQLiteConnection sql = new SQLiteConnection(form.nameData);

                await sql.OpenAsync();



                try
                {
                    // Set [Время выхода] 
                    string skript = string.Format(" UPDATE [{0}] SET [Время выхода] = @datas WHERE [Время выхода] = '@'", name);
                    SQLiteCommand command = new SQLiteCommand(skript, sql);
                    command.Parameters.AddWithValue("@datas", DateTime.Now.ToString());



                    command.ExecuteNonQuery();
                    SQLiteCommand commands = new SQLiteCommand($"DELETE FROM [Datas] WHERE User = '{name}'", sql);
                    commands.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {


                }
            }

        }

        private void MenuBut_Click(object sender, EventArgs e)
        {
            if (Menu.Visible)
            {
                TransMenu.HideSync(Menu);
            }
            else
            {
                TransMenu.ShowSync(Menu);
            }
        }

        private void exiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выйтиИзАкаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            File.Create(AppDomain.CurrentDomain.BaseDirectory + @"Datas\" + name + ".txt").Close();
            tim();

            Vhod form1 = new Vhod();
            form1.Show();
            this.Hide();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            metroContextMenu1.Show(bunifuImageButton3, new Point(0, bunifuImageButton3.Height));
        }

        private void перезагрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour < 10)
            {
                maskedTextBox1.Text = DateTime.Now.ToShortDateString() + "0" + DateTime.Now.ToLongTimeString();
                //Setting.Focus();

            }
            else
            {
                maskedTextBox1.Text = DateTime.Now.ToShortDateString() + "" + DateTime.Now.ToLongTimeString();
                //Setting.Focus();
            }
        }

        Administrator fowrm;
        private void AdForm_Click(object sender, EventArgs e)
        {
            if (fowrm == null || fowrm.IsDisposed)
            {
                fowrm = new Administrator();
                fowrm.Show();
            }
            else
            {
                MetroMessageBox.Show(this, string.Format("{0,100}", "Форма уже открыта"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        Vodoprovod form1 = null;
        Kanalizatia kanalizatia = null;
        Obrajhenia obrajhenia = null;


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (form1 == null || form1.IsDisposed)
                {

                    if (kanalizatia == null || kanalizatia.IsDisposed)
                    {


                    }
                    else if (kanalizatia != null || !kanalizatia.IsDisposed)
                    {
                        kanalizatia.Close();
                    }
                    if (obrajhenia == null || obrajhenia.IsDisposed)
                    {

                    }
                    else if (obrajhenia != null || !obrajhenia.IsDisposed)
                    {
                        obrajhenia.Close();
                    }

                    metroProgressSpinner1.Value = 0;
                    metroProgressSpinner1.Visible = true;

                    timer2.Enabled = true;
                    TransMenu.HideSync(Menu);

                    form1 = new Vodoprovod(profName, name);
                    int x = panel1.Width;
                    int y = panel1.Height;
                    form1.Dock = DockStyle.Fill;
                    //form.Size = new Size(x, y);
                    form1.FormBorderStyle = FormBorderStyle.None;
                    form1.TopLevel = false;
                    form1.AutoScroll = true;
                    panel1.Controls.Add(form1);
                    form1.Show();
                }
                else
                {
                    MetroMessageBox.Show(this, "Окно уже открыто", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            finally
            {

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (form1 == null || form1.IsDisposed)
                {
                    metroProgressSpinner1.Value += 10;
                    
                }
                else
                {
                    timer2.Enabled = false;
                    metroProgressSpinner1.Visible = false;
                    metroProgressSpinner1.Value = 0;
                    

                }


            }
            catch
            {

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (kanalizatia == null || kanalizatia.IsDisposed)
                {
                    if (form1 == null || form1.IsDisposed)
                    {





                    }
                    else if (form1 != null || !form1.IsDisposed)
                    {

                        form1.Close();
                    }

                    if (obrajhenia == null || obrajhenia.IsDisposed)
                    {



                    }
                    else if (obrajhenia != null || !obrajhenia.IsDisposed)
                    {

                        obrajhenia.Close();
                    }


                    metroProgressSpinner1.Value = 0;
                    metroProgressSpinner1.Visible = true;

                    timer3.Enabled = true;

                    TransMenu.HideSync(Menu);

                    kanalizatia = new Kanalizatia(profName, name);
                    int x = panel1.Width;
                    int y = panel1.Height;
                    kanalizatia.Dock = DockStyle.Fill;
                    //form.Size = new Size(x, y);
                    kanalizatia.FormBorderStyle = FormBorderStyle.None;
                    kanalizatia.TopLevel = false;
                    kanalizatia.AutoScroll = true;
                    panel1.Controls.Add(kanalizatia);
                    kanalizatia.Show();
                }
                else
                {
                    MetroMessageBox.Show(this, "Окно уже открыто", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            finally
            {

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (kanalizatia == null || kanalizatia.IsDisposed)
                {
                    this.Cursor = Cursors.WaitCursor;
                    metroProgressSpinner1.Value += 10;
                }
                else
                {
                    timer3.Enabled = false;
                    metroProgressSpinner1.Visible = false;
                    metroProgressSpinner1.Value = 0;
                    this.Cursor = Cursors.Default;
                }


            }
            catch
            {

            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                if (obrajhenia == null || obrajhenia.IsDisposed)
                {
                    this.Cursor = Cursors.WaitCursor;
                    metroProgressSpinner1.Value += 10;
                }
                else
                {
                    timer4.Enabled = false;
                    metroProgressSpinner1.Visible = false;
                    metroProgressSpinner1.Value = 0;
                    this.Cursor = Cursors.Default;
                }


            }
            catch
            {

            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {


                if (obrajhenia == null || obrajhenia.IsDisposed)
                {

                    if (form1 == null || form1.IsDisposed)
                    {


                    }
                    else if (form1 != null || !form1.IsDisposed)
                    {
                        form1.Close();
                    }
                    if (kanalizatia == null || kanalizatia.IsDisposed)
                    {


                    }
                    else if (kanalizatia != null || !kanalizatia.IsDisposed)
                    {
                        kanalizatia.Close();
                    }


                    metroProgressSpinner1.Value = 0;
                    metroProgressSpinner1.Visible = true;

                    timer4.Enabled = true;
                    TransMenu.HideSync(Menu);

                    obrajhenia = new Obrajhenia(name);
                    int x = panel1.Width;
                    int y = panel1.Height;
                    obrajhenia.Dock = DockStyle.Fill;
                    //form.Size = new Size(x, y);
                    obrajhenia.FormBorderStyle = FormBorderStyle.None;
                    obrajhenia.TopLevel = false;
                    obrajhenia.AutoScroll = true;
                    panel1.Controls.Add(obrajhenia);
                    obrajhenia.Show();
                }
                else
                {
                    MetroMessageBox.Show(this, "Окно уже открыто", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            finally
            {

            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(profName);
            form2.Show();
        }

        public GlavForm(string prof, string names)
        {
            InitializeComponent();
            profName = prof;
            name = names;
            if (profName == "Admin")
            {
                AdForm.Visible = true;
            }
        }
    }
}
