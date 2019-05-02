using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Voting.Infrastructure.Interfaces;

namespace Voting.Infrastructure
{
    public class PictureUpload : IPictureUpload
    {
            private IHostingEnvironment hosting { get; set; }
            public PictureUpload(IHostingEnvironment hostingEnvironment)
            {
                hosting = hostingEnvironment;
            }
            public async Task<string> UploadAsync(IFormFile formFile)
            {
                var name = HttpUtility.HtmlEncode(formFile.FileName);
                var serverPath = Path.Combine(hosting.WebRootPath, "pictures");
                string[] imageMimetypes = { "image/jpeg", "image/pjpeg", "image/x-png", "image/png" };
                string[] imageExt = { ".jpeg", ".jpg", ".png", };
                var mimetype = formFile.ContentType;
                var extension = Path.GetExtension(name);
                string newName = Guid.NewGuid().ToString().Substring(0, 8) + extension;
                string safeFilePath = Path.Combine(serverPath, newName);
                DirectoryInfo directoryInfo = new DirectoryInfo(serverPath);
                directoryInfo.Create();
                try
                {
                    if (Array.IndexOf(imageMimetypes, mimetype) >= 0 && Array.IndexOf(imageExt, extension) >= 0)
                    {
                        Stream stream = new MemoryStream();
                        await formFile.CopyToAsync(stream);
                        stream.Position = 0;
                        using (var fs = File.Create(safeFilePath))
                        {
                            await stream.CopyToAsync(fs);
                            return Path.Combine("pictures", newName);
                        }
                    }

                }
                catch (Exception) { }
                return string.Empty;

            }
        }
    }
