using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HilbertCurve
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1((int)numericUpDown1.Value, (int)numericUpDown2.Value, comboBox1.SelectedIndex);
            form1.Show();           
        }
    }
}
