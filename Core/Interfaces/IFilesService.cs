using Microsoft.AspNetCore.Http;


namespace Core.Interfaces
{
    public interface IFilesService
    {
        Task<string> SaveImage(IFormFile file);
        Task DeleteImage(string imagePath);
        Task<string> UpdateImage(string fileName, IFormFile file);
    }
}
