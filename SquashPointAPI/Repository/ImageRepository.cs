using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class ImageRepository(ApplicationDBContext context) : IImageRepository
{
    public async Task<Image> UploadImage(Image image)
    {
        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
        return image;
    }
}