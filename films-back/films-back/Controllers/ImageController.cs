using BLL.Model;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageRepository)
        {
            _imageService = imageRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var images = await _imageService.GetImage();
                return Ok(images);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneImage([FromRoute] int id)
        {
            try
            {
                var image = await _imageService.GetOneImage(id);
                if (image == null)
                    return NotFound();
                return Ok(image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST 
        [HttpPost("")]
        public async Task<IActionResult> AddNewImages([FromBody] List<ImageModel> imagesModel)
        {
            try
            {
                List<ImageModel> sendImages = new List<ImageModel>();
                for (int i = 0; i < imagesModel.Count; i++)
                {
                    var newImage = await _imageService.AddImage(imagesModel[i]);
                    if (newImage == null)
                        continue;
                    sendImages.Add(newImage);
                }
                return Ok(sendImages);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> updateImage([FromRoute] int id, [FromBody] ImageModel imageModel)
        {
            try
            {
                var updateImage = await _imageService.UpdateImageAsync(id, imageModel);
                if (updateImage == null)
                    return NotFound();
                return Ok(updateImage);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //patch
        [HttpPatch("{id}")]
        public async Task<IActionResult> updateImagePatch([FromRoute] int id, [FromBody] JsonPatchDocument imagePatch)
        {
            try
            {
                var updateImage = await _imageService.UpdateImagePatchAsync(id, imagePatch);
                if (updateImage == null)
                    return NotFound();
                return Ok(updateImage);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE 
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                await _imageService.DeleteImageAsync(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("uplaod-image-file")]
        public async Task<IActionResult> Upload()
        {
            // Handle the array of uploaded images here
            List<string> strings = new List<string>();
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var angularFolderName = Path.Combine("films-front", "src", "assets", "films-images");
                //var dirProject = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var dirProject = Directory.GetCurrentDirectory();
                //string lastFolderName = Path.GetFileName(Path.GetDirectoryName(pathToSave));
                string directoryName = Path.GetDirectoryName(dirProject);  //dir folder of this project
                directoryName = Path.GetDirectoryName(directoryName);
                var myUniqueFileName = $@"{DateTime.Now.Ticks}";
                //var myUniqueFileName = string.Format(@"{0}.txt", DateTime.Now.Ticks);


                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse((string)file.ContentDisposition).FileName.Trim('"');
                    string extention = fileName.Split('.')[1]; //type of file: "jpg, ..."
                    var dbPath = myUniqueFileName + "." + extention;
                    var fullPath = Path.Combine(directoryName, angularFolderName, dbPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    //return Ok(dbPath);
                    strings.Add(dbPath);
                }
                else
                {
                    //return BadRequest();
                    continue;
                }

                // Process each image (e.g., save to disk, process, etc.)

            }

            return Ok(strings);
        }

        [HttpPost("delete-images")]
        public async Task<IActionResult> DeleteImagesFile([FromBody] List<ImageModel> images) //remove file of image and from db-data //images film
        {
            try
            {
                for (int i = 0; i < images.Count; i++)
                {
                    await _imageService.RemoveImagesAsync(images);
                }
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("file/{path}")]
        public async Task<IActionResult> DeleteImageFile([FromRoute] string path) //remove file from assest in angular
        {
            try
            {
                await _imageService.RemoveImageFileAsync(path);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
