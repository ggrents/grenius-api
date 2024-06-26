﻿using AutoMapper;
using grenius_api.Application.Extensions;
using grenius_api.Application.Models.Requests;
using grenius_api.Application.Models.Responses;
using grenius_api.Domain.Entities;
using grenius_api.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Swashbuckle.AspNetCore.Annotations;

namespace grenius_api.Application.Controllers
{
    [Authorize]
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController: ControllerBase
    {
        private readonly ILogger<AlbumsController> _logger;
        private readonly GreniusContext _db;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public AlbumsController(GreniusContext db, ILogger<AlbumsController> logger, 
            IMapper mapper, IDistributedCache cache)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }


        [HttpGet]
        [SwaggerOperation(Summary ="Get a list of albums")]
        [SwaggerResponse(200, Type = typeof(List<AlbumResponseDTO>))]
        public async Task<IActionResult> GetAlbums(CancellationToken cancellationToken)
        {
            return Ok(_mapper.Map<List<AlbumResponseDTO>>(await _db.Albums.ToListAsync(cancellationToken)));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get album by id")]
        [SwaggerResponse(200, Type = typeof(AlbumResponseDTO))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAlbumById([SwaggerParameter("Album Id")] int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                _logger.LogWarning("The entered id is less than 1");
                return BadRequest("Id must be greater than 0");
            }

            string cacheKey = $"album_{id}";
            var cachedAlbum = await _cache.GetRecordAsync<AlbumResponseDTO>(cancellationToken, cacheKey);
            if (cachedAlbum != null)
            {
                return Ok(cachedAlbum);
            }
            else
            {

                Album? _album = await _db.Albums.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
                if (_album is null)
                {
                    _logger.LogWarning("No album with this id was found");
                    return NotFound();
                }
                else
                {
                    var responseAlbum = _mapper.Map<AlbumResponseDTO>(_album);
                    await _cache.SetRecordAsync(cancellationToken, cacheKey, responseAlbum);
                    return Ok(responseAlbum);
                }
            }
        }

        [HttpGet("artist/{id}")]
        [SwaggerOperation(Summary = "Get albums by artist id")]
        [SwaggerResponse(200, Type = typeof(List<AlbumResponseDTO>))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAlbumsByArtist([SwaggerParameter("Artist Id")] int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                _logger.LogWarning("The entered id is less than 1");
                return BadRequest("Id must be greater than 0");
            }

            return Ok(_mapper.Map<List<AlbumResponseDTO>>(await _db.Albums.Where(a => a.ArtistId == id).ToListAsync(cancellationToken)));
        }

        
        [HttpPost]
        [SwaggerOperation(Summary = "Add album")]
        [SwaggerResponse(200, Type = typeof(AlbumResponseDTO))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddAlbum([SwaggerRequestBody("Album details")] AlbumRequestDTO model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid request body : @{model}", model);
                return BadRequest("Invalid request body");
            }

            var entity = _db.Albums.Add(new Album
            {
                Title = model.Title,
                ReleaseDate = model.ReleaseDate,
                ArtistId = model.ArtistId,
                AlbumTypeId = (AlbumType) model.AlbumType
            }).Entity;

            await _db.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(AddAlbum), new { Id = entity.Id }, _mapper.Map<AlbumResponseDTO>(entity));
        }

        [Authorize(Roles = "editor,admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Album")]
        [SwaggerResponse(200, Type = typeof(AlbumResponseDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> UpdateAlbum([SwaggerParameter("Album Id")] int id, [SwaggerRequestBody("Album details")] AlbumRequestDTO model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid request body : @{model}", model);
                return BadRequest("Invalid request body");
            }
            var entity = await _db.Albums.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning("No album with this id was found");
                return NotFound();
            }

            entity.Title = model.Title;
            entity.ReleaseDate = model.ReleaseDate;
            entity.ArtistId = model.ArtistId;
            entity.AlbumTypeId = (AlbumType) model.AlbumType;

            await _db.SaveChangesAsync(cancellationToken);
            return Ok(_mapper.Map<AlbumResponseDTO>(entity));

        }

        [Authorize(Roles = "editor,admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove album")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> DeleteAlbum([SwaggerParameter("Album Id")] int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                _logger.LogWarning("Id must be greater than 0");
                return BadRequest("Id must be greater than 0");
            }
            Album? _album = await _db.Albums.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (_album is null)
            {
                _logger.LogWarning("No album with this id was found");
                return NotFound();
            }

            _db.Remove(_album);
            await _db.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

    }
}
