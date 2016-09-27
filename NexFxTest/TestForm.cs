using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NexFxTest
{
    public partial class TestForm : NexFx.Presentations.ExForm
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void exButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ボタン");
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
        }
    }
}
