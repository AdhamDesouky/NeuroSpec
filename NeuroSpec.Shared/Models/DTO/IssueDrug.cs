using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpec.Shared.Models.DTO
{
    public class IssueDrug: Issue
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
