using System;
using System.Collections.Generic;

#nullable disable

namespace Ferreteria.Models
{
    public partial class Seccion
    {
        public Seccion()
        {
            Productos = new HashSet<Producto>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; }
        public uint? IdDepartamento { get; set; }
        public uint? IdSeccionPrincipal { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
