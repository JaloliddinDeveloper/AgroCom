using AgroCom.Brokers.Storages;
using AgroCom.Models.Foundations.Ogits;
using AgroCom.Services.Ogits;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AgroCom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OgitController:RESTFulController
    {
        private readonly IStorageBroker storageBroker;
        private readonly IOgitService ogitService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OgitController(
            IStorageBroker storageBroker,
            IOgitService ogitService, 
            IWebHostEnvironment webHostEnvironment)
        {
            this.storageBroker = storageBroker;
            this.ogitService = ogitService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Ogit>> PostOgitAsync([FromForm] Ogit ogit, IFormFile picture)
        {
            if (picture != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                ogit.OgitPicture = $"images/{fileName}";
            }

            await this.storageBroker.InsertOgitAsync(ogit);

            return Created(ogit);
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Ogit>>> GetAllOgitsAsync()
        {
            try
            {
                IQueryable<Ogit> ogits =
                    await this.storageBroker.SelectAllOgitsAsync();
                return Ok(ogits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ogitId}")]
        public async Task<ActionResult<Ogit>> GetOgitById(int ogitId)
        {
            try
            {
                var ogit = await this.storageBroker.SelectOgitByIdAsync(ogitId);
                return Ok(ogit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOgitAsync(int id, [FromForm] Ogit ogit, IFormFile picture)
        {
            var existingOgit = await this.storageBroker.SelectOgitByIdAsync(id);
            if (existingOgit == null)
            {
                return NotFound(new { Message = $"O'g'it with Id = {id} not found" });
            }

            existingOgit.Name = ogit.Name;
            existingOgit.Description = ogit.Description;
            existingOgit.OgitPicture = ogit.OgitPicture;

            if (picture != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(picture.FileName)}";
                string newFilePath = Path.Combine(uploadsFolder, newFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                if (!string.IsNullOrEmpty(existingOgit.OgitPicture))
                {
                    string oldFilePath = Path.Combine(this.webHostEnvironment.WebRootPath, existingOgit.OgitPicture);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existingOgit.OgitPicture = $"images/{newFileName}";
            }

            await this.storageBroker.UpdateOgitAsync(existingOgit);

            return Ok(new { Message = "Product updated successfully", Ogit = existingOgit });
        }

        [HttpDelete("{ogitId}")]
        public async ValueTask<ActionResult<Ogit>> DeleteOgitByIdAsync(int ogitId)
        {
            try
            {
                return await this.ogitService.RemoveOgitByIdAsync(ogitId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("suyuq")]
        public async ValueTask<ActionResult<IQueryable<Ogit>>> GetAllOgitsSuyuqAsync()
        {
            try
            {
                IQueryable<Ogit> ogits =
                    await this.storageBroker.SelectAllOgitsSuyuqAsync();

                return Ok(ogits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("kukun")]
        public async ValueTask<ActionResult<IQueryable<Ogit>>> GetAllOgitsKukunAsync()
        {
            try
            {
                IQueryable<Ogit> ogits =
                    await this.storageBroker.SelectAllOgitsQuyuqAsync();

                return Ok(ogits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
