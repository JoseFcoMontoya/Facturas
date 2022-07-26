using Facturas.Models;
using Facturas.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Facturas.ViewModels
{
    public class FiltrarFacturasViewModel : INotifyPropertyChanged
    {
        public Command CancelarFiltrosCommand { get; set; }

        public FiltrarFacturasViewModel()
        {
            CancelarFiltrosCommand = new Command(CancelarFiltros);
        }

        private async void CancelarFiltros()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
