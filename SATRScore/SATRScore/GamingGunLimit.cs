using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SATRScore
{
    public partial class TotalGamers : Form
    {
        public int GamingGunLimit = 50;
        public TotalGamers()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            GamingGunLimit = Convert.ToInt32(GunUpDown.Value);
            this.Close();
        }
    }
}
