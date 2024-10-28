using System.Windows.Forms;
using System;
using System.Windows.Forms.VisualStyles;
using System.Text.Json.Serialization;
using System.Drawing.Imaging;
using System.Media;
using WMPLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fighting_Game
{
    public partial class Form1 : Form
    {
        private Image Jotaro;
        private Image Boss;
        private Image Starplatinum;
        private int gif1FrameIndex = 0;
        private int gif2FrameIndex = 0;
        private int gif3FrameIndex = 0;
        private int gif1FrameCount;
        private int gif2FrameCount;
        private int gif3FrameCount;
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
        WindowsMediaPlayer player=new WindowsMediaPlayer();
        private int tmpbx;
        private int tmpby;
        private int tmpjx;
        private int tmpjy;
        HealthBar jhp;
        HealthBarBoss bhp;
        string dmgto;
        int numar;

        private System.Windows.Forms.Timer sptimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer spmoving = new System.Windows.Forms.Timer();
        private int spx;
        
        System.Windows.Forms.Timer spAtack = new System.Windows.Forms.Timer
        {
            Interval = 1400
        };

        System.Windows.Forms.Timer BossTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer BossAttackTimer = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
            tmpbx = BXP;
            tmpby = BYP;
            tmpjx = JXP;
            tmpjy = JYP;

            spmoving.Tick += spmoving_Tick;
            spmoving.Interval = 50;
            //spmoving.Start();

            sptimer.Tick += sptimer_Tick;
            sptimer.Interval = 1500;
            //sptimer.Start();

            ChangeImg(ref Jotaro, "Idle", "Jotaro");
            ChangeImg(ref Boss, "Idle", "Boss");
            Starplatinum = Image.FromFile($"../../../Jotaro/StarPlatinum.gif");
            gif1FrameCount = Jotaro.GetFrameCount(FrameDimension.Time);
            gif2FrameCount = Boss.GetFrameCount(FrameDimension.Time);
            gif3FrameCount=Starplatinum.GetFrameCount(FrameDimension.Time);
            DoubleBuffered = true;
            if(!File.Exists("1.mp3"))File.Copy("../../../Music/1.mp3", "1.mp3");
            player.URL="1.mp3";
            player.settings.volume = 80;

            animationTimer = new System.Windows.Forms.Timer { Interval = 100 }; // Adjust for desired frame rate
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
            buttons bt = new buttons(this);
            bt.Show();
            shakeTimer = new System.Windows.Forms.Timer
            {
                Interval = 50 // Adjust to control the speed of the shake
            };
            spAtack.Tick += StopAnimation;
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
        public void spmoving_Tick(object sender, EventArgs e)
        {
            spx += numar;
            numar -= 1;
        }
        public void sptimer_Tick(object sender, EventArgs e)
        {
            ImageAnimator.StopAnimate(Starplatinum, new EventHandler(OnAnimationFrameChanged));
            sptimer.Stop();
            spmoving.Stop();
        }
        private void BkMusic()
        {
            player.controls.play();
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Advance the frames for each GIF
            gif1FrameIndex = (gif1FrameIndex + 1) % gif1FrameCount;
            gif2FrameIndex = (gif2FrameIndex + 1) % gif2FrameCount;
            gif3FrameIndex = (gif3FrameIndex + 1) % gif3FrameCount;

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
            ImageAnimator.UpdateFrames(Starplatinum);

            e.Graphics.DrawImage(Jotaro, new Rectangle(JXP, JYP, 350, 350)); // Resized position of the first GIF
            e.Graphics.DrawImage(Boss, new Rectangle(BXP, BYP, 350, 350));
            e.Graphics.DrawImage(Starplatinum, new Rectangle(spx, 180, 170, 170)); // Resized position of the second GIF
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
            Starplatinum.Dispose();
            ImageAnimator.StopAnimate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.StopAnimate(Boss, new EventHandler(OnAnimationFrameChanged));
            ImageAnimator.StopAnimate(Starplatinum, new EventHandler(OnAnimationFrameChanged));
            base.OnFormClosing(e);
        }
        private void JPunch_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            spAtack.Start();
            SoundPlayer tmp = new SoundPlayer("../../../Music/ORA.wav");
            tmp.Play();
            HealthTimer.Start();  
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
                ImageAnimator.Animate(Starplatinum, new EventHandler(OnAnimationFrameChanged));
                SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/getSerios.wav");
                simpleSound.Play();
                TriggerShakeEffect(Boss);
                spx = JXP + 100;
                sptimer.Start();
                spmoving.Start();
                
                dmgto = "Boss";
                dmg = 10;
                
                numar = 10;
            }
        }
        
        public void StopAnimation(object sender, EventArgs e)
        {
            ChangeImg(ref Jotaro, "Idle", "Jotaro");
            ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
            spAtack.Stop();
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
            healthBar.Health -= 5;
            //Winner
            if (healthBar.Health == 0)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/getSerios.wav");
                simpleSound.Play();
                ChangeImg(ref Boss, "Winning", "Boss");
                player.controls.stop();
                ImageAnimator.Animate(Boss, new EventHandler(OnAnimationFrameChanged));
                return;
            }
        }
        public void HealthRemoval(ref HealthBarBoss healthBar)
        {
            if (dmg != 1) dmg--;
            else
            {
                HealthTimer.Stop();
            }
            healthBar.Health -= 5;

            //Winner
            if(healthBar.Health == 0)
            {
                spAtack.Stop();
                ImageAnimator.StopAnimate(Jotaro,new EventHandler(OnAnimationFrameChanged));
                SoundPlayer simpleSound = new SoundPlayer(@"../../../Music/yareyare.wav");
                simpleSound.Play();
                ChangeImg(ref Jotaro, "Winning", "Jotaro");
                player.controls.stop();
                ImageAnimator.Animate(Jotaro, new EventHandler(OnAnimationFrameChanged));
                return;
            }
        }
        public void HealthRemoval_Tick(object sender,EventArgs e)
        {
            if (dmgto == "Jotaro") HealthRemoval(ref jhp);
            else HealthRemoval(ref bhp);
        }
    }
}
