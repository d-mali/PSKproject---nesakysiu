using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlanningBackend.Models;
using Microsoft.VisualBasic;
using Task = EventPlanningBackend.Models.Task;


namespace EventPlanningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly MainDbContext _context;

        public TaskController(MainDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var evt = await _context.Tasks.FindAsync(id);

            if (evt == null)
            {
                return NotFound();
            }

            return evt;
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task item)
        {
            var evt = await _context.Events.FindAsync(item.EventId);
            if (evt == null)
            {
                return NotFound($"Event with ID {item.EventId} not found.");
            }
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTask), new { Title = item.Title, Information = item.Information}, item);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task evt)
        {
            evt.TaskId = id;

            _context.Entry(evt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.TaskId == id))
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

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var evt = await _context.Tasks.FindAsync(id);

            if (evt == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(evt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

