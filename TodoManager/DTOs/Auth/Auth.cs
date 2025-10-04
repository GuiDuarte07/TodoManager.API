namespace TodoManager.DTOs.Auth
{
    public record RegisterDto(string Email, string Password, string Name);
    public record LoginDto(string Email, string Password);
}
