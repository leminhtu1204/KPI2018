using LunchManagement.BlobStorage;
using LunchManagement.Helper;
using LunchManagement.Models;
using LunchManagement.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public class MenuService : GenericService <Menu>, IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IBlobStorageService blobStorageService;
        private IHostingEnvironment _hostingEnvironment;
        public MenuService(IMenuRepository menuRepository, IHostingEnvironment hosting, IBlobStorageService blobStorageService) : base(menuRepository)
        {
            this.menuRepository = menuRepository;
            this._hostingEnvironment = hosting;
            this.blobStorageService = blobStorageService;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (this.CheckIfImageFile(file))
            {
                var result = await ProcessUploadImage(file);

                if (string.IsNullOrEmpty(result))
                {
                    return "upload failed";
                }
                var menu = new Menu
                {
                    MenuImage = result
                };
                await this.Add(menu);
                return "upload successfully";
            }

            return "Invalid image file";
        }

        private async Task<string> UploadAsync(Stream stream, string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await this.blobStorageService.GetCloudBlockBlobAsync(blobName);

            //Upload
            stream.Position = 0;
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<string> ProcessUploadImage(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadedFileUri = await this.UploadAsync(stream, fileName);

                        return uploadedFileUri;
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImageWriter.GetImageFormat(fileBytes) != ImageWriter.ImageFormat.unknown;
        }

        public async Task<IEnumerable<Menu>> GetMenuByCurrentWeek()
        {
            var result = await this.GetAll();

            return result.OrderByDescending(x => x.CreatedDate).Take(2);
        }

        private DateTime GetFirstDayOfWeek(DateTime dateTime)
        {
            var result = dateTime;
            while (result.DayOfWeek != DayOfWeek.Monday)
            {
                result.AddDays(-1);
            }

            return result;
        }
    }
}
