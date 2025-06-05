using System.ComponentModel.DataAnnotations.Schema;
using firstback.Oportunidades;
using firstback.user;

namespace firstback.UsersOpportunities
{
    public class UsersOpportunities
    {
        [ForeignKey("User")]
        public int Id_User { get; set; }
        [ForeignKey("Oportunidad")]
        public int Id_Opportunity { get; set; }

        public User? user { get; set; }
        public Oportunidad? Oportunidad { get; set; }
    }
}