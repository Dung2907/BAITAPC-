using Microsoft.EntityFrameworkCore;
using System.Net;
using TranAnhDung.API.Common.Helper;
using TranAnhDung.API.DataAccess.Context;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;

namespace TranAnhDung.API.Services
{
    public class ProductService : IProduct
    {
        private readonly EFDataContext _context;
        private readonly ApplicationSettings _appSettings;

        public ProductService(EFDataContext context, ApplicationSettings applicationSettings)
        {
            _context = context;
            _appSettings = applicationSettings;
        }

        public async Task<Response> Add(Domain.Product product)
{
    try
    {
        var entity = new DataAccess.Entity.Product
        {
            Title = product.Title,
            Slug = product.Slug,
            Summary = product.Summary,
            Description = product.Description,
            Photo = product.Photo,
            Stock = product.Stock,
            Size = product.Size,
            Condition = product.Condition,
            Status = product.Status,
            Price = product.Price,
            Discount = product.Discount,
            IsFeatured = product.IsFeatured,
            CatId = product.CatId,
            ChildCatId = product.ChildCatId,
            BrandId = product.BrandId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new Response
        {
            ProductId = (int)entity.ProductId,
            IsSuccess = true,
            Message = _appSettings.GetConfigurationValue("ProductMessages", "CreateProductSuccess"),
            HttpStatusCode = HttpStatusCode.OK,
        };
    }
    catch (Exception ex)
    {
        return new Response
        {
            IsSuccess = false,
            Message = _appSettings.GetConfigurationValue("ProductMessages", "CreateProductFailure") + " " + ex.Message,
            HttpStatusCode = HttpStatusCode.BadRequest,
        };
    }
}


        public async Task<Response> Update(Domain.Product product)
{
    try
    {
        if (product.ProductId <= 0)
        {
            return new Response
            {
                ProductId = (int)product.ProductId,
                IsSuccess = false,
                Message = _appSettings.GetConfigurationValue("ProductMessages", "ProductNotFound"),
                HttpStatusCode = HttpStatusCode.BadRequest,
            };
        }

        var entity = await _context.Products.FindAsync(product.ProductId);
        if (entity == null)
        {
            return new Response
            {
                ProductId = (int)product.ProductId,
                IsSuccess = false,
                Message = _appSettings.GetConfigurationValue("ProductMessages", "ProductNotFound"),
                HttpStatusCode = HttpStatusCode.BadRequest,
            };
        }

        // Cập nhật thuộc tính của entity
        entity.Title = product.Title;
        entity.Slug = product.Slug;
        entity.Summary = product.Summary;
        entity.Description = product.Description;
        entity.Photo = product.Photo;
        entity.Stock = product.Stock;
        entity.Size = product.Size;
        entity.Condition = product.Condition;
        entity.Status = product.Status;
        entity.Price = product.Price;
        entity.Discount = product.Discount;
        entity.IsFeatured = product.IsFeatured;
        entity.CatId = product.CatId;
        entity.ChildCatId = product.ChildCatId;
        entity.BrandId = product.BrandId;
        entity.CreatedAt = product.CreatedAt;
        entity.UpdatedAt = product.UpdatedAt;

        await _context.SaveChangesAsync();

        return new Response
        {
            ProductId = (int)entity.ProductId,
            IsSuccess = true,
            Message = _appSettings.GetConfigurationValue("ProductMessages", "UpdateProductSuccess"),
            HttpStatusCode = HttpStatusCode.OK,
        };
    }
    catch (Exception ex)
    {
        return new Response
        {
            IsSuccess = false,
            Message = _appSettings.GetConfigurationValue("ProductMessages", "UpdateProductFailure") + " " + ex.Message,
            HttpStatusCode = HttpStatusCode.BadRequest,
        };
    }
}


        public async Task<Response> Delete(long productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("ProductMessages", "ProductNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                var entity = await _context.Products.FindAsync(productId);
                if (entity == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("ProductMessages", "ProductNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    ProductId = (int)productId,
                    Message = _appSettings.GetConfigurationValue("ProductMessages", "DeleteProductSuccess"),
                    HttpStatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("ProductMessages", "DeleteProductFailure") + " " + ex.Message,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        public async Task<List<Domain.Product>> GetAll()
{
    try
    {
        var products = await (from pro in _context.Products
                              join cat in _context.Categories on pro.CatId equals cat.CategoryId into cats
                              from cat in cats.DefaultIfEmpty()
                              select new Domain.Product
                              {
                                  ProductId = pro.ProductId,
                                  Title = pro.Title,
                                  Slug = pro.Slug,
                                  Summary = pro.Summary,
                                  Description = pro.Description,
                                  Photo = pro.Photo,
                                  Stock = pro.Stock,
                                  Size = pro.Size,
                                  Condition = pro.Condition,
                                  Status = pro.Status,
                                  Price = pro.Price,
                                  Discount = pro.Discount,
                                  IsFeatured = pro.IsFeatured,
                                  CatId = pro.CatId,
                                  ChildCatId = pro.ChildCatId,
                                  BrandId = pro.BrandId,
                                  CreatedAt = pro.CreatedAt,
                                  UpdatedAt = pro.UpdatedAt,
                                  Cat = cat != null ? new Domain.Category
                                  {
                                      CategoryId = cat.CategoryId,
                                      Title = cat.Title,
                                      Slug = cat.Slug,
                                      Summary = cat.Summary,
                                      Photo = cat.Photo,
                                      IsParent = cat.IsParent,
                                      ParentId = cat.ParentId,
                                      CreatedAt = cat.CreatedAt,
                                      UpdatedAt = cat.UpdatedAt
                                  } : null
                              })
                              .OrderByDescending(x => x.ProductId)
                              .ToListAsync();

        return products;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.ToString());
    }
}

public async Task<Domain.Product> GetById(long productId)
{
    try
    {
        var product = await (from pro in _context.Products
                             join cat in _context.Categories on pro.CatId equals cat.CategoryId into cats
                             from cat in cats.DefaultIfEmpty()
                             where pro.ProductId == productId
                             select new Domain.Product
                             {
                                 ProductId = pro.ProductId,
                                 Title = pro.Title,
                                 Slug = pro.Slug,
                                 Summary = pro.Summary,
                                 Description = pro.Description,
                                 Photo = pro.Photo,
                                 Stock = pro.Stock,
                                 Size = pro.Size,
                                 Condition = pro.Condition,
                                 Status = pro.Status,
                                 Price = pro.Price,
                                 Discount = pro.Discount,
                                 IsFeatured = pro.IsFeatured,
                                 CatId = pro.CatId,
                                 ChildCatId = pro.ChildCatId,
                                 BrandId = pro.BrandId,
                                 CreatedAt = pro.CreatedAt,
                                 UpdatedAt = pro.UpdatedAt,
                                 Cat = cat != null ? new Domain.Category
                                 {
                                     CategoryId = cat.CategoryId,
                                     Title = cat.Title,
                                     Slug = cat.Slug,
                                     Summary = cat.Summary,
                                     Photo = cat.Photo,
                                     IsParent = cat.IsParent,
                                     ParentId = cat.ParentId,
                                     CreatedAt = cat.CreatedAt,
                                     UpdatedAt = cat.UpdatedAt
                                 } : null
                             })
                             .FirstOrDefaultAsync();

        return product;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.ToString());
    }
}


    }
}
