using keep.Models;
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
        Task<Note> Add(Note note); //post
        Task<Note> GetById(int id);
        Task<Note> Edit(int id, Note note); //put
        Task Remove(int id); //delete
        Task<List<Note>> GetByLabel(string label);
        Task<List<Note>> GetByPin(bool pin);
        Task<List<Note>> SearchByTitle(string title);
    }
}
