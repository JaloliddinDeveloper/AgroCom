using AgroCom.Brokers.Storages;
using AgroCom.Models.Foundations.Products;
using AgroCom.Services.Products;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AgroCom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : RESTFulController
    {
        private readonly IStorageBroker storageBroker;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(
            IStorageBroker storageBroker,
            IProductService productService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.storageBroker = storageBroker;
            this.productService = productService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Product>> PostProduct([FromForm] Product product, IFormFile picture)
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

                product.ProductPicture = $"images/{fileName}";
            }

            await this.storageBroker.InsertProductAsync(product);

            return Created(product);
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Product>>> GetAllProductsAsync()
        {
            try
            {
                IQueryable<Product> Products =
                    await this.storageBroker.SelectAllProductsAsync();
                return Ok(Products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            try
            {
                var Product = await this.storageBroker.SelectProductByIdAsync(productId);
                return Ok(Product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] Product Product, IFormFile picture)
        {
            var existingProduct = await this.storageBroker.SelectProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with Id = {id} not found" });
            }

            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.ProductType = Product.ProductType;

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

                if (!string.IsNullOrEmpty(existingProduct.ProductPicture))
                {
                    string oldFilePath = Path.Combine(this.webHostEnvironment.WebRootPath, existingProduct.ProductPicture);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existingProduct.ProductPicture = $"images/{newFileName}";
            }

            await this.storageBroker.UpdateProductAsync(existingProduct);

            return Ok(new { Message = "Product updated successfully", Product = existingProduct });
        }

        [HttpDelete("{productId}")]
        public async ValueTask<ActionResult<Product>> DeleteProductByIdAsync(int productId)
        {
            try
            {
                return await this.productService.RemoveProductByIdAsync(productId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("gerbesetlar")]
        public async ValueTask<ActionResult<IQueryable<Product>>> GetAllProductsGerbesetlarAsync()
        {
            try
            {
                IQueryable<Product> products =
                    await this.storageBroker.SelectAllProductsGerbesetlarAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fungisetlar")]
        public async ValueTask<ActionResult<IQueryable<Product>>> GetAllProductsFungisetlarAsync()
        {
            try
            {
                IQueryable<Product> products =
                    await this.storageBroker.SelectAllProductsFungisetlarAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
