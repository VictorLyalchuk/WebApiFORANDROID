using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using Size = SixLabors.ImageSharp.Size;


namespace Core.Services
{
    public class FilesService : IFilesService
    {
        private const string imageFolder = "uploads";
        int[] sizes = { 320, 600, 1200 };

        public async Task<string> SaveImage(IFormFile file)
        {
            try
            {
                string root = Directory.GetCurrentDirectory();
                string newNameFile = Guid.NewGuid().ToString();
                string fileName = $"{newNameFile}.webp";

                foreach (int size in sizes)
                {
                    string fullFileName = $"{size}_{newNameFile}.webp";
                    string imagePath = Path.Combine(root, imageFolder, fullFileName);
                    {
                        using (var image = Image.Load(file.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Size = new Size(size, size),
                                Mode = ResizeMode.Max
                            }));
                            await image.SaveAsync(imagePath, new WebpEncoder());
                        }
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні файлу {ex.Message}");
                return ex.Message;
            }
        }
        public async Task DeleteImage(string imagePath)
        {
            await Task.Run(() =>
            {
                try
                {
                    string root = Directory.GetCurrentDirectory();
                    foreach (int size in sizes)
                    {
                        string fullRoot = Path.Combine(root, imageFolder);
                        string imageSizePath = $"{size}_{imagePath}";
                        string imageFullPath = Path.Combine(fullRoot, imageSizePath);
                        if (File.Exists(imageFullPath))
                        {
                            File.Delete(imageFullPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при видалені файлу {ex.Message}");
                }
            });
        }
    }
}
