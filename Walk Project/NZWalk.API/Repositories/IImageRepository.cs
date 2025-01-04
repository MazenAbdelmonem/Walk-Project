using NZWalk.API.Models.Domin;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Uplode(Image image);
    }
}
