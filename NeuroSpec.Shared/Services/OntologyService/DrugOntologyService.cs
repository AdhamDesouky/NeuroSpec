using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Models.Ontology;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace NeuroSpec.Shared.Services.OntologyServices
{
    public class DrugOntologyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApi;
        public DrugOntologyService()
        {
            _httpClient = new HttpClient();
            _baseApi = "https://bioportal.bioontology.org/search/json_search/DRON?q=";
        }
        public async Task<List<DrugOntology>> SearchDrugOntologyAsync(string drugName)
        {
            var response = await _httpClient.GetAsync($"{_baseApi}{drugName}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            List<DrugOntology>searchResults= new List<DrugOntology>();

            var splitContent = content.Split("~!~");
            for(int i=0;i<splitContent.Length-1;i++)
            {
                var drugOntology = splitContent[i].Split("|");
                var drug = new DrugOntology
                {
                    Name = drugOntology[0],
                    URL = drugOntology[1]
                };
                searchResults.Add(drug);

            }
            return searchResults;
        }
    }
}
