using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Models
{
    public class FacturasData
    {
        public int numFacturas { get; set; }
        public Factura[] facturas { get; set; }
    }

    public class Factura
    {
        public string descEstado { get; set; }
        public float importeOrdenacion { get; set; }
        public string fecha { get; set; }
    }
}
