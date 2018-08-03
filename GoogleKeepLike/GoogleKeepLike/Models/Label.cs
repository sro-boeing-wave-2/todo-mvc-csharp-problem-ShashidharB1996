using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepLike.Models
{
    public class Label
    {
        //Foreign Key
        //public int ID { get; set; }
        //public virtual Note Note { get; set; }
        [Key]
        public int LabelID { get; set; }
        public string LabelText { get; set; }
    }
}
