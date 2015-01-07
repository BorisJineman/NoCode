using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoCode
{
    class Line
    {
        private Parameter begin;
        private Parameter end;

        public Parameter Begin
        {
            get { return begin; }
            set { begin = value; }
        }

        public Parameter End
        {
            get { return end; }
            set { end = value; }
        }
    }
}
