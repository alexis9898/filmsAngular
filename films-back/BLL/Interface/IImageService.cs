using AutoMapper;
using BLL.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageModel>> GetImage();
        Task<ImageModel> GetOneImage(int imageId);
        Task<ImageModel> AddImage(ImageModel image);
        Task<ImageModel> UpdateImageAsync(int imageId, ImageModel imageModel);
        Task<ImageModel> UpdateImagePatchAsync(int imageId, JsonPatchDocument imagePatch);
        Task<bool> DeleteImageAsync(int imageId);
        Task RemoveImagesAsync(List<ImageModel> images); //remove file of image amd from db-data
        Task RemoveImageFileAsync(string path); //remove file of image amd from data




    }
}
