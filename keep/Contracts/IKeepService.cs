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
        Task<List<Note>> GetAllItems(); //get
        Task<Note> Add(Note note); //post
        Task<Note> GetById(int id);
        Task Edit(int id, Note note); //put
        Task Remove(int id); //delete
        Task<List<Note>> GetByQuery(bool? PinnedStatus = null, string title = "", string labelName = "");
        //Task RemoveAll();
    }
}
