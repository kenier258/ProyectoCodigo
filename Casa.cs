using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCodigo
{
    public class Casa
    {
        public List<List<Habitacion>> plano = new List<List<Habitacion>>();
        private CatalogoItems catalogo = new CatalogoItems();
        private List<Habitante> habitantes = new List<Habitante>();
        public List<Trabajador> trabajadoresActuales = new List<Trabajador>();
        private Random random = new Random();

        public void ConfigurarPlano()
        {
            //Creamos Habitaciones
            Habitacion H1 = new Habitacion("H1", 5);
            Habitacion H2 = new Habitacion("H2", 5);
            Habitacion H3 = new Habitacion("H3", 5);
            Habitacion H4 = new Habitacion("H4", 5);
            Habitacion H5 = new Habitacion("H5", 5);
            Habitacion H6 = new Habitacion("H6", 5);
            Habitacion H7 = new Habitacion("H7", 5);
            Habitacion H8 = new Habitacion("H8", 5);
            Habitacion H9 = new Habitacion("H9", 5);

            //Habitantes de la casa y su habitacion favorita
            Habitante juan = new Habitante("Juan", H1);
            Habitante maria = new Habitante("Maria", H2);
            Habitante carlos = new Habitante("Carlos", H3);

            habitantes.Add(juan);
            habitantes.Add(maria);
            habitantes.Add(carlos);

            AgregarHabitanteEnHabitacion(0, 0, juan);
            AgregarHabitanteEnHabitacion(0, 1, maria);
            AgregarHabitanteEnHabitacion(0, 2, carlos);

            //Configuramos el plano inicial
            plano.Add(new List<Habitacion> { H1, H2, H3 });
            plano.Add(new List<Habitacion> { H4, H5, H6 });
            plano.Add(new List<Habitacion> { H7, H8, H9 });
        }

        public void AgregarTrabajadoresActuales(int cantidad)
        {
            trabajadoresActuales.Clear();
            foreach (Trabajador trabajador in Trabajador.ListaTrabajadores.Take(cantidad))
            {
                trabajadoresActuales.Add(trabajador);
            }
        }

        public bool HayPersonasEnHabitacionesOAdyacentes(int fila, int columna)
        {
            Habitacion habitacion = plano[fila][columna];

            if (habitacion.Habitantes.Count > 0)
            {
                return true;
            }

            //Verificar habitaciones adyacentes
            if (fila > 0 && plano[fila - 1][columna].Habitantes.Count > 0) return true; // Habitación arriba
            if (fila < plano.Count - 1 && plano[fila + 1][columna].Habitantes.Count > 0) return true; // Habitación abajo
            if (columna > 0 && plano[fila][columna - 1].Habitantes.Count > 0) return true; // Habitación izquierda
            if (columna < plano[fila].Count - 1 && plano[fila][columna + 1].Habitantes.Count > 0) return true; // Habitación derecha

            return false;
        }

        public void AgregarHabitanteEnHabitacion(int fila, int columna, Habitante habitante)
        {
            if (fila >= 0 && fila < plano.Count && columna >= 0 && columna < plano[fila].Count)
            {
                plano[fila][columna].Habitantes.Add(habitante);
                Console.WriteLine($"Se ha agregado a {habitante.Nombre} en la habitación {plano[fila][columna].Nombre}.");
            }
            else
            {
                Console.WriteLine("Coordenadas inválidas.");
            }
        }

        public void DecorarHabitacion()
        {
            Console.WriteLine("Ingrese el nombre del habitante para decorar su habitacion favorita: ");
            string nombreHabitante = Console.ReadLine();

            Habitante habitante = habitantes.Find(h => h.Nombre == nombreHabitante);
            Habitacion habitacionFavorita = habitante.HabitacionFavorita;

            if (!habitante.EstaEnHabitacionFavorita())
            {
                if (!habitante.EstaEnHabitacionFavorita())
                {
                    int filaHabitacionFavorita = -1;
                    int columnaHabitacionFavorita = -1;

                    for (int fila = 0; fila < plano.Count; fila++)
                    {
                        for (int columna = 0; columna < plano[fila].Count; columna++)
                        {
                            if (plano[fila][columna] == habitacionFavorita)
                            {
                                filaHabitacionFavorita = fila;
                                columnaHabitacionFavorita = columna;
                                break;
                            }
                        }

                        if (filaHabitacionFavorita != -1)
                        {
                            break;
                        }
                    }

                    if (filaHabitacionFavorita != -1)
                    {
                        AgregarHabitanteEnHabitacion(filaHabitacionFavorita, columnaHabitacionFavorita, habitante);
                    }
                }
            }

            Console.WriteLine("Trabajadores disponibles para el trabajo:");
            for (int i = 0; i < trabajadoresActuales.Count; i++)
            {
                Trabajador trabajador = trabajadoresActuales[i];
                Console.WriteLine($"{i + 1}. {trabajador.Nombre} ({(trabajador.EstaDisponible ? "Disponible" : "No Disponible")})");
            }

            int opcionTrabajador;

            do
            {
                Console.Write("Seleccione el número del trabajador que desea asignar al trabajo: ");
                opcionTrabajador = int.Parse(Console.ReadLine()) - 1;
            } while (opcionTrabajador < 0 || opcionTrabajador >= trabajadoresActuales.Count);    // Error AQUI!!!!

            Trabajador trabajadorAsignado = trabajadoresActuales[opcionTrabajador];

            if (trabajadorAsignado.EstaDisponible)
            {
                Console.WriteLine("Items disponibles para decorar: ");
                foreach (Item item in catalogo.ItemsDisponibles)
                {
                    Console.WriteLine(item.Nombre);
                }

                Console.WriteLine("Elija un item para decorar la habitacion: ");
                string opcion = Console.ReadLine();

                if (!string.IsNullOrEmpty(opcion))
                {
                    Item itemElegido = catalogo.ItemsDisponibles.Find(item => item.Nombre == opcion);

                    if (itemElegido != null && habitante.EstaEnHabitacionFavorita() && habitacionFavorita.EspacioDisponible >= itemElegido.EspacioRequerido)
                    {
                        habitacionFavorita.Items.Add(itemElegido);
                        Console.WriteLine($"Se ha decorado la habitacion con el item {itemElegido.Nombre}.");
                    }
                    else
                    {
                        Console.WriteLine($"No se puede decorar la habitacion con el item {opcion}.");
                    }
                }
                else
                {
                    Console.WriteLine("No ha ingresado ninguna opcion.");
                }
                trabajadorAsignado.AsignarTiempo(15);
                Console.WriteLine("Se ha asignado el trabajo");
            }
            else
            {
                Console.WriteLine($"El trabajador {trabajadorAsignado.Nombre} no está disponible en este momento.");
            }
            trabajadorAsignado.ActualizarDisponibilidad();
        }

        public void ArreglarItemEnHabitacion()
        {
            Console.WriteLine("Ingrese el nombre de la habitacion en la que desea arreglar los items: ");
            string nombreHabitacion = Console.ReadLine();

            Habitacion habitacion = BuscarHabitacionPorNombre(nombreHabitacion);

            if (habitacion != null)
            {
                Console.WriteLine("Items disponibles para arreglar:");
                foreach (Item item in habitacion.Items)
                {
                    Console.WriteLine(item.Nombre);
                }

                Console.WriteLine("Elija un item para arreglar: ");
                string opcion = Console.ReadLine();

                if (!string.IsNullOrEmpty(opcion))
                {
                    habitacion.ArreglarItem(opcion);
                }
                else
                {
                    Console.WriteLine("No ha ingresado ninguna opcion.");
                }
            }
            else
            {
                Console.WriteLine($"No se encontró la habitación '{nombreHabitacion}'.");
            }
        }

        public void AgregarNuevaHabitacion()
        {
            Console.WriteLine("Ingrese el nombre de la nueva habitacion: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el espacio de la nueva habitacion: ");
            int espacio = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la coordenada de fila para la nueva habitación:");
            int fila = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la coordenada de columna para la nueva habitación:");
            int columna = int.Parse(Console.ReadLine());

            if (fila >= 0 && columna >= 0)
            {
                // Verificar si se necesita agregar nuevas filas
                while (fila >= plano.Count)
                {
                    plano.Add(new List<Habitacion>());
                }

                // Verificar si se necesita agregar nueva columna a alguna fila
                foreach (var filaHabitaciones in plano)
                {
                    while (columna >= filaHabitaciones.Count)
                    {
                        filaHabitaciones.Add(new Habitacion(" ", 0)); // Habitación vacía sin espacio
                    }
                }

                Console.WriteLine("Trabajadores disponibles para el trabajo:");
                for (int i = 0; i < trabajadoresActuales.Count; i++)
                {
                    Trabajador trabajador = trabajadoresActuales[i];
                    Console.WriteLine($"{i + 1}. {trabajador.Nombre} ({(trabajador.EstaDisponible ? "Disponible" : "No Disponible")})");
                }

                int opcionTrabajador;

                do
                {
                    Console.Write("Seleccione el número del trabajador que desea asignar al trabajo: ");
                    opcionTrabajador = int.Parse(Console.ReadLine()) - 1;
                }
                while (opcionTrabajador < 0 || opcionTrabajador >= trabajadoresActuales.Count);

                Trabajador trabajadorAsignado = trabajadoresActuales[opcionTrabajador];

                if (trabajadorAsignado.EstaDisponible)
                {
                    if (!HayPersonasEnHabitacionesOAdyacentes(fila, columna))
                    {
                        // Crear la nueva habitación y agregarla
                        Habitacion nuevaHabitacion = new Habitacion(nombre, espacio);
                        plano[fila][columna] = nuevaHabitacion;

                        Console.WriteLine($"Se ha agregado una nueva habitación en la coordenada [{fila}, {columna}].");

                        if (espacio > 0 && espacio % 5 == 0)
                        {
                            int nuevasUnidades = (espacio / 5) - 1;

                            // Agregar nuevas unidades de espacio a la habitación
                            nuevaHabitacion.AmpliarEspacio(nuevasUnidades);

                            // Actualizar la representacion en el plano
                            for (int fila1 = 0; fila < plano.Count; fila1++)
                            {
                                int indiceHabitacion = plano[fila1].IndexOf(nuevaHabitacion);

                                if (indiceHabitacion != -1)
                                {
                                    for (int i = 0; i < nuevasUnidades; i++)
                                    {
                                        plano[fila1].Insert(indiceHabitacion + 1, nuevaHabitacion);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hay personas en habitaciones adyacentes. Por favor, indique el nombre de la persona que se va a mover: ");
                        string nombrePersona = Console.ReadLine();
                        Habitante habitante = habitantes.Find(h => h.Nombre == nombrePersona);

                        Console.WriteLine("Ingrese la coordenada de la fila a moverse: ");
                        int nuevaFila = int.Parse(Console.ReadLine());

                        Console.WriteLine("Ingrese la coordenada de la columna a moverse: ");
                        int nuevaColumna = int.Parse(Console.ReadLine());

                        if (habitante != null)
                        {
                            //Movemos a la persona de habitacion
                            AgregarHabitanteEnHabitacion(nuevaFila, nuevaColumna, habitante);
                            Habitacion nuevaHabitacion = new Habitacion(nombre, espacio);
                            plano[fila][columna] = nuevaHabitacion;
                            Console.WriteLine($"Se ha agregado una nueva habitación en la coordenada [{fila}, {columna}].");
                            Console.WriteLine($"El habitante {nombrePersona} ha sido movido a la nueva habitación.");
                        }
                        else
                        {
                            Console.WriteLine($"No se encontro al habitante {nombrePersona}.");
                        }
                    }
                    trabajadorAsignado.AsignarTiempo(30);
                    trabajadorAsignado.ActualizarDisponibilidad();
                    Console.WriteLine("Se ha asignado el trabajo");
                }
                else
                {
                    Console.WriteLine($"El trabajador {trabajadorAsignado.Nombre} no está disponible en este momento.");
                }
                Console.WriteLine("Plano de la casa actualizado: ");
                foreach (var filaPlano in plano)
                {
                    foreach (var habitacion in filaPlano)
                    {
                        if (habitacion != null)
                        {
                            Console.Write($"{habitacion.Nombre}, ");
                        }
                        else
                        {
                            Console.WriteLine(" ,");
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Las coordenadas ingresadas están fuera del rango válido.");
            }
        }

        public void AmpliarEspacioHabitacion()
        {
            Console.WriteLine("Ingrese el nombre de la habitación que desea ampliar:");
            string nombreHabitacion = Console.ReadLine();

            Habitacion habitacion = BuscarHabitacionPorNombre(nombreHabitacion);

            Console.WriteLine("Trabajadores disponibles para el trabajo:");
            for (int i = 0; i < trabajadoresActuales.Count; i++)
            {
                Trabajador trabajador = trabajadoresActuales[i];
                Console.WriteLine($"{i + 1}. {trabajador.Nombre} ({(trabajador.EstaDisponible ? "Disponible" : "No Disponible")})");
            }

            int opcionTrabajador;

            do
            {
                Console.Write("Seleccione el número del trabajador que desea asignar al trabajo: ");
                opcionTrabajador = int.Parse(Console.ReadLine()) - 1;
            } while (opcionTrabajador < 0 || opcionTrabajador >= trabajadoresActuales.Count);    // Error AQUI!!!!

            Trabajador trabajadorAsignado = trabajadoresActuales[opcionTrabajador];

            if (trabajadorAsignado.EstaDisponible)
            {
                if (habitacion != null)
                {
                    Console.WriteLine($"La habitación '{nombreHabitacion}' tiene un espacio de {habitacion.Espacio}. ¿Cuánto espacio adicional desea agregar? (Ingrese un número divisible por 5)");

                    int espacioAdicional = int.Parse(Console.ReadLine());

                    if (espacioAdicional > 0 && espacioAdicional % 5 == 0)
                    {
                        int nuevasUnidades = espacioAdicional / 5;

                        // Agregar nuevas unidades de espacio a la habitación
                        habitacion.AmpliarEspacio(nuevasUnidades);

                        // Actualizar la representacion en el plano
                        for (int fila = 0; fila < plano.Count; fila++)
                        {
                            int indiceHabitacion = plano[fila].IndexOf(habitacion);

                            if (indiceHabitacion != -1)
                            {
                                for (int i = 0; i < nuevasUnidades; i++)
                                {
                                    plano[fila].Insert(indiceHabitacion + 1, habitacion);
                                }
                                break;
                            }
                        }

                        Console.WriteLine($"Se ha ampliado el espacio de la habitación '{nombreHabitacion}' en {espacioAdicional} unidades.");
                    }
                    else
                    {
                        Console.WriteLine("El espacio adicional debe ser un número mayor a 0 y divisible por 5.");
                    }
                    trabajadorAsignado.AsignarTiempo(20);
                    trabajadorAsignado.ActualizarDisponibilidad();
                    Console.WriteLine("Se ha asignado el trabajo");
                }
                else
                {
                    Console.WriteLine($"No se encontró la habitación '{nombreHabitacion}'.");
                }
            }
            else
            {
                Console.WriteLine($"El trabajador {trabajadorAsignado.Nombre} no está disponible en este momento.");
            }
        }

        private Habitacion BuscarHabitacionPorNombre(string nombre)
        {
            foreach (var filaHabitaciones in plano)
            {
                foreach (var habitacion in filaHabitaciones)
                {
                    if (habitacion.Nombre == nombre)
                    {
                        return habitacion;
                    }
                }
            }
            return null;
        }

        public void EliminarHabitacion()
        {
            Console.WriteLine("Ingrese la coordenada de fila de la habitación que desea eliminar:");
            int fila = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la coordenada de columna de la habitación que desea eliminar:");
            int columna = int.Parse(Console.ReadLine());

            if (fila >= 0 && fila < plano.Count && columna >= 0 && columna < plano[fila].Count)
            {
                Habitacion habitacionAEliminar = plano[fila][columna];

                if (habitacionAEliminar.Nombre != " ")
                {
                    if (trabajadoresActuales.Count >= 4 && HayHabitacionesAdyacentes(fila, columna, 2))
                    {
                        Console.WriteLine("Trabajadores disponibles para el trabajo:");

                        for (int i = 0; i < trabajadoresActuales.Count; i++)
                        {
                            Trabajador trabajador = trabajadoresActuales[i];
                            Console.WriteLine($"{i + 1}. {trabajador.Nombre} ({(trabajador.EstaDisponible ? "Disponible" : "No Disponible")})");
                        }

                        int opcionTrabajador1;
                        int opcionTrabajador2;
                        int opcionTrabajador3;
                        int opcionTrabajador4;

                        do
                        {
                            Console.Write("Seleccione el número del primer trabajador que desea asignar al trabajo: ");
                            opcionTrabajador1 = int.Parse(Console.ReadLine()) - 1;
                        } while (opcionTrabajador1 < 0 || opcionTrabajador1 >= trabajadoresActuales.Count);

                        do
                        {
                            Console.Write("Seleccione el número del segundo trabajador que desea asignar al trabajo: ");
                            opcionTrabajador2 = int.Parse(Console.ReadLine()) - 1;
                        } while (opcionTrabajador2 < 0 || opcionTrabajador2 >= trabajadoresActuales.Count || opcionTrabajador2 == opcionTrabajador1);

                        do
                        {
                            Console.Write("Seleccione el número del tercer trabajador que desea asignar al trabajo: ");
                            opcionTrabajador3 = int.Parse(Console.ReadLine()) - 1;
                        } while (opcionTrabajador1 < 0 || opcionTrabajador1 >= trabajadoresActuales.Count);

                        do
                        {
                            Console.Write("Seleccione el número del cuarto trabajador que desea asignar al trabajo: ");
                            opcionTrabajador4 = int.Parse(Console.ReadLine()) - 1;
                        } while (opcionTrabajador1 < 0 || opcionTrabajador1 >= trabajadoresActuales.Count);

                        Trabajador trabajadorAsignado1 = trabajadoresActuales[opcionTrabajador1];
                        Trabajador trabajadorAsignado2 = trabajadoresActuales[opcionTrabajador2];
                        Trabajador trabajadorAsignado3 = trabajadoresActuales[opcionTrabajador3];
                        Trabajador trabajadorAsignado4 = trabajadoresActuales[opcionTrabajador4];

                        if (trabajadorAsignado1.EstaDisponible && trabajadorAsignado2.EstaDisponible && trabajadorAsignado3.EstaDisponible && trabajadorAsignado4.EstaDisponible)
                        {
                            // Realizar el trabajo de eliminar la habitación

                            // Luego de realizar el trabajo, actualiza la disponibilidad de los trabajadores
                            trabajadorAsignado1.AsignarTiempo(25);
                            trabajadorAsignado2.AsignarTiempo(25);
                            trabajadorAsignado3.AsignarTiempo(25);
                            trabajadorAsignado4.AsignarTiempo(25);
                            habitacionAEliminar.Nombre = " ";
                            habitacionAEliminar.Espacio = 0;

                            Console.WriteLine($"Se ha eliminado la habitación en la coordenada [{fila}, {columna}].");
                        }
                        else
                        {
                            Console.WriteLine($"Alguno de los trabajadores seleccionados no está disponible en este momento.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pueden eliminar la habitación debido a la falta de trabajadores disponibles o habitaciones adyacentes suficientes.");
                    }
                }
                else
                {
                    Console.WriteLine("No se puede eliminar una habitación vacía.");
                }
            }
            else
            {
                Console.WriteLine("Las coordenadas ingresadas están fuera del rango válido.");
            }
        }

        private bool HayHabitacionesAdyacentes(int fila, int columna, int cantidad)
        {
            int filas = plano.Count;
            int columnas = filas > 0 ? plano[0].Count : 0;

            int habitacionesAdyacentes = 0;

            for (int i = Math.Max(0, fila - 1); i <= Math.Min(filas - 1, fila + 1); i++)
            {
                for (int j = Math.Max(0, columna - 1); j <= Math.Min(columnas - 1, columna + 1); j++)
                {
                    if (i == fila && j == columna)
                    {
                        continue; // No contamos la habitación en sí misma
                    }

                    if (plano[i][j].Nombre != " ")
                    {
                        habitacionesAdyacentes++;
                        if (habitacionesAdyacentes >= cantidad)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void Menu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("== Menú de Opciones ==");
                Console.WriteLine("1. Añadir nuevas habitaciones");
                Console.WriteLine("2. Ampliar área de habitaciones");
                Console.WriteLine("3. Decorar habitaciones");
                Console.WriteLine("4. Arreglo de daños en habitaciones");
                Console.WriteLine("5. Eliminar habitacion");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarNuevaHabitacion();
                        break;
                    case 2:
                        AmpliarEspacioHabitacion();
                        break;
                    case 3:
                        DecorarHabitacion();
                        break;
                    case 4:
                        ArreglarItemEnHabitacion();
                        break;
                    case 5:
                        EliminarHabitacion();
                        break;
                    case 6:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
        }
    }
}

