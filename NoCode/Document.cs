using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoCode.FBDCore;

namespace NoCode
{
    [Serializable]
    public class Document
    {
        public Document()
        {
            this.networks.Add(new FBDNetwork());
            this.networks.Add(new FBDNetwork());
            this.networks.Add(new FBDNetwork());
        }

        public Document(string name)
            : this()
        {
            this.name = name;
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        List<FBDNetwork> networks=new List<FBDNetwork>();
        public List<FBDNetwork> Networks
        {
            get
            {
                return networks;
            }
        }
    }
}
