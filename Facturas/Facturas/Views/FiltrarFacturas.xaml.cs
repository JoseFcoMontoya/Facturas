using Facturas.Class;
using Facturas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Facturas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltrarFacturas : ContentPage
    {
        private FiltrarFacturasViewModel filtrarFacturasViewModel;
        public FiltrarFacturas()
        {
            InitializeComponent();
            filtrarFacturasViewModel = new FiltrarFacturasViewModel();
            BindingContext = filtrarFacturasViewModel;
            lblImporte.Text = 0.ToString("C");
        }

        private void CambiarDineroMaximo(object sender, ValueChangedEventArgs e)
        {
            lblImporte.Text = e.NewValue.ToString("C");
        }

        private async void AplicarFiltros(object sender, EventArgs e)
        {
            Datas.fechaDesde = dpDeste.Date;
            Datas.fechaHasta = dpHasta.Date;
            Datas.dineroMaximo = (float) sDinero.Value;
            Datas.pagadas = cbPagadas.IsChecked;
            Datas.anuladas = cbAnuladas.IsChecked;
            Datas.cuotaFija = cbCuotaFija.IsChecked;
            Datas.pendientesPago = cbPendientesPago.IsChecked;
            Datas.planPago = cbPlanPago.IsChecked;

            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(true));
        }

        private void EliminarFiltros(object sender, EventArgs e)
        {
            dpDeste.Date = DateTime.Now;
            dpHasta.Date = DateTime.Now;

            sDinero.Value = 0;

            cbPagadas.IsChecked = false;
            cbAnuladas.IsChecked = false; ;
            cbCuotaFija.IsChecked = false; ;
            cbPendientesPago.IsChecked = false; ;
            cbPlanPago.IsChecked = false;
        }
    }
}