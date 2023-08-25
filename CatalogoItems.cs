using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCodigo
{
    public class CatalogoItems
    {
        public List<Item> ItemsDisponibles = new List<Item>();
        private Random random = new Random();

        public CatalogoItems()
        {
            AgregarItem("Cuadro", 1, random.Next(2) == 0);
            AgregarItem("Lampara", 1, random.Next(2) == 0);
            AgregarItem("Silla", 2, random.Next(2) == 0);
            AgregarItem("Tv", 4, random.Next(2) == 0);
            AgregarItem("Mueble", 5, random.Next(2) == 0);
            AgregarItem("Mesa", 3, random.Next(2) == 0);
            AgregarItem("Jarron", 1, random.Next(2) == 0);
            AgregarItem("Computador", 8, random.Next(2) == 0);
            AgregarItem("Closet", 6, random.Next(2) == 0);
        }

        public void AgregarItem(string nombre, int espacioRequerido, bool necesitaArreglo)
        {
            Item nuevoItem = new Item(nombre, espacioRequerido, necesitaArreglo);
            ItemsDisponibles.Add(nuevoItem);
        }
    }
}
