using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppEasyCs92B
{
    // public partial class Form1B : Form
    // {
    //     public Form1B()
    //     {
    //         InitializeComponent();
    //     }
    // }
    
    public partial class Form1B : Form
    {
        private Ball bl;
        private Cart ct;
        private Image im;
        private int dx, dy;
        private bool isOver;
        private bool isIn;
        private string imgPath = "C:\\Users\\Enin\\RiderProjects\\WindowsFormsAppEasyCs92B\\WindowsFormsAppEasyCs92B\\img\\";

        // public static void Main()
        // {
        //     Application.Run(new Sample4());
        // }
        public Form1B()
        {
            this.Text = "サンプル";
            this.ClientSize = new Size(600, 300);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
           
            im = Image.FromFile(imgPath + "sky.bmp");

            isOver = false;
            isIn = false;

            bl = new Ball();

            Point blp = new Point(0, 0);
            Image bim = Image.FromFile(imgPath + "apple.png");

            bl.Point = blp;
            bl.Image = bim;

            dx = 4;
            dy = 4;

            ct = new Cart();

            Point ctp = new Point(this.ClientSize.Width/2, this.ClientSize.Height-80);
            Image cim = Image.FromFile(imgPath + "cart.png");

            ct.Point = ctp;
            ct.Image = cim;
     
            Timer tm = new Timer();
            tm.Interval = 100;
            tm.Start();

            this.Paint += new PaintEventHandler(fm_Paint);
            tm.Tick += new EventHandler(tm_Tick);

            this.KeyDown += new KeyEventHandler(fm_KeyDown);
        }
        public void fm_Paint(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(im, 0, 0, im.Width, im.Height);

            Point blp = bl.Point;
            Image bim = bl.Image;

            g.DrawImage(bl.Image, blp.X, blp.Y, bim.Width, bim.Height);

            Point ctp = ct.Point;
            Image cim = ct.Image;
            g.DrawImage(ct.Image, ctp.X, ctp.Y, cim.Width, cim.Height);

            if (isOver == true)
            {
                Font f = new Font("SansSerif", 30);
                SizeF s = g.MeasureString("Game Over", f);

                float cx = (this.ClientSize.Width - s.Width) / 2;
                float cy = (this.ClientSize.Height - s.Height) / 2;

                g.DrawString("Game Over", f, new SolidBrush(Color.Blue), cx, cy);
            }
        }
        public void tm_Tick(Object sender, EventArgs e)
        {
            Point blp = bl.Point;
            Point ctp = ct.Point;

            Image bim = bl.Image;
            Image cim = ct.Image;

            if (blp.X < 0 || blp.X > this.ClientSize.Width - bim.Width) dx = -dx;
            if (blp.Y < 0) dy = -dy;
            if ((isIn == false) && (blp.X > ctp.X - bim.Width && blp.X < ctp.X + cim.Width) 
                                    && (blp.Y > ctp.Y - bim.Height && blp.Y < ctp.Y - bim.Height/2))
            {
                isIn = true;
                dy = -dy;
            }
            if(blp.Y < ctp.Y - bim.Height)
            {
                isIn = false;
            }
            if (blp.Y > this.ClientSize.Height)
            {
                isOver = true;
                Timer t = (Timer)sender;
                t.Stop();
            }

            blp.X = blp.X + dx;
            blp.Y = blp.Y + dy;

            bl.Point = blp;

            this.Invalidate();
        }
        public void fm_KeyDown(Object sender, KeyEventArgs e)
        {
            Point ctp = ct.Point;
            Image cim = ct.Image;

            if (e.KeyCode == Keys.Right)
            {
                ctp.X = ctp.X+2;
                if (ctp.X > this.ClientSize.Width-cim.Width)
                    ctp.X = this.ClientSize.Width-cim.Width;
            }
            else if (e.KeyCode == Keys.Left)
            {
                ctp.X = ctp.X-2;
                if (ctp.X < 0)
                    ctp.X = 0;
            }
            ct.Point = ctp;
            this.Invalidate();
        }
    }

    // class Ball
    // {
    //     public Image Image;
    //     public Point Point;
    // }
    // class Cart
    // {
    //     public Image Image;
    //     public Point Point;
    // }
}