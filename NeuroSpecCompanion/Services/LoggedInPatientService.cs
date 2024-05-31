using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecCompanion.Services
{
    public static class LoggedInPatientService
    {
        public static Patient? LoggedInPatient { get; set; }
    }
}
