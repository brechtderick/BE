using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class Artwork
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Artist { get; set; }
        public ICollection<Tag> Tags { get; private set; }
        #endregion
        public Artwork()
        {
            Tags = new List<Tag>();
            Created = DateTime.Now;
        }

        public Artwork(string name) : this()
        {
            Name = name;
        }

        public void AddTag(Tag tag) => Tags.Add(tag);

        public Tag GetTag(int id) => Tags.SingleOrDefault(t => t.Id == id);
    }
}
