namespace university_management_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FacultyController : ControllerBase
{
  private readonly IFaculty _facultyService;
  private readonly UserManager<UserModel> _userManager;
  private readonly INotification _notificationService;
  private readonly IDepartment _departmentService;

  public FacultyController(IFaculty facultyService, UserManager<UserModel> userManager, INotification notificationService, IDepartment departmentService)
  {
    _facultyService = facultyService;
    _userManager = userManager;
    _notificationService = notificationService;
    _departmentService = departmentService;
  }

  [HttpPost]
  [Route("Create")]
  public async Task<ActionResult<object>> CreateFaculty(CreateFacultyDto dto)
  {
    UserModel dean = await _userManager.FindByIdAsync(dto.DeanId);

    if (dean is null)
      return BadRequest(new { code = "DeanNotFound", error = "Dean is not found" });
    else if (dean.Role != "Dean")
      return BadRequest(new { code = "InvalidRole", error = $"User '{dean.FullName}' is not Dean" });

    FacultyModel? faculty = await _facultyService.FindByNameAsync(dto.Name);

    if (faculty is not null)
      return BadRequest(new { code = "NameFound", error = $"Faculty name '{faculty.Name}' is already taken" });

    try
    {
      FacultyModel model = await _facultyService.CreateAsync(dean.Id, dean.FullName, dto.Name);
      await _notificationService.CreateAsync($"Congrats, A new faculty has been assigned to you. {model.Name}", dean.Id, "success");
      return Ok(new { succeeded = true, faculty = model });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while creating the faculty" });
    }
  }

  [HttpGet]
  [Route("GetAll")]
  public async Task<ActionResult<IEnumerable<FacultyModel>>> GetAllFaculties()
  {
    return Ok(await _facultyService.GetAllAsync());
  }

  [HttpGet]
  [Route("GetById/{id}")]
  public async Task<ActionResult<FacultyModel>> GetFacultyById(string id)
  {
    try
    {
      FacultyModel? faculty = await _facultyService.FindByIdAsync(id);

      if (faculty is null)
        return BadRequest(new { code = "NotFound", error = "Faculty is not found" });

      return Ok(faculty);
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding the faculty" });
    }
  }

  [HttpPut]
  [Route("UpdateDean")]
  public async Task<ActionResult<object>> UpdateFacultyDean(UpdateFacultyDeanDto dto)
  {
    try
    {
      FacultyModel? faculty = await _facultyService.FindByIdAsync(dto.FacultyId);

      if (faculty is null)
        return BadRequest(new { code = "FacultyNotFound", error = "Faculty is not found" });
      else if (faculty.DeanId == dto.DeanId)
        return BadRequest(new { code = "SameDean", error = $"Dean '{faculty.DeanName}' already have this Faculty" });

      UserModel dean = await _userManager.FindByIdAsync(dto.DeanId);

      if (dean is null)
        return BadRequest(new { code = "DeanNotFound", error = "Dean is not found" });
      else if (dean.Role != "Dean")
        return BadRequest(new { code = "UserNotDean", error = $"User '{dean.FullName}' is not dean" });

      // Name and Id of old dean
      string name = faculty.DeanName;
      string id = faculty.DeanId;

      FacultyModel model = _facultyService.UpdateDean(faculty, dean);
      await _notificationService.CreateAsync($"Congrats, A new faculty '{faculty.Name}' has been assigned you", dean.Id, "success");
      await _notificationService.CreateAsync($"Your faculty '{faculty.Name}' has assigned to another dean '{dean.FullName}'", id, "warning");

      IEnumerable<DepartmentModel> departments = await _departmentService.FindByFacultyIdAsync(faculty.Id);

      foreach (var department in departments)
      {
        await _notificationService.CreateAsync($"Faculty dean has been changed", department.HodId, "info");
      }

      return Ok(new { succeeded = true, faculty = faculty });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while updating faculty dean" });
    }
  }

  // TODO Complete this function Delete Faculty and Delete info from Departments and Send Notification
  [HttpGet]
  [Route("Delete/{id}")]
  public async Task<ActionResult<object>> DeleteFaculty(string id)
  {
    try
    {
      FacultyModel? faculty = await _facultyService.FindByIdAsync(id);

      if (faculty is null)
        return BadRequest(new { code = "NotFound", error = "Faculty is not found" });

      return Ok(new { msg = "UnCompletedMethod" });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while Deleting the faculty" });
    }
  }
}