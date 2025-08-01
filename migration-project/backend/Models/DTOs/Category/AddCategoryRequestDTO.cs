using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs.Category;

public class AddCategoryRequestDTO
{
    [Required]
    public string Name { get; set; }
}