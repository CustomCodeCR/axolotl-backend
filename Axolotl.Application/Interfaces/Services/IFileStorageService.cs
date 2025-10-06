using Microsoft.AspNetCore.Http;

namespace Axolotl.Application.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> SaveFile(string container, IFormFile file);
    Task<string> EditFile(string container, IFormFile file, string route);
    Task RemoveFile(string route, string container);
}