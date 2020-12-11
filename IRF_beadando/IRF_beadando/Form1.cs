using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IRF_beadando.Entities;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace IRF_beadando
{
    public partial class Form1 : Form
    {
        List<koronasnap> koronasnapok = new List<koronasnap>();
        List<koronasnap> valasztottnapok = new List<koronasnap>();
        int diagramtipus = 1;
        public Form1()
        {
            InitializeComponent();
            beolvasas();
            datumkorlat();
           
        }

        void beolvasas()
        {
            try
            {
                using (StreamReader sr = new StreamReader("korona.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] adatok = sr.ReadLine().Split(';');
                        koronasnap k = new koronasnap();
                        k.nap = Convert.ToDateTime(adatok[0]);
                        k.napibeteg = Convert.ToInt32(adatok[1]);
                        k.napihalott = Convert.ToInt32(adatok[2]);
                        k.osszes = Convert.ToInt32(adatok[3]);
                        k.Sulyossag = Convert.ToInt32(adatok[1]);
                        koronasnapok.Add(k);


                    }

                };
            }
            catch (Exception)
            {

                MessageBox.Show("Probléma a fájl beolvasásakor. Kérlek próbáld újra!");
            }
           
           

        }

        void datumkorlat()
        {
            DateTime minimum = Convert.ToDateTime("2050.01.01");
            DateTime maximum = Convert.ToDateTime("2000.01.01");



            foreach (var x in koronasnapok)
            {
                if (x.nap > maximum) maximum = x.nap;
                if (x.nap < minimum) minimum = x.nap;
            }
            dateTimePicker1.MaxDate = maximum.Date.AddMonths(-1);
            dateTimePicker1.MinDate = minimum.Date;
            dateTimePicker1.Value = Convert.ToDateTime("2020.11.01");
        }

        void diagram()
        {
            dataGridView1.DataSource = valasztottnapok;
            chart1.DataSource = valasztottnapok;
            dataGridView1.Columns[0].HeaderText = "Dátum";
            dataGridView1.Columns[1].HeaderText = "Napi fert.";
            dataGridView1.Columns[2].HeaderText = "Halálesetek";
            dataGridView1.Columns[3].HeaderText = "Összes fert.";
            dataGridView1.Columns[4].HeaderText = "Terjedés";
            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "nap";
            series.BorderWidth = 2;
            

            if (diagramtipus == 1)
            {
                series.YValueMembers = "napibeteg";
                series.Color = Color.Blue;
                dataGridView1.Columns["napibeteg"].Visible = true;
                dataGridView1.Columns["napihalott"].Visible = false;
                dataGridView1.Columns["osszes"].Visible = false;
                dataGridView1.Columns["Sulyossag"].Visible = true;
                dataGridView1.Refresh();
                dateTimePicker1.Enabled = true;
                label7.Text = "Napi fertőzöttek száma";                
            }
            else
                if(diagramtipus==2)
            {
                series.YValueMembers = "napihalott";
                series.Color = Color.Black;
                dataGridView1.Columns["napibeteg"].Visible = false;
                dataGridView1.Columns["napihalott"].Visible = true;
                dataGridView1.Columns["osszes"].Visible = false;
                dataGridView1.Columns["Sulyossag"].Visible = false;
                dataGridView1.Refresh();
                dateTimePicker1.Enabled = true;
                label7.Text = "Napi halálesetek száma";
            }
            else
                if(diagramtipus==3)
            {
                chart1.DataSource = koronasnapok;
                series.YValueMembers = "osszes";
                series.Color = Color.Red;
                dataGridView1.DataSource = koronasnapok;
                dataGridView1.Columns["napibeteg"].Visible = false;
                dataGridView1.Columns["napihalott"].Visible = false;
                dataGridView1.Columns["osszes"].Visible = true;
                dataGridView1.Columns["Sulyossag"].Visible = true;
                dateTimePicker1.Enabled = false;
                label7.Text = "Teljes járványgörbe";
            }

               

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }

        void diagramchangeforward()
        {
            if(diagramtipus<3)
            {
                diagramtipus++;
            }
            else
            {
                diagramtipus = 1;
            }
            label1.Text = diagramtipus.ToString();
            diagram();
        }

        void diagramchangeback()
        {
            if (diagramtipus > 1)
            {
                diagramtipus--;
            }
            else
            {
                diagramtipus = 3;
            }
            label1.Text = diagramtipus.ToString();
            diagram();
        }

        void datummegadas()
        {
            valasztottnapok.Clear();
            foreach (var nap in koronasnapok)
            {
              if(nap.nap>=idointervallum.kezdo && nap.nap<=idointervallum.vege)
                {
                    valasztottnapok.Add(nap);
                }

            }

        }


        private void roundButton1_Click(object sender, EventArgs e)
        {
            diagramchangeforward();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //nem akartam használni semmire, véletlen került ide
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            diagramchangeback();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                idointervallum.kezdo = dateTimePicker1.Value;
                idointervallum.vege = idointervallum.kezdo.AddMonths(1);
                datummegadas();
                diagram();
            }
            catch (Exception)
            {

                MessageBox.Show("Hiba! Kérlek próbáld újra!");
            }
            
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //véletlenül került ide
        }
    }
}
