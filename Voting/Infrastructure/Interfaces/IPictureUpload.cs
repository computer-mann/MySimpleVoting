using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Voting.Infrastructure.Interfaces
{
  public  interface IPictureUpload
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
