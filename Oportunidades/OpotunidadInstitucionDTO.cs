using System.ComponentModel.DataAnnotations;

namespace firstback.Oportunidades
{
    public class OportunidadesInstitucionesDTO
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string nombre { get; set; } = null!;

        public string? observaciones { get; set; }

        [StringLength(50)]
        public string? tipo { get; set; }

        public string? descripcion { get; set; }

        public string? requisitos { get; set; }

        public string? guia { get; set; }

        public string? datos_adicionales { get; set; }

        public string? canales_atencion { get; set; }

        [StringLength(255)]
        public string? encargado { get; set; }

        [StringLength(50)]
        public string? modalidad { get; set; }

        public int? id_categoria { get; set; }

        public int InstitucionId { get; set; }
        public string? NombreInstitucion { get; set; }
    }
}