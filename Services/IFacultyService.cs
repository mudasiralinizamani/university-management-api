namespace university_management_api.Services;

public class IFacultyService : IFaculty
{
  private readonly ApiContext _context;

  public IFacultyService(ApiContext context)
  {
    _context = context;
  }
  public async Task<FacultyModel> CreateAsync(string DeanId, string DeanName, string Name)
  {
    ArgumentNullException.ThrowIfNull(DeanId);
    ArgumentNullException.ThrowIfNull(Name);
    ArgumentNullException.ThrowIfNull(DeanName);


    FacultyModel model = new()
    {
      DeanId = DeanId,
      DeanName = DeanName,
      Id = Guid.NewGuid().ToString(),
      Name = Name,
      CreateAt = DateTime.Now,
      UpdateAt = DateTime.Now,
    };

    await _context.Faculties.AddAsync(model);
    await _context.SaveChangesAsync();
    return model;
  }

  public void Delete(FacultyModel faculty)
  {
    ArgumentNullException.ThrowIfNull(faculty);
    _context.Remove(faculty);
    _context.SaveChanges();
  }

  public async Task<FacultyModel?> FindByIdAsync(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    return await _context.Faculties.Where(f => f.Id == id).FirstOrDefaultAsync<FacultyModel>();
  }

  public async Task<FacultyModel?> FindByNameAsync(string name)
  {
    ArgumentNullException.ThrowIfNull(name);
    return await _context.Faculties.Where(f => f.Name == name).FirstOrDefaultAsync<FacultyModel>();
  }

  public async Task<IEnumerable<FacultyModel>> GetAllAsync()
  {
    return await _context.Faculties.ToListAsync<FacultyModel>();
  }

  public FacultyModel UpdateDean(FacultyModel faculty, UserModel dean)
  {
    ArgumentNullException.ThrowIfNull(faculty);
    ArgumentNullException.ThrowIfNull(dean);

    faculty.DeanId = dean.Id;
    faculty.DeanName = dean.FullName;
    faculty.UpdateAt = DateTime.Now;
    _context.SaveChanges();
    
    return faculty;
  }

  public FacultyModel UpdateName(FacultyModel faculty, string name)
  {
    ArgumentNullException.ThrowIfNull(faculty);
    ArgumentNullException.ThrowIfNull(name);

    faculty.Name = name;
    faculty.UpdateAt = DateTime.Now;

    return faculty;
  }
}