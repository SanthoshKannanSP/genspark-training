using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models.DTOs;

public class PaginatedResponseDTO<T> where T : class
{
    [Required]
    public PaginationModel Pagination { get; set; }
    
    public T Data { get; set; }
    
}