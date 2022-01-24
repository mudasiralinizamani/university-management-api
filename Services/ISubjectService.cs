namespace university_management_api.Services;

public class ISubjectService : ISubject
{
  private readonly ApiContext _context;

  public ISubjectService(ApiContext context)
  {
    _context = context;
  }
  public async Task<SubjectModel> CreateAsync(string TeacherId, string TeacherName, string DepartmentId, string DepartmentName, string name)
  {
    ArgumentNullException.ThrowIfNull(TeacherId);
    ArgumentNullException.ThrowIfNull(TeacherName);
    ArgumentNullException.ThrowIfNull(DepartmentId);
    ArgumentNullException.ThrowIfNull(DepartmentName);
    ArgumentNullException.ThrowIfNull(name);

    SubjectModel model = new()
    {
      DepartmentId = DepartmentId,
      DepartmentName = DepartmentName,
      Id = Guid.NewGuid().ToString(),
      Name = name,
      TeacherId = TeacherId,
      TeacherName = TeacherName,
    };

    await _context.Subjects.AddAsync(model);
    await _context.SaveChangesAsync();
    return model;
  }

  public void DeleteSubject(SubjectModel subject)
  {
    ArgumentNullException.ThrowIfNull(subject);
    _context.Subjects.Remove(subject);
    _context.SaveChanges();
  }

  public async Task<SubjectModel?> FindByIdAsync(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    return await _context.Subjects.Where(s => s.Id == id).FirstOrDefaultAsync<SubjectModel>();
  }

  public async Task<SubjectModel?> FindByNameAsync(string name)
  {
    ArgumentNullException.ThrowIfNull(name);
    return await _context.Subjects.Where(s => s.Name == name).FirstOrDefaultAsync<SubjectModel>();
  }

  public async Task<IEnumerable<SubjectModel>> GetAllAsync()
  {
    return await _context.Subjects.ToListAsync<SubjectModel>();
  }
}