using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Facturas.Converters
{
    public class FloatToMoney : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dinero = (float) value;

            return dinero.ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dinero = (string) value;

            return dinero.Remove(0);
        }
    }

    public class FormatDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fecha = (string) value;
            var mes = fecha.Substring(fecha.IndexOf("/") + 1, fecha.Substring(fecha.IndexOf("/") + 1).IndexOf("/"));
            
            switch (mes)
            {
                case "01":
                    mes = "Enero";
                    break;
                case "02":
                    mes = "Febrero";
                    break;
                case "03":
                    mes = "Marzo";
                    break;
                case "04":
                    mes = "Abril";
                    break;
                case "05":
                    mes = "Mayo";
                    break;
                case "06":
                    mes = "Junio";
                    break;
                case "07":
                    mes = "Julio";
                    break;
                case "08":
                    mes = "Agosto";
                    break;
                case "09":
                    mes = "Septiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
            }

            StringBuilder dateformat = new StringBuilder();
            
            dateformat.Append(fecha.Substring(0, fecha.IndexOf("/")));
            dateformat.Append(" ");
            dateformat.Append(mes);
            dateformat.Append(" ");
            dateformat.Append(fecha.Substring(fecha.LastIndexOf("/") + 1));

            return dateformat.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fecha = (string)value;
            var mes = fecha.Substring(fecha.IndexOf("/") + 1, fecha.Substring(fecha.IndexOf("/") + 1).IndexOf("/"));

            switch (mes)
            {
                case "Enero":
                    mes = "01";
                    break;
                case "Febrero":
                    mes = "02";
                    break;
                case "Marzo":
                    mes = "03";
                    break;
                case "Abril":
                    mes = "04";
                    break;
                case "Mayo":
                    mes = "05";
                    break;
                case "Junio":
                    mes = "06";
                    break;
                case "Julio":
                    mes = "07";
                    break;
                case "Agosto":
                    mes = "08";
                    break;
                case "Septiembre":
                    mes = "09";
                    break;
                case "Octubre":
                    mes = "10";
                    break;
                case "Noviembre":
                    mes = "11";
                    break;
                case "Diciembre":
                    mes = "12";
                    break;
            }

            StringBuilder dateformat = new StringBuilder();

            dateformat.Append(fecha.Substring(0, fecha.IndexOf(" ")));
            dateformat.Append("/");
            dateformat.Append(mes);
            dateformat.Append("/");
            dateformat.Append(fecha.Substring(fecha.LastIndexOf(" ") + 1));

            return dateformat.ToString();
        }
    }
}
