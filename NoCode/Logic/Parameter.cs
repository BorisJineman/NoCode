using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoCode.Logic
{
    public class Parameter
    {
        private string name;
        private ParameterState state;
        private ParameterType type;
        private ParameterDataType dataType;

        public ParameterState State
        {
            get { return state; }
            set { state = value; }
        }

        public ParameterType Type
        {
            get { return type; }
            set { type = value; }
        }

        public ParameterDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
