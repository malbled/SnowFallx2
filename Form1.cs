using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnowFall
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap background = Properties.Resources.фон_зима;
        Graphics g;
        Bitmap snezh = Properties.Resources.снежинка;
        static int n = 50;
        Snow[] snow = new Snow[n];
        Random rnd = new Random();
        private Timer timer1;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            g = Graphics.FromImage(bmp);
            for (int i = 0; i < n; i++)
            {
                snow[i] = new Snow();
                snow[i].x = rnd.Next(5, Screen.PrimaryScreen.WorkingArea.Width);
                snow[i].y = -rnd.Next(1, Screen.PrimaryScreen.WorkingArea.Height);
                snow[i].size = rnd.Next(5, 30);
            }
            timer1 = new Timer();
            timer1.Interval = 100;
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < n; i++)
            {
                snow[i].y += snow[i].size;
                if (snow[i].y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snow[i].x = rnd.Next(5, Screen.PrimaryScreen.WorkingArea.Width);
                    snow[i].y = 0;
                }
            }
            Outline();
            timer1.Start();
        }

        private void Outline()
        {
            g.DrawImage(background, new Rectangle(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height));
            for (int i = 0; i < n; i++)
            {
                if(snow[i].y > -30)
                {
                    g.DrawImage(snezh,
                   new Rectangle(snow[i].x, snow[i].y, snow[i].size, snow[i].size));

                }
            }
            var graf = this.CreateGraphics();
            graf.DrawImage(bmp,0,0);
        }      

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Outline();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }
    }
}
