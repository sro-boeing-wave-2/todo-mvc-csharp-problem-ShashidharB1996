using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepLike.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string PlainText { get; set; }
        public bool PinnedStatus { get; set; }
        [Required]
        public List<Label> Label { get; set; }
        [Required]
        public List<CheckList> ChkList { get; set; }
    }
}
