namespace PokemonReviewApp.Models
{
  public class Owner
  {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Gym { get; set; }
    public required Country Country { get; set; }
    public ICollection<PokemonOwner>? PokemonOwners { get; set; }
  }
}