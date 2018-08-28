using keep.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Data
{

    public class DataContext : IDataContext
    {
        private readonly IMongoDatabase _db;
        public DataContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Note> NoteCollection => _db.GetCollection<Note>("NoteCollection");
    }

}
