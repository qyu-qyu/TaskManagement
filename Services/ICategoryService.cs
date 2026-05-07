using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}