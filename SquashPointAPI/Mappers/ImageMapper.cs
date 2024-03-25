using SquashPointAPI.Models;

namespace SquashPointAPI.Mappers;

public static class ImageMapper
{
    public static async Task<Image> ToImage(this IFormFile imageFile)
    {

        using (MemoryStream memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            return new Image
            {
                ImageData = memoryStream.ToArray(),
                FileExtension = Path.GetExtension(imageFile.FileName),
            };
        }
    }
}