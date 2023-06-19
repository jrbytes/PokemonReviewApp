using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Repository
{
  public class CategoryRepository : ICategoryRepository
  {
    private DataContext _context;

    public CategoryRepository(DataContext context)
    {
      _context = context;
    }

    public bool CategoryExists(int id)
    {
      return _context.Categories.Any(c => c.Id == id);
    }

    public ICollection<Category> GetCategories()
    {
      return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
      return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
    }

    public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
    {
      return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
    }

    public bool UpdateCategory(Category category)
    {
      _context.Update(category);
      return Save();
    }

    public bool Save()
    {
      var saved = _context.SaveChanges();
      return saved > 0 ? true : false;
    }
  }
}