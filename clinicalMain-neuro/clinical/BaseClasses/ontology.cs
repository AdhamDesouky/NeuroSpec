using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace clinical.BaseClasses
{
    public class ontology
    {

        static string FileName = "..\\..\\..\\BaseClasses\\Terms.txt";

        public ontology()
        {
            // Assign the file name
            FileName = "..\\..\\..\\BaseClasses\\Terms.txt";
        }

        // A method to return the first 10 ontologies from the text file
        public List<OntologyTerm> GetFirstTenOntologies()
        {
            // Declare a list to store the ontologies
            List<OntologyTerm> ontologies = new List<OntologyTerm>();

            // Declare a variable to store the current term
            OntologyTerm currentTerm = null;

            // Declare a variable to store the current line
            string line;

            // Declare a variable to store the number of ontologies
            int ontologyCount = 0;

            // Use a stream reader to read the text file
            using (var sr = new StreamReader(FileName))
            {
                // Read the file line by line until the end or the ontology limit is reached
                while ((line = sr.ReadLine()) != null && ontologyCount < 10)
                {
                    // Check if the line starts with a term tag
                    if (line.StartsWith("[Term]"))
                    {
                        // If there is a previous term, add it to the list
                        if (currentTerm != null)
                        {
                            ontologies.Add(currentTerm);
                            ontologyCount++;
                        }

                        // Create a new term and initialize its properties
                        currentTerm = new OntologyTerm();
                        currentTerm.Name = "";
                        currentTerm.Id = "";
                        currentTerm.Def = "";
                        currentTerm.Urls = new List<string>();
                        currentTerm.Synonyms = new List<string>();
                    }
                    // Check if the line starts with a name tag
                    else if (line.StartsWith("name: "))
                    {
                        // Extract the name value and assign it to the current term
                        currentTerm.Name = line.Substring(6);
                    }
                    // Check if the line starts with an id tag
                    else if (line.StartsWith("id: "))
                    {
                        // Extract the id value and assign it to the current term
                        currentTerm.Id = line.Substring(4);
                    }
                    // Check if the line starts with a def tag
                    else if (line.StartsWith("def: "))
                    {
                        // Extract the def value and assign it to the current term
                        currentTerm.Def = line.Substring(6, line.IndexOf('[') - 7);

                        // Extract the urls from the def value and add them to the current term
                        foreach (var url in line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1).Split(','))
                        {
                            currentTerm.Urls.Add(url.Trim().Substring(4));
                        }
                    }
                    // Check if the line starts with a synonym tag
                    else if (line.StartsWith("synonym: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Synonyms.Add(line.Substring(10, line.IndexOf('"', 11)));
                    }
                }

                // If there is a last term, add it to the list
                if (currentTerm != null)
                {
                    ontologies.Add(currentTerm);
                    ontologyCount++;
                }
            }

            // Return the list of ontologies
            return ontologies;
        }
        public List<OntologyTerm> GetAllOntologies()
        {
            // Declare a list to store the ontologies
            List<OntologyTerm> ontologies = new List<OntologyTerm>();

            // Declare a variable to store the current term
            OntologyTerm currentTerm = null;

            // Declare a variable to store the current line
            string line;


            // Use a stream reader to read the text file
            using (var sr = new StreamReader(FileName))
            {
                // Read the file line by line until the end or the ontology limit is reached
                while ((line = sr.ReadLine()) != null)
                {
                    // Check if the line starts with a term tag
                    if (line.StartsWith("[Term]"))
                    {
                        // If there is a previous term, add it to the list
                        if (currentTerm != null)
                        {
                            ontologies.Add(currentTerm);
                        }

                        // Create a new term and initialize its properties
                        currentTerm = new OntologyTerm();
                        currentTerm.Name = "";
                        currentTerm.Id = "";
                        currentTerm.Def = "";
                        currentTerm.Urls = new List<string>();
                        currentTerm.Synonyms = new List<string>();
                    }
                    // Check if the line starts with a name tag
                    else if (line.StartsWith("name: "))
                    {
                        // Extract the name value and assign it to the current term
                        currentTerm.Name = line.Substring(6);
                    }
                    // Check if the line starts with an id tag
                    else if (line.StartsWith("id: "))
                    {
                        // Extract the id value and assign it to the current term
                        currentTerm.Id = line.Substring(4);
                    }
                    // Check if the line starts with a def tag
                    else if (line.StartsWith("def: "))
                    {
                        // Extract the def value and assign it to the current term
                        currentTerm.Def = line.Substring(6, line.IndexOf('[') - 7);
                        if(line.IndexOf(']') - line.IndexOf('[') - 1 > 0)
                        {
                            // Extract the urls from the def value and add them to the current term
                            foreach (var url in line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1).Split(','))
                            {
                                if(url.Trim().Length>4)
                                currentTerm.Urls.Add(url.Trim().Substring(4));
                            }
                        }
                        
                    }
                    // Check if the line starts with a synonym tag
                    else if (line.StartsWith("synonym: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Synonyms.Add(line.Substring(10, line.IndexOf('"', 11) - 10));
                    }
                    else if (line.StartsWith("is_a: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Parent=line.Substring(line.IndexOf('!')+1);
                    }
                }
                
                // If there is a last term, add it to the list
                if (currentTerm != null)
                {
                    ontologies.Add(currentTerm);
                }
            }

            // Return the list of ontologies
            return ontologies;
        }

        public static OntologyTerm searchForTerm(string searchTerm)
        {
            // Declare a variable to store the current term
            OntologyTerm currentTerm = null;

            // Declare a variable to store the current line
            string line;

            // Use a stream reader to read the text file
            using (var sr = new StreamReader(FileName))
            {
                // Read the file line by line until the end
                while ((line = sr.ReadLine()) != null)
                {
                    // Check if the line starts with a term tag
                    if (line.StartsWith("[Term]"))
                    {
                        // If there is a previous term, check if it matches the search term
                        if (currentTerm != null)
                        {
                            // If the name or the synonyms of the current term match the search term, return it
                            if (currentTerm.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                currentTerm.Synonyms.Any(s => s.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)))
                            {
                                return currentTerm;
                            }
                        }

                        // Create a new term and initialize its properties
                        currentTerm = new OntologyTerm();
                        currentTerm.Name = "";
                        currentTerm.Id = "";
                        currentTerm.Def = "";
                        currentTerm.Urls = new List<string>();
                        currentTerm.Synonyms = new List<string>();
                    }
                    // Check if the line starts with a name tag
                    else if (line.StartsWith("name: "))
                    {
                        // Extract the name value and assign it to the current term
                        currentTerm.Name = line.Substring(6);
                    }
                    // Check if the line starts with an id tag
                    else if (line.StartsWith("id: "))
                    {
                        // Extract the id value and assign it to the current term
                        currentTerm.Id = line.Substring(4);
                    }
                    // Check if the line starts with a def tag
                    else if (line.StartsWith("def: "))
                    {
                        // Extract the def value and assign it to the current term
                        currentTerm.Def = line.Substring(6, line.IndexOf('[') - 7);
                        // Extract the urls from the def value and add them to the current term
                        //foreach (var url in line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1).Split(','))
                        //{
                        //    currentTerm.Urls.Add(url.Trim().Substring(4));
                        //}
                    }
                    // Check if the line starts with a synonym tag
                    else if (line.StartsWith("synonym: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Synonyms.Add(line.Substring(10, line.IndexOf('"', 11) - 10));
                    }
                    else if (line.StartsWith("is_a: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Parent = line.Substring(line.IndexOf('!') + 1);
                    }
                }

                // If there is a last term, check if it matches the search term
                if (currentTerm != null)
                {
                    // If the name or the synonyms of the current term match the search term, return it
                    if (currentTerm.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        currentTerm.Synonyms.Any(s => s.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)))
                    {
                        return currentTerm;
                    }
                }
            }

            // If no term matches the search term, return null
            return null;
        }

    }
}

