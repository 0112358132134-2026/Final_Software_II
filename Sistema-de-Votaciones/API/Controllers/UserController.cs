using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly VotingsystemContext _context = new();

        //[Route("CreateVote")]
        //[HttpPost]
        //public async Task<IActionResult> CreateVote(SVModel.Vote vote)
        //{
        //    try
        //    {
        //        if (_context.Statuses.Any(s => s.TableName == "Vote" && s.Status1))
        //        {
        //            if (!_context.Votes.Any(v => v.PersonId == vote.PersonId))
        //            {
        //                Vote aVote = new()
        //                {
        //                    PersonId = vote.PersonId,
        //                    CandidateId = vote.CandidateId,
        //                    Vote1 = vote.Vote1,
        //                    Date = DateTime.Now,
        //                    IpAddress = vote.IpAddress,
        //                };

        //                _context.Votes.Add(aVote);
        //                await _context.SaveChangesAsync();
        //                return Ok();
        //            }                    
        //        }

        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }            
        //}
    }
}