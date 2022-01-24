namespace university_management_api.Models;

public class SubjectModel
{
  public string Id { get; set; }= string.Empty;
  public string Name { get; set; } = string.Empty;
  public string TeacherName { get; set; } = string.Empty;
  public string TeacherId { get; set; } = string.Empty;
  public string DepartmentId { get; set; } = string.Empty;
  public string DepartmentName { get; set; } = string.Empty;
}