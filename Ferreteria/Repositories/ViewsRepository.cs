using Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Repositories
{
    public class ViewsRepository
    {
        ferreteriaContext Context = new();

        public IEnumerable<Vwdepartamentosordenado> GetVwDepartamentosOrdenados()
        {
            return Context.Vwdepartamentosordenados;
        }

        public IEnumerable<Vwlistadoentre500y100> GetvwListadEentre500Y1000()
        {
            return Context.Vwlistadoentre500y100s;
        }

        public IEnumerable<Vwlistadomenoralpromedio> GetvwProductosMenorAlPromedio ()
        {
            return Context.Vwlistadomenoralpromedios;
        }
    }
}
