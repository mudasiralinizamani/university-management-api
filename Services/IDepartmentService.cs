namespace university_management_api.Services;

public class IDepartmentService : IDepartment
{
  private readonly ApiContext _context;

  public IDepartmentService(ApiContext context)
  {
    _context = context;
  }
  public async Task<DepartmentModel> CreateAsync(string HodId, string HodName, string FacultyId, string FacultName, string Name)
  {
    ArgumentNullException.ThrowIfNull(HodId);
    ArgumentNullException.ThrowIfNull(HodName);
    ArgumentNullException.ThrowIfNull(FacultyId);
    ArgumentNullException.ThrowIfNull(FacultName);
    ArgumentNullException.ThrowIfNull(Name);

    DepartmentModel model = new()
    {
      FacultyId = FacultyId,
      CreatedAt = DateTime.Now,
      FacultyName = FacultName,
      HodId = HodId,
      HodName = HodName,
      Id = Guid.NewGuid().ToString(),
      Name = Name,
      UpdateAt = DateTime.Now
    };

    await _context.Departments.AddAsync(model);
    await _context.SaveChangesAsync();
    return model;
  }

  public void DeleteDepartment(DepartmentModel department)
  {
    ArgumentNullException.ThrowIfNull(department);
    _context.Departments.Remove(department);
    _context.SaveChanges();
  }

  public async Task<DepartmentModel?> FindByIdAsync(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    return await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync<DepartmentModel>();
  }

  public async Task<DepartmentModel?> FindByNameAsync(string name)
  {
    ArgumentNullException.ThrowIfNull(name);
    return await _context.Departments.Where(d => d.Name == name).FirstOrDefaultAsync<DepartmentModel>();
  }

  public async Task<IEnumerable<DepartmentModel>> GetAllAsync()
  {
    return await _context.Departments.ToListAsync<DepartmentModel>();
  }
}