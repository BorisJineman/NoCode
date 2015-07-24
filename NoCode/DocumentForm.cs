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
        public Document Document { get; private set; }

        public DocumentForm(Document doc)
        {
            InitializeComponent();
            this.Document = doc;
            this.Tag = doc;
            this.Text = doc.Name;
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {
            foreach (FBDNetwork network in this.Document.Networks)
            {
                FBDNetworkControl networkControl = new FBDNetworkControl();
                networkControl.ID = this.Document.Networks.IndexOf(network);
                networkControl.FBDNetwork = network;
                this.flowLayoutPanel1.Controls.Add(networkControl);
            }

        }

        public void AddNetwork()
        {
            var network = new FBDNetwork();
            this.Document.Networks.Add(network);

            FBDNetworkControl networkControl = new FBDNetworkControl();
            networkControl.ID = this.Document.Networks.IndexOf(network);
            networkControl.FBDNetwork = network;
            this.flowLayoutPanel1.Controls.Add(networkControl);

            this.Refresh();
        }

        public void DeleteNetwork()
        {
            if (this.ActiveControl is FBDNetworkControl)
            {
                if (MessageBox.Show("Delete Current Network?", "Info", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                this.Document.Networks.Remove((this.ActiveControl as FBDNetworkControl).FBDNetwork);

                this.flowLayoutPanel1.Controls.Remove(this.ActiveControl);

                foreach (FBDNetworkControl networkControl in this.flowLayoutPanel1.Controls)
                {
                    networkControl.ID = this.Document.Networks.IndexOf(networkControl.FBDNetwork);
                }

                this.Refresh();
            }
        }

        public void AddPinPara()
        {
            if (this.ActiveControl is FBDNetworkControl)
            {
                (this.ActiveControl as FBDNetworkControl).AddInputParaPin();
            }
        }

        public void RemovePinPara()
        {
            if (this.ActiveControl is FBDNetworkControl)
            {
                (this.ActiveControl as FBDNetworkControl).RemoveInputParaPin();
            }
            
        }

        public void ReversePinPara()
        {
            if (this.ActiveControl is FBDNetworkControl)
            {
                (this.ActiveControl as FBDNetworkControl).ReverseSelectedParaPin();
            }
        }
    }
}
