using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class CompanyView
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string Title { get; set; } = null!;
}