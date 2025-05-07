using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class ReporteventView
{
    [Required(ErrorMessage = "Taskassignmentsid is required")] 
    public Guid Taskassignmentsid { get; set; }

    [MaxLength(2000, ErrorMessage = "Information cannot exceed 2000 characters")]
    public string? Body { get; set; }

    [Required(ErrorMessage = "Resultid ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Resultid ID must be a positive integer")]
    public int Resultid { get; set; }

    [RegularExpression(@"^(InProgress|Completed|Overdue)$", ErrorMessage = "Invalid Status value. Can be (InProgress, Completed, Overdue)")]
    public string? Status { get; set; }
}