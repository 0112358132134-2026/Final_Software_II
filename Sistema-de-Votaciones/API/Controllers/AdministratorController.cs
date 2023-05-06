using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : Controller
    {
        private readonly VotingsystemContext _context = new();

        [Route("CreateCandidate")]
        [HttpPost]
        public async Task<IActionResult> CreateCandidate(SVModel.Candidate candidate)
        {
            try
            {
                if (_context.Statuses.Any(s => s.TableName == "Candidate" && s.Status1))
                {
                    if (!_context.Candidates.Any(c => c.Dpi == candidate.Dpi))
                    {
                        Candidate aCandidate = new()
                        {
                            Dpi = candidate.Dpi,
                            Name = candidate.Name,
                            Party = candidate.Party,
                            Proposal = candidate.Proposal
                        };

                        _context.Candidates.Add(aCandidate);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("FinishProcess")]
        [HttpPost]
        public async Task<IActionResult> FinishProcess(string tableName)
        {
            try
            {
                if (_context.Statuses.Any(s => s.TableName.ToLower() == tableName.ToLower()))
                {
                    SVModel.Status? status = await
                        (from s in _context.Statuses
                         select new SVModel.Status
                         {
                             Id = s.Id,
                             TableName = s.TableName
                         }).FirstOrDefaultAsync(s => s.TableName == tableName);

                    if (status != null)
                    {
                        var tableStatus = await _context.Statuses.FindAsync(status.Id);

                        if (tableStatus != null)
                        {
                            tableStatus.Status1 = false;

                            _context.Statuses.Update(tableStatus);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [Route("GetStatistics")]
        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            return null;
        }
    }
}