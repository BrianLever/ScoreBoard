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
    public partial class BootScreen : Form
    {
        public BootScreen()
        {
            InitializeComponent();
        }

        private void BootScreen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sATRScoreDataSet.Genre' table. You can move, or remove it, as needed.
            this.genreTableAdapter.Fill(this.sATRScoreDataSet.Genre);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BootScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
