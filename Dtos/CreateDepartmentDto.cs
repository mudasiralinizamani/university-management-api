namespace university_management_api.Dtos;

public class CreateDepartmentDto
{
  [Required]
  [MaxLength(20)]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string HodId { get; set; } = string.Empty;
  [Required]
  public string FacultyId { get; set; } = string.Empty;
}