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
        private List<Note> Notes { get; set; }
        public object GenFu { get; }

        //private readonly keepContext _context;
        //public KeepService(keepContext context)
        //{
        //    _context = context;
        //}



        public KeepService()
        {
            var i = 0;

            Notes = A.ListOf<Note>(50);
            Notes.ForEach(note =>
            {
                i++;
                note.ID = i;
            });
        }

        //private readonly keepContext _context;

        public Task<IEnumerable<Note>> GetAllItems()
        {
            var items = Notes.Select(x => x);
            return Task.FromResult(items);

            //return _context.Note.Include(x => x.ChkList).Include(x => x.Label).ToListAsync();
        }

        public Task<Note> Add(Note note)
        {
            var nextid = Notes.OrderBy(x => x.ID).Last().ID + 1;
            note.ID = nextid;

            Notes.Add(note);

            return Task.FromResult(note);

            //_context.Note.Add(note);
            //_context.SaveChanges();
            //return Task.FromResult(note);


        }

        public Task Edit(int id, Note note)
        {
            var existing = Notes.First(x => x.ID == id);
            existing.PlainText = note.PlainText;
            existing.Title = note.Title;
            existing.PinnedStatus = note.PinnedStatus;
            existing.Label = note.Label;
            existing.ChkList = note.ChkList;

            return Task.CompletedTask;
            //throw new NotImplementedException();

            //_context.Note.Update(note);
            //_context.SaveChanges();
            //return Task.CompletedTask;
        }



        public Task<Note> GetById(int id)
        {
            var items = Notes.First(x => x.ID == id);
            //throw new NotImplementedException();
            return Task.FromResult(items);

            //var result = _context.Note.Include(x => x.ChkList).Include(x => x.Label).First(x => x.ID == id);
            //return Task.FromResult(result);
        }

        public Task Remove(int id)
        {
            var old = Notes.First(x => x.ID == id);
            Notes.Remove(old);
            return Task.CompletedTask;
            //throw new NotImplementedException();

            //var present = _context.Note.Include(x => x.ChkList).Include(x => x.Label).First(z => z.ID == id);
            //_context.Note.Remove(present);
            //_context.SaveChanges();
            //return Task.CompletedTask;

        }

        //public Task RemoveAll()
        //{
        //    var notes = _context.Note.Include(x => x.ChkList).Include(x => x.Label);
        //    _context.Note.RemoveRange(notes);
        //    _context.SaveChanges();
        //    return Task.CompletedTask;
        //}
        //private bool IsNoteExists(int id)
        //{
        //    return _context.Note.Any(e => e.ID == id);
        //}

        //public Task<List<Note>> GetByQuery(bool? PinnedStatus = null, string title = "", string labelName = "")
        //{
        //    var val = _context.Note.Include(x => x.ChkList).Include(x => x.Label).Where(
        //       m => ((title == "") || (m.Title == title)) && ((!PinnedStatus.HasValue) || (m.PinnedStatus == PinnedStatus)) && ((labelName == "") || (m.Label).Any(b => b.LabelText == labelName))).ToList();
        //    return Task.FromResult(val);

        //    //throw new NotImplementedException();
        //}
    }
}
