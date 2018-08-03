using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepLike.Models
{
    public class CheckList
    {
        //public int ID { get; set; }
        //public virtual Note Note { get; set; }
        [Key]
        public int CheckListID { get; set; }
        public string CheckListText { get; set; }
    }
}
