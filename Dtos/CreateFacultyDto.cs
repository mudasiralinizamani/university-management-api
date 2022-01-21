namespace university_management_api.Dtos;

public class CreateFacultyDto
{
  [Required]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string DeanId { get; set; } = string.Empty;
}