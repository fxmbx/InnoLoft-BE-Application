using System.ComponentModel.DataAnnotations;
namespace EventModuleApi.Core.Models;
public class Event
{
    [Key]
    public string? Id { get; set; }
    public string? EventTitle { get; set; }
    public string? EventDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? EventType { get; set; }
    public string? TimeZone { get; set; }
    public DateTime CreatedAt { get; set; } = new DateTime();
}
