﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kt.api.Models
{
    public class Deck : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime CreationDate
        {
            get;
            set;
        }

        public ICollection<Card> Cards { get; set; }
    }
}