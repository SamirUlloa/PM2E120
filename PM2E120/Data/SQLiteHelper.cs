using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Ejercicio1_3.Models;
using System.Threading.Tasks;

namespace Ejercicio1_3.Data
{
    public class SQLiteHelper
    {
        SQLite.SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Personas>().Wait();
        }

        public Task<int> GuardarPersona(Personas perso)
        {
            if (perso.id != 0)
            {
                return db.UpdateAsync(perso);
            }
            else
            {
                return db.InsertAsync(perso);
            }
        }

        public Task<int> EliminarPersona(Personas persona)
        {
            return db.DeleteAsync(persona);
        }

        /// <summary>
        /// Recuperar todos los registros
        /// </summary>
        /// <returns></returns>

        public Task<List<Personas>> GetPersonas()
        {
            return db.Table<Personas>().ToListAsync();
        }

        /// <summary>
        /// Recuperar registros por id (Busqueda)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<Personas> GetPersonasByIdAsync(int id)
        {
            return db.Table<Personas>().Where(a => a.id == id).FirstOrDefaultAsync();
        }
    }
}
