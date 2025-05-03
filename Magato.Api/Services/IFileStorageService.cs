public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file);
}
