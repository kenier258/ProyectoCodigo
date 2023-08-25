using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCodigo
{
    public class Item
    {
        public string Nombre;
        public int EspacioRequerido;
        public bool NecesitaArreglo;

        public Item(string nombre, int espacioRequerido, bool necesitaArreglo)
        {
            Nombre = nombre;
            EspacioRequerido = espacioRequerido;
            NecesitaArreglo = necesitaArreglo;
        }
    }
}
