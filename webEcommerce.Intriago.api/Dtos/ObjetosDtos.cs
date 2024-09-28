namespace WebApiPerson.Dtos
{
    public class PagoDto
    {
        public int? Id { get; set; }

        public string? Nombre { get; set; }

        public string? Cuenta { get; set; }

        public string? Tipo { get; set; }

        public decimal Cantidad { get; set; }

    }
}
