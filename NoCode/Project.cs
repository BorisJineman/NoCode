using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoCode
{
    [Serializable]
    public class Project
    {
        public Project()
        {
            documents.Add(new Document("main"));
        }

        [NonSerialized]
        private string currentFile;
        public string CurrentFile
        {
            get { return currentFile; }
            set { currentFile = value; }
        }

        private string name="New Project";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        private List<Document> documents = new List<Document>();

        public List<Document> Documents
        {
            get { return documents; }
            set { documents = value; }
        }

    }
}
