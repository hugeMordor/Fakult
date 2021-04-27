using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fakult1
{
    public partial class Form2 : Form
    {
        Bitmap Bmp;
        Graphics Gr;
        double Kh, Kv, X, Y; int X1scr, Y1scr, X2scr, Y2scr;
        Pen P1 = new Pen(Color.Black, 1);

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        Pen P2 = new Pen(Color.Black, 2);
        SolidBrush Br = new SolidBrush(Color.Black);
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.FF1.посмотретьToolStripMenuItem.Enabled = true;
            Form1.FF1.button1.Enabled = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Gr = Graphics.FromImage(Bmp);
            Draw();
        }
        private void Draw()
        {
            if (Form1.FF1.Nans == 0) return;
            Gr.Clear(pictureBox1.BackColor);
            Kh = Math.Pow(1.1, trackBar1.Value)*( Bmp.Width / (Form1.FF1.dV * Form1.FF1.Vmax));
            Kv = Math.Pow(1.1, trackBar2.Value)*(10 * Bmp.Height);
            
            for(int i = 0; i < 500; i++)
            {
                Gr.DrawRectangle(P1, (int)(Kh * i * Form1.FF1.dV), 
                    (int)(Bmp.Height - Kv * Form1.FF1.DistrV[i] / (Form1.FF1.N * Form1.FF1.Nans)), 
                    (int)(Kh * Form1.FF1.dV), (int)(Kv * Form1.FF1.DistrV[i] / (Form1.FF1.N * Form1.FF1.Nans)));
                Gr.FillRectangle(Br, (int)(Kh * i * Form1.FF1.dV),
                    (int)(Bmp.Height - Kv * Form1.FF1.DistrV[i] / (Form1.FF1.N * Form1.FF1.Nans)),
                    (int)(Kh * Form1.FF1.dV), (int)(Kv * Form1.FF1.DistrV[i] / (Form1.FF1.N * Form1.FF1.Nans)));
            }
            



            pictureBox1.Image = Bmp;
        }
    }
}
