using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppEasyCs92B
{
    // public partial class Form1 : Form
    // {
    //     public Form1()
    //     {
    //         InitializeComponent();
    //     }
    // }
    
    public partial class Form1 : Form
    {
        private Ball bl;
        private Cart ct;
        private Image im;
        private int dx, dy;
        private bool isOver;
        private bool isIn;
        private string imgPath = "C:\\Users\\Enin\\RiderProjects\\WindowsFormsAppEasyCs92B\\WindowsFormsAppEasyCs92B\\img\\";
        
        public Form1()
        {
            InitializeComponent();
            this.Text = "Receive Apple";
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
            Point ctp = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 80);
            Image cim = Image.FromFile(imgPath + "cart.png");
    
            ct.Point = ctp;
            ct.Image = cim;
    
            Timer tm = new Timer();
            tm.Interval = 100;
            tm.Start();
    
            this.Paint += new PaintEventHandler(FmPaint2);
            tm.Tick += new EventHandler(TmTick2);
            this.KeyDown += new KeyEventHandler(FmKeyDown);
        }
    
        public void FmPaint2(Object sender, PaintEventArgs e)
        {
            // Graphics g = new Graphics();
            // Graphics g = e.Graphics();
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
    
        public void TmTick2(Object sender, EventArgs e)
        {
            Point blp = bl.Point;
            Point ctp = ct.Point;
    
            Image bim = bl.Image;
            Image cim = ct.Image;
    
            // The ball bounces back when it hits to the wall
            if (blp.X < 0 || blp.X > this.ClientSize.Width - bim.Width)
            {
                dx = -dx;
            }
    
            // When the ball hits to the ceiling
            if (blp.Y < 0)
            {
                dy = -dy;
            }
    
            // When tha ball hits to the Cart
            if ((isIn == false) && (blp.X > ctp.X - bim.Width && blp.X < ctp.X + cim.Width) &&
                (blp.Y > ctp.Y - bim.Height && blp.Y < ctp.Y - bim.Height / 2))
            {
                isIn = true;
                dy = -dy;
            }
    
            if (blp.Y < ctp.Y - bim.Height)
            {
                isIn = false;
            }
    
            // GameOver
            if (blp.Y > this.ClientSize.Height)
            {
                isOver = true;
                Timer t = (Timer) sender;
                t.Stop();
            }
    
            blp.X += dx;
            blp.Y += dy;
    
            bl.Point = blp;
            this.Invalidate();
        }
    
        // public void TmTick2(Object sender, EventArgs e)
        // {
        //     Point blp = bl.Point;
        //     Point ctp = ct.Point;
        //     Image bim = bl.Image;
        //     Image cim = ct.Image;
        // }
    
        public void FmKeyDown(Object sender, KeyEventArgs e)
        {
            Point ctp = ct.Point;
            Image cim = ct.Image;
    
            if (e.KeyCode == Keys.Right)
            {
                ctp.X += 2;
                if (ctp.X > this.ClientSize.Width - cim.Width)
                {
                    ctp.X = this.ClientSize.Width - cim.Width;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                ctp.X -= 2;
                if (ctp.X < 0)
                {
                    ctp.X = 0;
                }
    
                ct.Point = ctp;
                this.Invalidate();
            }
        }
    }
        
    // class Ball
    // {
    //     public Color Color;
    //     public Point Point;
    //     public Image Image;
    // }
    //
    // class Cart
    // {
    //     public Image Image;
    //     public Point Point;
    // }
}