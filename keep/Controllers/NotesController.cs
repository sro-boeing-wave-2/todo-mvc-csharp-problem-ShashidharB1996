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
        //private readonly keepContext _context;

        private IKeepService _keepService;

        public NotesController(keepContext _context)
        {
            _keepService = new KeepService(_context);
            //_keepService = keepService;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> GetNote()
        {
            var notes = await _keepService.GetAllItems();
            if (notes == null)
            {
                return NotFound();
            }

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

        //[HttpGet]
        //[Route("query")]
        //public async Task<IActionResult> GetByQuery([FromQuery] bool? Ispinned = null, [FromQuery]string title = "", [FromQuery] string labelName = "")
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var notes = await _keepService.GetByQuery(Ispinned, title, labelName);
        //    if (notes.Count() == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(notes);
        //}



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

            return CreatedAtAction("GetNote", new { id = note.ID }, note);
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

        [HttpDelete]
        [Route("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _keepService.RemoveAll();

            return Ok();
        }

    }
}