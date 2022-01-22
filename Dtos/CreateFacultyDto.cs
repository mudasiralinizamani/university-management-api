namespace university_management_api.Dtos;

public class CreateFacultyDto
{
  [Required]
  [MaxLength(20)]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string DeanId { get; set; } = string.Empty;
}