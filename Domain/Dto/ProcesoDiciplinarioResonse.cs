namespace Domain.Dto
{
    public class ProcesoDiciplinarioResponse
    {
        public int IdEmpleado { get; set; }

        public DateTime Fecha { get; set; }

        public string? Comentario { get; set; }

        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}