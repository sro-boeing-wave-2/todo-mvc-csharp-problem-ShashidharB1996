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
using keep.Data;

namespace keep.Services
{
    public class KeepService : IKeepService
    {
        //private IMongoCollection<Note> NoteCollection { get; }

        //public KeepService(string databaseName, string collectionName, string databaseUrl)
        //{
        //    var mongoClient = new MongoClient(databaseUrl);
        //    var mongoDatabase = mongoClient.GetDatabase(databaseName);

        //    NoteCollection = mongoDatabase.GetCollection<Note>(collectionName);
        //}


        private readonly IDataContext _context;
        public KeepService(IDataContext context)
        {
            _context = context;
        }


        public async Task InsertNote(Note note) => await _context.NoteCollection.InsertOneAsync(note);

        public async Task<List<Note>> GetAllNotes()
        {
            var notes = new List<Note>();
            var allDocuments = await _context.NoteCollection.FindAsync(new BsonDocument());

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;
        }

        public async Task<Note> DeleteNote(int id)
        {
            var note = await _context.NoteCollection.FindOneAndDeleteAsync(x => x.ID == id);
            return note;
                       
        }

        public async Task<List<Note>> SearchByTitle(string title)
        {
            var notes = new List<Note>();
            var allDocuments = await _context.NoteCollection.FindAsync(X => X.Title == title);

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;

        }
       
        public async Task<List<Note>> GetByPin(bool pin)
        {
            var notes = new List<Note>();
            var allDocuments = await _context.NoteCollection.FindAsync(X => X.PinnedStatus == pin);

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;
        }


        public async Task UpdateNote(int id, Note note)
        {
            var filter = Builders<Note>.Filter.Eq(x => x.ID, id);
            //var update = Builders<Note>.Update.Set(x => x.Items.Single(p => p.Id.Equals(itemId)).Title, title);
            var update = Builders<Note>.Update.Set(x => x.Title, note.Title).Set(x => x.PlainText, note.PlainText).Set(x => x.PinnedStatus, note.PinnedStatus).Set(x => x.Labels, note.Labels).Set(x => x.CheckList, note.CheckList);
            var result = await _context.NoteCollection.UpdateOneAsync(filter, update);
            //return result;
        }

        public async Task<List<Note>> GetByLabel(string label)
        {
            var notes = new List<Note>();
            var allDocuments = await _context.NoteCollection.FindAsync(t => t.Labels.Any(x => x.LabelText == label));

            await allDocuments.ForEachAsync(doc => notes.Add(doc));

            return notes;
        }

        public async Task<Note> GetByID(int id)
        {
            //var notes = new List<Note>();
            var note = await _context.NoteCollection.Find(t => t.ID == id).FirstOrDefaultAsync();

            return note;
        }
    }
}
