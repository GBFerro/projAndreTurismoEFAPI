using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoEF.Data;
using projAndreTurismoEF.Models;

namespace projAndreTurismoEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly projAndreTurismoEFContext _context;

        public PackagesController(projAndreTurismoEFContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            return await _context.Package.Include(p => p.Hotel.Address.City).Include(p => p.Ticket.Departure.City).
                Include(p => p.Ticket.Arrival.City).Include(p => p.Client.Address.City).ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.Include(p => p.Hotel.Address.City).Include(p => p.Ticket.Departure.City).
                Include(p => p.Ticket.Arrival.City).Include(p => p.Client.Address.City).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'projAndreTurismoEFContext.Package'  is null.");
            }

            var hotel = await new HotelsController(_context).GetHotel(package.Hotel.Id);
            if (hotel != null)
                package.Hotel = hotel.Value;

            var ticket = await new TicketsController(_context).GetTicket(package.Ticket.Id);
            if (ticket != null)
                package.Ticket = ticket.Value;

            var client = await new ClientsController(_context).GetClient(package.Client.Id);
            if (client != null)
                package.Client = client.Value;

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
