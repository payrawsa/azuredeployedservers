using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGateWay.Models;

namespace APIGateWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IteratorsController : ControllerBase
    {
        private readonly IteratorContext _context;

        public IteratorsController(IteratorContext context)
        {
            _context = context;
        }

        // GET: api/Iterators
        [HttpGet]
        public async Task<IEnumerable<Iterator>> GetIteratorItem()
        {
            var results = await _context.Iterator.ToListAsync();
            return results.OrderBy(x => x.Value);
        }

        // GET: api/Iterators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Iterator>> GetIterator(long id)
        {
            var iterator = await _context.Iterator.FindAsync(id);

            if (iterator == null)
            {
                return NotFound();
            }

            return iterator;
        }

        // PUT: api/Iterators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIterator(long id, Iterator iterator)
        {
            if (id != iterator.Id)
            {
                return BadRequest();
            }

            _context.Entry(iterator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IteratorExists(id))
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

        // POST: api/Iterators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<int> PostIterator(Request iterator)
        {
            _context.Database.ExecuteSqlRaw("Truncate Table Iterator");
            var listOfItemsToAdd = new List<Iterator>();
            for (var i = 1; i < iterator.Value; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    listOfItemsToAdd.Add(new Iterator() { Value = i, Text = iterator.FirstWord + iterator.SecondWord });
                }
                else if (i % 3 == 0)
                {
                    listOfItemsToAdd.Add(new Iterator() { Value = i, Text = iterator.SecondWord });
                }
                else if (i % 5 == 0)
                {
                    listOfItemsToAdd.Add(new Iterator() { Value = i, Text = iterator.SecondWord });
                }
                else
                {
                    listOfItemsToAdd.Add(new Iterator() { Value = i, Text = i.ToString() });
                }
            }
            _context.Iterator.AddRange(listOfItemsToAdd);
            return await _context.SaveChangesAsync();
        }

        // DELETE: api/Iterators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Iterator>> DeleteIterator(long id)
        {
            var iterator = await _context.Iterator.FindAsync(id);
            if (iterator == null)
            {
                return NotFound();
            }

            _context.Iterator.Remove(iterator);
            await _context.SaveChangesAsync();

            return iterator;
        }

        private bool IteratorExists(long id)
        {
            return _context.Iterator.Any(e => e.Id == id);
        }
    }
}
