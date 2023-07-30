using System;
using backend.Models;
using backend.Dtos.Character;

namespace backend.Services.CharacterService
{
	public interface ICharacterService
	{
		Task<ServiceResponse<IEnumerable<GetCharacterDto>>> GetAllCharacters();
		Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int characterId);
		Task<ServiceResponse<IEnumerable<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
	}
}

