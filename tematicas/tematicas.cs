using System.ComponentModel.DataAnnotations;

namespace firstback.tematicas
{
    public class Tematica
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}