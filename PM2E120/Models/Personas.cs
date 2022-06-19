using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ejercicio1_3.Models
{
    public class Personas
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }

        public string latitud { get; set; }

        public string longitud { get; set; }

        public string descripcion { get; set; }
    }
}
