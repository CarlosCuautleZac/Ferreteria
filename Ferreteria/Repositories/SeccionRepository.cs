using Ferreteria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Repositories
{
    public class SeccionRepository
    {
        ferreteriaContext context = new();

        public IEnumerable<Seccion> GetAllSecciones()
        {
            return context.Seccions.Include(x => x.IdDepartamentoNavigation).OrderBy(x=>x.Nombre);
        }
    }
}
