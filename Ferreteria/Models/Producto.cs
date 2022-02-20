using System;
using System.Collections.Generic;

#nullable disable

namespace Ferreteria.Models
{
    public partial class Producto
    {
        public ulong Sku { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public uint IdSeccion { get; set; }

        public virtual Seccion IdSeccionNavigation { get; set; }
    }
}
