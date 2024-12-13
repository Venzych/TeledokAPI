using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeledokAPI.Models;

namespace TeledokAPI.Controllers
{
    [Route("/api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ApplicationContext _context;
        public ClientController(ApplicationContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<ActionResult<Client>> Post([FromBody] Client client)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            client.Id = Guid.NewGuid();
            client.DateOfCreation = DateTime.UtcNow;
            client.DateOfModification = DateTime.UtcNow;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), client);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            var clients = await _context.Clients
                .Include(c => c.Founders)
                .ToListAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(Guid id)
        {
            var client = await _context.Clients
                .Include(c => c.Founders)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound(new { Message = $"Client with ID {id} not found." });
            }

            return Ok(client);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Guid id, [FromBody] Client newClient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var client =  await _context.Clients
                .Include(c => c.Founders)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound(new { Message = $"Client with ID {id} not found." });
            }
            
            client.DateOfModification = DateTime.UtcNow;
            client.INN = newClient.INN;
            client.Title = newClient.Title;
            client.Type = newClient.Type;
            if (newClient.Founders != null && newClient.Founders.Any())
            {
                _context.Founders.RemoveRange(newClient.Founders);

                foreach (var founder in newClient.Founders)
                {
                    founder.Id = Guid.NewGuid();
                    founder.DateOfCreation = DateTime.UtcNow;
                    founder.DateOfModification = DateTime.UtcNow;
                }

                client.Founders = newClient.Founders;
            }

            await _context.SaveChangesAsync();
            return Ok(client);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = await _context.Clients
                .Include(c => c.Founders)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound(new { Message = $"Client with ID {id} not found." });
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return Ok(new { Message = $"Client with ID {id} has been deleted." });
        }
    }
}
