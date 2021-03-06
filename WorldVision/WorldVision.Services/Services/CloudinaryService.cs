using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Services.Configuration;
using WorldVision.Services.IServices;
using WorldVision.Services.Models;

namespace WorldVision.Services.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IMemoryCache cache;
        private readonly ICloudinaryCredentials _cloudinaryCredentials;

        public CloudinaryService(IMemoryCache memoryCache, ICloudinaryCredentials cloudinaryCredentials)
        {
            cache = memoryCache;
            _cloudinaryCredentials = cloudinaryCredentials;
        }

        public void AddToCache(string url, long imgSize, string fileName, string email, string pageId)
        {
            var imageKey = pageId + email;
            var model = new ReviewImageModel
            {
                ImageName = fileName,
                ImageURL = url,
                ImageSize = imgSize
            };
            if (GetFromCache(pageId, email) == null)
            {
                List<ReviewImageModel> models = new List<ReviewImageModel>();
                models.Add(model);
                cache.Set(imageKey, models, TimeSpan.FromDays(1));
            }
            else
            {
                var models = GetFromCache(pageId, email);
                models.Add(model);
                cache.Set(imageKey, models, TimeSpan.FromDays(1));
            }
        }

        public List<ReviewImageModel> GetFromCache(string pageId, string email)
        {
            var imageKey = pageId + email;
            return cache.Get<List<ReviewImageModel>>(imageKey);
        }

        public void DeleteCacheElement(string pageId, string email, string filename)
        {
            var models = GetFromCache(pageId, email);
            var model = models.FirstOrDefault(x => x.ImageName == filename);
            models.Remove(model);
        }

        public async Task<ImageUploadResult> LoadAsync(string fileName, Stream stream)
        {
            Account account = new Account(_cloudinaryCredentials.Name, _cloudinaryCredentials.Key, _cloudinaryCredentials.Secret);
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream),
            };

            return await cloudinary.UploadAsync(uploadParams);

        }

        public async Task DeleteAsync(string url)
        {
            Account account = new Account(_cloudinaryCredentials.Name, _cloudinaryCredentials.Key, _cloudinaryCredentials.Secret);
            var cloudinary = new Cloudinary(account);

            var publicId = url.Split(new char[] { '/' })
                .Last()
                .Split(new char[] { '.' })
                .First();

            var deleteParams = new DeletionParams(publicId);

            await cloudinary.DestroyAsync(deleteParams);
        }

        public async Task DeleteImageOnUpdateAsync(string filename)
        {
            Account account = new Account(_cloudinaryCredentials.Name, _cloudinaryCredentials.Key, _cloudinaryCredentials.Secret);
            var cloudinary = new Cloudinary(account);

            var publicId = filename.Split(new char[] { '.' }).First();

            var deleteParams = new DeletionParams(publicId);

            await cloudinary.DestroyAsync(deleteParams);
        }
    }
}
