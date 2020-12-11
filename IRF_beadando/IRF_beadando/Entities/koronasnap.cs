using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRF_beadando.Entities
{
    public class koronasnap
    {
        public DateTime nap { get; set; }
        public int napibeteg { get; set; }
        public int napihalott { get; set; }
        public int osszes { get; set; }
        private int cuccoska;

        public int Sulyossag
        {
            get { return cuccoska; }
            set { cuccoska = value;
                if (cuccoska < 100) cuccoska = 0;
                else if (cuccoska < 300) cuccoska = 1;
                else if (cuccoska < 1000) cuccoska = 2;
                else if (cuccoska < 3000) cuccoska = 3;
                else if (cuccoska < 5000) cuccoska = 4;
                else cuccoska = 5;
            
                }
        }

    }
}
