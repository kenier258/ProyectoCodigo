using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace ProyectoCodigo
{
    public class Trabajador
    {
        public string Nombre;
        public bool EstaDisponible;
        private LocalDateTime _tiempoLimite;
        public static List<Trabajador> ListaTrabajadores = new List<Trabajador>();

        public Trabajador(string nombre, bool estaDisponible = true)
        {
            Nombre = nombre;
            EstaDisponible = estaDisponible;
            _tiempoLimite = LocalDateTime.FromDateTime(DateTime.MinValue);
            ListaTrabajadores.Add(this);
        }

        public void AsignarTiempo(int segundos)
        {
            _tiempoLimite = SystemClock.Instance.GetCurrentInstant()
                                .InZone(DateTimeZoneProviders.Tzdb.GetSystemDefault())
                                .LocalDateTime.PlusSeconds(segundos);
            EstaDisponible = false;
        }

        public void ActualizarDisponibilidad()
        {
            var currentTime = SystemClock.Instance.GetCurrentInstant()
                                .InZone(DateTimeZoneProviders.Tzdb.GetSystemDefault())
                                .LocalDateTime;

            if (currentTime >= _tiempoLimite)
            {
                EstaDisponible = true;
                _tiempoLimite = LocalDateTime.FromDateTime(DateTime.MinValue);
            }
        }
    }
}

