using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs.Color;

namespace Backend.Services;

public class ColorService : IColorService
{
    private readonly IRepository<int, Color> _colorRepository;
    public ColorService(IRepository<int, Color> colorRepository)
    {
        _colorRepository = colorRepository;
    }
    public async Task<ColorResponseDTO> AddColor(AddColorRequestDTO addColorRequestDTO)
    {
        var color = new Color() { Name = addColorRequestDTO.Name };
        color = await _colorRepository.AddAsync(color);
        var response = new ColorResponseDTO() { ColorId = color.ColorId, Name = color.Name };
        return response;
    }

    public async Task<bool> DeleteColor(int id)
    {
        await _colorRepository.DeleteAsync(id);
        return true;
    }

    public async Task<ColorResponseDTO> EditColor(EditColorRequestDTO editColorRequestDTO)
    {
        var color = await _colorRepository.GetByIdAsync(editColorRequestDTO.ColorId);
        if (color == null)
            throw new Exception("Color not found");
        color.Name = editColorRequestDTO.Name;
        color = await _colorRepository.UpdateAsync(color.ColorId, color);
        var response = new ColorResponseDTO() { ColorId = color.ColorId, Name = color.Name };
        return response;
    }

    public async Task<List<ColorResponseDTO>> GetAllColors()
    {
        var colors = await _colorRepository.GetAllAsync();
        var response = colors.Select(c => new ColorResponseDTO() { ColorId = c.ColorId, Name = c.Name }).ToList();
        return response;
    }

    public async Task<ColorResponseDTO> GetColor(int id)
    {
        var color = await _colorRepository.GetByIdAsync(id);
        var response = new ColorResponseDTO() { ColorId = color.ColorId, Name = color.Name };
        return response;
    }
}