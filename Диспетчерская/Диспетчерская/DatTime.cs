using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диспетчерская
{
    public partial class DatTime : Form
    {
        private Form form;
        string ss, dr,tip; int X, Y,ind;
        public DatTime(string imia, int x, int y, Form d, string ssd,string tipe,int index)
        {
            InitializeComponent();
            ss = imia;
            X = x;
            Y = y;
            form = d;
            dr = ssd;
            tip = tipe;
            ind = index;
        }

        private void DatTime_Load(object sender, EventArgs e)
        {
            try
            {
                if (dr != "")
                {
                    string[] sw = dr.Split(' ');
                    string[] se = sw[0].Split('.');
                    int d = Convert.ToInt32(se[0]);
                    int m = Convert.ToInt32(se[1]);
                    int y = Convert.ToInt32(se[2]);
                    DateTime myVacation1 = new DateTime(y, m, d);

                    monthCalendar1.SetDate(myVacation1);

                    string[] time = sw[1].Split(':');

                    int h = Convert.ToInt32(time[0]); ;
                    int M = Convert.ToInt32(time[1]); ;
                    trackBar1.Value = h;
                    trackBar2.Value = M;
                    label2.Text = sw[1];

                }
                else
                {
                    trackBar1.Value = DateTime.Now.Hour;
                    trackBar2.Value = DateTime.Now.Minute;

                    label2.Text = DateTime.Now.ToShortTimeString();
                }
            }
            catch
            {
                trackBar1.Value = DateTime.Now.Hour;
                trackBar2.Value = DateTime.Now.Minute;
                if (trackBar1.Value < 10)
                {
                    label2.Text = "0"+trackBar1.Value.ToString()+":"+trackBar2.Value.ToString();
                }
                else
                {
                    label2.Text = trackBar1.Value.ToString() + ":" + trackBar2.Value.ToString();
                }
               
                

                //label2.Text = DateTime.Now.ToShortTimeString();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (tip == "data")
            {

                if (ss == "Vodo")
                {
                    if (ind == 0)
                    {
                        VodoForm f = (VodoForm)form;
                        f.Rabota.Rows[X].Cells[Y].Value = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    else if (ind == 1 )
                    {
                        VodoForm f = (VodoForm)form;
                        f.Obsled.Rows[X].Cells[Y].Value = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                   

                }
                else if (ss == "Kanal")
                {
                    if (ind == 0)
                    {
                        KanalForm f = (KanalForm)form;
                        f.Rabota.Rows[X].Cells[Y].Value = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    else if (ind == 1)
                    {
                        KanalForm f = (KanalForm)form;
                        f.Obsled.Rows[X].Cells[Y].Value = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    

                }

            }
            else if(tip == "text")
            {

                if (ss == "Vodo")
                {
                    if (ind == 0)
                    {
                        VodoForm f = (VodoForm)form;
                        f.maskedTextBox1.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    else if (ind == 1)
                    {
                        VodoForm f = (VodoForm)form;
                        f.maskedTextBox2.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }

                }
                else if (ss == "Kanal")
                {
                    if (ind == 0)
                    {
                        KanalForm f = (KanalForm)form;
                        f.maskedTextBox1.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    else if (ind == 1)
                    {
                        KanalForm f = (KanalForm)form;
                        f.maskedTextBox2.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                }
                else if (ss == "Obrajh")
                {
                    if (ind == 0)
                    {
                        ObrajhForm f = (ObrajhForm)form;
                        f.maskedTextBox1.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                    else if(ind == 1)
                    {
                        ObrajhForm f = (ObrajhForm)form;
                        f.maskedTextBox2.Text = monthCalendar1.SelectionRange.Start.ToShortDateString() + " " + label2.Text.ToString();
                    }
                   
                }

            }
           


            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            monthCalendar1.SetDate(DateTime.Now);
            trackBar1.Value = DateTime.Now.Hour;
            trackBar2.Value = DateTime.Now.Minute;

            if (trackBar1.Value < 10)
            {
                label2.Text = "0" + trackBar1.Value.ToString() + ":" + trackBar2.Value.ToString();
            }
            else
            {
                label2.Text = trackBar1.Value.ToString() + ":" + trackBar2.Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tip == "data")
            {

                if (ss == "Vodo")
                {
                    if (ind == 0)
                    {
                        VodoForm f = (VodoForm)form;
                        f.Rabota.Rows[X].Cells[Y].Value = "";
                    }
                    else if (ind == 1)
                    {
                        VodoForm f = (VodoForm)form;
                        f.Obsled.Rows[X].Cells[Y].Value = "";
                    }
                    

                }
                else if (ss == "Kanal")
                {

                    KanalForm f = (KanalForm)form;
                    f.Rabota.Rows[X].Cells[Y].Value = "";

                }

            }
            else if (tip == "text")
            {

                if (ss == "Vodo")
                {
                    if (ind == 0)
                    {
                        VodoForm f = (VodoForm)form;
                        f.maskedTextBox1.Text = "";
                    }
                    else if (ind == 1)
                    {
                        VodoForm f = (VodoForm)form;
                        f.maskedTextBox2.Text = "";
                    }

                }
                else if (ss == "Kanal")
                {

                    if (ind == 0)
                    {
                        KanalForm f = (KanalForm)form;
                        f.maskedTextBox1.Text = "";
                    }
                    else if (ind == 1)
                    {
                        KanalForm f = (KanalForm)form;
                        f.maskedTextBox2.Text = "";
                    }
                }
                else if (ss == "Obrajh")
                {
                    if (ind == 0)
                    {
                        ObrajhForm f = (ObrajhForm)form;
                        f.maskedTextBox1.Text = "";
                    }
                    else if (ind == 1)
                    {
                        ObrajhForm f = (ObrajhForm)form;
                        f.maskedTextBox2.Text = "";
                    }

                }

            }



            this.Close();
        }

        string time = "00:00";
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            var trb = (TrackBar)sender;
            if (trb.TabIndex == 1)
            {
                if (trb.Value < 10)
                {
                    
                    if (trackBar2.Value < 10)
                    {
                        time = "0" + trb.Value.ToString() + ":0" + trackBar2.Value.ToString();
                    }
                    else
                    {
                        time = "0" + trb.Value.ToString() + ":" + trackBar2.Value.ToString();
                    }
                }
                else
                {
                    if (trackBar2.Value < 10)
                    {
                        time = trb.Value.ToString() + ":0" + trackBar2.Value.ToString();
                    }
                    else
                    {
                        time = trb.Value.ToString() + ":" + trackBar2.Value.ToString();
                    }
                }
            }
            else if (trb.TabIndex == 2)
            {
                if (trb.Value < 10)
                {
                    if (trackBar1.Value < 10)
                    {
                        time = "0" + trackBar1.Value.ToString() + ":0" + trb.Value.ToString();
                    }
                    else
                    {
                        time = trackBar1.Value.ToString() + ":0" + trb.Value.ToString();
                    }

                }
                else
                {
                    if (trackBar1.Value < 10)
                    {
                        time = "0" + trackBar1.Value.ToString() + ":" + trb.Value.ToString();
                    }
                    else
                    {
                        time = trackBar1.Value.ToString() + ":" + trb.Value.ToString();
                    }
                }
            }
            label2.Text = time;
        }





    }
}
