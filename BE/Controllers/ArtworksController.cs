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
        [HttpGet]
        public IEnumerable<Artwork> GetArtworks()
        {
            return _artworkRepository.GetAll().OrderBy(a => a.Name);
        }

        [HttpGet("{id}")]
        public ActionResult<Artwork> GetArtwork(int id)
        {
            Artwork artwork = _artworkRepository.GetBy(id);
            if (artwork == null) return NotFound();
            return artwork;
        }

        [HttpPost]
        public ActionResult<Artwork> PostArtwork(Artwork artwork)
        {
            _artworkRepository.Add(artwork);
            _artworkRepository.SaveChanges();

            return CreatedAtAction(nameof(GetArtwork), new { id = artwork.Id }, artwork);
        }

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