using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpec.Shared.Models.DTO
{
    public class IssueDrug: Issue
    {
        public int DrugID { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
    }
}
