using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> LoadAsync(string fileName, Stream stream);
        public Task DeleteAsync(string url);
        public void AddToCache(string url, long imgSize, string fileName, string email, string pageId);
        public List<ReviewImageModel> GetFromCache(string pageId, string email);
        public void DeleteCacheElement(string pageId, string email, string filename);
        public Task DeleteImageOnUpdateAsync(string filename);
    }
}
