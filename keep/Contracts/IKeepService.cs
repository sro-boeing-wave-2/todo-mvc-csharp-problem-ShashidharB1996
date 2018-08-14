using keep.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Contracts
{
    //Interface for Note
    public interface IKeepService
    {
        Task<List<Note>> GetAllNotes(); //get
        Task InsertNote(Note note); //post
        //Task<Note> GetById(int id);
        void UpdateNote(int id, Note note); //put
        Task DeleteNote(int id); //delete
        //Task<List<Note>> GetByLabel(string label);
        Task<List<Note>> GetByPin(bool pin);
        Task<List<Note>> SearchByTitle(string title);
    }
}
