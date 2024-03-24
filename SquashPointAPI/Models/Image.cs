namespace SquashPointAPI.Models;

public class Image
{
    public int Id { get; set; }
    public byte[] ImageData { get; set; }
    public string FileExtension { get; set; }
}