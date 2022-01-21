namespace university_management_api.Services;

public class INotificationService : INotification
{
  private readonly ApiContext _context;

  public INotificationService(ApiContext context)
  {
    _context = context;
  }
  public async Task<NotificationModel> CreateAsync(string text, string userId, string variant)
  {
    ArgumentNullException.ThrowIfNull(text);
    ArgumentNullException.ThrowIfNull(userId);
    ArgumentNullException.ThrowIfNull(variant);

    NotificationModel model = new()
    {
      CreateAt = DateTime.Now,
      UpdateAt = DateTime.Now,
      Id = Guid.NewGuid().ToString(),
      Text = text,
      UserId = userId,
      Variant = variant,
    };

    await _context.Notifications.AddAsync(model);
    await _context.SaveChangesAsync();
    return model;
  }

  public async Task<NotificationModel?> FindByIdAsync(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    return await _context.Notifications.Where(n => n.Id == id).FirstOrDefaultAsync<NotificationModel>();
  }

  public async Task<IEnumerable<NotificationModel>> FindByUserIdAsync(string userId)
  {
    ArgumentNullException.ThrowIfNull(userId);
    return await _context.Notifications.Where(x => x.UserId == userId).ToListAsync<NotificationModel>();
  }

  public async Task<IEnumerable<NotificationModel>> GetAllAsync()
  {
    return await _context.Notifications.ToListAsync<NotificationModel>();
  }
}