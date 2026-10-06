using FlexSpace.DAL;

namespace FlexSpace.BLL
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool VIP { get; set; }
        public int Sanciones_Activas { get; set; }
    }

    public class Puesto
    {
        public int ID { get; set; }
        public long Codigo { get; set; }
        public string TipoPuesto { get; set; }
        public decimal TBPH { get; set; }
    }

    public class Reserva
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public int PuestoID { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public string Estado { get; set; }
        public decimal CostoTotal { get; set; }
    }

    public class ClienteSancionadoException : Exception
    {
        public ClienteSancionadoException() : base("El cliente tiene 3 o más sanciones activas y no puede realizar reservas."){}

        public ClienteSancionadoException(string mensaje) : base(mensaje){} 
    }

    public class FlexSpaceBLL
    {
        private FlexSpaceDAL _datos = new FlexSpaceDAL();

        public Cliente ObtenerCliente(int ID)
        {
            var resultado = _datos.BuscarPorID0(ID);

            if (resultado == null) return null;

            return new Cliente
            {
                ID = resultado.Value.ID,
                Nombre = resultado.Value.Nombre,
                Email = resultado.Value.Email,
                VIP = resultado.Value.VIP,
                Sanciones_Activas = resultado.Value.Sanciones_Activas,
            };
        }

        public List<Cliente> BuscarPorSanciones()
        {
            var resultados = _datos.BuscarPorSanciones();

            List<Cliente> clientes = new List<Cliente>();

            foreach (var resultado in resultados)
            {
                clientes.Add(new Cliente
                {
                    ID = resultado.ID,
                    Nombre = resultado.Nombre,
                    Email = resultado.Email,
                    VIP = resultado.VIP,
                    Sanciones_Activas = resultado.Sanciones_Activas
                });
            }

            return clientes;
        }

        public bool IncrementarSancion(int ID)
        {
            return _datos.IncrementarSancion(ID);
        }

        public Puesto ObtenerPuesto(int ID)
        {
            var resultado = _datos.BuscarPorID1(ID);

            if (resultado == null) return null;

            return new Puesto
            {
                ID = resultado.Value.ID,
                Codigo = resultado.Value.Codigo,
                TipoPuesto = resultado.Value.TipoPuesto,
                TBPH = resultado.Value.TBPH,
            };
        }
        public Reserva ObtenerReserva(int ID)
        {
            var resultado = _datos.BuscarPorID2(ID);

            if (resultado == null) return null;

            return new Reserva
            {
                ID = resultado.Value.ID,
                ClienteID = resultado.Value.ClienteID,
                PuestoID = resultado.Value.PuestoID,
                Fecha_Inicio = resultado.Value.Fecha_Inicio,
                Fecha_Fin = resultado.Value.Fecha_Fin,
                Estado = resultado.Value.Estado,
                CostoTotal = resultado.Value.CostoTotal,
            };
        }

        public decimal CalcularPrecio(int ClienteID, int PuestoID, DateTime Fecha_Inicio, DateTime Fecha_Fin)
        {
            Cliente cliente = ObtenerCliente(ClienteID);
            Puesto puesto = ObtenerPuesto(PuestoID);
            if (cliente == null)
                return 0;
            if (puesto == null)
                return 0;
            TimeSpan duracion = Fecha_Fin - Fecha_Inicio;
            decimal horas = (decimal)duracion.TotalHours;
            decimal subtotalBase = horas * puesto.TBPH;
            decimal total = subtotalBase;
            bool incluyeFinDeSemana = false;
            DateTime dia = Fecha_Inicio.Date;
            while (dia <= Fecha_Fin.Date)
            {
                if (dia.DayOfWeek == DayOfWeek.Saturday ||
                    dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    incluyeFinDeSemana = true;
                    break;
                }
                dia = dia.AddDays(1);
            }
            if (incluyeFinDeSemana){total = total * 1.15m;}
            if (horas >= 5){total = total * 0.90m;}
            if (cliente.VIP){total = total * 0.95m;}
            if (cliente.Sanciones_Activas > 0){total = subtotalBase;total = total * 1.20m;}
            return Math.Round(total, 2);
        }
        public bool AgregarReserva(
            int ClienteID,
            int PuestoID,
            DateTime Fecha_Inicio,
            DateTime Fecha_Fin)
        {

            Cliente cliente = ObtenerCliente(ClienteID);

            if (cliente == null)
                return false;

            if (cliente.Sanciones_Activas >= 3)
            {
                throw new ClienteSancionadoException();
            }

            Puesto puesto = ObtenerPuesto(PuestoID);

            if (puesto == null)
                return false;

            if (Fecha_Inicio >= Fecha_Fin)
                return false;
            decimal CostoTotal = CalcularPrecio(ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin);

            string Estado = "Confirmada";

            return _datos.AgregarReserva(ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin, Estado,  CostoTotal);
        }
        public bool CancelarReserva(int ID)
        {
            Reserva reserva = ObtenerReserva(ID);
            if (reserva == null)
                return false;
            if (reserva.Estado == "Cancelada")
                return false;
            DateTime ahora = DateTime.Now;
            TimeSpan tiempoRestante =
                reserva.Fecha_Inicio - ahora;
            bool menosDeDosHoras =
                tiempoRestante.TotalHours < 2 &&
                tiempoRestante.TotalHours >= 0;
            bool resultado =
                _datos.CancelarReserva(ID);
            if (resultado && menosDeDosHoras)
            {
                _datos.IncrementarSancion(
                    reserva.ClienteID);
            }
            return resultado;
        }
        public List<Reserva> BuscarReservasPorPuesto(int PuestoID)
        {
            var resultados =
                _datos.BuscarReservasPorPuesto(PuestoID);

            List<Reserva> reservas =
                new List<Reserva>();

            foreach (var resultado in resultados)
            {
                reservas.Add(new Reserva
                {
                    ID = resultado.ID,
                    ClienteID = resultado.ClienteID,
                    PuestoID = resultado.PuestoID,
                    Fecha_Inicio = resultado.Fecha_Inicio,
                    Fecha_Fin = resultado.Fecha_Fin,
                    Estado = resultado.Estado,
                    CostoTotal = resultado.CostoTotal
                });
            }

            return reservas;
        }
    }
}