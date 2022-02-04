namespace university_management_api.Interfaces;

public interface ISubject
{
  Task<SubjectModel> CreateAsync(string TeacherId, string TeacherName, string DepartmentId, string DepartmentName, string name);
  Task<SubjectModel?> FindByIdAsync(string id);
  Task<SubjectModel?> FindByNameAsync(string name);
  Task<IEnumerable<SubjectModel>> GetAllAsync();
  void DeleteSubject(SubjectModel subject);

  Task<IEnumerable<SubjectModel>> FindByDepartmentIdAsync(string department_id);
}