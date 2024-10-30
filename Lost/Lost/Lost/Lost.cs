using Lost.Properties;
using System.Media;
using System.Numerics;
using WMPLib;

namespace Lost
{
    public partial class Lost : Form
    {
        System.Windows.Forms.Timer frame1 = new System.Windows.Forms.Timer
        {
            Interval = 4500
        };
        System.Windows.Forms.Timer frame2 = new System.Windows.Forms.Timer
        {
            Interval = 1800
        };
        System.Windows.Forms.Timer frame3 = new System.Windows.Forms.Timer
        {
            Interval = 1700
        };
        System.Windows.Forms.Timer Text1 = new System.Windows.Forms.Timer
        {
            Interval = 3300
        };
        System.Windows.Forms.Timer Text2 = new System.Windows.Forms.Timer
        {
            Interval = 440
        };
        System.Windows.Forms.Timer Text3 = new System.Windows.Forms.Timer
        {
            Interval = 1600
        };
        System.Windows.Forms.Timer Frame4 = new System.Windows.Forms.Timer
        {
            Interval = 1700
        };
        System.Windows.Forms.Timer Text4 = new System.Windows.Forms.Timer
        {
            Interval = 1000
        };
        System.Windows.Forms.Timer SoundStop = new System.Windows.Forms.Timer
        {
            Interval = 500
        };
        System.Windows.Forms.Timer DedTime = new System.Windows.Forms.Timer
        {
            Interval = 3000
        };
        System.Windows.Forms.Timer lumina = new System.Windows.Forms.Timer
        {
            Interval = 500
        };
        SoundPlayer talking=new SoundPlayer("../../../sounds/talking.wav");
        SoundPlayer ended=new SoundPlayer("../../../sounds/ended.wav");
        SoundPlayer ded=new SoundPlayer("../../../sounds/Ded.wav");
        SoundPlayer finalAtack = new SoundPlayer("../../../sounds/atackFinal.wav");
        SoundPlayer switich=new SoundPlayer("../../../sounds/swtich.wav");
        WindowsMediaPlayer player; 
        int nr = 0;
        PictureBox gameover1;
        public Lost()
        {

            InitializeComponent();
            this.Opacity = 0;
            gameover1=new PictureBox();
            gameover1.Location = new Point(1, -1);
            gameover1.Name = "gameover";
            gameover1.Size = new Size(598, 528);
            gameover1.SizeMode = PictureBoxSizeMode.Zoom;
            gameover1.TabIndex = 2;
            gameover1.TabStop = false;
            gameover1.Image = Image.FromFile("../../../Photos/GameOver.png");
            Josuke.Enabled = false;
            this.FormBorderStyle=FormBorderStyle.None;
            lumina.Tick += Lumina_tick;
            lumina.Start();

        }
        public void Lumina_tick(object sender, EventArgs e)
        {
            if (this.Opacity == 1)
            {
                Josuke.Enabled = true;
                if (!File.Exists("bkmusic.mp3")) { File.Copy("../../../sounds/bkmusic.mp3", "bkmusic.mp3"); }
                player = new WindowsMediaPlayer();
                player.URL = "bkmusic.mp3";
                frame1.Tick += frame1_tick;
                frame1.Start();
                frame2.Tick += frame2_tick;
                Text1.Tick += text1_tick;
                Text1.Start();
                Text2.Tick += text2_tick;
                Text3.Tick += text3_tick;
                Frame4.Tick += frame4_tick;
                Text4.Tick += text4_tick;
                SoundStop.Tick += SoundStop_Tick;
                DedTime.Tick += Dedtime_Tick;
                lumina.Stop();
            }
            else
            {
                switich.Play();
                this.Opacity += 0.25;
            }
        }
        public void Dedtime_Tick(object sender, EventArgs e)
        {
            DedTime.Stop();
            Text4.Start();
        }
        int ending=0;
        public void SoundStop_Tick(object sender, EventArgs e)
        {
            talking.Stop();
            text.Enabled = false;
            ending++;
            if (ending == 6) {
                this.Controls.Remove(text);
                DedTime.Tick -= Dedtime_Tick;
                DedTime.Tick += ending_tick;
                Controls.Add(gameover1);
                DedTime.Start();
                
            } 
        }
        int tmp = 0;
        public void ending_tick(object sender, EventArgs e)
        {
            Controls.Remove(text);
            ended.PlaySync();
            this.Close();
        }
        public void text4_tick(object sender, EventArgs e)
        {
            text.Enabled = true;
            //text.Visible = false;
            text.Image = Image.FromFile("../../../Photos/studymore.gif");
            talking.Play();
            SoundStop.Start();
            nr++;
            Text4.Stop();
        }
        public void frame4_tick(object sender, EventArgs e)
        {
            Frame4.Stop();
            ded.Play();
            player.controls.stop();
            this.Controls.Remove(Josuke);
            //text.Visible = false;
            nr++;
        }
        public void text3_tick(object sender, EventArgs e)
        {
            text.Enabled = false;
            talking.Stop();
            Text3.Stop();
        }
        public void text2_tick(object sender, EventArgs e)
        {
            text.Enabled= false;
            talking.Stop();
            Text2.Stop();
        }
        public void text1_tick(object sender, EventArgs e)
        {
            text.Image = Image.FromFile("../../../Photos/whyuhere.gif");
            talking.Play();
            Text1.Stop();
        }
        public void frame1_tick(object sender, EventArgs e)
        {
            frame1.Stop();
            text.Enabled=false;
            Josuke.Image = Image.FromFile("../../../Photos/0.2.gif");
            talking.Stop();
            nr++;
        }
        public void frame2_tick(object sender, EventArgs e)
        {
            Josuke.Image = Image.FromFile("../../../Photos/1.9.gif");
            nr++;
            frame2.Stop();
        }

        private void MouseClick(object sender, EventArgs e)
        {
            if (nr == 1) {
                Josuke.Image = Image.FromFile("../../../Photos/1.8.gif");
                text.Image = Image.FromFile("../../../Photos/ulost.gif");
                text.Enabled = true;
                talking.Play();
                Text2.Start();
                frame2.Start();
            }
            if (nr == 2)
            {
                Josuke.Image = Image.FromFile("../../../Photos/1.7.gif");
                text.Enabled = true;
                text.Image = Image.FromFile("../../../Photos/finalone.gif");
                DedTime.Start();
                finalAtack.Play();
                Frame4.Start();
            }
        }
    }
}
