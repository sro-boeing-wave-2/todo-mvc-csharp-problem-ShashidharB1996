using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Models
{
    public class Note
    {
        [BsonId]
        public int ID { get; set; }

        public string Title { get; set; }
        public string PlainText { get; set; }
        public bool PinnedStatus { get; set; } = false;
        
        public List<Label> Labels { get; set; }
        
        public List<CheckListItem> CheckList { get; set; }

    }
}
