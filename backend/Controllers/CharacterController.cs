using System;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Xml.Linq;
using backend.Services.CharacterService;
namespace backend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CharacterController : ControllerBase
	{
		// CONSTRUCTOR With: Dependency injection of the Character service Interface
		private readonly ICharacterService _characterService;

        public CharacterController (ICharacterService characterService)
		{
			this._characterService = characterService;
		}


		// GET ALL CHARACTERS
		[HttpGet("all", Name = "GetAllCharacter")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ServiceResponse<IEnumerable<Character>>>> GetAllCharacters()
		{
			return Ok(await _characterService.GetAllCharacters());
		}

		// GET A SINGLE CHARACTER
		[HttpGet("{characterId}", Name ="GetSingleCharacter")]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ServiceResponse<Character>>> GetSingleCharacter(int characterId)
		{
			// If characterId is negative
			if (characterId < 0)
			{
				return BadRequest("Id must be greather that 0");
			}
			var character_db = await _characterService.GetCharacterById(characterId);

			// If character does not exist
			if (character_db.Success == false)
			{
				return NotFound("Character Not found");
			}
			return Ok(character_db);
		}

		//CREATE A CHARACTER
		[HttpPost("newcharacter", Name ="AddNewCharacter")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ServiceResponse<IEnumerable<Character>>>> CreateCharacter([FromBody]Character newCharacter)
		{
			// If data sent is null
			if (newCharacter == null)
			{
				return BadRequest("Add Character Info");
			}

			return Ok(await _characterService.AddCharacter(newCharacter));
		}


	}
}

