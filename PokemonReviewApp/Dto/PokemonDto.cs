namespace PokemonReviewApp.Dto
{
  public class PokemonDto
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
  }
}