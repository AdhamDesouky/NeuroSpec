using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Models.Ontology;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Services.OntologyServices
{
    public class SNOMEDOntologyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        public SNOMEDOntologyService()
        {
            _httpClient = new HttpClient();
            _baseApi = "https://bioportal.bioontology.org/search/json_search/SNOMEDCT?q=";
        }
        public async Task<List<SNOMEDOntology>> SearchSNOMEDOntologyAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}{searchTerm}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            List<SNOMEDOntology> searchResults = new List<SNOMEDOntology>();

            var splitContent = content.Split("~!~");
            for (int i = 0; i < splitContent.Length - 1; i++)
            {
                var ontology = splitContent[i].Split("|");
                var term = new SNOMEDOntology
                {
                    SNOMEDID = ontology[1],
                    SNOMEDName = ontology[0],
                    SNOMEDDescription = ontology[^1]
                };
                searchResults.Add(term);

            }
            return searchResults;
        }
    }
}
