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
    public partial class Form1 : Form
    {
        int N;
        double R, Vmax, Xmax, Ymax, dt, K;
        double[] X; double[] Y; double[] Vx; double[] Vy;
        Random Rnd = new Random();
        Bitmap Bmp1, Bmp2;
        Graphics Gr1, Gr2;
        Pen P1 = new Pen(Color.Red, 2);

        

        Pen P2 = new Pen(Color.Green, 2);

        

        SolidBrush Br = new SolidBrush(Color.Yellow);

        

        // Xscr = (int)(Bmp1.Width/2 + K*x)
        // Yscr = (int)(Bmp1.Height/2 - K*y)

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Gr1 = Graphics.FromImage(Bmp1);
            Bmp2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Gr2 = Graphics.FromImage(Bmp2);

            

        }
        public void Calc()
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            
            N = Convert.ToInt32(textBox1.Text);                      //int.Parse
            R = Convert.ToDouble(textBox2.Text);
            Vmax = Convert.ToDouble(textBox3.Text);
            Xmax = Convert.ToDouble(textBox4.Text)/2;
            Ymax = Convert.ToDouble(textBox5.Text)/2;
            dt = Convert.ToDouble(textBox6.Text);
            K = Convert.ToDouble(textBox7.Text);
            X = new double[N]; Y = new double[N];
            Vx = new double[N]; Vy = new double[N];
            Gr1.Clear(pictureBox1.BackColor); Gr2.Clear(pictureBox1.BackColor);
            Gr1.DrawRectangle(P2, (int)(Bmp1.Width / 2 - K * Xmax), (int)(Bmp1.Height / 2 - K * Ymax),
                (int)(2 * K * Xmax), (int)(2 * K * Ymax));
            Gr2.DrawImage(Bmp1, 0, 0);
            for (int i=0; i<N; i++)
            {
                X[i] = (2 * Rnd.NextDouble() - 1) * (Xmax - R);
                Y[i] = (2 * Rnd.NextDouble() - 1) * (Ymax - R);
                Vx[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
                Vy[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
            }
            for (int i=0; i<N; i++)
            {
                Gr2.DrawEllipse(P1, (int)(Bmp1.Width / 2 + K * (X[i] - R)), 
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2*K*R), (int)(2*K*R));
                Gr2.FillEllipse(Br, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));

            }
            
            pictureBox1.Image = Bmp2;
            button1.Enabled = false;
            button2.Enabled = true;
            timer1.Enabled = true;
            
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {

            
            
            for (int i = 0; i < N; i++)
            {
                X[i] += Vx[i] * dt;
                Y[i] += Vy[i] * dt;
                if ((X[i] >= Xmax - R) && (Vx[i] > 0)) Vx[i] = -Vx[i];
                if ((X[i] <= -Xmax + R) && (Vx[i] < 0)) Vx[i] = -Vx[i];
                if ((Y[i] >= Ymax - R) && (Vy[i] > 0)) Vy[i] = -Vy[i];
                if ((Y[i] <= -Ymax + R) && (Vy[i] < 0)) Vy[i] = -Vy[i];
            }
            Gr2.DrawImage(Bmp1, 0, 0);
            for (int i = 0; i < N; i++)
            {
                Gr2.DrawEllipse(P1, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
                Gr2.FillEllipse(Br, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));

            }
            pictureBox1.Image = Bmp2;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            timer1.Enabled = false;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Draw()
        {

        }
    }
}
