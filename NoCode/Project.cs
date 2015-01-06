using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoCode
{
    public class Project
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<string> documentFileList = new List<string>();
        public List<String> DocumentFileList
        {
            get { return documentFileList; }
        }

        private string currentFile;
        public string CurrentFile
        {
            get { return currentFile; }
            set { currentFile = value; }
        }
    }
}
