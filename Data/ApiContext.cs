namespace university_management_api.Data;

public class ApiContext : DbContext
{
  public ApiContext(DbContextOptions options) : base(options) { }

  public DbSet<FacultyModel> Faculties { get; set; }
  public DbSet<NotificationModel> Notifications { get; set; }
  public DbSet<DepartmentModel> Departments { get; set; }
}