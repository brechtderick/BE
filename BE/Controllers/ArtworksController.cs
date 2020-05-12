using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.DTOs;
using BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkRepository _artworkRepository;
        public ArtworksController(IArtworkRepository context)
        {
            _artworkRepository = context;
        }

        // GET: api/Artworks
        /// <summary>
        /// Get all artworks ordered by name
        /// </summary>
        /// <returns>array of artworks</returns>
        [HttpGet]
        public IEnumerable<Artwork> GetArtworks(string name =null, string artist = null, string tagName=null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(artist) && string.IsNullOrEmpty(tagName))
                return _artworkRepository.GetAll();
           return _artworkRepository.GetBy(name, artist, tagName);
        }

        // GET: api/Artworks/5
        /// <summary>
        /// Get the artwork with given id
        /// </summary>
        /// <param name="id">the id of the artwork</param>
        /// <returns>The artwork</returns>
        [HttpGet("{id}")]
        public ActionResult<Artwork> GetArtwork(int id)
        {
            Artwork artwork = _artworkRepository.GetBy(id);
            if (artwork == null) return NotFound();
            return artwork;
        }

        // POST: api/Artworks
        /// <summary>
        /// Adds a new artwork
        /// </summary>
        /// <param name="artwork">the new artwork</param>
        [HttpPost]
        public ActionResult<Artwork> PostArtwork(ArtworkDTO artwork)
        {
            Artwork artworkToCreate = new Artwork() { Name = artwork.Name, Artist = artwork.Artist };
            foreach (var t in artwork.Tags)
                artworkToCreate.AddTag(new Tag(t.Name));
            _artworkRepository.Add(artworkToCreate);
            _artworkRepository.SaveChanges();

            return CreatedAtAction(nameof(GetArtwork), new { id = artworkToCreate.Id }, artworkToCreate);
        }

        // PUT: api/Artworks/5
        /// <summary>
        /// Modifies a artwork
        /// </summary>
        /// <param name="id">id of the artwork to be modified</param>
        /// <param name="artwork">the modified artwork</param>
        [HttpPut("{id}")]
        public IActionResult PutArtwork(int id, Artwork artwork)
        {
            if (id != artwork.Id)
            {
                return BadRequest();
            }
            _artworkRepository.Update(artwork);
            _artworkRepository.SaveChanges();
            return NoContent();

        }

        // DELETE: api/Artwork/5
        /// <summary>
        /// Deletes an artwork
        /// </summary>
        /// <param name="id">the id of the artwork to be deleted</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteArtwork( int id)
        {
            Artwork artwork = _artworkRepository.GetBy(id);
            if (artwork == null)
            {
                return NotFound();
            }
            _artworkRepository.Delete(artwork);
            _artworkRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Get a tag for an artwork
        /// </summary>
        /// <param name="id">id of the artwork</param>
        /// <param name="tagId">id of the tag</param>
        [HttpGet("{id}/tags/{tagId}")]
        public ActionResult<Tag> getTag(int id, int tagId)
        {
            if(!_artworkRepository.TryGetArtwork(id, out var artwork))
            {
                return NotFound();
            }
            Tag tag = artwork.GetTag(tagId);
            if (tag == null)
                return NotFound();
            return tag;
        }

        /// <summary>
        /// Adds a tag to an artwork
        /// </summary>
        /// <param name="id">the id of the artwork</param>
        /// <param name="tag">the tag to be added</param>
        [HttpPost("{id}/tags")]
        public ActionResult<Tag> PostTag(int id, TagDTO tag)
        {
            if (!_artworkRepository.TryGetArtwork(id, out var artwork))
            {
                return NotFound();
            }
            var tagToCreate = new Tag(tag.Name);
            artwork.AddTag(tagToCreate);
            _artworkRepository.SaveChanges();
            return CreatedAtAction("GetTag", new { id = artwork.Id, tagId = tagToCreate.Id }, tagToCreate);

        }

    }
}