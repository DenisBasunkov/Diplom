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
using Excel = Microsoft.Office.Interop.Excel;
using MetroFramework;
using System.Diagnostics;
using System.Threading;

namespace Диспетчерская
{
    public partial class KanalOtch : Form
    {
        public KanalOtch()
        {
            InitializeComponent();
        }

        private void KanalOtch_Load(object sender, EventArgs e)
        {
            Lists("Канализация", OsnovList);
            Lists("КаналОбращ", ObsledList);
            Lists("КаналРабота", RabList);
        }

        Vhod form = new Vhod();
        CheckedListBox box;
        async void Lists(string nameT, CheckedListBox Check)
        {
            SQLiteConnection con = new SQLiteConnection(form.nameData, true);
            string customers = String.Format("PRAGMA table_info('{0}');", nameT);

            box = Check;

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
                            if (reader.GetString(1) == "Номер"|| reader.GetString(1) == "№ заявки")
                            {

                            }
                            else
                            {
                                box.Items.Add(reader.GetString(1));
                            }
                            
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string where = "";
        CheckBox radioButton;
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

            radioButton = (CheckBox)sender;
            if (radioButton.Checked)
            {
                if (radioButton.TabIndex == 1)
                {

                    if (radioButton.Checked)
                    {
                        panel2.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }

                }
                else if (radioButton.TabIndex == 2)
                {

                    if (radioButton.Checked)
                    {
                        Statys.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }

                }
                else if (radioButton.TabIndex == 3)
                {
                    if (radioButton.Checked)
                    {
                        VidZav.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }

                }
                else if (radioButton.TabIndex == 4)
                {
                    if (radioButton.Checked)
                    {
                        Razrit.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }
                }
                else if (radioButton.TabIndex == 5)
                {
                    if (radioButton.Checked)
                    {
                        VidPovr.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }
                }
                else if (radioButton.TabIndex == 6)
                {
                    if (radioButton.Checked)
                    {
                        Period.Enabled = true;
                        where += "," + radioButton.TabIndex.ToString();
                    }
                }
            }
            else if (!radioButton.Checked)
            {
                if (radioButton.TabIndex == 1)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    panel2.Enabled = false;
                    maskedTextBox1.Clear();
                    comboBox1.Text = "";

                }
                else if (radioButton.TabIndex == 2)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    Statys.Enabled = false;
                    Statys.Text = "";

                }
                else if (radioButton.TabIndex == 3)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    VidZav.Enabled = false;
                    VidZav.Text = "";
                }
                else if (radioButton.TabIndex == 4)
                {

                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    Razrit.Enabled = false;
                    Razrit.Text = "";


                }
                else if (radioButton.TabIndex == 5)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    VidPovr.Enabled = false;
                    VidPovr.Text = "";
                }
                else if (radioButton.TabIndex == 6)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    maskedTextBox2.Clear();
                    maskedTextBox3.Clear();
                    Period.Enabled = false;
                }

            }

        }

        private void Checked_checked(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Checked)
            {
                if (check.TabIndex == 1)
                {


                    ObsledList.Enabled = true;
                }
                else if (check.TabIndex == 2)
                {
                    RabList.Enabled = true;
                }


            }
            else if (!check.Checked)
            {
                if (check.TabIndex == 1)
                {


                    ObsledList.Enabled = false;
                }
                else if (check.TabIndex == 2)
                {
                    RabList.Enabled = false;
                }

            }

        }

        private void Formirovka_Click(object sender, EventArgs e)
        {

            string customers = "";
            string customer = "";
            string customera = "";
            string nomer = "";
            try
            {
                customers = string.Format("SELECT [Номер],{0}  FROM [Канализация] {1}  ORDER BY [Номер] ASC ", osnov(OsnovList).Remove(0, 1), Where(0));
                nomer = string.Format("SELECT [Номер] FROM [Канализация] {0}", Where(0));
            }
            catch
            {
                customers = string.Format("SELECT [Номер],{0}  FROM [Канализация] {1}  ORDER BY [Номер] ASC ", "*", Where(0));
                nomer = string.Format("SELECT [Номер] FROM [Канализация] {0}", Where(0));
            }
           
            if (checkBox8.Checked)
            {
                try
                {
                    customer = string.Format("SELECT [№ заявки],{0}  FROM [КаналОбращ] {1}  ORDER BY [№ заявки] ASC, [№ обращения] ASC ", osnov(ObsledList).Remove(0, 1), Wheres(1, nomer));
                }
                catch
                {
                    customer = string.Format("SELECT [№ заявки],{0}  FROM [КаналОбращ] {1}  ORDER BY [№ заявки] ASC, [№ обращения] ASC ", "*", Wheres(1, nomer));
                }

            }
            if (checkBox9.Checked)
            {
                try
                {
                    customera = string.Format("SELECT [№ заявки],{0}  FROM [КаналРабота] {1} ORDER BY [№ заявки] ASC, [№ работы] ASC", osnov(RabList).Remove(0, 1), Wheres(2, nomer));
                }
                catch
                {
                    customera = string.Format("SELECT [№ заявки],{0}  FROM [КаналРабота] {1} ORDER BY [№ заявки] ASC, [№ работы] ASC", "*", Wheres(2, nomer));
                }


            }


            for (int a = 0; a < this.Controls.Count; a++)
            {
                if (Controls[a] is DataGridView)
                {
                    if (Controls[a].TabIndex == 1)
                    {
                        data(customers, (DataGridView)Controls[a]);
                    }
                    else if (Controls[a].TabIndex == 2)
                    {
                        data(customer, (DataGridView)Controls[a]);
                    }
                    else if (Controls[a].TabIndex == 3)
                    {
                        data(customera, (DataGridView)Controls[a]);
                    }


                }
            }
            ViviodOtch.Enabled = true;
        }

        void data(string skript, DataGridView dataGrid)
        {
            int count = dataGrid.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                dataGrid.Columns.RemoveAt(0);
            }
            SQLiteConnection con = new SQLiteConnection(form.nameData, true);

            try
            {



                SQLiteDataAdapter da = new SQLiteDataAdapter(skript, con);
                var commandBuilder = new SQLiteCommandBuilder(da);

                DataTable ds = new DataTable();
                da.Fill(ds);



                dataGrid.AutoGenerateColumns = true;
                dataGrid.DataSource = ds;




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        string osnov(CheckedListBox chec)
        {
            string polia = "";
            try
            {
                for (int i = 0; i < chec.Items.Count; i++)
                {
                    if (chec.GetItemChecked(i))
                    {
                        polia = polia + ",[" + chec.Items[i].ToString() + "]";

                    }

                }
            }
            catch
            {
                polia = ",*";
            }


            return polia;
        }

        string tu;
        string[] vs = null;
        string Where(int data)
        {
            string wheres = "";
            SQLiteConnection sql = new SQLiteConnection(form.nameData, true);
            sql.Open();
            if (where == "")
            {

                SQLiteCommand command = new SQLiteCommand("SELECT[Номер] FROM[Канализация]", sql);

                SQLiteDataReader reader = command.ExecuteReader();

                if (data == 0)
                {
                    wheres = "";
                }
               
            }
            else
            {

                vs = where.Remove(0, 1).Split(',');
                string txt = "";

                try
                {
                    foreach (var po in vs)
                    {

                        if (po == "1")
                        {
                            if (po == "1")
                            {
                                if (data == 0)
                                {
                                    if (comboBox1.Text == "Ввода")
                                    {
                                        string imia = "Дата поступления";
                                        txt += string.Format(" AND [{0}] LIKE  '%{1}%' ", imia, maskedTextBox1.Text);


                                    }
                                    else if (comboBox1.Text == "Закрытия")
                                    {
                                        string imia = "Закрытие";
                                        txt += string.Format(" AND [{0}] LIKE  '%{1}%'", imia, maskedTextBox1.Text);

                                    }
                                }
                            }
                            else
                            {
                                if (data == 0)
                                {
                                    if (comboBox1.Text == "Ввода")
                                    {
                                        string imia = "Дата поступления";
                                        txt = txt.Replace(string.Format(" AND [{0}] LIKE  '%{1}%' ", imia, maskedTextBox1.Text), "");
                                        tu = maskedTextBox1.Text;

                                    }
                                    else if (comboBox1.Text == "Закрытия")
                                    {
                                        string imia = "Закрытие";
                                        txt = txt.Replace(string.Format(" AND [{0}] LIKE  '%{1}%'", imia, maskedTextBox1.Text), "");
                                        tu = maskedTextBox1.Text;
                                    }
                                }

                            }

                        }
                        else if (po == "2")
                        {
                            if (po == "2")
                            {
                                if (data == 0)
                                {

                                    string stat = Statys.Text;

                                    txt += string.Format(" AND [Статус] LIKE '%{0}%'", stat);

                                }

                            }
                            else
                            {
                                if (data == 0)
                                {

                                    string stat = Statys.Text;

                                    txt = txt.Replace(string.Format(" AND [Статус] LIKE '%{0}%'", stat), "");

                                }

                            }


                        }


                        else if (po == "3")
                        {
                            if (po == "3")
                            {
                                if (data == 0)
                                {
                                    txt += string.Format(" AND [Вид заявки] LIKE '%{0}%'", VidZav.Text);
                                }

                            }
                            else
                            {
                                if (data == 0)
                                {
                                    txt = txt.Replace(string.Format(" AND [Вид заявки] LIKE '%{0}%'", VidZav.Text), "");
                                }

                            }




                        }
                        else if (po == "4")
                        {
                            if (po == "4")
                            {
                                if (data == 0)
                                {
                                    string s = "";

                                    string skript = string.Format("Select [№ заявки] From [КаналРабота] where [Разрытие] LIKE '%{0}%'", Razrit.Text);
                                    SQLiteCommand command = new SQLiteCommand(skript, sql);

                                    SQLiteDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        s += string.Format("OR [Номер] LIKE '{0}'", reader["№ заявки"]);
                                    }

                                    txt += string.Format(" AND ({0})", s.Remove(0, 2));


                                }

                            }
                            else
                            {
                                if (data == 0)
                                {

                                    string s = "";

                                    string skript = string.Format("Select [№ заявки] From [КаналРабота] where [Разрытие] LIKE '%{0}%'", Razrit.Text);
                                    SQLiteCommand command = new SQLiteCommand(skript, sql);

                                    SQLiteDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        s += string.Format("OR [Номер] LIKE '{0}'", reader["№ заявки"]);
                                    }

                                    txt = txt.Replace(string.Format(" AND ({0})", s.Remove(0, 2)), "");

                                }

                            }



                        }
                        else if (po == "5")
                        {
                            if (po == "5")
                            {
                                if (data == 0)
                                {
                                    string s = "";

                                    string skript = string.Format("SELECT [№ заявки] FROM [КаналРабота] WHERE [Вид повреждения] LIKE '%{0}%'", VidPovr.Text);
                                    SQLiteCommand command = new SQLiteCommand(skript, sql);

                                    SQLiteDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        s += string.Format("OR [Номер] LIKE '{0}'", reader["№ заявки"]);
                                    }

                                    txt += string.Format(" AND ({0})", s.Remove(0, 2));

                                }

                            }
                            else
                            {
                                if (data == 0)
                                {
                                    string s = "";

                                    string skript = string.Format("SELECT [№ заявки] FROM [КаналРабота] WHERE [Вид повреждения] LIKE '%{0}%'", VidPovr.Text);
                                    SQLiteCommand command = new SQLiteCommand(skript, sql);

                                    SQLiteDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        s += string.Format("OR [Номер] LIKE '{0}'", reader["№ заявки"]);
                                    }

                                    txt = txt.Replace(string.Format(" AND ({0})", s.Remove(0, 2)), "");
                                }

                            }


                        }
                        else if (po == "6")
                        {
                            if (po == "6")
                            {
                                if (data == 0)
                                {
                                    DateTime data1 = Convert.ToDateTime(maskedTextBox2.Text);
                                    DateTime data2 = Convert.ToDateTime(maskedTextBox3.Text);
                                    txt += string.Format(" AND ((substr([Дата поступления],7,4)|| '.'|| substr([Дата поступления], 4, 2)|| '.'|| substr([Дата поступления], 1, 2))  BETWEEN '{0}' AND '{1}' )", data1.ToString("yyyy.MM.dd"), data2.ToString("yyyy.MM.dd"));

                                }

                            }
                            else
                            {
                                if (data == 0)
                                {

                                    txt = txt.Replace(string.Format(" AND ((substr([Дата поступления],7,4)|| '.'|| substr([Дата поступления], 4, 2)|| '.'|| substr([Дата поступления], 1, 2))  BETWEEN '{0}' AND '{1}' )", maskedTextBox2.Text, maskedTextBox3.Text), "");

                                }

                            }


                        }


                    }

                    wheres = "WHERE " + txt.Remove(0, 5);
                }
                catch
                {


                }
                finally
                {

                }
            }




            return wheres;
        }

        int n = 1;
        private void ViviodOtch_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog openFile = new SaveFileDialog();
                openFile.Filter = "Excel Files | *.xlsx";
                openFile.DefaultExt = "xlsx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    Excel.Application app = new Excel.Application();

                    Excel.Workbook book = app.Workbooks.Add();

                    Excel.Worksheet sheet = (Excel.Worksheet)book.Worksheets[1];

                    for (int i = 0; i < OsnovData.RowCount; i++)
                    {
                        n = n + 1;
                        for (int j = 0; j < OsnovData.ColumnCount; j++)
                        {
                            sheet.Cells[1, j + 1] = OsnovData.Columns[j].HeaderText;
                            sheet.Cells[1, j + 1].Font.Bold = true;
                            sheet.Cells[i + n, j + 1] = OsnovData.Rows[i].Cells[j].Value;


                        }

                        if (checkBox8.Checked)
                        {

                            sheet.Cells[i + n + 1, 2] = "Обследование";
                            for (int col = 0; col < ObsledData.Columns.Count; col++)
                            {
                                sheet.Cells[i + n + 1, col + 3] = ObsledData.Columns[col].HeaderText;
                                sheet.Cells[i + n + 1, col + 3].Font.italic = true;
                            }
                            for (int t = 0; t < ObsledData.RowCount; t++)
                            {

                                if (OsnovData.Rows[i].Cells[0].Value.ToString() == ObsledData.Rows[t].Cells[0].Value.ToString())
                                {
                                    n = n + 1;


                                    for (int r = 0; r < ObsledData.ColumnCount; r++)
                                    {
                                        sheet.Cells[i + n + 1, r + 4] = ObsledData.Rows[t].Cells[r].Value;
                                    }



                                }



                            }
                            n = n + 1;
                        }
                        if (checkBox9.Checked)
                        {
                            sheet.Cells[i + n + 1, 2] = "Работы";
                            for (int col = 0; col < RabData.Columns.Count; col++)
                            {
                                sheet.Cells[i + n + 1, col + 3] = RabData.Columns[col].HeaderText;
                                sheet.Cells[i + n + 1, col + 3].Font.italic = true;
                            }
                            for (int t = 0; t < RabData.RowCount; t++)
                            {

                                if (OsnovData.Rows[i].Cells[0].Value.ToString() == RabData.Rows[t].Cells[0].Value.ToString())
                                {
                                    n = n + 1;


                                    for (int r = 0; r < RabData.ColumnCount; r++)
                                    {
                                        sheet.Cells[i + n + 1, r + 4] = RabData.Rows[t].Cells[r].Value;
                                    }



                                }



                            }
                            n = n + 1;
                        }



                    }
                    book.SaveAs(@"" + openFile.FileName.ToString());

                    app.Workbooks.Close();
                    app.Quit();
                    this.Cursor = Cursors.Default;
                    Thread.Sleep(1000);
                    n = 1;
                    DialogResult result = MetroMessageBox.Show(this, "Хотите открыть сохраненный файл?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(openFile.FileName.ToString());
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                n = 1;
            }
            finally
            {
                ViviodOtch.Enabled = false;

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DataGridView dataGrid;
            for (int a = 0; a < this.Controls.Count; a++)
            {
                if (Controls[a] is DataGridView)
                {
                    if (Controls[a].TabIndex == 1)
                    {
                        dataGrid = (DataGridView)Controls[a];
                        int count = dataGrid.Columns.Count;
                        for (int i = 0; i < count; i++)
                        {
                            dataGrid.Columns.RemoveAt(0);
                        }
                    }
                    else if (Controls[a].TabIndex == 2)
                    {
                        dataGrid = (DataGridView)Controls[a];
                        int count = dataGrid.Columns.Count;
                        for (int i = 0; i < count; i++)
                        {
                            dataGrid.Columns.RemoveAt(0);
                        }
                    }
                    else if (Controls[a].TabIndex == 3)
                    {
                        dataGrid = (DataGridView)Controls[a];
                        int count = dataGrid.Columns.Count;
                        for (int i = 0; i < count; i++)
                        {
                            dataGrid.Columns.RemoveAt(0);
                        }
                    }


                }
            }


            CheckedListBox list;
            for (int a = 0; a < this.Controls.Count; a++)
            {
                if (Controls[a] is CheckedListBox)
                {
                    if (Controls[a].TabIndex == 1)
                    {
                        list = (CheckedListBox)Controls[a];
                        for (int i = 0; i < list.Items.Count; i++)
                        {
                            list.SetItemChecked(i, false);
                        }


                    }
                    else if (Controls[a].TabIndex == 2)
                    {

                        list = (CheckedListBox)Controls[a];
                        for (int i = 0; i < list.Items.Count; i++)
                        {
                            list.SetItemChecked(i, false);
                        }

                    }
                    else if (Controls[a].TabIndex == 3)
                    {

                        list = (CheckedListBox)Controls[a];
                        for (int i = 0; i < list.Items.Count; i++)
                        {
                            list.SetItemChecked(i, false);
                        }

                    }
                }
            }

            panel2.Enabled = false;
            Statys.Enabled = false;
            VidZav.Enabled = false;
            Razrit.Enabled = false;
            VidPovr.Enabled = false;
            Period.Enabled = false;
            Period.Enabled = false;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            comboBox1.Text = "";
            Statys.Text = "";
            VidZav.Text = "";
            Razrit.Text = "";
            VidPovr.Text = "";
            ViviodOtch.Enabled = false;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;


        }

        string Wheres(int data, string nomer)
        {
            string wheres = null;
            SQLiteConnection sql = new SQLiteConnection(form.nameData, true);
            sql.Open();
            if (where == "")
            {

                SQLiteCommand command = new SQLiteCommand("SELECT[Номер] FROM[Водопровод]", sql);

                SQLiteDataReader reader = command.ExecuteReader();
                if (data == 1)
                {
                    string s = "";
                    while (reader.Read())
                    {
                        s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                    }
                    wheres = "WHERE" + s.Remove(0, 2);
                }
                else if (data == 2)
                {
                    string s = "";
                    while (reader.Read())
                    {
                        s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                    }
                    wheres = "WHERE" + s.Remove(0, 2);

                }
            }
            else
            {
                vs = where.Remove(0, 1).Split(',');
                int n = 0;
                foreach (var po in vs)
                {
                    if (po == "4")
                    {
                        if (data == 1)
                        {
                            SQLiteCommand command = new SQLiteCommand(nomer, sql);

                            SQLiteDataReader reader = command.ExecuteReader();
                            string s = "";
                            while (reader.Read())
                            {
                                s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                            }
                            wheres = "WHERE" + s.Remove(0, 2);

                        }
                        else if (data == 2)
                        {

                            SQLiteCommand command = new SQLiteCommand(nomer, sql);

                            SQLiteDataReader reader = command.ExecuteReader();
                            string s = "";
                            while (reader.Read())
                            {
                                s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                            }
                            wheres = "WHERE" + string.Format(" ({1}) AND [Разрытие] LIKE '{0}'", Razrit.Text, s.Remove(0, 2));


                        }
                        n++;
                    }
                    else if (po == "5")
                    {
                        if (data == 1)
                        {
                            SQLiteCommand command = new SQLiteCommand(nomer, sql);

                            SQLiteDataReader reader = command.ExecuteReader();
                            string s = "";
                            while (reader.Read())
                            {
                                s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                            }
                            wheres = "WHERE" + s.Remove(0, 2);

                        }
                        else if (data == 2)
                        {

                            SQLiteCommand command = new SQLiteCommand(nomer, sql);

                            SQLiteDataReader reader = command.ExecuteReader();
                            string s = "";
                            while (reader.Read())
                            {
                                s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                            }
                            wheres = "WHERE" + string.Format(" ({1})  AND [Вид повреждения] LIKE '{0}'", VidPovr.Text, s.Remove(0, 2));


                        }
                        n++;
                    }
                    else
                    {
                        if (n == 0)
                        {
                            if (data == 1)
                            {
                                SQLiteCommand command = new SQLiteCommand(nomer, sql);

                                SQLiteDataReader reader = command.ExecuteReader();
                                string s = "";
                                while (reader.Read())
                                {
                                    s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                                }
                                wheres = "WHERE" + s.Remove(0, 2);

                            }
                            else if (data == 2)
                            {

                                SQLiteCommand command = new SQLiteCommand(nomer, sql);

                                SQLiteDataReader reader = command.ExecuteReader();
                                string s = "";
                                while (reader.Read())
                                {
                                    s += string.Format("OR [№ заявки] LIKE '{0}'", reader["Номер"]);
                                }
                                wheres = "WHERE" + s.Remove(0, 2);


                            }
                        }

                    }
                }

            }



            return wheres;
        }





    }
}
