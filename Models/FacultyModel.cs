namespace university_management_api.Models;

public class FacultyModel
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string DeanId { get; set; } = string.Empty;
  public string DeanName { get; set; } = string.Empty;
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
}