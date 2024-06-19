using MvvmHelpers;
using NeuroSpec.Shared.Models.DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuroSpecCompanion.ViewModels
{
    public class ViewAppointmentViewModel : BaseViewModel
    {
        private Visit visit;
        public Visit Visit
        {
            get => visit;
            set
            {
                SetProperty(ref visit, value);
            }
        }
    }
}
