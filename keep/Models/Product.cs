using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Models
{
    public class Product
    {
        [Required]
        public string Title { get; set; }
        public string PlainText { get; set; }
        public List<string> CheckList { get; set; }
        public string Label { get; set; }
        public bool PinnedStatus { get; set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
