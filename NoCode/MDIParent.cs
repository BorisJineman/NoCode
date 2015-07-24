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
            var doc=Current.ProjectExplorerTool.AddDocument(new Document("Logic" + childFormNumber++));
            OpenDocument(doc);
        }

        public void OpenDocument(Document doc)
        {
            foreach (var form in this.MdiChildren)
            {
                if (form.Tag == doc)
                {
                    this.ActivateMdiChild(form);
                    return;
                }
            }
            DocumentForm childForm = new DocumentForm(doc);
            childForm.MdiParent = this;
            childForm.Show(this.dockPanel);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Project Files (*.lpp)|*.lpp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                Current.ProjectExplorerTool.LoadProjectFromFile(fileName);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Project Files (*.lpp)|*.lpp|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                Current.ProjectExplorerTool.SaveProjectToFile(fileName);
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
        

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
        }

        public void CloseAllChildForms()
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
            Current.ProjectExplorerTool.LoadProject(new Project());
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            Current.ProjectExplorerTool.LoadProject(new Project());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Current.ProjectExplorerTool.SaveProjectToFile();
        }

        private void Compile_Click(object sender, EventArgs e)
        {
            foreach (var doc in Current.Project.Documents)
            {
                foreach (var network in doc.Networks)
                {
                    network.Check();
                    network.Compile();
                }
            }

            foreach (var form in this.MdiChildren)
            {
                form.Refresh();
            }
        }

        private void AddNetwork_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null &&
                this.ActiveMdiChild is DocumentForm)
            {
                DocumentForm form = this.ActiveMdiChild as DocumentForm;

                form.AddNetwork();
            }
        }

        private void DeleteNetwork_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null &&
               this.ActiveMdiChild is DocumentForm)
            {
                DocumentForm form = this.ActiveMdiChild as DocumentForm;

                form.DeleteNetwork();
            }
        }

        private void AddParaButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null &&
              this.ActiveMdiChild is DocumentForm)
            {
                DocumentForm form = this.ActiveMdiChild as DocumentForm;

                form.AddPinPara();
            }
        }

        private void RemoveParaButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null &&
              this.ActiveMdiChild is DocumentForm)
            {
                DocumentForm form = this.ActiveMdiChild as DocumentForm;

                form.RemovePinPara();
            }
        }

        private void ReverseButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null &&
              this.ActiveMdiChild is DocumentForm)
            {
                DocumentForm form = this.ActiveMdiChild as DocumentForm;

                form.ReversePinPara();
            }
        }
    }
}
