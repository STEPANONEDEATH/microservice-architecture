namespace ProfilesService.Application.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public decimal Balance { get; set; }

        // Конструктор с Guid для удобства
        public UserDto(System.Guid id, string username, string email, string role, decimal balance)
        {
            Id = id.ToString();
            Username = username;
            Email = email;
            Role = role;
            Balance = balance;
        }
    }
}