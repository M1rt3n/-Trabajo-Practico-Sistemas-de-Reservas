using FlexSpace.BLL;

namespace FlexSpace.UI
{
    public class Program
    {
        public static void Main()
        {
            int op;

            Console.WriteLine("===== FLEXSPACE =====");
            Console.WriteLine("1. Buscar Cliente");
            Console.WriteLine("2. Buscar Puesto");
            Console.WriteLine("3. Buscar Reserva");
            Console.WriteLine("4. Registrar Nueva Reserva");
            Console.WriteLine("5. Cancelar Reserva");
            Console.WriteLine("6. Consultar Reservas Activas por Puesto");
            Console.WriteLine("7. Listar Clientes Sancionados");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");

            op = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.Write("Ingrese ID: ");
                    int ID = Convert.ToInt32(Console.ReadLine());

                    FlexSpaceBLL negocio = new FlexSpaceBLL();

                    Cliente cliente = negocio.ObtenerCliente(ID);

                    if (cliente != null)
                        Console.WriteLine($"Cliente encontrado: {cliente.ID}");
                    else
                        Console.WriteLine("Cliente no encontrado");

                    break;


                case 2:
                    Console.Write("Ingrese ID: ");
                    int ID2 = Convert.ToInt32(Console.ReadLine());

                    FlexSpaceBLL negocio2 = new FlexSpaceBLL();

                    Puesto puesto = negocio2.ObtenerPuesto(ID2);

                    if (puesto != null)
                        Console.WriteLine($"Puesto encontrado: {puesto.ID}");
                    else
                        Console.WriteLine("Puesto no encontrado");

                    break;


                case 3:
                    Console.Write("Ingrese ID: ");
                    int ID3 = Convert.ToInt32(Console.ReadLine());

                    FlexSpaceBLL negocio3 = new FlexSpaceBLL();

                    Reserva reserva = negocio3.ObtenerReserva(ID3);

                    if (reserva != null)
                        Console.WriteLine($"Reserva encontrada: {reserva.ID}");
                    else
                        Console.WriteLine("Reserva no encontrada");

                    break;

                case 4:
                    Console.WriteLine("===== REGISTRAR NUEVA RESERVA =====");

                    Console.Write("Ingrese Cliente ID: ");
                    int ClienteID = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Ingrese Puesto ID: ");
                    int PuestoID = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Ingrese Fecha y Hora de Inicio: ");
                    DateTime Fecha_Inicio =
                        Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Ingrese Fecha y Hora de Fin: ");
                    DateTime Fecha_Fin =
                        Convert.ToDateTime(Console.ReadLine());

                    FlexSpaceBLL negocio4 = new FlexSpaceBLL();

                    decimal CostoTotal = negocio4.CalcularPrecio(PuestoID, Fecha_Inicio, Fecha_Fin);

                    Console.WriteLine();
                    Console.WriteLine("===== RESUMEN =====");
                    Console.WriteLine($"Cliente: {ClienteID}");
                    Console.WriteLine($"Puesto: {PuestoID}");
                    Console.WriteLine($"Inicio: {Fecha_Inicio}");
                    Console.WriteLine($"Fin: {Fecha_Fin}");
                    Console.WriteLine($"Costo total: ${CostoTotal}");

                    Console.WriteLine();
                    Console.Write("¿Desea confirmar la reserva? (S/N): ");
                    string confirmacion = Console.ReadLine();

                    if (confirmacion.ToUpper() == "S")
                    {
                        bool resultado =
                            negocio4.AgregarReserva(ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin, CostoTotal);

                        if (resultado)
                            Console.WriteLine("Reserva registrada correctamente.");
                        else
                            Console.WriteLine("No se pudo registrar la reserva.");
                    }
                    else
                    {
                        Console.WriteLine("Reserva cancelada por el usuario.");
                    }

                    break;

                case 5:
                    Console.WriteLine("===== CANCELAR RESERVA =====");

                    Console.Write("Ingrese ID de la reserva: ");
                    int IDReserva =
                        Convert.ToInt32(Console.ReadLine());

                    FlexSpaceBLL negocio5 = new FlexSpaceBLL();

                    bool cancelada =
                        negocio5.CancelarReserva(IDReserva);

                    if (cancelada)
                        Console.WriteLine("Reserva cancelada correctamente.");
                    else
                        Console.WriteLine("No se pudo cancelar la reserva.");

                    break;

                case 6:
                    Console.WriteLine("===== RESERVAS ACTIVAS POR PUESTO =====");

                    Console.Write("Ingrese ID del puesto: ");
                    int IDPuesto =
                        Convert.ToInt32(Console.ReadLine());

                    FlexSpaceBLL negocio6 = new FlexSpaceBLL();

                    var reservas =
                        negocio6.BuscarReservasPorPuesto(IDPuesto);

                    if (reservas != null && reservas.Count > 0)
                    {
                        foreach (var reservaPuesto in reservas)
                        {
                            Console.WriteLine("--------------------------------");
                            Console.WriteLine($"ID: {reservaPuesto.ID}");
                            Console.WriteLine($"Cliente: {reservaPuesto.ClienteID}");
                            Console.WriteLine($"Puesto: {reservaPuesto.PuestoID}");
                            Console.WriteLine($"Inicio: {reservaPuesto.Fecha_Inicio}");
                            Console.WriteLine($"Fin: {reservaPuesto.Fecha_Fin}");
                            Console.WriteLine($"Estado: {reservaPuesto.Estado}");
                            Console.WriteLine($"Costo: ${reservaPuesto.CostoTotal}");
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "No hay reservas activas futuras para este puesto."
                        );
                    }

                    break;

                case 7:
                    Console.WriteLine("===== CLIENTES SANCIONADOS =====");

                    FlexSpaceBLL negocio7 = new FlexSpaceBLL();

                    var clientes =
                        negocio7.BuscarPorSanciones();

                    if (clientes != null && clientes.Count > 0)
                    {
                        foreach (var clienteSancionado in clientes)
                        {
                            Console.WriteLine("--------------------------------");
                            Console.WriteLine($"ID: {clienteSancionado.ID}");
                            Console.WriteLine($"Nombre: {clienteSancionado.Nombre}");
                            Console.WriteLine($"Email: {clienteSancionado.Email}");
                            Console.WriteLine($"VIP: {clienteSancionado.VIP}");
                            Console.WriteLine(
                                $"Sanciones activas: {clienteSancionado.Sanciones_Activas}"
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "No hay clientes con sanciones activas."
                        );
                    }

                    break;


                case 8:
                    Console.WriteLine("Saliendo del programa...");
                    break;


                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}