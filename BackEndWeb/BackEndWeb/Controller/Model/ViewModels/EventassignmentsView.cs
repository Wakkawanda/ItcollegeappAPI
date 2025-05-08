using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class EventassignmentsView
{
    [Required(ErrorMessage = "Eventid is required")]
    public Guid Eventid { get; set; }

    [Required(ErrorMessage = "Studentid is required")]
    [MaxLength(8, ErrorMessage = "Studentid cannot exceed 8 characters")]
    public string Studentid { get; set; } = null!;
    
    [RegularExpression(@"^(InProgress|Completed|Overdue)$", ErrorMessage = "Invalid Status value. Can be (InProgress, Completed, Overdue)")]
    public string? Status { get; set; }
    
    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string? Notes { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }
}