using keep.Contracts;
using keep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;


namespace keep.Services
{
    public class KeepService : IKeepService
    {
        private readonly keepContext _context;
        public KeepService(keepContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllNotes()
        {

            return await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).ToListAsync();
        }

        public async Task<Note> Add(Note note)
        {
            _context.Note.Add(note);
            await _context.SaveChangesAsync();
            return note;

        }

        public async Task<Note> Edit(int id, Note note)
        {
            _context.Note.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }



        public async Task<Note> GetById(int id)
        {
            var result = await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public Task Remove(int id)
        {
            var old = _context.Note.Include(x => x.CheckList).Include(x => x.Labels).FirstOrDefault(z => z.ID == id);
            _context.Note.Remove(old);
            _context.SaveChanges();
            return Task.CompletedTask;
        }



        public async Task<List<Note>> GetByLabel(string label)
        {
            var notes = await _context.Note.Include(s => s.CheckList).Include(s => s.Labels).Where(x => x.Labels.Any(y => y.LabelText == label)).ToListAsync();
            return notes;
            
            //throw new NotImplementedException();
        }

        public async Task<List<Note>> GetByPin(bool pin)
        {

            var notes = await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).Where(x => x.PinnedStatus == pin).ToListAsync();


            return notes;
            //throw new NotImplementedException();
        }

        public async Task<List<Note>> SearchByTitle(string title)
        {
            var notes = await _context.Note.Include(s => s.CheckList).Include(s => s.Labels).Where(x => x.Title != null).Where(y => y.Title == title).ToListAsync();

            return notes;
            //throw new NotImplementedException();
        }


        private bool IsNoteExists(int id)
        {
            return _context.Note.Any(e => e.ID == id);
        }

    }
}
