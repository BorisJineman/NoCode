using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoCode.FBDCore;
using WeifenLuo.WinFormsUI.Docking;

namespace NoCode
{
    public partial class DocumentForm : DockContent
    {
        private Document doc;
        public DocumentForm(Document doc)
        {
            InitializeComponent();
            this.doc = doc;
            this.Text = doc.Name;
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {
            foreach (FBDNetwork network in this.doc.Networks)
            {
                FBDNetworkControl networkControl = new FBDNetworkControl();
                networkControl.FBDNetwork = network;
                this.flowLayoutPanel1.Controls.Add(networkControl);
            }

        }
    }
}
