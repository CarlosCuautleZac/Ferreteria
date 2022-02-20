using System;
using System.Collections.Generic;

#nullable disable

namespace Ferreteria.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Seccions = new HashSet<Seccion>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
