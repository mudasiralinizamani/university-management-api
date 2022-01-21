namespace university_management_api.Interfaces;

public interface INotification
{
  Task<NotificationModel> CreateAsync(string text, string userId, string variant);
  Task<IEnumerable<NotificationModel>> GetAllAsync();
  Task<NotificationModel?> FindByIdAsync(string id);
  Task<IEnumerable<NotificationModel>> FindByUserIdAsync(string userId);
}