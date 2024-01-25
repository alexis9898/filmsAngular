using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDataContext _context;

        public ImageRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return images;
        }

        public async Task<Image> GetOneImage(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            return image;
        }

        //add
        public async Task<Image> AddImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        //PUT
        public async Task UpdateImageAsync(Image image)
        {

            _context.Images.Update(image); //????
            await _context.SaveChangesAsync();
            return;
        }

        //delete
        public async Task DeleteImageAsync(Image image)
        {
            _context.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
