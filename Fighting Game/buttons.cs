using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fighting_Game
{
    public partial class buttons : Form
    {
        Form1 scene;
        public buttons(Form1 scene)
        {
            InitializeComponent();
            this.scene = scene;
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            scene.Fight();
        }

        private void GeHurt_Click(object sender, EventArgs e)
        {
            scene.GetHurt();
        }
    }
}
