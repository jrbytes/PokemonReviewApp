using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using PokemonReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PokemonReviewApp.Repository
{
  public class ReviewerRepository : IReviewerRepository
  {
    private DataContext _context;

    public ReviewerRepository(DataContext context)
    {
      _context = context;
    }

    public Reviewer GetReviewer(int reviewerId)
    {
      return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
    }

    public ICollection<Reviewer> GetReviewers()
    {
      return _context.Reviewers.ToList();
    }

    public ICollection<Review> GetReviewsOfAReviewer(int reviewerId)
    {
      return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
    }

    public bool ReviewerExists(int reviewerId)
    {
      return _context.Reviewers.Any(r => r.Id == reviewerId);
    }
  }
}