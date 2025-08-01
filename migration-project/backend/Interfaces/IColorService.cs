using Backend.Models.DTOs.Color;

namespace Backend.Interfaces;

public interface IColorService
{
    Task<List<ColorResponseDTO>> GetAllColors();
    Task<ColorResponseDTO> GetColor(int id);
    Task<ColorResponseDTO> AddColor(AddColorRequestDTO addColorRequestDTO);
    Task<ColorResponseDTO> EditColor(EditColorRequestDTO editColorRequestDTO);
    Task<bool> DeleteColor(int id);
}