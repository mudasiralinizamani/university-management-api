namespace university_management_api.Models;

public class DepartmentModel
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string HodId { get; set; } = string.Empty;
  public string HodName { get; set; } = string.Empty;
  public string FacultyId { get; set; } = string.Empty;
  public string FacultyName { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime UpdateAt { get; set; }
}