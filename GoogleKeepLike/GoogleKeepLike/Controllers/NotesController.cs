using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeepLike.Models;
using System.Data;

namespace GoogleKeepLike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly GoogleKeepLikeContext _context;

        public NotesController(GoogleKeepLikeContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public IEnumerable<Note> GetNote()
        {
            return _context.Note.Include(x => x.ChkList).Include(x => x.Label);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Note.Include(x => x.ChkList).Include(x => x.Label).SingleOrDefaultAsync(x => x.ID == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // GET: api/Notes/pin/true of false
        [HttpGet("getPin/{pin}")]
        public IActionResult GetNoteByPin([FromRoute] bool pin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = _context.Note.Include(x => x.ChkList).Include(x => x.Label).Where(x => x.PinnedStatus == pin);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }


        // GET: api/Notes/getTitle/title
        [HttpGet("getTitle/{title}")]
        public async Task<IActionResult> GetNoteByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Note> list = new List<Note>();
            await _context.Note.Include(x => x.ChkList).Include(x => x.Label).ForEachAsync(x =>
            {
                if (x.Title == title)
                {
                    list.Add(x);
                }
            });

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }


        // GET: api/Notes/getLabel/title
        [HttpGet("getLabel/{label}")]
        public IActionResult GetNoteByLabel([FromRoute] string label)
        {
            //var NotesWithLabel = _context.Note.Include(s => s.ChkList).Include(s => s.Label).Where(x => x.Label != null);
            //return Ok(NotesWithLabel.Where(x => x.Label.Any(y => y.LabelText == label)));

            //var NotesWithLabel = _context.Note.Include(s => s.ChkList).Include(s => s.Label).Where(x => x.Label != null);
            return Ok(_context.Note.Include(s => s.ChkList).Include(s => s.Label).Where(x => x.Label != null).Where(x => x.Label.Any(y => y.LabelText == label)));
        }



        // PUT: api/Notes/edit
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.ID)
            {
                return BadRequest();
            }

            //var Newnote = _context.Note.Include(s => s.ChkList).Include(s => s.Label).Where(x => x.ID == id);
            //_context.Entry(note).State = EntityState.Modified;

            //_context.Note.Remove(Label);

            //_context.Note.Include(x => x.Label).Include(x => x.ChkList).SingleOrDefault(n => n.ID == id);
            _context.Note.Update(note)
                ;
            //_context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNote", new { id = note.ID }, note);
            //return Ok();
        }


        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.ID)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.ID }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Note.Include(x => x.ChkList).Include(x => x.Label).SingleOrDefaultAsync(x => x.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return Ok(note);
        }



        // DELETE: api/Notes/Title
        //[HttpDelete("delete/{title}")]
        //public async Task<IActionResult> DeleteNoteByTitle([FromRoute] string title)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var note = _context.Note.Include(x => x.ChkList).Include(x => x.Label);
        //    List<>
        //    await note.ForEachAsync<Note>(x =>
        //    {
        //        if (x.Title == title)
        //            return x;
        //    }
        //    );
        //    if (note == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Note.Remove(note);
        //    await _context.SaveChangesAsync();
        //    //_context.SaveChanges();
        //    return Ok(note);
        //}

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.ID == id);
        }
    }
}