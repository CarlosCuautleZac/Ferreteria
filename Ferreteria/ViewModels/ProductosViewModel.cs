using Ferreteria.Models;
using Ferreteria.Views;
using GalaSoft.MvvmLight.Command;
using Practica1.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ferreteria.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand VerAgregarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }


        public ProductoRepository repository = new();
        public SeccionRepository SR = new();

        private ObservableCollection<Producto> productos;

        public ObservableCollection<Producto> Productos
        {
            get { return productos; }
            set { productos = value; Actualizar(); }
        }

        public List<Seccion> Secciones { get; set; }

        public AgregarProductosView agregarview;
        public EditarView editarview;

        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value; Actualizar(); }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; Actualizar(); }
        }


        //Constructor
        public ProductosViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);
            AgregarCommand = new RelayCommand(Agregar);
            VerEditarCommand = new RelayCommand(VerEditar);
            EditarCommand = new RelayCommand(Editar);
            EliminarCommand = new RelayCommand(Eliminar);

            Productos = new(repository.GetAllProductos());
            Secciones = new(SR.GetAllSecciones());
        }

        private void Eliminar()
        {
            if (Producto == null)
                throw new ApplicationException("Seleccione un producto a eliminar");

            else
            {
                repository.DELETE(Producto);
                Productos = new(repository.GetAllProductos());
            }
        }

        private void Editar()
        {
            if (Valiar(Producto, false))
            {
                Error = "";
                repository.UPDATE(Producto);
                Productos = new(repository.GetAllProductos());
                editarview.Close();
            }
        }

        private void VerEditar()
        {
            if (Producto == null)
                throw new ApplicationException("Seleccione un producto");
            else
            {
                Error = "";


                var clon = new Producto()
                {
                    Marca = Producto.Marca,
                    Nombre = Producto.Nombre,
                    Descripcion = Producto.Descripcion,
                    IdSeccion = Producto.IdSeccion,
                    IdSeccionNavigation = Producto.IdSeccionNavigation,
                    Precio = Producto.Precio,
                    Sku = Producto.Sku
                };

                Producto = clon;

                editarview = new EditarView() { DataContext = this };
                editarview.ShowDialog();

            }
        }

        private void Agregar()
        {
            if (Valiar(Producto, true))
            {
                repository.INSERT(Producto);
                Error = "";
                Productos = new(repository.GetAllProductos());
                agregarview.Close();
            }
        }

        private bool Valiar(Producto producto, bool IsInserting)
        {
            if (string.IsNullOrWhiteSpace(producto.Marca))
                throw new ArgumentException("El nombre de la marca no debe ir vacio");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre no debe ir vacio");

            if (string.IsNullOrWhiteSpace(producto.Descripcion))
                throw new ArgumentException("La descripcion no debe ir vacia");

            if (producto.Precio < 0)
                throw new ArgumentException("Precio incorrecto");

            if (IsInserting == true)
                if (Productos.Any(x => x.Marca == producto.Marca) && Productos.Any(x => x.Nombre == producto.Nombre))
                    throw new ArgumentException("Producto ya registrado");

            return true;
        }

        private void VerAgregar()
        {
            Producto = new();
            Error = "";
            agregarview = new AgregarProductosView() { DataContext = this };
            agregarview.ShowDialog();

        }

        private void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
