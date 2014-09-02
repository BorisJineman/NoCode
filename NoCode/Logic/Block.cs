using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NoCode.Logic
{
    public class Block
    {
        private Size size;
        private string name;


        private List<Parameter> inputParaList;
        private List<Parameter> outPutParaList;

        public List<Parameter> InputParaList
        {
            get { return inputParaList; }
            set { inputParaList = value; }
        }

        public List<Parameter> OutPutParaList
        {
            get { return outPutParaList; }
            set { outPutParaList = value; }
        }

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
