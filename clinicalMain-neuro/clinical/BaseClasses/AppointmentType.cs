using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class AppointmentType
    {
        public int TypeID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeInMinutes {  get; set; }
        public double Cost { get; set; }

        public AppointmentType(int typeId, string name, string description, int timeInMinutes, double cost)
        {
            TypeID = typeId;
            Name = name;
            Description = description;
            TimeInMinutes = timeInMinutes;
            Cost = cost;
        }
        public override string ToString()
        {
            return $"{Name}, ${Cost}";
        }
    }
}
