using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Models.DTOs;
public interface IBatchService
{
    Task<IEnumerable<BatchResponseDto>> GetAllBatchesAsync();
    Task<BatchResponseDto> GetBatchByIdAsync(int id);
    Task<BatchResponseDto> CreateBatchAsync(BatchCreateRequestDto batchDto);
    Task<BatchResponseDto> AssignStudentToBatchAsync(AssignStudentRequestDto assignDto);
}
