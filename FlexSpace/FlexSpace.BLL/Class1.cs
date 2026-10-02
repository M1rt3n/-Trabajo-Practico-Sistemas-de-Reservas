using FlexSpace.DAL;
namespace FlexSpace.BLL
{    public class Cliente
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
    }
}