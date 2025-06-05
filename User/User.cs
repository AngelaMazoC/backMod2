using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using firstback.roles;

namespace firstback.user
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public Roles? Role { get; set; }

        [JsonIgnore]
         public ICollection<firstback.UsersOpportunities.UsersOpportunities>? UsersOpportunities { get; set; }
    }
}