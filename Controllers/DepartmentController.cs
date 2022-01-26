namespace university_management_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
  private readonly IDepartment _departmentService;
  private readonly INotification _notificationService;
  private readonly UserManager<UserModel> _userManager;
  private readonly IFaculty _facultyService;

  public DepartmentController(IDepartment departmentService, IFaculty facultyService, INotification notificationService, UserManager<UserModel> userManager)
  {
    _departmentService = departmentService;
    _notificationService = notificationService;
    _userManager = userManager;
    _facultyService = facultyService;
  }

  [HttpPost]
  [Route("Create")]
  public async Task<ActionResult<object>> CreateDepartment(CreateDepartmentDto dto)
  {
    UserModel hod = await _userManager.FindByIdAsync(dto.HodId);

    if (hod is null)
      return BadRequest(new { code = "HodNotFound", error = "Hod is not found" });
    else if (hod.Role != "Hod")
      return BadRequest(new { code = "InvalidRole", error = $"User {hod.FullName} is not Hod" });

    FacultyModel? faculty = await _facultyService.FindByIdAsync(dto.FacultyId);

    if (faculty is null)
      return BadRequest(new { code = "FacultyNotFound", error = "Faculty is not found" });

    DepartmentModel? department = await _departmentService.FindByNameAsync(dto.Name);

    if (department is not null)
      return BadRequest(new { code = "NameFound", error = $"Department name `{dto.Name}` is already taken" });

    try
    {
      DepartmentModel model = await _departmentService.CreateAsync(hod.Id, hod.FullName, faculty.Id, faculty.Name, dto.Name);
      await _notificationService.CreateAsync($"Congrats, A new department has been assigned to you", hod.Id, "success");
      await _notificationService.CreateAsync($"A new department has been assigned you your faculty", faculty.DeanId, "info");
      return Ok(new { succeeded = true, department = model });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while creating the Department" });
    }
  }

  [HttpGet]
  [Route("GetAll")]
  public async Task<ActionResult<DepartmentModel>> GetAllDepartments()
  {
    return Ok(await _departmentService.GetAllAsync());
  }

  [HttpGet]
  [Route("GetById/{id}")]
  public async Task<ActionResult<DepartmentModel>> GetDepartmentById(string id)
  {
    try
    {
      DepartmentModel? department = await _departmentService.FindByIdAsync(id);

      if (department is null)
        return BadRequest(new { code = "NotFound", error = "Department is not found" });
      return Ok(department);
    }
    catch
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding the department" });
    }
  }

  [HttpGet]
  [Route("GetByFacultyId/{faculty_id}")]
  public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetDepartmentByFacultyId(string faculty_id)
  {
    try
    {
      FacultyModel? faculty = await _facultyService.FindByIdAsync(faculty_id);

      if (faculty is null)
        return BadRequest(new { code = "FacultyNotFound", error = "Faculty is not found" });

      IEnumerable<DepartmentModel> departments = await _departmentService.FindByFacultyIdAsync(faculty.Id);

      return Ok(departments);
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding departments" });
    }
  }
}