using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpec.Shared.Models.DTO
{
    public class IssueExercise: Issue
    {
        public int ExerciseID { get; set; }
        public int Quantity { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
        public string Intensity { get; set; }
    }
}
