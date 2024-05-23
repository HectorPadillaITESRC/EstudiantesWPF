using CommunityToolkit.Mvvm.Input;
using EstudiantesWPF.Models;
using EstudiantesWPF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EstudiantesWPF.ViewModels
{
    public class EstudiantesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Alumno> Alumnos { get; set; } = new();
        private AlumnosRepository repository = new();
        public Alumno? Alumno { get; set; }
        public string Vista { get; set; } = "mostrar";
        public string Error { get; set; } = "";

        //private string filtro;

        //public string Filtro
        //{
        //    get { return filtro; }
        //    set
        //    {
        //        filtro = value;
        //        MostrarDatos();
        //    }
        //}

        public string Filtro { get; set; }


        public ICommand AgregarCommand { get; set; }
        public ICommand MostrarAgregarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ConfirmarEliminarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand MostrarEditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }

        public EstudiantesViewModel()
        {
            FiltrarCommand = new RelayCommand<string>(Filtrar);
            CancelarCommand = new RelayCommand(Cancelar);
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
            MostrarEditarCommand = new RelayCommand<Alumno>(MostrarEditar);
            GuardarCommand = new RelayCommand(Guardar);
            ConfirmarEliminarCommand = new RelayCommand<Alumno>(Confirmar);
            MostrarAgregarCommand = new RelayCommand(MostrarAgregar);
            MostrarDatos();
        }

        private void Filtrar(string? obj)
        {
            Filtro = obj??"";
            MostrarDatos();
        }

        private void Guardar()
        {
            if (Alumno != null)
            {
                Error = "";

                if (string.IsNullOrWhiteSpace(Alumno.NumeroControl))
                {
                    Error += "\nEscriba el número de control del alumno.";
                }

                if (string.IsNullOrWhiteSpace(Alumno.Nombre))
                {
                    Error += "\nEscriba el nombre del alumno.";
                }

                if (repository.GetAll().Any(x => x.NumeroControl == Alumno.NumeroControl && x.Id != Alumno.Id))
                {
                    Error += "\nEl número de control ya existe.";
                }

                Actualizar(nameof(Error));

                if (Error == "")
                {
                    repository.Update(Alumno);
                    Cancelar();
                    MostrarDatos();
                }


            }
        }

        private void MostrarEditar(Alumno? alumno)
        {
            if (alumno != null)
            {
                //Creamos una copia para no editar el original
                Alumno = new Alumno
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    NumeroControl = alumno.NumeroControl
                };
                Actualizar(nameof(Alumno));

                Vista = "editar";
                Actualizar(nameof(Vista));
            }
        }

        private void Eliminar()
        {
            if (Alumno != null)
            {
                repository.Delete(Alumno);
                Cancelar();
                MostrarDatos();
            }
        }

        private void Confirmar(Alumno? a)
        {
            Alumno = a;
            Actualizar(nameof(Alumno));

            Vista = "eliminar";
            Actualizar(nameof(Vista));
        }

        private void MostrarAgregar()
        {
            Alumno = new();
            Actualizar(nameof(Alumno));

            Vista = "agregar";
            Actualizar(nameof(Vista));
        }

        private void Agregar()
        {
            if (Alumno != null)
            {
                Error = "";

                if (string.IsNullOrWhiteSpace(Alumno.NumeroControl))
                {
                    Error += "\nEscriba el número de control del alumno.";
                }

                if (string.IsNullOrWhiteSpace(Alumno.Nombre))
                {
                    Error += "\nEscriba el nombre del alumno.";
                }

                if (repository.GetAll().Any(x => x.NumeroControl == Alumno.NumeroControl))
                {
                    Error += "\nEl número de control ya existe.";
                }

                Actualizar(nameof(Error));

                if (Error == "")
                {
                    repository.Insert(Alumno);
                    Cancelar();
                    MostrarDatos();
                }

            }

        }

        private void Cancelar()
        {
            Error = "";
            Actualizar(nameof(Error));

            Vista = "mostrar";
            Actualizar(nameof(Vista));
        }

        void MostrarDatos()
        {
            Alumnos.Clear();
            var datos = repository.GetByFilter(Filtro ?? "");
            foreach (var a in datos)
            {
                Alumnos.Add(a);
            }


        }



        private void Actualizar(string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new(nombre));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
