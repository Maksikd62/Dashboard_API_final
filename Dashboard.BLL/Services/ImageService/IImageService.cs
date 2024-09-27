using Dashboard.DAL.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Dashboard.BLL.Services.ImageService
{
    public interface IImageService
    {
        Task<ServiceResponse> SaveImageUserAsync(IFormFile image);
        Task<ServiceResponse> SaveImageProductAsync(IFormFile image);

    }
}
