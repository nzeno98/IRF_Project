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
        public Form1()
        {
            InitializeComponent();
            beolvasas();
        }

        void beolvasas()
        {
            using (StreamReader sr = new StreamReader("korona.csv"))
            {
                while(!sr.EndOfStream)
                {
                    string[] adatok = sr.ReadLine().Split(';');
                    koronasnap k = new koronasnap();
                    k.nap = Convert.ToDateTime(adatok[0]);
                    k.napibeteg = Convert.ToInt32(adatok[1]);
                    k.napihalott = Convert.ToInt32(adatok[2]);
                    k.osszes = Convert.ToInt32(adatok[3]);
                    koronasnapok.Add(k);
                    

                }

            };

        }

        void diagram()
        {
            dataGridView1.DataSource = valasztottnapok;
            chart1.DataSource = valasztottnapok;
            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "nap";
            series.YValueMembers = "napibeteg";
            series.BorderWidth = 2;
            series.Color = Color.Blue;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

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

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //nem akartam használni semmire, véletlen került ide
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            idointervallum.kezdo = dateTimePicker1.Value;
            idointervallum.vege = idointervallum.kezdo.AddMonths(1);
            datummegadas();
            diagram();
        }
    }
}
