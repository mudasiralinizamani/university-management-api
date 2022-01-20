using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace university_management_api.Data;

public class AuthContext : IdentityDbContext
{
  public AuthContext(DbContextOptions<AuthContext> opts) : base(opts) { }

  public DbSet<UserModel> Users { get; set; }
}