namespace university_management_api.Interfaces;

public interface IDepartment
{
  Task<DepartmentModel> CreateAsync(string HodId, string HodName, string FacultyId, string FacultName, string Name);
  Task<DepartmentModel?> FindByNameAsync(string name);
  Task<IEnumerable<DepartmentModel>> GetAllAsync();
  Task<DepartmentModel?> FindByIdAsync(string id);
  void DeleteDepartment(DepartmentModel department);
  Task<IEnumerable<DepartmentModel>> FindByFacultyIdAsync(string faculty_id);
}