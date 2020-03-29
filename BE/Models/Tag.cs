﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Tag(string name)
        {
            Name = name;
        }
    }
}