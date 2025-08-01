using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs.Category;

public class EditCategoryRequestDTO
{
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
}