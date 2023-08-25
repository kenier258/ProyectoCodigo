using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCodigo
{
    public class Habitante
    {
        public string Nombre;
        public Habitacion HabitacionFavorita;

        public Habitante(string nombre, Habitacion habitacionFavorita)
        {
            Nombre = nombre;
            HabitacionFavorita = habitacionFavorita;
        }

        public bool EstaEnHabitacionFavorita()
        {
            return HabitacionFavorita != null && HabitacionFavorita.Habitantes.Contains(this);
        }
    }
}

