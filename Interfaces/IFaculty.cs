namespace university_management_api.Interfaces;

public interface IFaculty
{
  Task<FacultyModel> CreateAsync(string DeanId, string DeanName, string Name);
  Task<FacultyModel?> FindByNameAsync(string name);
  Task<IEnumerable<FacultyModel>> GetAllAsync();
  Task<FacultyModel?> FindByIdAsync(string id);

  void Delete(FacultyModel faculty);

  FacultyModel UpdateDean(FacultyModel faculty, UserModel dean);
  FacultyModel UpdateName(FacultyModel faculty, string name);
}