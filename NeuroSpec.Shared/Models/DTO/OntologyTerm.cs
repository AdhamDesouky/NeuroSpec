using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace NeuroSpec.Shared.Models.DTO
{
    public class OntologyTerm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("lbl")]
        public string Lbl { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("meta")]
        public Meta Meta { get; set; }

        public OntologyTerm()
        {
            Id = "";
            Lbl = "";
            Type = "";
            Meta = new Meta();
        }

        public OntologyTerm(string id, string lbl, string type, Meta meta)
        {
            Id = id;
            Lbl = lbl;
            Type = type;
            Meta = meta;
        }

    }

    public class Meta
    {
        [BsonElement("definition")]
        public Definition Definition { get; set; }
        [BsonElement("subsets")]
        public List<string> Subsets { get; set; }
        [BsonElement("synonyms")]
        public List<Synonym> Synonyms { get; set; }
        [BsonElement("xrefs")]
        public List<Xref> Xrefs { get; set; }
        [BsonElement("basicPropertyValues")]
        public List<BasicPropertyValue> BasicPropertyValues { get; set; }

        public Meta()
        {
            Definition = new Definition();
            Subsets = new List<string>();
            Synonyms = new List<Synonym>();
            Xrefs = new List<Xref>();
            BasicPropertyValues = new List<BasicPropertyValue>();
        }

        public Meta(Definition definition, List<string> subsets, List<Synonym> synonyms, List<Xref> xrefs, List<BasicPropertyValue> basicPropertyValues)
        {
            Definition = definition;
            Subsets = subsets;
            Synonyms = synonyms;
            Xrefs = xrefs;
            BasicPropertyValues = basicPropertyValues;
        }
    }

    public class Definition
    {
        [BsonElement("val")]
        public string Val { get; set; }
        [BsonElement("xrefs")]
        public List<string> Xrefs { get; set; }

        public Definition()
        {
            Val = "";
            Xrefs = new List<string>();
        }

        public Definition(string val, List<string> xrefs)
        {
            Val = val;
            Xrefs = xrefs;
        }
    }

    public class Synonym
    {
        [BsonElement("pred")]
        public string Pred { get; set; }
        [BsonElement("val")]
        public string Val { get; set; }

        public Synonym()
        {
            Pred = "";
            Val = "";
        }

        public Synonym(string pred, string val)
        {
            Pred = pred;
            Val = val;
        }
    }

    public class Xref
    {
        [BsonElement("val")]
        public string Val { get; set; }

        public Xref()
        {
            Val = "";
        }

        public Xref(string val)
        {
            Val = val;
        }
    }

    public class BasicPropertyValue
    {
        [BsonElement("pred")]
        public string Pred { get; set; }
        [BsonElement("val")]
        public string Val { get; set; }

        public BasicPropertyValue()
        {
            Pred = "";
            Val = "";
        }

        public BasicPropertyValue(string pred, string val)
        {
            Pred = pred;
            Val = val;
        }
    }
}
