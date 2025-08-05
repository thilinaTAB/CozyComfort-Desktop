using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyComfort_Desktop
{
    public partial class CozyComfort : Form
    {
        public CozyComfort()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void CloseAllMdiChildren()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }

        private void blanketModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildren();

            BlanketModel blanket = new BlanketModel();
            blanket.MdiParent = this;
            blanket.Show();
        }

        private void materialInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildren();
           
            ManageMaterial manageMaterial = new ManageMaterial();
            manageMaterial.MdiParent = this;
            manageMaterial.Show();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {   CloseAllMdiChildren();
            OrderView orderView = new OrderView();
            orderView.MdiParent = this;
            orderView.Show();

        }
    }
}
