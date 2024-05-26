using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class OntologyTerm
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Def { get; set; }
        public List<string> Urls { get; set; }
        public List<string> Synonyms { get; set; }
        public string Parent { get; set; }
        public OntologyTerm() {
            Name = "";
            Id = "";
            Def = "";
            Urls = new List<string>();
            Synonyms = new List<string>();
            Parent = "";
        }
        public OntologyTerm(string name, string id, string def, List<string> urls, List<string> synonyms, string parent)
        {
            Name = name;
            Id = id;
            Def = def;
            Urls = urls;
            Synonyms = synonyms;
            Parent = parent;
        }


    }
}
