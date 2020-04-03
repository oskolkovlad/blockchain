using System;
using System.Windows.Forms;

namespace BlockChain
{
    public partial class Form1 : Form
    {
        Chain chain;
        public Form1()
        {
            InitializeComponent();

            chain = new Chain();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            blocksListBox.Items.Clear();

            chain.Add(dataTextBox.Text, "Admin");
            foreach (var block in chain.Blocks)
            {
                blocksListBox.Items.Add(block);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var block in chain.Blocks)
            {
                blocksListBox.Items.Add(block);
            }
        }
    }
}
