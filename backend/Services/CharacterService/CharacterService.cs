using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos.Character;

namespace backend.Services.CharacterService
{
	public class CharacterService : ICharacterService
    { 
        public static List<Character> Characters = new List<Character>
        {
            new Character {characterId = 0, Name = "Sam"},
            new Character {characterId = 1, Name = "Rene"},
            new Character {characterId = 2, Name = "Alex"}
        };

        // Covertign all the record into a Dto
        public List<GetCharacterDto> CharactersDto = Characters.Select(c => new GetCharacterDto
        {
            characterId = c.characterId,
            Name = c.Name,
            HitPoint = c.HitPoint,
            Strength = c.Strength,
            Defense = c.Defense,
            Intelligence = c.Intelligence,
            Class = c.Class
        }).ToList();

        // GET ALL CHARACTERS
        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> GetAllCharacters()
        {
            // Create a New ServiceRespond instance with the data
            var NewResponce = new ServiceResponse<IEnumerable<GetCharacterDto>>();

            NewResponce.Data = CharactersDto;
            //NewResponce.Success = true;
            NewResponce.Message = "These are all they Characters";

            return NewResponce;
        }

        // GET CHARACTER BY ID
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int characterId)
        {
            var charcter = Characters.FirstOrDefault(c => c.characterId == characterId);

            //return charcter;
            //return Characters.FirstOrDefault(c => c.characterId == characterId)!; // Using null forgiving Character (!)

            // Creating a New GetCharacter Dto

            var NewServiceResponse = new ServiceResponse<GetCharacterDto>();

            if (charcter == null)
            {
                NewServiceResponse.Data = null;
                NewServiceResponse.Success = false;
                NewServiceResponse.Message = "Character Not Found!";

                return NewServiceResponse;
            }

            var characterDto = new GetCharacterDto
            {
                Name = charcter.Name,
                HitPoint = charcter.HitPoint,
                Strength = charcter.Strength,
                Defense = charcter.Defense,
                Intelligence = charcter.Intelligence,
            };

            NewServiceResponse.Data = characterDto;
            NewServiceResponse.Message = "This is your Character";

            return NewServiceResponse;
        }

        // CRAETE CHARACTER
        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> AddCharacter([FromBody]AddCharacterDto newCharacter)
        {
            // Make a new instance of GetCharacterDto then add it

            var characterDto = new Character
            {
                //characterId = 4,
                Name = newCharacter.Name,
                HitPoint = newCharacter.HitPoint,
                Strength = newCharacter.Strength,
                Defense = newCharacter.Defense,
                Intelligence = newCharacter.Intelligence,
            };


            Characters.Add(characterDto);


            var CharactersDto = Characters.Select(c => new GetCharacterDto
            {
                characterId = c.characterId,
                Name = c.Name,
                HitPoint = c.HitPoint,
                Strength = c.Strength,
                Defense = c.Defense,
                Intelligence = c.Intelligence,
                Class = c.Class
            }).ToList();

            var NewServiceResponse = new ServiceResponse<IEnumerable<GetCharacterDto>>();

            NewServiceResponse.Data = CharactersDto;
            NewServiceResponse.Message = "Charater Added";

            return NewServiceResponse;
        }
    }
}

