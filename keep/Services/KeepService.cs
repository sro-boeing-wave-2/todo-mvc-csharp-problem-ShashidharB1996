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



        public Task<List<Note>> GetAllItems()
        {

            return _context.Note.Include(x => x.ChkList).Include(x => x.Label).ToListAsync();
        }

        public Task<Note> Add(Note note)
        {
            _context.Note.Add(note);
            _context.SaveChanges();
            return Task.FromResult(note);

        }

        public Task Edit(int id, Note note)
        {
            _context.Note.Update(note);
            _context.SaveChanges();
            return Task.CompletedTask;
        }



        public Task<Note> GetById(int id)
        {
            var result = _context.Note.Include(x => x.ChkList).Include(x => x.Label).FirstOrDefault(x => x.ID == id);
            return Task.FromResult(result);
        }

        public Task Remove(int id)
        {
            var present = _context.Note.Include(x => x.ChkList).Include(x => x.Label).FirstOrDefault(z => z.ID == id);
            _context.Note.Remove(present);
            _context.SaveChanges();
            return Task.CompletedTask;

        }

        private bool IsNoteExists(int id)
        {
            return _context.Note.Any(e => e.ID == id);
        }



    }
}
