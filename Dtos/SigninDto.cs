namespace university_management_api.Dtos;

public class SigninDto
{
  [Required]
  public string Email { get; set; } = string.Empty;
  [Required]
  public string Password { get; set; } = string.Empty;
}