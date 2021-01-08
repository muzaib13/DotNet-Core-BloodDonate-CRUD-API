using CRUD_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DCandidateController : Controller
    {
        private readonly DonationDBContext _context;
        public DCandidateController(DonationDBContext context)
        {
            _context = context;
        }

        // GEt: api/DCandidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetDCandidate() {
            return await _context.DCandidates.ToListAsync();
        }

        // GEt : api/DCandidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetDCandidate(int id) {
            var dCandidate = await _context.DCandidates.FindAsync(id);

            if (dCandidate == null) return NotFound();
            
            return dCandidate;
        
        }

        //PUT: api/DCandidate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDCandidate(int id, DCandidate dCandidate) {
            dCandidate.id = id;
            _context.Entry(dCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                if(!DCandidateExists(id)) return NotFound();
            
            }
            return NoContent();
        }

        // POST: /api/DCandidate
        [HttpPost]
        public async Task<ActionResult<DCandidate>> PostDCandidate(DCandidate dCandidate) {
            _context.DCandidates.Add(dCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDCandidate", new { id = dCandidate.id }, dCandidate);

        }

        // DELETE api/DCandidate
        [HttpDelete("{id}")]
        public async Task<ActionResult<DCandidate>> DeleteCandidate(int id) {
            var dCandidate = await _context.DCandidates.FindAsync(id);
            if (dCandidate == null) return NotFound();

            _context.DCandidates.Remove(dCandidate);
            await _context.SaveChangesAsync();

            return dCandidate;
        }

        private bool DCandidateExists(int id) {
            return _context.DCandidates.Any(e => e.id == id);
        }











    }
}
