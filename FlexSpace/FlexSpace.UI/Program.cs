using FlexSpace.BLL;
namespace FlexSpace.UI
{
    public class Program
    {
        public static void Main()
        {
            int op;
            op = Convert.ToInt32(Console.ReadLine());
            switch (op) 
            {
                case 1:
                Console.Write("Ingrese ID: ");
                int ID = Convert.ToInt32(Console.ReadLine());
                FlexSpaceBLL negocio = new FlexSpaceBLL();

                Cliente cliente = negocio.ObtenerCliente(ID);

                if (cliente != null)
                    Console.WriteLine($"Cliente encontrado: {cliente.ID},");
                else
                    Console.WriteLine("Cliente no encontrado");
                break;
                case 2:
                    Console.Write("Ingrese ID: ");
                    int ID2 = Convert.ToInt32(Console.ReadLine());
                    FlexSpaceBLL negocio2 = new FlexSpaceBLL();

                    Puesto puesto = negocio2.ObtenerPuesto(ID2);

                    if (puesto != null)
                        Console.WriteLine($"Puesto encontrado: {puesto.ID},");
                    else
                        Console.WriteLine("Puesto no encontrado");
                break;
                case 3:
                    Console.Write("Ingrese ID: ");
                    int ID3 = Convert.ToInt32(Console.ReadLine());
                    FlexSpaceBLL negocio3 = new FlexSpaceBLL();

                    Reserva reserva = negocio3.ObtenerReserva(ID3);

                    if (reserva != null)
                        Console.WriteLine($"Reserva encontrada: {reserva.ID},");
                    else
                        Console.WriteLine("Reserva no encontrada");
                break;
            }
        }
    }
}