﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.DTOs
{
    public class TagDTO
    {
        [Required]
        public string Name { get; set; }
        
    }
}
