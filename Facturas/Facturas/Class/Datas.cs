using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Class
{
    public static class Datas
    {
        public static DateTime fechaDesde { get; set; }
        public static DateTime fechaHasta { get; set; }
        public static float dineroMaximo { get; set; }
        public static bool pagadas { get; set; }
        public static bool anuladas { get; set; }
        public static bool cuotaFija { get; set; }
        public static bool pendientesPago { get; set; }
        public static bool planPago { get; set; }
        public static bool entradaAplicacion { get; set; } = true;
    }
}
