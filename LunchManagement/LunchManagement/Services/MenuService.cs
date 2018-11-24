using LunchManagement.Helper;
using LunchManagement.Models;
using LunchManagement.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private IHostingEnvironment _hostingEnvironment;
        public MenuService(IMenuRepository menuRepository, IHostingEnvironment hosting) : base(menuRepository)
        {
            this.menuRepository = menuRepository;
            this._hostingEnvironment = hosting;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (this.CheckIfImageFile(file))
            {
                var result = ProcessUploadImage(file);

                var relativeUri = new Uri(result);

                if (string.IsNullOrEmpty(result))
                {
                    return "upload failed";
                }
                var menu = new Menu
                {
                    MenuImage = relativeUri.AbsolutePath
                };
                await this.Add(menu);
                return "upload successfully";
            }

            return "Invalid image file";
        }

        private string ProcessUploadImage(IFormFile file)
        {
            try
            {
                string folderName = "images";
                string webRootPath = Directory.GetCurrentDirectory();
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return fullPath;
                }
                return newPath;
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
