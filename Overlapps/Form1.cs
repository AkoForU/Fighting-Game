using System.Drawing.Imaging;
using static System.Windows.Forms.Timer;
namespace Overlapps
{
    public partial class Form1 : Form
    {
        private Image gif1;
        private Image gif2;
        private int gif1FrameIndex = 0;
        private int gif2FrameIndex = 0;
        private int gif1FrameCount;
        private int gif2FrameCount;
        private int spx;
        

        private System.Windows.Forms.Timer sptimer=new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer spmoving = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer animationTimer;
        public Form1()
        {
            InitializeComponent();
            gif1 = Image.FromFile(@"../../../Jotaro/JotaroPunching.gif");
            gif2 = Image.FromFile(@"../../../Jotaro/StarPlatinum.gif");
            spmoving.Tick += spmoving_Tick;
            spmoving.Interval = 50;
            spmoving.Start();
            // Set up the ImageAnimator
            gif1FrameCount = gif1.GetFrameCount(FrameDimension.Time);
            gif2FrameCount = gif2.GetFrameCount(FrameDimension.Time);
            DoubleBuffered = true;
            // Set up a timer to control the animation
            animationTimer = new System.Windows.Forms.Timer { Interval = 100 }; // Adjust for desired frame rate
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            // Enable the animator for both GIFs
            ImageAnimator.Animate(gif1, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.Animate(gif2, new EventHandler(OnAnimationFrameChanged));
            

            HealthBar healthBar = new HealthBar
            {
                Location = new Point(50, 50),
                Size = new Size(200, 30),
                Health = 100 // Start at full health
            };
            Controls.Add(healthBar);
            healthBar.Health -= 10;
            sptimer.Tick += sptimer_Tick;
            sptimer.Interval = 1500;
            sptimer.Start();
            spx = 270;
        }
        int numar = 10;
        public void spmoving_Tick(object sender, EventArgs e)
        {
            spx += numar;
            numar -= 1;
        }
        public void sptimer_Tick(object sender, EventArgs e)
        {
            ImageAnimator.StopAnimate(gif2, new EventHandler(OnAnimationFrameChanged));
            gif1 = Image.FromFile(@"../../../Jotaro/JotaroIdle.gif");
            ImageAnimator.Animate(gif1, new EventHandler(OnAnimationFrameChanged));
            sptimer.Stop();
            spmoving.Stop();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Advance the frames for each GIF
            gif1FrameIndex = (gif1FrameIndex + 1) % gif1FrameCount;
            gif2FrameIndex = (gif2FrameIndex + 1) % gif2FrameCount;

            // Request a repaint
            Invalidate();
        }
        private void OnAnimationFrameChanged(object sender, EventArgs e)
        {
            // Request a repaint whenever the animation frame changes
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw both GIFs at specified locations
            ImageAnimator.UpdateFrames(gif1);
            ImageAnimator.UpdateFrames(gif2);

            e.Graphics.DrawImage(gif1, new Rectangle(100, 100, 350, 350)); // Resized position of the first GIF
            e.Graphics.DrawImage(gif2, new Rectangle(spx, 180, 170, 170)); // Resized position of the second GIF
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose of images to free resources
            gif1.Dispose();
            gif2.Dispose();
            ImageAnimator.StopAnimate(gif1, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.StopAnimate(gif2, new EventHandler(OnAnimationFrameChanged));
            base.OnFormClosing(e);
        }
    }
}
