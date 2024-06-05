using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Диспетчерская
{
    public partial class Form2 : MetroForm
    {
        string profill;
        public Form2(string _profill)
        {
            InitializeComponent();
            profill = _profill;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            ZaiavkaPDF.Navigate(AppDomain.CurrentDomain.BaseDirectory + "Datas/Работа по заявкам.pdf");
            OtchetPDF.Navigate(AppDomain.CurrentDomain.BaseDirectory + "Datas/Формирование отчета.pdf");
            AdminPDF.Navigate(AppDomain.CurrentDomain.BaseDirectory + "Datas/Администрирование.pdf");
            if (profill == "")
            {
                Администрирование.Enabled = false;
            }
            else
            {
                if (profill == "Admin")
                {
                    Администрирование.Enabled = true;
                    //PDFAdmin.src = AppDomain.CurrentDomain.BaseDirectory + "Datas/лабораторная access.pdf";

                }
                else
                {
                    Администрирование.Enabled = false;

                }
            }
            
            
        }

        

        
    }
}
