using Microsoft.AspNetCore.Http;

namespace SentinelGear.Helpers
{
    public static class ImageUploader
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            if (file.Length > MaxFileSize)
            {
                return false;
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return AllowedExtensions.Contains(extension);
        }

        public static async Task<string?> SaveImageAsync(IFormFile file, string webRootPath)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
            {
                return null;
            }

            string uploadsFolder = Path.Combine(webRootPath, "images", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/products/{uniqueFileName}";
        }

        public static void DeleteImage(string? imageUrl, string webRootPath)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            string trimmedPath = imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            string fullPath = Path.Combine(webRootPath, trimmedPath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}