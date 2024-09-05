using TranAnhDung.API.Common.Helper;
using TranAnhDung.API.DataAccess.Context;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace TranAnhDung.API.Services
{
    public class CategoryService : ICategory
    {
        private readonly EFDataContext _context;
        private readonly ApplicationSettings _appSettings;

        public CategoryService(EFDataContext context, ApplicationSettings applicationSettings)
        {
            _context = context;
            _appSettings = applicationSettings;
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var categories = await _context.Categories
                    .Select(cat => new Category
                    {
                        CategoryId = cat.CategoryId, // Sử dụng Id từ domain model
                        Title = cat.Title,
                        Slug = cat.Slug,
                        Summary = cat.Summary,
                        Photo = cat.Photo,
                        IsParent = cat.IsParent,
                        ParentId = cat.ParentId,
                        AddedBy = cat.AddedBy,
                        Status = cat.Status,
                        CreatedAt = cat.CreatedAt,
                        UpdatedAt = cat.UpdatedAt
                    })
                    .OrderByDescending(x => x.CategoryId)
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<Category> GetById(long CategoryId)
        {
            try
            {
                var category = await _context.Categories
                    .Where(cat => cat.CategoryId == CategoryId)
                    .Select(cat => new Category
                    {
                        CategoryId = cat.CategoryId,
                        Title = cat.Title,
                        Slug = cat.Slug,
                        Summary = cat.Summary,
                        Photo = cat.Photo,
                        IsParent = cat.IsParent,
                        ParentId = cat.ParentId,
                        AddedBy = cat.AddedBy,
                        Status = cat.Status,
                        CreatedAt = cat.CreatedAt,
                        UpdatedAt = cat.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<Response> Add(Category category)
        {
            try
            {
                var entity = new TranAnhDung.API.DataAccess.Entity.Category
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    Summary = category.Summary,
                    Photo = category.Photo,
                    IsParent = category.IsParent,
                    ParentId = category.ParentId,
                    AddedBy = category.AddedBy,
                    Status = category.Status,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt
                };

                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "CreateCategorySuccess"),
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "CreateCategoryFailure") + " " + ex.Message.ToString(),
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<Response> Update(Category category)
        {
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(cat => cat.CategoryId == category.CategoryId);
                if (entity == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("CategoryMessages", "CategoryNotFound"),
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }

                entity.Title = category.Title;
                entity.Slug = category.Slug;
                entity.Summary = category.Summary;
                entity.Photo = category.Photo;
                entity.IsParent = category.IsParent;
                entity.ParentId = category.ParentId;
                entity.AddedBy = category.AddedBy;
                entity.Status = category.Status;
                entity.CreatedAt = category.CreatedAt;
                entity.UpdatedAt = category.UpdatedAt;

                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "UpdateCategorySuccess"),
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "UpdateCategoryFailure") + " " + ex.Message.ToString(),
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<Response> Delete(long CategoryId)
        {
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(cat => cat.CategoryId == CategoryId);
                if (entity == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("CategoryMessages", "CategoryNotFound"),
                        HttpStatusCode = HttpStatusCode.NotFound
                    };
                }

                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "DeleteCategorySuccess"),
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("CategoryMessages", "DeleteCategoryFailure") + " " + ex.Message.ToString(),
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
