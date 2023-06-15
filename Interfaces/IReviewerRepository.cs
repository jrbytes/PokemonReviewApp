using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
  public interface IReviewerRepository
  {
    ICollection<Reviewer> GetReviewers();
    Reviewer GetReviewer(int reviewerId);
    ICollection<Review> GetReviewsOfAReviewer(int reviewerId);
    bool ReviewerExists(int reviewerId);
  }
}