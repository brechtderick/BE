using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.DTOs
{
    public class ArtworkDTO
    {
        [Required]
        public string Name { get; set; }
        public string Artist { get; set; }
        public IList<TagDTO> Tags { get; set; }
    }
}
