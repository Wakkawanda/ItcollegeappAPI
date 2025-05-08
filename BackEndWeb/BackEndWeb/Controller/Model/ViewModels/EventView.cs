using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class EventView
{
    [Required(ErrorMessage = "Title is required")] 
    [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Type ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Type ID must be a positive integer")]
    public int Typeid { get; set; }

    [MaxLength(255, ErrorMessage = "Information cannot exceed 255 characters")]
    public string? Information { get; set; }
    
    [Required(ErrorMessage = "Company ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Company ID must be a positive integer")]
    public int Companyid { get; set; }

    [MaxLength(1000, ErrorMessage = "Theme cannot exceed 1000 characters")]
    public string? Themeevent { get; set; }
    
    [MaxLength(2000, ErrorMessage = "Task body cannot exceed 2000 characters")]
    public string? Bodytask { get; set; }
    
    public DateTime? Startdate { get; set; }
    
    public DateTime? Enddate { get; set; }
}