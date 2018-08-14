using keep.Contracts;
using keep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using MongoDB.Driver;
using MongoDB.Bson;
using keep.Contracts;

namespace keep.Services
{
    public class KeepService : IKeepService
    {
        private IMongoCollection<Note> NoteCollection { get; }

        public KeepService(string databaseName, string collectionName, string databaseUrl)
        {
            var mongoClient = new MongoClient(databaseUrl);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            NoteCollection = mongoDatabase.GetCollection<Note>(collectionName);
        }

        public async Task InsertNote(Note note) => await NoteCollection.InsertOneAsync(note);

        public async Task<List<Note>> GetAllNotes()
        {
            var notes = new List<Note>();
            var allDocuments = await NoteCollection.FindAsync(new BsonDocument());

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;
        }

        public async Task DeleteNote(int id)
        {
            await NoteCollection.FindOneAndDeleteAsync(x => x.ID == id);
                       
        }

        public async Task<List<Note>> SearchByTitle(string title)
        {
            var notes = new List<Note>();
            var allDocuments = await NoteCollection.FindAsync(X => X.Title == title);

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;

        }
       
        //public async Task<List<Note>> GetByLabel(string label)
        //{
        //    var notes = new List<Note>();
        //    var allDocuments = await NoteCollection.(x => x.);

        //    await allDocuments.ForEachAsync(doc => notes.Add(doc));

        //    return notes;
        //}

        public async Task<List<Note>> GetByPin(bool pin)
        {
            var notes = new List<Note>();
            var allDocuments = await NoteCollection.FindAsync(X => X.PinnedStatus == pin);

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;
        }


        public async Task UpdateNote(int id, Note note)
        {
            var filter = Builders<Note>.Filter.Eq(x => x.ID, id);
            //var update = Builders<Note>.Update.Set(x => x.Items.Single(p => p.Id.Equals(itemId)).Title, title);
            var update = Builders<Note>.Update.Set(x => x.Title, note.Title).Set(x => x.PlainText, note.PlainText).Set(x => x.PinnedStatus, note.PinnedStatus).Set(x => x.Labels, note.Labels).Set(x => x.CheckList, note.CheckList);
            var result = await NoteCollection.UpdateOneAsync(filter, update);

            //return result;
        }

        //public class KeepService : IKeepService
        //{
        //    private readonly keepContext _context;
        //    public KeepService(keepContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<List<Note>> GetAllNotes()
        //    {

        //        return await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).ToListAsync();
        //    }

        //    public async Task<Note> Add(Note note)
        //    {
        //        _context.Note.Add(note);
        //        await _context.SaveChangesAsync();
        //        return note;

        //    }

        //    public async Task<Note> Edit(int id, Note note)
        //    {
        //        _context.Note.Update(note);
        //        await _context.SaveChangesAsync();
        //        return note;
        //    }



        //    public async Task<Note> GetById(int id)
        //    {
        //        var result = await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).FirstOrDefaultAsync(x => x.ID == id);
        //        return result;
        //    }

        //    public Task Remove(int id)
        //    {
        //        var old = _context.Note.Include(x => x.CheckList).Include(x => x.Labels).FirstOrDefault(z => z.ID == id);
        //        _context.Note.Remove(old);
        //        _context.SaveChanges();
        //        return Task.CompletedTask;
        //    }



        //    public async Task<List<Note>> GetByLabel(string label)
        //    {
        //        var notes = await _context.Note.Include(s => s.CheckList).Include(s => s.Labels).Where(x => x.Labels.Any(y => y.LabelText == label)).ToListAsync();
        //        return notes;

        //        //throw new NotImplementedException();
        //    }

        //    public async Task<List<Note>> GetByPin(bool pin)
        //    {

        //        var notes = await _context.Note.Include(x => x.CheckList).Include(x => x.Labels).Where(x => x.PinnedStatus == pin).ToListAsync();


        //        return notes;
        //        //throw new NotImplementedException();
        //    }

        //    public async Task<List<Note>> SearchByTitle(string title)
        //    {
        //        var notes = await _context.Note.Include(s => s.CheckList).Include(s => s.Labels).Where(x => x.Title != null).Where(y => y.Title == title).ToListAsync();

        //        return notes;
        //        //throw new NotImplementedException();
        //    }


        //    private bool IsNoteExists(int id)
        //    {
        //        return _context.Note.Any(e => e.ID == id);
        //    }

        //}
    }
}
