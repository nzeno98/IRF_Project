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

namespace IRF_beadando
{
    public partial class Form1 : Form
    {
        List<koronasnap> koronasnapok = new List<koronasnap>();
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
    }
}
