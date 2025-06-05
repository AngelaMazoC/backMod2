namespace firstback.user
{
    public class UserRegisterDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}