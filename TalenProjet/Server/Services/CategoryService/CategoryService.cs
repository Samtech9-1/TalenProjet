using TalenProjet.Server.Data;

namespace TalenProjet.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategories(Category category)
        {
            category.Editing = category.Isnew = false;
           _context.Categories.Add(category);   
            _context.SaveChanges();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategories(int id)
        {
            Category categorytodel = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categorytodel == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Message = "Cat not found",
                    Success = false,
                };
            }
            categorytodel.Deleted = true;
            _context.SaveChanges();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
        {
            var categories = await _context.Categories
                              .Where(c => !c.Deleted)
                             .ToListAsync();
            var response = new ServiceResponse<List<Category>>
            {
                Data = categories
            };
            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
            var categories = await _context.Categories
                             .Where( c => !c.Deleted && c.Visible)
                            .ToListAsync();
            var response = new ServiceResponse<List<Category>>
            {
                Data = categories
            };
            return response;
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategories(Category category)
        {
            var categorytoupdate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categorytoupdate == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Message = "Cat not found",
                    Success = false,
                };
            }
            categorytoupdate.Name = category.Name;
            categorytoupdate.Url = category.Url;
            categorytoupdate.Visible = category.Visible;
            _context.SaveChanges();
            return await GetAdminCategories();
        }
    }
}
