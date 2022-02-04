namespace university_management_api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController : ControllerBase
{
  private readonly ISubject _subjectService;
  private readonly IDepartment _departmentService;
  private readonly UserManager<UserModel> _userManager;
  private readonly INotification _notificationService;

  public SubjectController(ISubject subjectService, INotification notificationService, IDepartment departmentsService, UserManager<UserModel> userManager)
  {
    _subjectService = subjectService;
    _departmentService = departmentsService;
    _userManager = userManager;
    _notificationService = notificationService;
  }

  [HttpPost]
  [Route("Create")]
  public async Task<ActionResult<object>> CreateSubject(CreateSubjectDto dto)
  {
    UserModel teacher = await _userManager.FindByIdAsync(dto.TeacherId);

    if (teacher is null)
      return BadRequest(new { code = "TeacherNotFound", error = "Teacher is not found" });
    else if (teacher.Role != "Teacher")
      return BadRequest(new { code = "InvalidRole", error = $"User '{teacher.FullName}' is not Teacher" });

    DepartmentModel? department = await _departmentService.FindByIdAsync(dto.DepartmentId);

    if (department is null)
      return BadRequest(new { code = "DepartmentNotFound", error = "Department is not found" });

    SubjectModel? subject = await _subjectService.FindByNameAsync(dto.Name);

    if (subject is not null)
      return BadRequest(new { code = "NameFound", error = $"Subject name '{dto.Name}' is already taken" });

    try
    {
      SubjectModel model = await _subjectService.CreateAsync(teacher.Id, teacher.FullName, department.Id, department.Name, dto.Name);
      await _notificationService.CreateAsync($"Congrats, A new subject has been assigned to you", teacher.Id, "success");
      await _notificationService.CreateAsync($"A new subject has been added to you department", department.HodId, "info");
      return Ok(new { succeeded = true, subject = model });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while creating Subject" });
    }
  }

  [HttpGet]
  [Route("GetById/{id}")]
  public async Task<ActionResult<SubjectModel>> GetSubjectById(string id)
  {
    try
    {
      SubjectModel? subject = await _subjectService.FindByIdAsync(id);

      if (subject is null)
        return BadRequest(new { code = "NotFound", error = "Subject is not found" });
      return Ok(subject);
    }
    catch
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding the subject" });
    }
  }

  [HttpGet]
  [Route("GetAll")]
  public async Task<ActionResult<IEnumerable<SubjectModel>>> GetAllSubjects()
  {
    return Ok(await _subjectService.GetAllAsync());
  }

  [HttpGet]
  [Route("GetByDepartmentId/{department_id}")]
  public async Task<ActionResult<IEnumerable<SubjectModel>>> GetSubjectsByDepartmentId(string department_id)
  {
    try
    {
      DepartmentModel? department = await _departmentService.FindByIdAsync(department_id);

      if (department is null)
        return BadRequest(new {code = "DepartmentNotFound", error = "Department is not found"});
      
      return Ok(await _subjectService.FindByDepartmentIdAsync(department.Id));
    }
    catch(Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding subjects" });
    }
  }
}