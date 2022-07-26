﻿using Facturas.Class;
using Facturas.Models;
using Facturas.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Facturas.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel mainPageViewModel;
        private const string url = "http://viewnextandroid.mocklab.io/facturas";

        public MainPage()
        {
            InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            BindingContext = mainPageViewModel;
        }

        public MainPage(bool filtrar)
        {
            InitializeComponent();
            mainPageViewModel = new MainPageViewModel(filtrar);
            BindingContext = mainPageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await mainPageViewModel.GetFacturasData(url);
        }

        private void AlertaClickCeltaListView(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Información", "Esta funcionalidad aún no está disponible", "Cerrar");
        }
    }
}