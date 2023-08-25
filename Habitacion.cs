using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCodigo
{
    public class Habitacion
    {
        public string Nombre;
        public int Espacio;
        public List<Habitante> Habitantes = new List<Habitante>();
        public List<Item> Items = new List<Item>();

        public Habitacion(string nombre, int espacio)
        {
            Nombre = nombre;
            Espacio = espacio;
        }

        public double EspacioDisponible
        {
            get
            {
                int espacioOcupado = Items.Sum(item => item.EspacioRequerido);
                return Espacio - espacioOcupado;
            }
        }

        public void AmpliarEspacio(int aumento)
        {
            Espacio += aumento;
            Console.WriteLine($"Se ha ampliado el espacio de la habitacion {Nombre} en {aumento} unidades.");
        }

        public void ArreglarItem(string nombreItem)
        {
            Item itemEnHabitacion = Items.Find(item => item.Nombre == nombreItem);

            if (itemEnHabitacion != null)
            {
                if (itemEnHabitacion.NecesitaArreglo)
                {
                    itemEnHabitacion.NecesitaArreglo = false;
                    Console.WriteLine($"Se ha arreglado el item {nombreItem} en la habitacion {Nombre}.");
                }
                else
                {
                    Console.WriteLine($"El item {nombreItem} en la habitacion {Nombre} esta en buen estado y no necesita arreglo.");
                }
            }
            else
            {
                Console.WriteLine($"El item {nombreItem} no se encuentra en la habitacion {Nombre}.");
            }
        }
    }
}

