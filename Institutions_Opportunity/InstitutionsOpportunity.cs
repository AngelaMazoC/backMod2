using System.ComponentModel.DataAnnotations.Schema;
using firstback.Instituciones;
using firstback.Oportunidades;

namespace firstback.InstitutionsOpportunity
{
    public class InstitutionOpportunity
    {
        [ForeignKey("Institucion")]
        public int Id_Institution { get; set; }
        [ForeignKey("Oportunidad")]
        public int Id_Opportunity { get; set; }

        public Institucion? Institucion { get; set; } 
        public Oportunidad? Oportunidad { get; set; } 
    }
}