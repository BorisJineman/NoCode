using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.Serialization.Formatters;

namespace NoCode
{
    public partial class ProjectExplorerTool : DockContent
    {
        public ProjectExplorerTool()
        {
            InitializeComponent();
        }

        public Document AddDocument(Document doc)
        {
            Current.Project.Documents.Add(doc);

            TreeNode projNode = this.treeView1.Nodes["ProjectNode"];

            projNode.Nodes.Add("DocumentNode", doc.Name).Tag = doc;

            return doc;

        }

        public void LoadProjectFromFile(string filename)
        {
            BinaryFormatter formatter=new BinaryFormatter();
            using (FileStream fs = new FileStream(filename,FileMode.Open))
            {
                Project proj = (Project)formatter.Deserialize(fs);
                this.LoadProject(proj);
            }
        }

        public void LoadProject(Project proj)
        {
            Current.MainForm.CloseAllChildForms();

            Current.Project = proj;
            
            TreeNode projNode = this.treeView1.Nodes["ProjectNode"];

            projNode.Nodes.Clear();

            projNode.Text = "Project - " + proj.Name;

            foreach (Document doc in proj.Documents)
            {
                projNode.Nodes.Add("DocumentNode", doc.Name).Tag = doc;

                if (doc.Name == "main")
                {
                    Current.MainForm.OpenDocument(doc);
                }
            }

            

            this.treeView1.ExpandAll();
        }

        public void SaveProjectToFile(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, Current.Project);
            }
        }

        public void SaveProjectToFile()
        {
            if (Current.Project.CurrentFile == null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Current.Project.CurrentFile = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }

            SaveProjectToFile(Current.Project.CurrentFile);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "DocumentNode")
            {
                Current.MainForm.OpenDocument(e.Node.Tag as Document);
            }
        }
    }
}
