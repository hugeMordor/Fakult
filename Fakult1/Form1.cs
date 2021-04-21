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
        public static Form1 FF1;
        public static Form2 FF2;
        int N;
        double R, Vmax, Xmax, Ymax, dt, K;
        double[] X; double[] Y; double[] Vx; double[] Vy;
        Random Rnd = new Random();
        Bitmap Bmp1, Bmp2;
        Graphics Gr1, Gr2;
        Pen P1 = new Pen(Color.Red, 2);
        Pen P2 = new Pen(Color.Green, 2);
        SolidBrush Br = new SolidBrush(Color.Yellow);

        double Px, Py, Psq;
        double VP1x, VP1y, VN1x, VN1y;

        double Vsr, Lsr, nsr, V2sr;
        double dV = 5;
        int[] DistrV = new int[500];
        int Nans;
        int Time = 1000;
        private void посмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FF2 = new Form2();
            FF2.Visible = true;
            button1.Enabled = false;
            посмотретьToolStripMenuItem.Enabled = false;
            Vsr = 0; Lsr = 0; nsr = 0; V2sr = 0;
            for (int i = 0; i<N; i++)
            {
                Vsr += CS[i];
                Lsr += CS[i] / (double)(CN[i] + 1);
                nsr += (double)CN[i];
                V2sr += Vx[i] * Vx[i] + Vy[i] * Vy[i];
            }
            Vsr /= N * T * dt;
            Lsr /= N;
            nsr /= N * T * dt;
            V2sr /= N;
            FF2.textBox1.Text = Convert.ToString(Vsr);
            FF2.textBox2.Text = Convert.ToString(Lsr);
            FF2.textBox3.Text = Convert.ToString(nsr);
            FF2.dataGridView1.RowCount = 500;
            for (int i =0; i<500; i++)
            {
                FF2.dataGridView1[0, i].Value = string.Format("[ {0:f2}; {1:f2})", i+dV, (i+1)*dV);//2 знака после запятой, string.Format похож на Convert.ToString
                FF2.dataGridView1[1, i].Value = DistrV[i];
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        double VP2x, VP2y, VN2x, VN2y;

        int T;
        double[] CS; int[] CN;//Счетчики времени, пути и столкновений

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
            FF1 = this;
            

        }
        
        private void Button1_Click(object sender, EventArgs e)
        {

            T = 0; Nans = 0;
            N = Convert.ToInt32(textBox1.Text);                      //int.Parse
            R = Convert.ToDouble(textBox2.Text);
            Vmax = Convert.ToDouble(textBox3.Text);
            Xmax = Convert.ToDouble(textBox4.Text)/2;
            Ymax = Convert.ToDouble(textBox5.Text)/2;
            dt = Convert.ToDouble(textBox6.Text);
            K = Convert.ToDouble(textBox7.Text);
            X = new double[N]; Y = new double[N]; CS = new double[N];
            Vx = new double[N]; Vy = new double[N]; CN = new int[N];
            Gr1.Clear(pictureBox1.BackColor); Gr2.Clear(pictureBox1.BackColor);
            Gr1.DrawRectangle(P2, (int)(Bmp1.Width / 2 - K * Xmax), (int)(Bmp1.Height / 2 - K * Ymax),
                (int)(2 * K * Xmax), (int)(2 * K * Ymax));
            Gr2.DrawImage(Bmp1, 0, 0);

            for (int i=0; i<N; i++)
            {
                CS[i] = 0; CN[i] = 0;
                X[i] = (2 * Rnd.NextDouble() - 1) * (Xmax - R);
                Y[i] = (2 * Rnd.NextDouble() - 1) * (Ymax - R);
                Vx[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
                Vy[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
            }
            for (int i = 0; i<500; i++)
            {
                DistrV[i] = 0;
            }
            for (int i=0; i<N; i++)
            {
                Gr2.DrawEllipse(P1, (int)(Bmp1.Width / 2 + K * (X[i] - R)), 
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2*K*R), (int)(2*K*R));
                Gr2.FillEllipse(Br, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
            }  
            pictureBox1.Image = Bmp2;

            посмотретьToolStripMenuItem.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            timer1.Enabled = true;
            

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            T++;
            textBox8.Text = Convert.ToString(T*dt);
            
            for (int i = 0; i < N; i++)
            {
                X[i] += Vx[i] * dt;
                Y[i] += Vy[i] * dt;

                CS[i] += Math.Sqrt(Vx[i] * Vx[i] + Vy[i] * Vy[i]) * dt;
                
                if (((X[i] >= Xmax - R) && (Vx[i] > 0)) || ((X[i] <= -Xmax + R) && (Vx[i] < 0))) Vx[i] = -Vx[i];
                
                if (((Y[i] >= Ymax - R) && (Vy[i] > 0)) || ((Y[i] <= -Ymax + R) && (Vy[i] < 0))) Vy[i] = -Vy[i];
                
                
                
            }

            for (int i = 0; i<N-1; i++)
            {
                for (int j = i+1; j<N; j++)
                {

                    Px = X[j] - X[i];
                    Py = Y[j] - Y[i];
                    Psq = Px * Px + Py * Py;
                    if (Psq <= 4 * R * R)
                    { 
                        VP1x = (Vx[i] * Px + Vy[i] * Py) * Px / Psq;
                        VP1y = (Vx[i] * Px + Vy[i] * Py) * Py / Psq;
                        VP2x = (Vx[j] * Px + Vy[j] * Py) * Px / Psq;
                        VP2y = (Vx[j] * Px + Vy[j] * Py) * Py / Psq;
                        if ((VP2x-VP1x)*Px+(VP2y-VP1y)*Py < 0)
                        { 

                            VN1x = Vx[i] - VP1x; VN1y = Vy[i] - VP1y;
                            VN2x = Vx[j] - VP2x; VN2y = Vy[j] - VP2y;
                            Vx[i] = VP2x + VN1x;
                            Vy[i] = VP2y + VN1y;
                            Vx[j] = VP1x + VN2x;
                            Vy[j] = VP1y + VN2y;
                            CN[i]++;
                            CN[j]++;
                        }
                    }

                }
            }
            if (T % Time == 0)
            {
                Nans++;
                for (int i = 0; i < N - 1; i++)
                {
                    int index = (int)(Math.Sqrt(Vx[i] * Vx[i] + Vy[i] * Vy[i]) / dV);
                    DistrV[index]++;
                }
            }

            Gr2.DrawImage(Bmp1, 0, 0);
            for (int i = 0; i < N; i++)
            {
                Gr2.DrawEllipse(P1, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));
                Gr2.FillEllipse(Br, (int)(Bmp1.Width / 2 + K * (X[i] - R)),
                    (int)(Bmp1.Height / 2 - K * (Y[i] + R)), (int)(2 * K * R), (int)(2 * K * R));

            }
            Bmp2.GetPixel(200, 200);
            pictureBox1.Image = Bmp2;

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            посмотретьToolStripMenuItem.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            timer1.Enabled = false;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
    }
}
