using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace keep.Models
{
    public class CheckListItem
    {
        [Key]
        public int CheckListID { get; set; }
        public string CheckListText { get; set; }
    }
}
