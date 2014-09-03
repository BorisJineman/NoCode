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

        public void LoadProject(string filename)
        {
            BinaryFormatter formatter=new BinaryFormatter();
            using (FileStream fs = new FileStream(filename,FileMode.Open))
            {
                Current.Project = (Project)formatter.Deserialize(fs);
            }
        }
    }
}
