using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Infra;

namespace Scheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomsContext _context;
        public RoomsController(RoomsContext roomsContext)
        {
            _context = roomsContext;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var rooms = _context.Rooms;

            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            var roomWithSameName = _context.Rooms.FirstOrDefault(x => x.Name == room.Name);
            if (roomWithSameName is not null)
            {
                return BadRequest("This Name Already Exists!");
            }

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return Created("", room);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Room room)
        {
            if(id != room.Id)
            {
                return BadRequest("Ids must be equal!");
            }

            var savedRoom = _context.Rooms.FirstOrDefault(x => x.Id == room.Id);
            if (savedRoom is null)
            {
                return NotFound();
            }

            savedRoom.Name = room.Name;
            savedRoom.Status = room.Status;

            _context.Rooms.Update(savedRoom);
            _context.SaveChanges();
            return Ok(room);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Update(int id)
        {
            var savedRoom = _context.Rooms.FirstOrDefault(x => x.Id == id);
            if (savedRoom is null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(savedRoom);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
