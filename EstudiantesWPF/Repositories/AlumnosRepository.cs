using EstudiantesWPF.Models;
using SQLite;//ORM : Object Relational Model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudiantesWPF.Repositories
{
    public class AlumnosRepository
    {

        SQLiteConnection conexion;

        public AlumnosRepository()
        {
            conexion = new("data/alumnos.sqlite");
        }

        public IEnumerable<Alumno> GetAll()
        {
            return conexion.Table<Alumno>().OrderBy(x => x.Nombre);
        }

        //LINQ TO SQL
        public IEnumerable<Alumno> GetByFilter(string filter)
        {
            return conexion.Table<Alumno>().Where(x=> x.Nombre.ToUpper().Contains(filter.ToUpper()) 
            || x.NumeroControl.ToUpper().Contains(filter.ToUpper()));
        }

        public void Insert(Alumno alumno)
        {
            conexion.Insert(alumno);
        }

        public void Update(Alumno alumno)
        {
            conexion.Update(alumno);
        }

        public void Delete(Alumno alumno)
        {
            conexion.Delete(alumno);
        }

    }
}
