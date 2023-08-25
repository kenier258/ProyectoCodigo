using ProyectoCodigo;
using System;


class Program
{
    static void Main(string[] args)
    {
        Casa miCasa = new Casa();
        CatalogoItems catalogo = new CatalogoItems();
        miCasa.ConfigurarPlano();

        Trabajador cristian = new Trabajador("Cristian", true);
        Trabajador paco = new Trabajador("Paco", true);
        Trabajador pedro = new Trabajador("Pedro", true);
        Trabajador diego = new Trabajador("Diego", true);
        Trabajador miguel = new Trabajador("Miguel", true);
        Trabajador kevin = new Trabajador("Kevin", true);
        Trabajador camilo = new Trabajador("Camilo", true);
        Trabajador simon = new Trabajador("Simon", true);
        Trabajador andres = new Trabajador("Andres", true);
        Trabajador mateo = new Trabajador("Mateo", true);
        Trabajador luis = new Trabajador("Luis", true);
        Trabajador carlos = new Trabajador("Carlos", true);
        Trabajador kenier = new Trabajador("Kenier", true);
        Trabajador stiven = new Trabajador("Stiven", true);
        
        

        Console.WriteLine("Plano de la casa: ");

        foreach (var fila in miCasa.plano)
        {
            foreach (var habitacion in fila)
            {
                Console.Write($"{habitacion.Nombre}, ");
            }
            Console.WriteLine(); // Cambiar de línea después de imprimir una fila
        }
        Console.WriteLine("Ingrese la cantidad de trabajadores que desea tener en la casa: ");
        int cantidadTrabajadores = int.Parse(Console.ReadLine());
        miCasa.AgregarTrabajadoresActuales(cantidadTrabajadores);
        miCasa.Menu();
    }
}

