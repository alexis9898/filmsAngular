using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using DAL.Data;
using DAL.Repository;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService:IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImageService(IMapper mapper, IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageModel>> GetImage()
        {
            var images = await _imageRepository.GetImages();
            return _mapper.Map<List<ImageModel>>(images);
        }
       
        public async Task<ImageModel> GetOneImage(int imageId)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;
            return _mapper.Map<ImageModel>(image);
        }

        //add
        public async Task<ImageModel> AddImage(ImageModel image)
        {
            //var newCategory = new Category()
            //{
            //    Name = category.Name,
            //    ParentId = category.ParentId,
            //};

            var newImage = _mapper.Map<Image>(image);
            newImage= await _imageRepository.AddImage(newImage);
            image = _mapper.Map<ImageModel>(newImage);
            return image;
        }

        //PUT
        public async Task<ImageModel> UpdateImageAsync(int imageId, ImageModel imageModel)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;

            image.Path = imageModel.Path;
            image.FilmId = imageModel.FilmId;

            await _imageRepository.UpdateImageAsync(image);
            imageModel.Id = imageId;
            return imageModel;
        }

        //patch
        public async Task<ImageModel> UpdateImagePatchAsync(int imageId, JsonPatchDocument imagePatch)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;
            imagePatch.ApplyTo(image);
            await _imageRepository.UpdateImageAsync(image);
            return _mapper.Map<ImageModel>(image);
        }

        //delete
        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return false;
            await _imageRepository.DeleteImageAsync(image);
            return true;
        }

        public async Task RemoveImagesAsync(List<ImageModel> images) //remove file of image amd from data
        {
            for (int i = 0; i < images.Count; i++)
            {
                await RemoveImageFileAsync(images[i].Path);

                await DeleteImageAsync(images[i].Id);
            }
        }
        public async Task RemoveImageFileAsync(string path) //remove file of image amd from data
        {

            var dirProject = Directory.GetCurrentDirectory();
            string directoryName = Path.GetDirectoryName(dirProject);  //dir folder of this project
            directoryName = Path.GetDirectoryName(directoryName);  
            var angularFolderName = Path.Combine("films-front", "src", "assets", "films-images");
            var fullPath = Path.Combine(directoryName, angularFolderName, path);
            System.IO.File.Delete(fullPath);
        }






    }
}
