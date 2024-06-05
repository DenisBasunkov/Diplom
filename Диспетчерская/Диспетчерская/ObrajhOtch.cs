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
    public partial class ObrajhOtch : Form
    {
        public ObrajhOtch()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ObrajhOtch_Load(object sender, EventArgs e)
        {
            Lists("Обращения", OsnovList);
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
                            box.Items.Add(reader.GetString(1));
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
                        VidObr.Enabled = true;
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
                    //maskedTextBox1.Clear();
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
                    VidObr.Enabled = false;
                    VidObr.Text = "";
                }
                
                else if (radioButton.TabIndex == 6)
                {
                    where = where.Replace("," + radioButton.TabIndex.ToString(), "");
                    //maskedTextBox2.Clear();
                    //maskedTextBox3.Clear();
                    Period.Enabled = false;
                }

            }

        }

        private void Formirovka_Click(object sender, EventArgs e)
        {
            string customers = "";
            

            try
            {
                customers = string.Format("SELECT {0}  FROM [Обращения] {1}  ORDER BY [Номер] ASC ", osnov(OsnovList).Remove(0, 1), Where(0));
            }
            catch
            {
                customers = string.Format("SELECT {0}  FROM [Обращения] {1}  ORDER BY [Номер] ASC ", "*", Where(0));

            }

            

            for (int a = 0; a < this.Controls.Count; a++)
            {
                if (Controls[a] is DataGridView)
                {
                    if (Controls[a].TabIndex == 1)
                    {
                        data(customers, (DataGridView)Controls[a]);
                    }
                    


                }
            }
            ViviodOtch.Enabled = true;
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

        string tu;
        string[] vs = null;
        string Where(int data)
        {
            string wheres = "";
            SQLiteConnection sql = new SQLiteConnection(form.nameData, true);
            sql.Open();
            if (where == "")
            {

                SQLiteCommand command = new SQLiteCommand("SELECT[Номер] FROM[Обращения]", sql);

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
                                        string imia = "Дата ввода";
                                        txt += string.Format(" AND [{0}] LIKE  '%{1}%' ", imia, maskedTextBox1.Text);


                                    }
                                    else if (comboBox1.Text == "Закрытие")
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
                                        string imia = "Дата ввода";
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
                                    txt += string.Format(" AND [Вид обращения] LIKE '%{0}%'", VidObr.Text);
                                }
                                
                            }
                            else
                            {
                                if (data == 0)
                                {
                                    txt = txt.Replace(string.Format(" AND [Вид обращения] LIKE '%{0}%'", VidObr.Text), "");
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
                                    txt += string.Format(" AND ((substr([Дата ввода],7,4)|| '.'|| substr([Дата ввода], 4, 2)|| '.'|| substr([Дата ввода], 1, 2))  BETWEEN '{0}' AND '{1}' )", data1.ToString("yyyy.MM.dd"), data2.ToString("yyyy.MM.dd"));

                                }
                                
                            }
                            else
                            {
                                if (data == 0)
                                {

                                    txt = txt.Replace(string.Format(" AND ((substr([Дата ввода],7,4)|| '.'|| substr([Дата ввода], 4, 2)|| '.'|| substr([Дата ввода], 1, 2))  BETWEEN '{0}' AND '{1}' )", maskedTextBox2.Text, maskedTextBox3.Text), "");

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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OsnovData.Columns.RemoveAt(0);

            panel2.Enabled = false;
            Statys.Enabled = false;
            VidObr.Enabled = false;
            Period.Enabled = false;
            Period.Enabled = false;
            
            comboBox1.Text = "";
            Statys.Text = "";
            VidObr.Text = "";
            ViviodOtch.Enabled = false;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox6.Checked = false;
            


        }

        int n = 0;
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
                       
                        for (int j = 0; j < OsnovData.ColumnCount; j++)
                        {
                            sheet.Cells[1, j + 1] = OsnovData.Columns[j].HeaderText;
                            sheet.Cells[1, j + 1].Font.Bold = true;
                            sheet.Cells[i + 2, j + 1] = OsnovData.Rows[i].Cells[j].Value;


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
    }
}
