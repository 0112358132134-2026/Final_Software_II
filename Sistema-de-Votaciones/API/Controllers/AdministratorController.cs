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

        [Route("StartProccess")]
        [HttpPost]
        public async Task<IActionResult> StartProccess(string tableName)
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
                            tableStatus.Status1 = true;

                            _context.Statuses.Update(tableStatus);
                            await _context.SaveChangesAsync();
                        }
                    }

                    return Ok();
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

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }            
        }

        [Route("GetStatistics")]
        [HttpGet]
        public async Task<IEnumerable<SVModel.Candidate>> GetStatistics()
        {
            IEnumerable<SVModel.Candidate> candidates = await
               (from C in _context.Candidates                
                select new SVModel.Candidate
                {
                    Id = C.Id,
                    Dpi = C.Dpi,
                    Name = C.Name,
                    Party = C.Party,
                    Proposal = C.Proposal,
                    TotalVotes = _context.Votes.Count(v => v.CandidateId == C.Id),
                    NullVotes = _context.Votes.Count(v => v.CandidateId == C.Id && v.Vote1 == 0),
                    PositiveVotes = _context.Votes.Count(v => v.CandidateId == C.Id && v.Vote1 == 1),
                    NegativeVotes = _context.Votes.Count(v => v.CandidateId == C.Id && v.Vote1 == 2),
                }).ToListAsync();

            return candidates;
        }
    }
}