using System.ComponentModel.DataAnnotations;

namespace EventModuleApi.Dto;
public class EventDto
{
    public string? Id { get; set; }
    public string? EventTitle { get; set; }
    public string? EventDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? EventType { get; set; }
    public string? TimeZone {get; set;}
    public DateTime CreatedAt { get; set; } 
}

public class CreateEventDto {
    [Required(ErrorMessage ="Event Title is Required")]
    public string? EventTitle { get; set; }
    [Required(ErrorMessage ="Event Description is required")]
    public string? EventDescription { get; set; }
    [Required(ErrorMessage ="Start Date  is required")]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage ="End Date  is required")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    [Required(ErrorMessage ="Time Zone is required")]
    public string? TimeZone {get; set;}

    public string? EventType { get; set; } 
}


