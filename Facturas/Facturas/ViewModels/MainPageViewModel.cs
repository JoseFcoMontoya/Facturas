using Facturas.Models;
using Facturas.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Facturas.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private FacturasData facturas;

        public bool filtrar { get; set; }

        public FacturasData Facturas 
        { 
            get => facturas;
            set 
            { 
                facturas = value;
                OnPropertyChanged();
            }
        }

        public Command FiltrarFacturasCommand { get; set; }

        
        public MainPageViewModel()
        {
            FiltrarFacturasCommand = new Command(ViewFiltrarFacturar);
        }

        public MainPageViewModel(bool filtrar)
        {
            FiltrarFacturasCommand = new Command(ViewFiltrarFacturar);
            this.filtrar = filtrar;
        }

        public async Task GetFacturasData(string url)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FacturasData>(jsonResult);
            
            Facturas = result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ViewFiltrarFacturar()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FiltrarFacturas());
        }
    }
}
