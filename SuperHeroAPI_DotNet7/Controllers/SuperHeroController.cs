using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet7.Data;
using SuperHeroAPI_DotNet7.Models;
using SuperHeroAPI_DotNet7.Models.DTO;
using SuperHeroAPI_DotNet7.Services.SuperHeroServices;

namespace SuperHeroAPI_DotNet7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        private readonly IMapper _mapper;

        public SuperHeroController(IMapper mapper, ISuperHeroService superHeroService)
        {
            _mapper = mapper;
            _superHeroService = superHeroService;
        }

        [HttpGet(Name = "GetAllSuperHeroes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            var superHeroes = await _superHeroService.Get();
            if (superHeroes.Count == 0)
            { 
                return NoContent();
            }
            if(superHeroes is null)
            {
                return NotFound("Super heroes database is empty");
            }
            return Ok(superHeroes);
        }

        [HttpGet("{id:int}",Name = "GetSuperHero")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Id can`t be below or equal 0");
            }

            var superHero = await _superHeroService.GetById(id);

            if (superHero is null)
            {
                return NotFound("Super hero dasn`t exist");
            }
            
            return Ok(superHero);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SuperHeroDTO superHeroDTO)
        {
            var isExist = await _superHeroService.IsExist(superHeroDTO.Name);
            if (isExist)
            {
                return BadRequest("Super hero with the same name is already exist");
            }

            var newSuperHero = _mapper.Map<SuperHero>(superHeroDTO);

            await _superHeroService.Create(newSuperHero);

            var createdHero = await _superHeroService.GetLast();

            return CreatedAtRoute("GetSuperHero", new { id = createdHero.Id}, superHeroDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id can`t be below or equal 0");
            }

            var superHero = await _superHeroService.GetById(id);

            if (superHero is null)
            {
                return NotFound("Super hero dasn`t exist");
            }

            await _superHeroService.Delete(superHero);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSuperHero updatedSuperHero)
        {
            if (id <= 0 || id != updatedSuperHero.Id)
            {
                return BadRequest("Id can`t be below or equal 0 or id from body not equal to input id");
            }

            var isExist = await _superHeroService.IsExist(id);

            if (!isExist)
            {
                return NotFound("Super hero dasn`t exist");
            }

            var newSuperHero = _mapper.Map<SuperHero>(updatedSuperHero);

            await _superHeroService.Update(newSuperHero);

            return NoContent();
        }
    }
}
