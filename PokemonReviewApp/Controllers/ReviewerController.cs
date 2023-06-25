using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewerController : Controller
  {
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;
    private readonly IReviewRepository _reviewRepository;

    public ReviewerController(
      IReviewerRepository reviewerRepository,
      IMapper mapper,
      IReviewRepository reviewRepository)
    {
      _reviewerRepository = reviewerRepository;
      _mapper = mapper;
      _reviewRepository = reviewRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
    public IActionResult GetReviewers()
    {
      var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return Ok(reviewers);
    }

    [HttpGet("{reviewerId}")]
    [ProducesResponseType(200, Type = typeof(Reviewer))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewer(int reviewerId)
    {
      if (!_reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();

      var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      
      return Ok(reviewer);
    }

    [HttpGet("{reviewerId}/reviews")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewsByAReviewer(int reviewerId)
    {
      if (!_reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();
        
      var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsOfAReviewer(reviewerId));

      if(!ModelState.IsValid)
        return BadRequest();

      return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
    {
      if (reviewerCreate == null)
        return BadRequest(ModelState);

      var reviewers = _reviewerRepository.GetReviewers()
        .Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper())
        .FirstOrDefault();

      if (reviewers != null)
      {
        ModelState.AddModelError("", "Reviewer already exists");
        return StatusCode(422, ModelState);
      }

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

      if (!_reviewerRepository.CreateReviewer(reviewerMap))
      {
        ModelState.AddModelError("", "Something went wrong while savin");
        return StatusCode(500, ModelState);
      }

      return Ok("Successfully created");
    }

    [HttpPut("{reviewerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
    {
      if(updatedReviewer == null)
        return BadRequest(ModelState);

      if(reviewerId != updatedReviewer.Id)
        return BadRequest(ModelState);

      if(!_reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();

      if(!ModelState.IsValid)
        return BadRequest();

      var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

      if(!_reviewerRepository.UpdateReviewer(reviewerMap))
      {
        ModelState.AddModelError("", "Something went wrong updating category");
        return StatusCode(500, ModelState);
      }

      return NoContent();
    }

    [HttpDelete("{reviewerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteReviewer(int reviewerId)
    {
      if(!_reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();

      var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

      if(!ModelState.IsValid)
        return BadRequest(ModelState);

      if(!_reviewerRepository.DeleteReviewer(reviewerToDelete))
        ModelState.AddModelError("", "Something went wrong deleting reviewer");

      return NoContent();
    }
  }
}