namespace PokemonReviewApp.Dto
{
  public class ReviewDto
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required int Rating { get; set; }
  }
}