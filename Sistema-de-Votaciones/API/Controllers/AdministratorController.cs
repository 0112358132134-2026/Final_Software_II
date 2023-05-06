using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : Controller
    {
        private readonly VotingsystemContext _context = new();


        [Route("CreateCandidate")]
        [HttpPost]
        public async Task<SVModel.GeneralResult> CreateCandidate(SVModel.Candidate newCandidate) 
        {
            SVModel.GeneralResult generalResult = new()
            {
                Result = false
            };

            try
            {
                if (!_context.Candidates.Any(c => c.Dpi == newCandidate.Dpi))
                {
                    Candidate candidate = new()
                    {
                        Dpi = newCandidate.Dpi,
                        Name = newCandidate.Name,
                        Party = newCandidate.Party,
                        Proposal = newCandidate.Proposal
                    };

                    _context.Candidates.Add(candidate);
                    await _context.SaveChangesAsync();
                    generalResult.Result = true;
                }
            }
            catch (Exception e)
            {
                generalResult.Status = e.Message;
            }
            return generalResult;
        }
    }
}