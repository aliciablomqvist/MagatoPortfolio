using Magato.Api.DTO;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magato.Api.Controllers;

    [ApiController]
    [Route("api/blog")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _service;

        public BlogPostController(IBlogPostService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
            => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _service.Get(id);
            return post == null ? NotFound() : Ok(post);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(BlogPostDto dto)
        {
            _service.Add(dto);
            return CreatedAtAction(nameof(Get), new
            {
                id = dto.Id
            }, dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogPostDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            _service.Update(dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }

