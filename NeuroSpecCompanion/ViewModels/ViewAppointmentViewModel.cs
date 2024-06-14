using MvvmHelpers;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.ViewModels
{
    [QueryProperty(nameof(Visit), "Visit")]
    public class ViewAppointmentViewModel:BaseViewModel
    {
        private Visit visit;
        public Visit Visit
        {
            get => visit;
            set
            {
                visit = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
