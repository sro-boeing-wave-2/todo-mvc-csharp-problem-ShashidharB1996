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
using MongoDB.Bson.IO;
using Newtonsoft;
using MongoDB.Bson;

namespace keep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        //KeepService mongoDbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");

        private readonly IKeepService _context;
        public NotesController(IKeepService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongoDbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            var allnotes = await _context.GetAllNotes();

            return Ok(allnotes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            await _context.InsertNote(note);

            //return CreatedAtAction("Get", new { id = note.ID }, note);
            return Ok(note);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
             var note = await _context.DeleteNote(id);


            return Ok(note);
        }

        [HttpGet("getbytitle/{title}")]
        public async Task<IActionResult> GetByTitile([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            var notes = await _context.SearchByTitle(title);

            return Ok(notes);
        }


        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            var note = await _context.GetByID(id);

            return Ok(note);
        }

        [HttpGet("getbypin/{pin}")]
        public async Task<IActionResult> GetByPinnedStatus([FromRoute] bool pin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            var notes = await _context.GetByPin(pin);

            return Ok(notes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            await _context.UpdateNote(id, note);

            return Ok(note);

        }


        [HttpGet("getbylabel/{label}")]
        public async Task<IActionResult> GetByLabel([FromRoute] string label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var mongodbService = new KeepService("NotesDatabase", "Notes", "mongodb://127.0.0.1:27017");
            var notes = await _context.GetByLabel(label);

            return Ok(notes);
        }
    }
}