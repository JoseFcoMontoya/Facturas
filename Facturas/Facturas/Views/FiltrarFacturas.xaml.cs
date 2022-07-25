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

        private void AplicarFiltros(object sender, EventArgs e)
        {

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