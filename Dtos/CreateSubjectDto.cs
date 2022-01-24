namespace university_management_api.Dtos;

public class CreateSubjectDto
{
  [Required]
  [MaxLength(20)]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string DepartmentId { get; set; } = string.Empty;
  [Required]
  public string TeacherId { get; set; } = string.Empty;
}