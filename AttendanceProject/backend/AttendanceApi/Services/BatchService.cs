using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Services;

public class BatchService : IBatchService
{
    private readonly IRepository<int, Batch> _batchRepository;
    private readonly IRepository<int, Student> _studentRepository;
    private readonly IMapper _mapper;

    public BatchService(
        IRepository<int, Batch> batchRepository,
        IRepository<int, Student> studentRepository,
        IMapper mapper)
    {
        _batchRepository = batchRepository;
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BatchResponseDto>> GetAllBatchesAsync()
    {
        var batches = await _batchRepository.GetAll();
        return _mapper.Map<IEnumerable<BatchResponseDto>>(batches);
    }

    public async Task<BatchResponseDto> GetBatchByIdAsync(int id)
    {
        var batch = await _batchRepository.Get(id);
        return _mapper.Map<BatchResponseDto>(batch);
    }

    public async Task<BatchResponseDto> CreateBatchAsync(BatchCreateRequestDto batchDto)
    {
        var batch = _mapper.Map<Batch>(batchDto);
        var created = await _batchRepository.Add(batch);
        return _mapper.Map<BatchResponseDto>(created);
    }

    public async Task<BatchResponseDto> AssignStudentToBatchAsync(AssignStudentRequestDto assignDto)
    {
        var student = await _studentRepository.Get(assignDto.StudentId);
        if (student == null) throw new Exception("Student not found");

        student.BatchId = assignDto.BatchId;
        await _studentRepository.Update(assignDto.StudentId, student);

        var batch = await _batchRepository.Get(assignDto.BatchId);
        return _mapper.Map<BatchResponseDto>(batch);
    }
}
