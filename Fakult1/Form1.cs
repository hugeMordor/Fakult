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
        double R, Vmax, Xmax, Ymax, dt, k;
        double[] X; double[] Y; double[] Vx; double[] Vy;
        Random Rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void Calc()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            N = Convert.ToInt32(textBox1.Text);
            R = Convert.ToDouble(textBox2.Text);
            Vmax = Convert.ToDouble(textBox3.Text);
            Xmax = Convert.ToDouble(textBox4.Text)/2;
            Ymax = Convert.ToDouble(textBox5.Text)/2;
            dt = Convert.ToDouble(textBox6.Text);
            k = Convert.ToDouble(textBox7.Text);
            X = new double[N]; Y = new double[N];
            Vx = new double[N]; Vy = new double[N];
            for (int i=0; i<N; i++)
            {
                X[i] = (2 * Rnd.NextDouble() - 1) * (Xmax - R);
                Y[i] = (2 * Rnd.NextDouble() - 1) * (Ymax - R);
                Vx[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
                Vy[i] = (2 * Rnd.NextDouble() - 1) * Vmax;
            }
        }
    }
}
