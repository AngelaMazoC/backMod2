using System.ComponentModel.DataAnnotations;

namespace firstback.Oportunidades
{
    public class OportunidadDTO
    {
        [StringLength(255)]
                public string Nombre { get; set; } = null!;

                public string? Observaciones { get; set; }

                [StringLength(50)]
                public string? Tipo { get; set; }

                public string? Descripcion { get; set; }

                public string? Requisitos { get; set; }

                public string? Guia { get; set; }

                public string? DatosAdicionales { get; set; }

                public string? CanalesAtencion { get; set; }

                [StringLength(255)]
                public string? Encargado { get; set; }

                [StringLength(50)]
                public string? Modalidad { get; set; }

                public int CategoriaId { get; set; }

                public int InstitucionId { get; set; }
    }

}