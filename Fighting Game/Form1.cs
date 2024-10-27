using System.Windows.Forms;
using System;
using System.Windows.Forms.VisualStyles;
using System.Text.Json.Serialization;
using System.Drawing.Imaging;
using System.Media;

namespace Fighting_Game
{
    public partial class Form1 : Form
    {
        private Image Jotaro;
        private Image Boss;
        private int gif1FrameIndex = 0;
        private int gif2FrameIndex = 0;
        private int gif1FrameCount;
        private int gif2FrameCount;
        private System.Windows.Forms.Timer animationTimer;

        private System.Windows.Forms.Timer shakeTimer;
        private int shakeCount;
        private Point originalPosition;
        private Random random;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        System.Windows.Forms.Timer HealthTimer = new System.Windows.Forms.Timer();

        private int JXP=-60;
        private int BXP = 350;
        int JYP = 90;
        int BYP = 90;
        private int tmpbx;
        private int tmpby;
        private int tmpjx;
        private int tmpjy;
        HealthBar jhp;
        HealthBarBoss bhp;
        string dmgto;
        public Form1()
        {
            InitializeComponent();
            tmpbx = BXP;
            tmpby = BYP;
            tmpjx = JXP;
            tmpjy = JYP;
            ChangeImg(ref Jotaro, "Idle", "Jotaro");
            ChangeImg(ref Boss, "Idle", "Boss");
            gif1FrameCount = Jotaro.GetFrameCount(FrameDimension.Time);
            gif2FrameCount = Boss.GetFrameCount(FrameDimension.Time);
            DoubleBuffered = true;
            
            animationTimer = new System.Windows.Forms.Timer { Interval = 100 }; // Adjust for desired frame rate
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
            buttons bt = new buttons(this);
            bt.Show();
            shakeTimer = new System.Windows.Forms.Timer
            {
                Interval = 50 // Adjust to control the speed of the shake
            };
            shakeTimer.Tick += ShakeTimer_Tick;
            timer.Interval = 50;
            timer.Tick+=Moving_Tick;
            HealthTimer.Interval = 100;
            HealthTimer.Tick += HealthRemoval_Tick;
            ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
            BkMusic();
            BossTimer.Interval = 50;
            BossTimer.Tick += BossWalking;
            jhp = new HealthBar
            {
                Location = new Point(40, 20),
                Size = new Size(200, 20),
                Health = 100 // Start at full health
            };
            Controls.Add(jhp);
            bhp = new HealthBarBoss
            {
                Location = new Point(400, 20),
                Size = new Size(200, 20),
                Health = 100 // Start at full health
            };
            Controls.Add(bhp);
        }
        private void BkMusic()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/bk(1).wav");
            simpleSound.PlayLooping();
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
            ImageAnimator.UpdateFrames(Jotaro);
            ImageAnimator.UpdateFrames(Boss);

            e.Graphics.DrawImage(Jotaro, new Rectangle(JXP, JYP, 350, 350)); // Resized position of the first GIF
            e.Graphics.DrawImage(Boss, new Rectangle(BXP, BYP, 350, 350)); // Resized position of the second GIF
        }
        public Image ChangeImg(ref Image img,string action,string Character)
        {
            Image image;
            using (Stream stream = File.OpenRead($"../../../{Character}/{Character}{action}.gif"))
            {
                image = System.Drawing.Image.FromStream(stream);
            }
            img = Image.FromFile($"../../../{Character}/{Character}{action}.gif");
            return img;
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose of images to free resources
            Jotaro.Dispose();
            Boss.Dispose();
            ImageAnimator.StopAnimate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.StopAnimate(Boss, new EventHandler(OnAnimationFrameChanged));
            base.OnFormClosing(e);
        }
        private void JPunch_Tick(object sender, EventArgs e)
        {
            ChangeImg(ref Jotaro, "Idle", "Jotaro");
            ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            timer1.Stop();
            TriggerShakeEffect(Boss);
            HealthTimer.Start();  
            dmgto = "Boss";
            dmg = 10;
        }
        public void Fight()
        {
            timer.Start();
            //Point tmp = Jotaro.Location;
            ChangeImg(ref Jotaro, "Walking", "Jotaro");
            ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
        }
        public Image img;
        public void TriggerShakeEffect(Image characterimg)
        {
            img= characterimg;
            shakeCount = 0; // Reset shake count
            shakeTimer.Start(); // Start shaking
        }
        private void Moving_Tick(object sender, EventArgs e)
        {
            JXP += 7;
            if (JXP > 200)
            {
                timer.Stop();
                timer1.Start();
                ChangeImg(ref Jotaro, "Punching", "Jotaro");
                ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            }
        }
        
        private void ShakeTimer_Tick(object sender, EventArgs e)
        {
            ImageAnimator.StopAnimate(img, new EventHandler(OnAnimationFrameChanged));
            random =new Random();
            if (shakeCount < 20) // Adjust the shake duration by changing the max count
            {
                // Generate a small random offset to move the PictureBox
                int offsetX = random.Next(-10, 10);
                int offsetY = random.Next(-5, 6);
                shakeCount++;
                if (img == Jotaro)
                {
                    JXP += offsetX;
                    JYP += offsetY;
                }
                else
                {
                    BXP += offsetX;
                    BYP += offsetY;
                }
            }
            else
            {
                JXP = tmpjx;
                JYP = tmpjy;
                BXP = tmpbx;
                BYP = tmpby;
                // Stop shaking and reset to the original position
                shakeTimer.Stop();
                ImageAnimator.Animate(img, new EventHandler(OnAnimationFrameChanged));
            }
        }
        System.Windows.Forms.Timer BossTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer BossAttackTimer = new System.Windows.Forms.Timer();

        public void GetHurt()
        {
            BossTimer.Start();
            BossAttackTimer.Interval = 1100;
            BossAttackTimer.Tick += BossAttack;
            ChangeImg(ref Boss, "Walking", "Boss");
            ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
        }
        public void BossWalking(object sender,EventArgs e) {
            BXP -= 13;
            if (BXP < 90)
            {
                BossTimer.Stop();
                BossAttackTimer.Start();
                ChangeImg(ref Boss, "Punching", "Boss");
                ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
            }
        }
        
        int dmg = 10;
        public void BossAttack(object sender,EventArgs e)
        {
            ChangeImg(ref Boss, "Idle", "Boss");
            ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
            BossAttackTimer.Stop();
            TriggerShakeEffect(Jotaro);
            HealthTimer.Start();
            dmgto = "Jotaro";
            dmg = 10;
        }
        public void HealthRemoval(ref HealthBar healthBar)
        {
            if(dmg!=1) dmg--;
            else
            {
                HealthTimer.Stop();
            }
            healthBar.Health -= 1;
            if (healthBar.Health == 0)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/getSerios.wav");
                simpleSound.Play();
                ChangeImg(ref Boss, "Winning", "Boss");
                ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
            }
        }
        public void HealthRemoval(ref HealthBarBoss healthBar)
        {
            if (dmg != 1) dmg--;
            else
            {
                HealthTimer.Stop();
            }
            healthBar.Health -= 1;
            if(healthBar.Health == 0)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/yareyare.wav");
                simpleSound.Play();
                ChangeImg(ref Jotaro, "Winning", "Jotaro");
                ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            }
        }
        public void HealthRemoval_Tick(object sender,EventArgs e)
        {
            if (dmgto == "Jotaro") HealthRemoval(ref jhp);
            else HealthRemoval(ref bhp);
        }
    }
}
