using mpillajoS5.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mpillajoS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessague { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");
                Persona person = new() { Name = nombre };
                result = conn.Insert(person);

                StatusMessague = string.Format("{0} dato(s), Nombre: {1} insertado correctamente", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessague = string.Format("FATAR ERROR AL INSERTAR {0}. Error: {1}", nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessague = string.Format("Falied to retrive data. {0}", ex.Message);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();
                var person = conn.Get<Persona>(id);
                if (person != null)
                {
                    person.Name = newName;
                    conn.Update(person);
                    StatusMessague = string.Format("Persona con ID {0} actualizada correctamente", id);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessague = string.Format("Error al actualizar la persona con ID {0}. Error: {1}", id, ex.Message);
            }

        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                var person = conn.Get<Persona>(id);
                if (person != null)
                {
                    conn.Delete(person);
                    StatusMessague = string.Format("Persona con ID {0} eliminada correctamente", id);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessague = string.Format("Error al eliminar la persona con ID {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}

