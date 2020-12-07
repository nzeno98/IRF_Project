using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IRF_beadando
{
    public class RoundButton: Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(3, 3, ClientSize.Width-8, ClientSize.Height-8);
            this.Region = new System.Drawing.Region(path);
            base.OnPaint(e);
        }
    }
}
