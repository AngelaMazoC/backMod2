using System.ComponentModel.DataAnnotations.Schema;
using firstback.bootcamps;
using firstback.Instituciones;

namespace firstback.InstitucionesBootcamps
{
    public class InstitucionBootcamp
    {
        [ForeignKey("Institucion")]
        public int Id_Institucion { get; set; }

        [ForeignKey("Bootcamp")]
        public int Id_Bootcamp { get; set; }

        public Institucion Institucion { get; set; } = null!;
        public Bootcamp Bootcamp { get; set; } = null!;
    }
}