using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace SATRScoreDisplay
{
    public partial class ResultForm : Form
    {
        int WinningTeamCode = 1;
        public ResultForm()
        {
            InitializeComponent();
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            string directoryName = Program.rootdirectory;

            string filename;
            if (WinningTeamCode == 1)
                filename = "AlphaWins.gif";
            else
                filename = "BravoWins.gif";

                //SEARCH GENRE FOLDER FIRST
                string ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\"+filename;
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\"+filename;
            if (File.Exists(ImageFileName))
            {
                
                Bitmap bitmap = (Bitmap) Bitmap.FromFile(ImageFileName);
                bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
                this.BackgroundImage = bitmap;
                this.TransparencyKey = bitmap.GetPixel(0, 0);
            }

        }
    }
}
