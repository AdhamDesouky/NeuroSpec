using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpec.Shared.Models.DTO
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseType { get; set; }
        public string ExerciseCategory { get; set; }
        public string ExerciseDescription { get; set; }
        public string ExerciseDuration { get; set; }
        public string ExerciseFrequency { get; set; }
        public string ExerciseIntensity { get; set; }
        public string ExerciseEquipment { get; set; }
        public string ExerciseInstructions { get; set; }
        public string ExerciseImage { get; set; }
    }
}
