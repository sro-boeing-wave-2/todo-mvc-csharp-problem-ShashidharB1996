using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string PlainText { get; set; }
        public bool PinnedStatus { get; set; }
        
        public List<Label> Label { get; set; }
        
        public List<CheckList> ChkList { get; set; }

    }
}
