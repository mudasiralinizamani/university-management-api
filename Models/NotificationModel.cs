namespace university_management_api.Models;

public class NotificationModel
{
  public string Id { get; set; } = string.Empty;
  public string Text { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string Variant { get; set; } = string.Empty;
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
}