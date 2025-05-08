using System.ComponentModel.DataAnnotations;

namespace BackEndWeb.Controller.Model.ViewModels;

public class TypeeventView
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(45, ErrorMessage = "Title cannot exceed 45 characters")]
    public string Title { get; set; } = null!;
}