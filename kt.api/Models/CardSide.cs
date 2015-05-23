using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kt.api.Models
{
    public class CardSide
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}