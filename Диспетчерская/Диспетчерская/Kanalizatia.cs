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
    public partial class Kanalizatia : Form
    {
        string profname, names;
        public Kanalizatia(string prof, string name)
        {
            InitializeComponent();
            profname = prof;
            names = name;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Vhod Form1 = new Vhod();
        private void Kanalizatia_Load(object sender, EventArgs e)
        {
            combox();


            bunifuImageButton1.Visible = true;


            bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuCustomDataGrid1.ReadOnly = true;
            bunifuCustomDataGrid1.Enabled = true;
            string customers = "SELECT [Номер],[Район],[Адрес],[Заявитель],[Описание],[Вид утечки],[Дата поступления],[Статус],[Закрытие] FROM [Канализация]";

            SQLiteConnection con = new SQLiteConnection(Form1.nameData, true);

            try
            {
                DataSet ds = new DataSet();

                SQLiteDataAdapter da = new SQLiteDataAdapter(customers, con);
                da.Fill(ds, "Customers");
                bunifuCustomDataGrid1.AutoGenerateColumns = true;
                bunifuCustomDataGrid1.DataSource = ds;

                bunifuCustomDataGrid1.DataMember = "Customers";
                for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                {
                    bunifuCustomDataGrid1[7, i].Style.BackColor = Color.DimGray;
                    if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "закрыта")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Red;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "обследована")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Orange;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "в работе")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.OrangeRed;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая (ред.)")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Kanalizatia_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
            {
                bunifuCustomDataGrid1[7, i].Style.BackColor = Color.DimGray;
                if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "закрыта")
                {
                    bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Red;
                }
                else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "обследована")
                {
                    bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Orange;
                }
                else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "в работе")
                {
                    bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.OrangeRed;
                }
                else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая")
                {
                    bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                }
                else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая (ред.)")
                {
                    bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                }

            }
        }
        CheckBox check;
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            datt();
            bunifuTextbox1.Text = "Поиск";
            metroComboBox1.PromptText = "";
        }

        string q;

        async void combox()
        {
            metroComboBox1.Items.Clear();

            string customers = "PRAGMA table_info('Канализация');";

            SQLiteConnection con = new SQLiteConnection(Form1.nameData, true);

            try
            {
                await con.OpenAsync();
                SQLiteCommand command = new SQLiteCommand(customers, con);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == "ID")
                        {

                        }
                        else
                        {
                            metroComboBox1.Items.Add(reader.GetString(1));
                        }


                    }
                }

            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            finally
            {
                con.Close();

            }

        }

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
            string o = bunifuCustomDataGrid1.CurrentRow.Cells[0].Value.ToString();
            
            KanalForm form = new KanalForm(1, o, names);
            form.Show();
        }

        string aich;

        private void bunifuTextbox1_TextChanged(object sender, EventArgs e)
        {
            string customers;
            if (bunifuTextbox1.Text.Equals("Поиск"))
            {
                customers = "SELECT [Номер],[Район],[Адрес],[Заявитель],[Описание],[Вид утечки],[Дата поступления],[Статус],[Закрытие] FROM [Канализация]";

            }
            else
            {
                customers = string.Format("SELECT [Номер],[Район],[Адрес],[Заявитель],[Описание],[Вид утечки],[Дата поступления],[Статус],[Закрытие] FROM [Канализация] WHERE [{0}] LIKE '%{1}%';", aich, bunifuTextbox1.Text);

            }




            bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuCustomDataGrid1.ReadOnly = true;
            bunifuCustomDataGrid1.Enabled = true;

            SQLiteConnection con = new SQLiteConnection(Form1.nameData, true);

            try
            {
                DataSet ds = new DataSet();

                SQLiteDataAdapter da = new SQLiteDataAdapter(customers, con);
                da.Fill(ds, "Customers");
                bunifuCustomDataGrid1.AutoGenerateColumns = true;
                bunifuCustomDataGrid1.DataSource = ds;

                bunifuCustomDataGrid1.DataMember = "Customers";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                {
                    bunifuCustomDataGrid1[7, i].Style.BackColor = Color.DimGray;
                    if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "закрыта")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Red;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "обследована")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Orange;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "в работе")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.OrangeRed;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая (ред.)")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }

                }
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            aich = metroComboBox1.SelectedItem.ToString();
            if (aich == "Id")
            {
                bunifuTextbox1.Visible = false;
                bunifuImageButton4.Visible = false;
                MetroMessageBox.Show(this, "По данному полю нельзя сделать запрос", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bunifuImageButton4.Visible = true;
                bunifuTextbox1.Visible = true;

            }
        }

        private void bunifuTextbox1_Enter(object sender, EventArgs e)
        {
            if (!bunifuTextbox1.Text.Equals(""))
            {
                bunifuTextbox1.Text = "";
            }
        }

        private void bunifuTextbox1_Leave(object sender, EventArgs e)
        {
            if (bunifuTextbox1.Text.Equals(""))
            {
                bunifuTextbox1.Text = "Поиск";
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string nomer = "";
            KanalForm form = new KanalForm(0, nomer, names);
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            datt();
        }

        public void datt()
        {
            bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuCustomDataGrid1.ReadOnly = true;
            bunifuCustomDataGrid1.Enabled = true;
            string customers;
            if (bunifuTextbox1.Text.Equals("Поиск"))
            {
                customers = "SELECT [Номер],[Район],[Адрес],[Заявитель],[Описание],[Вид утечки],[Дата поступления],[Статус],[Закрытие] FROM [Канализация]";

            }
            else
            {
                customers = string.Format("SELECT [Номер],[Район],[Адрес],[Заявитель],[Описание],[Вид утечки],[Дата поступления],[Статус],[Закрытие] FROM [Канализация] WHERE [{0}] LIKE '%{1}%';", aich, bunifuTextbox1.Text);

            }

            SQLiteConnection con = new SQLiteConnection(Form1.nameData, true);

            try
            {
                DataSet ds = new DataSet();

                SQLiteDataAdapter da = new SQLiteDataAdapter(customers, con);
                da.Fill(ds, "Customers");
                bunifuCustomDataGrid1.AutoGenerateColumns = true;
                bunifuCustomDataGrid1.DataSource = ds;

                bunifuCustomDataGrid1.DataMember = "Customers";
                for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                {
                    bunifuCustomDataGrid1[7, i].Style.BackColor = Color.DimGray;
                    if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "закрыта")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Red;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "обследована")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Orange;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "в работе")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.OrangeRed;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая (ред.)")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
            {
                //vodo.bunifuCustomDataGrid1.Rows[i].Selected = false;
                try
                {
                    if (bunifuCustomDataGrid1.Rows[i].Cells[0].Value != null)
                        if (bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString().Contains(q))
                        {
                            bunifuCustomDataGrid1.Rows[i].Selected = true;

                            bunifuCustomDataGrid1.CurrentCell = bunifuCustomDataGrid1[0, i];


                            break;
                        }
                }
                catch
                {

                    bunifuCustomDataGrid1.Rows[i].Selected = true;

                    bunifuCustomDataGrid1.CurrentCell = bunifuCustomDataGrid1[0, 0];


                    break;

                }

            }

            if (checkBox1.Checked)
            {
                bunifuCustomDataGrid1.CurrentCell = null;

                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox1.Text)
                        {
                            row.Visible = false;

                        }
                    }

                }
            }
            else
            {
                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox1.Text)
                        {
                            row.Visible = true;
                        }
                    }

                }
            }

            if (checkBox2.Checked)
            {
                bunifuCustomDataGrid1.CurrentCell = null;

                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox2.Text)
                        {
                            row.Visible = false;

                        }
                    }

                }
            }
            else
            {
                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox2.Text)
                        {
                            row.Visible = true;
                        }
                    }

                }
            }

            if (checkBox3.Checked)
            {
                bunifuCustomDataGrid1.CurrentCell = null;

                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox3.Text)
                        {
                            row.Visible = false;

                        }
                    }

                }
            }
            else
            {
                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value).ToString() == checkBox3.Text)
                        {
                            row.Visible = true;
                        }
                    }

                }
            }


            if (checkBox4.Checked)
            {
                bunifuCustomDataGrid1.CurrentCell = null;

                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                {
                    if (row.Cells[7].Value != null)
                    {
                        if ((row.Cells[7].Value.ToString() == checkBox4.Text) | (row.Cells[7].Value.ToString() == checkBox4.Text + " (ред.)"))
                        {
                            row.Visible = false;
                        }
                    }

                }
            }
            else
            {
                foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                        {
                            if (row.Cells[7].Value != null)
                            {
                                if ((row.Cells[7].Value.ToString() == checkBox4.Text) | (row.Cells[7].Value.ToString() == checkBox4.Text + " (ред.)"))
                                {

                                    row.Visible = true;
                                }
                            }

                        }
            }




        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            KanalOtch otch = new KanalOtch();
            otch.Show();
        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bunifuCustomDataGrid1.CurrentCell = null;

                check = (CheckBox)sender;
                if (check.Checked)
                {

                    if (check.TabIndex == 1)
                    {
                        if (check.Checked)
                        {
                            foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                            {
                                if (row.Cells[7].Value != null)
                                {
                                    if ((row.Cells[7].Value).ToString() == check.Text)
                                    {
                                        row.Visible = false;
                                    }
                                }

                            }
                        }

                    }
                    else if (check.TabIndex == 2)
                    {
                        if (check.Checked)
                        {
                            foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                            {
                                if (row.Cells[7].Value != null)
                                {
                                    if ((row.Cells[7].Value).ToString() == check.Text)
                                    {
                                        row.Visible = false;
                                    }
                                }

                            }
                        }

                    }
                    else if (check.TabIndex == 3)
                    {
                        if (check.Checked)
                        {
                            foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                            {
                                if (row.Cells[7].Value != null)
                                {
                                    if ((row.Cells[7].Value).ToString() == check.Text)
                                    {
                                        row.Visible = false;
                                    }
                                }

                            }
                        }

                    }
                    else if (check.TabIndex == 4)
                    {
                        if (check.Checked)
                        {
                            foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                            {
                                if (row.Cells[7].Value != null)
                                {
                                    if ((row.Cells[7].Value.ToString() == check.Text) | (row.Cells[7].Value.ToString() == check.Text + " (ред.)"))
                                    {
                                        row.Visible = false;
                                    }
                                }

                            }
                        }

                    }

                }
                else if (!check.Checked)
                {
                    if (check.TabIndex == 1)
                    {

                        foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                        {
                            if (row.Cells[7].Value != null)
                            {
                                if ((row.Cells[7].Value).ToString() == check.Text)
                                {
                                    row.Visible = true;
                                }
                            }

                        }




                    }
                    else if (check.TabIndex == 2)
                    {

                        foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                        {
                            if (row.Cells[7].Value != null)
                            {
                                if ((row.Cells[7].Value).ToString() == check.Text)
                                {
                                    row.Visible = true;
                                }
                            }

                        }
                    }
                    else if (check.TabIndex == 3)
                    {

                        foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                        {
                            if (row.Cells[7].Value != null)
                            {
                                if ((row.Cells[7].Value).ToString() == check.Text)
                                {
                                    row.Visible = true;
                                }
                            }

                        }
                    }
                    else if (check.TabIndex == 4)
                    {



                        foreach (DataGridViewRow row in bunifuCustomDataGrid1.Rows)
                        {
                            if (row.Cells[7].Value != null)
                            {
                                if ((row.Cells[7].Value.ToString() == check.Text) | (row.Cells[7].Value.ToString() == check.Text + " (ред.)"))
                                {

                                    row.Visible = true;
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
                check.Checked = false;
            }
            finally
            {
                for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                {
                    bunifuCustomDataGrid1[7, i].Style.BackColor = Color.DimGray;
                    if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "закрыта")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Red;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "обследована")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.Orange;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "в работе")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.OrangeRed;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }
                    else if (bunifuCustomDataGrid1[7, i].FormattedValue.ToString() == "новая (ред.)")
                    {
                        bunifuCustomDataGrid1[7, i].Style.ForeColor = Color.YellowGreen;
                    }

                }
            }


        }




    }

    



}
