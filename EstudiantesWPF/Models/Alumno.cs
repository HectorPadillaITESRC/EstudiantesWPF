using System;
using System.Collections.Generic;
using SQLite; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudiantesWPF.Models
{
    [Table("estudiantes")]
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("NumControl"), NotNull]
        public string NumeroControl { get; set; } = null!;

        [NotNull]
        public string Nombre { get; set; } = null!;
    }
}
