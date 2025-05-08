using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class ResulteventView
{
    [Required(ErrorMessage = "Result is required")]
    [MaxLength(50, ErrorMessage = "Result cannot exceed 50 characters")]
    public string Result { get; set; } = null!;
}