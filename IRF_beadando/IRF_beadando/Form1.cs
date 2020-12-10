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
            dateTimePicker1.Value = Convert.ToDateTime("2020.11.01");
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
            series.BorderWidth = 2;
            

            if (diagramtipus == 1)
            {
                series.YValueMembers = "napibeteg";
                series.Color = Color.Blue;
                dataGridView1.Columns["napibeteg"].Visible = true;
                dataGridView1.Columns["napihalott"].Visible = false;
                dataGridView1.Columns["osszes"].Visible = false;
            }
            else
                if(diagramtipus==2)
            {
                series.YValueMembers = "napihalott";
                series.Color = Color.Black;
                dataGridView1.Columns["napibeteg"].Visible = false;
                dataGridView1.Columns["napihalott"].Visible = true;
                dataGridView1.Columns["osszes"].Visible = false;
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
            idointervallum.kezdo = dateTimePicker1.Value;
            idointervallum.vege = idointervallum.kezdo.AddMonths(1);
            datummegadas();
            diagram();
        }
    }
}
