using Facturas.Class;
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
        private string facturasNoEncontradas;
        private bool listaFacturasVisible;

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

        public string FacturasNoEncontradas
        {
            get => facturasNoEncontradas;
            set
            {
                facturasNoEncontradas = value;
                OnPropertyChanged();
            }
        }

        public bool ListaFacturasVisible
        {
            get => listaFacturasVisible;
            set
            {
                listaFacturasVisible = value;
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
            ListaFacturasVisible = true;
            /*
             * Se extraen los datos en formato json y luego se convierten a la clase de FacturasData para poder trabajar con ellos
             */
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FacturasData>(jsonResult);

            if (!filtrar)
            {

                Facturas = result;
            }
            else
            {
                List<Factura> facturas = new List<Factura>();
                /*
                 * Se va recorriendo cada una de las facturas que se obtienen, para ser comprobadas segun el filtro
                 * y si pasan el filtro se pasan a una lista. 
                 * Cuando no queden facturas por comprobar se mostrara un mensaje si no hay facturas que mostrar o
                 * la lista de las facturas que han pasado el filtro
                 */
                foreach (Factura factura in result.facturas)
                {
                    int dia = int.Parse(factura.fecha.Substring(0, factura.fecha.IndexOf("/")));

                    string fechaMes = factura.fecha.Substring(factura.fecha.IndexOf("/") + 1);
                    fechaMes = fechaMes.Substring(0, fechaMes.IndexOf("/"));
                    int mes = int.Parse(fechaMes);

                    int anio = int.Parse(factura.fecha.Substring(factura.fecha.LastIndexOf("/") + 1));

                    if (anio > Datas.fechaDesde.Year && anio < Datas.fechaHasta.Year)
                    {
                        if (comprobarFactura(factura))
                        {
                            facturas.Add(factura);
                        }
                    }
                    else if (anio == Datas.fechaDesde.Year && anio == Datas.fechaHasta.Year)
                    {
                        if (mes > Datas.fechaDesde.Month && mes < Datas.fechaHasta.Month)
                        {
                            if (comprobarFactura(factura))
                            {
                                facturas.Add(factura);
                            }
                        }
                        else if (mes == Datas.fechaDesde.Month && mes == Datas.fechaHasta.Month)
                        {
                            if (dia >= Datas.fechaDesde.Day && dia <= Datas.fechaHasta.Day)
                            {
                                if (comprobarFactura(factura))
                                {
                                    facturas.Add(factura);
                                }
                            }
                        }
                    }
                    else if (anio == Datas.fechaDesde.Year)
                    {
                        if (mes > Datas.fechaDesde.Month)
                        {
                            if (comprobarFactura(factura))
                            {
                                facturas.Add(factura);
                            }
                        }
                        else if (mes == Datas.fechaDesde.Month)
                        {
                            if (dia >= Datas.fechaDesde.Day)
                            {
                                if (comprobarFactura(factura))
                                {
                                    facturas.Add(factura);
                                }
                            }
                        }
                    }
                    else if (anio == Datas.fechaHasta.Year)
                    {
                        if (mes < Datas.fechaHasta.Month)
                        {
                            if (comprobarFactura(factura))
                            {
                                facturas.Add(factura);
                            }
                        }
                        else if (mes == Datas.fechaHasta.Month)
                        {
                            if (dia <= Datas.fechaHasta.Day)
                            {
                                if (comprobarFactura(factura))
                                {
                                    facturas.Add(factura);
                                }
                            }
                        }
                    }
                }

                if (facturas.Count == 0)
                {
                    ListaFacturasVisible = false;
                    FacturasNoEncontradas = "No se han encontrado facturas con el filtro que se ha escogido";
                }
                else
                {
                    result.facturas = facturas.ToArray();

                    Facturas = result;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void ViewFiltrarFacturar()
        {
            //Comprueba si la aplicacion se ha arrando ya ha arrancado por primera la pagina de filtras facturas, para carga la configuracion guardada
            if (Datas.entradaAplicacion)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FiltrarFacturas());
                Datas.entradaAplicacion = false;
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FiltrarFacturas(Datas.fechaDesde, Datas.fechaHasta, Datas.dineroMaximo, Datas.pagadas, Datas.anuladas, Datas.cuotaFija, Datas.pendientesPago, Datas.planPago));
            }
        }

        private bool comprobarFactura(Factura factura)
        {
            bool mostrar = false;
            bool insertar = false;

            if (factura.importeOrdenacion >= 0 && factura.importeOrdenacion <= Datas.dineroMaximo)
            {
                if (Datas.pagadas && !insertar)
                {
                    mostrar = comprararEstado(factura.descEstado, "Pagada");
                    if (comprararEstado(factura.descEstado, "Pagada"))
                    {
                        insertar = true;
                    }
                }
                if (Datas.anuladas && !insertar)
                {
                    mostrar = comprararEstado(factura.descEstado, "Anulada");
                    if (comprararEstado(factura.descEstado, "Anulada"))
                    {
                        insertar = true;
                    }
                }
                if (Datas.cuotaFija && !insertar)
                {
                    mostrar = comprararEstado(factura.descEstado, "Cuota Fija");
                    if (comprararEstado(factura.descEstado, "Cuota Fija"))
                    {
                        insertar = true;
                    }
                }
                if (Datas.pendientesPago && !insertar)
                {
                    mostrar = comprararEstado(factura.descEstado, "Pendiente de pago");
                    if (comprararEstado(factura.descEstado, "Pendiente de pago"))
                    {
                        insertar = true;
                    }
                }
                if (Datas.planPago && !insertar)
                {
                    mostrar = comprararEstado(factura.descEstado, "Plan de pago");
                    if (comprararEstado(factura.descEstado, "Plan de pago"))
                    {
                        insertar = true;
                    }
                }

                insertar = false;
            }

            return mostrar;
        }

        private bool comprararEstado(string descEstado, string estado)
        {
            bool mostrar = false;

            switch (descEstado)
            {
                case "Pagada":
                    if (estado.Equals("Pagada"))
                    {
                        mostrar = true;
                    }
                    break;
                case "Anulada":
                    if (estado.Equals("Anulada"))
                    {
                        mostrar = true;
                    }
                    break;
                case "Cuota Fija":
                    if (estado.Equals("Cuota Fija"))
                    {
                        mostrar = true;
                    }
                    break;
                case "Pendiente de pago":
                    if (estado.Equals("Pendiente de pago"))
                    {
                        mostrar = true;
                    }
                    break;
                case "Plan de pago":
                    if (estado.Equals("Plan de pago"))
                    {
                        mostrar = true;
                    }
                    break;
            }

            return mostrar;
        }
    }
}
