using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NoCode
{
    public partial class MDIParent : Form
    {
       
        private int childFormNumber = 0;

        private BlockListTool blockListTool;

        public MDIParent()
        {
            Current.MainForm = this;

            InitializeComponent();
            blockListTool=new BlockListTool();
            Current.ProjectExplorerTool = new ProjectExplorerTool();

            blockListTool.Show(dockPanel,DockState.DockLeft);
            Current.ProjectExplorerTool.Show(blockListTool.Pane, DockAlignment.Top, 0.5);

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
           OpenDocument(new Document("Document " + childFormNumber++));
        }

        public void OpenDocument(Document doc)
        {
            DocumentForm childForm = new DocumentForm(doc);
            childForm.MdiParent = this;
            childForm.Show(this.dockPanel);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Current.Project != null
                &&MessageBox.Show("Close current project?","Info",MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }
            Current.Project = new Project();
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            Current.ProjectExplorerTool.LoadProject(new Project());
        }
    }
}
