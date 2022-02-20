using Ferreteria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Repositories
{
    public class ProductoRepository
    {
        ferreteriaContext context = new();

        //Aqui vamos a hacer el CRUD

        //CREATE
        public void INSERT(Producto producto)
        {
            context.Add(producto);
            context.SaveChanges();
        }

        //READ
        public IEnumerable<Producto> GetAllProductos()
        {
            return context.Productos.Include(x => x.IdSeccionNavigation)
                .ThenInclude(x => x.IdDepartamentoNavigation).OrderBy(x => x.Nombre);
        }

        //UPDATE
        public void UPDATE(Producto producto)
        {
            var temp = context.Productos.FirstOrDefault(x => x.Sku == producto.Sku);
            temp.IdSeccion = producto.IdSeccion;
            temp.Descripcion = producto.Descripcion;
            temp.IdSeccionNavigation = producto.IdSeccionNavigation;
            temp.Marca = producto.Marca;
            temp.Nombre = producto.Nombre;
            temp.Precio = producto.Precio;
            context.SaveChanges();
        }


        //DELETE

        public void DELETE(Producto producto)
        {
            context.Remove(producto);
            context.SaveChanges();
        }


    }
}
