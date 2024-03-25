using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IImageRepository
{
    Task<Image> UploadImage(Image image);
}