using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using keep.Models;
using keep.Contracts;
using keep.Services;

namespace keep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private IKeepService _keepService;

        public NotesController(keepContext _context)
        {
            _keepService = new KeepService(_context);

        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetNote()
        {
            var notes = await _keepService.GetAllItems();


            return Ok(notes);
        }


        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _keepService.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }





        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutById(int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.ID)
            {
                return NotFound();
            }
            await _keepService.Edit(id, note);

            return Ok(note);
        }


        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newnote = await _keepService.Add(note);

            return CreatedAtAction("GetNote", new { id = newnote.ID }, newnote);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _keepService.Remove(id);
            return Ok();
        }
    }
}