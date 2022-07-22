using Facturas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private FacturasData facturas;

        public FacturasData Facturas 
        { 
            get => facturas;
            set 
            { 
                facturas = value;
                OnPropertyChanged();
            }
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
    }
}
