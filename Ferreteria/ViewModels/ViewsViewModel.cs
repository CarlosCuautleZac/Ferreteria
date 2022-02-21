using Ferreteria.Models;
using Ferreteria.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.ViewModels
{
    public class ViewsViewModel
    {
        ViewsRepository viewsrepository = new();

        public List<Vwdepartamentosordenado> DepartamentosOrdenados { get; set; }

        public List<Vwlistadoentre500y100> ProductosEntre500y1000 { get; set; }

        public List<Vwlistadomenoralpromedio> ProductosMenorAlPromedio { get; set; }

        public ViewsViewModel()
        {
            DepartamentosOrdenados = new(viewsrepository.GetVwDepartamentosOrdenados());
            ProductosEntre500y1000 = new(viewsrepository.GetvwListadEentre500Y1000());
            ProductosMenorAlPromedio = new(viewsrepository.GetvwProductosMenorAlPromedio());
        }
    }
}
