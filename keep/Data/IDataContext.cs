using keep.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Data
{
    public interface IDataContext
    {
        IMongoCollection<Note> NoteCollection { get; }
    }
}
