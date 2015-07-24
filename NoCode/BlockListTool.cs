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
    public partial class BlockListTool : DockContent
    {
        public BlockListTool()
        {
            InitializeComponent();

            //  treeView1.Nodes[0].Nodes[0].Nodes[0].Tag=
        }

        private void BlockListTool_Load(object sender, EventArgs e)
        {
            InitFBDs();

            this.treeView1.ExpandAll();
        }

        private void InitFBDs()
        {
            TreeNode rootNode = treeView1.Nodes[0];

            TreeNode groupNode = new TreeNode("Bit Logic");

            TreeNode opNode = new TreeNode("AND");
            FBD fBD = new FBD();
            fBD.ID = 0;
            fBD.Name = "AND";
            fBD.Size = new Size(3, 2);
            FBDPara fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "";
            fBDPara.Index = 0;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "";
            fBDPara.Index = 1;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.Out;
            fBDPara.Name = "Output";
            fBDPara.Symbol = "";
            fBDPara.Index = 0;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.OutputParaList.Add(fBDPara);
            opNode.Tag = fBD;
            groupNode.Nodes.Add(opNode);

            opNode = new TreeNode("OR");
            fBD = new FBD();
            fBD.ID = 0;
            fBD.Name = "OR";
            fBD.Size = new Size(3, 2);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "";
            fBDPara.Index = 0;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "";
            fBDPara.Index = 1;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.Out;
            fBDPara.Name = "Output";
            fBDPara.Symbol = "";
            fBDPara.Index = 0;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.OutputParaList.Add(fBDPara);
            opNode.Tag = fBD;
            groupNode.Nodes.Add(opNode);

            rootNode.Nodes.Add(groupNode);
            
            groupNode = new TreeNode("Word Logic");

            opNode = new TreeNode("MOV_W");
            fBD = new FBD();
            fBD.ID = 0;
            fBD.Name = "MOV_W";
            fBD.Size = new Size(3, 3);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "En";
            fBDPara.Index = 1;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.In;
            fBDPara.Name = "Input";
            fBDPara.Symbol = "In";
            fBDPara.Index = 2;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.InputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.Out;
            fBDPara.Name = "Output";
            fBDPara.Symbol = "EnO";
            fBDPara.Index = 1;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.OutputParaList.Add(fBDPara);
            fBDPara = new FBDPara();
            fBDPara.Owner = fBD;
            fBDPara.ParaType = FBDParaTypeEnum.Out;
            fBDPara.Name = "Output";
            fBDPara.Symbol = "Out";
            fBDPara.Index = 2;
            fBDPara.CanLine = true;
            fBDPara.DefaultValue = "";
            fBD.OutputParaList.Add(fBDPara);
            opNode.Tag = fBD;
            groupNode.Nodes.Add(opNode);

            rootNode.Nodes.Add(groupNode);
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode && (e.Item as TreeNode).Tag is FBD)
            {
                DoDragDrop((e.Item as TreeNode).Tag, DragDropEffects.All);
            }
        }
    }
}
