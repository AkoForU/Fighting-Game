using System.Media;
using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Principal;
using Fighting_Game;
using System.Windows.Forms.VisualStyles;


namespace Winner
{
    public partial class JotaroWinning : Form
    {
        WindowsMediaPlayer player =new WindowsMediaPlayer();
        System.Windows.Forms.Timer opacity = new System.Windows.Forms.Timer {
            Interval = 20
        };
        System.Windows.Forms.Timer WinTimer = new System.Windows.Forms.Timer
        {
            Interval = 20
        };
        System.Windows.Forms.Timer WalkTimer = new System.Windows.Forms.Timer
        {
            Interval = 20
        };
        private readonly FightGame form1;
        public JotaroWinning(FightGame tmp)
        {
            this.StartPosition=FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            if (!File.Exists("JotaroTheme.mp3")) File.Copy("../../../Music/JotaroTheme.mp3","JotaroTheme.mp3");
            player.URL = "JotaroTheme.mp3";
            player.settings.volume = 25;
            player.controls.play();
            InitializeComponent();
            this.Opacity = 0;
            form1 = tmp;
            tmp.Visible = false;
            WalkTimer.Tick += WalkTimer_Tick;
            WinTimer.Tick += WinTimer_Tick;
            opacity.Tick += OpTick;
            opacity.Start();

        }
        System.Windows.Forms.Timer SmokingTimer = new System.Windows.Forms.Timer
        {
            Interval = 2400
        };
        public void WalkTimer_Tick(object sender, EventArgs e)
        {
            if (Jotaro.Location.X < -131) Jotaro.Location = new Point(Jotaro.Location.X+1, Jotaro.Location.Y);
            else
            {
                WalkTimer.Stop();
                Jotaro.Image = Image.FromFile("../../../Photos/JotaroWinning.gif");
                Jotaro.Location = new Point(Jotaro.Location.X - 20, Jotaro.Location.Y);
                SoundPlayer yare = new SoundPlayer("../../../Music/yareyare.wav");
                yare.Play();
                SmokingTimer.Tick += Smooking;
                SmokingTimer.Start();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose of images to free resources
            form1.Close();
            base.OnFormClosing(e);
        }
        public void Smooking(object sender, EventArgs e)
        {
            Jotaro.Size = new Size(465, 310);
            Jotaro.Location=new Point(-40, Jotaro.Location.Y+23);
            Jotaro.Image=Image.FromFile("../../../Photos/jotarosmooking.gif");
            SmokingTimer.Stop();
        }
        public void WinTimer_Tick(object sender, EventArgs e)
        {
            if (WinningText.Location.Y!=0)WinningText.Location = new Point(WinningText.Location.X, WinningText.Location.Y + 1);
            else
            {
                WinTimer.Interval = 500;
                if (WinningText.Visible) WinningText.Visible = false;
                else WinningText.Visible = true;
            }
        }
        public void OpTick(object sender, EventArgs e)
        {
            if (this.Opacity == 1)
            {
                opacity.Stop();
                WinTimer.Start();
                WalkTimer.Start();
            }
            this.Opacity += 0.025;
        }
    }
}

