using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos.Character;
using AutoMapper;

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

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }


        // GET ALL CHARACTERS
        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> GetAllCharacters()
        {
            //// Covertign all the record into a Dto Manually
            //var CharactersDto = Characters.Select(c => new GetCharacterDto
            //{
            //    characterId = c.characterId,
            //    Name = c.Name,
            //    HitPoint = c.HitPoint,
            //    Strength = c.Strength,
            //    Defense = c.Defense,
            //    Intelligence = c.Intelligence,
            //    Class = c.Class
            //}).ToList();

            // Covertign all the record into a Dto using Auto Mapper
            var CharactersDto = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            // Create a New ServiceRespond instance with the data
            var NewResponce = new ServiceResponse<IEnumerable<GetCharacterDto>>();

            NewResponce.Data = CharactersDto;
            //NewResponce.Success = true;
            NewResponce.Message = "These are all the Characters";

            return NewResponce;
        }

        // GET CHARACTER BY ID
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int characterId)
        {
            var charcter = Characters.FirstOrDefault(c => c.characterId == characterId);

            //return charcter;
            //return Characters.FirstOrDefault(c => c.characterId == characterId)!; // Using null forgiving Character (!)

            var NewServiceResponse = new ServiceResponse<GetCharacterDto>();

            if (charcter == null)
            {
                NewServiceResponse.Data = null;
                NewServiceResponse.Success = false;
                NewServiceResponse.Message = "Character Not Found!";

                return NewServiceResponse;
            }

            //// Creating a New GetCharacter Dto Manually
            //var characterDto = new GetCharacterDto
            //{
            //    Name = charcter.Name,
            //    HitPoint = charcter.HitPoint,
            //    Strength = charcter.Strength,
            //    Defense = charcter.Defense,
            //    Intelligence = charcter.Intelligence,
            //};

            // Creating a New GetCharacter Dto Using AutoMapper
            var characterDto = _mapper.Map<GetCharacterDto>(charcter);


            NewServiceResponse.Data = characterDto;
            NewServiceResponse.Message = "This is your Character";

            return NewServiceResponse;
        }

        // CRAETE CHARACTER
        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> AddCharacter([FromBody]AddCharacterDto newCharacter)
        {
            // Make a new instance of GetCharacterDto then add it

            //var characterDto = new Character
            //{
            //    //characterId = 4,
            //    Name = newCharacter.Name,
            //    HitPoint = newCharacter.HitPoint,
            //    Strength = newCharacter.Strength,
            //    Defense = newCharacter.Defense,
            //    Intelligence = newCharacter.Intelligence,
            //};

            var characterDto = _mapper.Map<Character>(newCharacter);
            characterDto.characterId = Characters.Max(c => c.characterId) + 1;
            Characters.Add(characterDto);

            var CharactersDto = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            var NewServiceResponse = new ServiceResponse<IEnumerable<GetCharacterDto>>();
            NewServiceResponse.Data = CharactersDto;
            NewServiceResponse.Message = "Charater Added";

            return NewServiceResponse;
        }

        // UPDATE CHARACTER
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedChar)
        {
            var NewServiceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var existingChar = Characters.FirstOrDefault(c => c.characterId == updatedChar.characterId);
                if (existingChar is null)
                {
                    throw new Exception($"Character with Id {updatedChar.characterId} not found!");
                }

                existingChar.Name = updatedChar.Name;
                existingChar.HitPoint = updatedChar.HitPoint;
                existingChar.Strength = updatedChar.Strength;
                existingChar.Defense = updatedChar.Defense;
                existingChar.Intelligence = updatedChar.Intelligence;

                NewServiceResponse.Data = _mapper.Map<GetCharacterDto>(existingChar);
                NewServiceResponse.Success = true;
                NewServiceResponse.Message = "Character Updated!";

            }
            catch (Exception ex)
            {
                NewServiceResponse.Success = false;
                NewServiceResponse.Message = ex.Message;
            
            }


            //if (existingChar != null)
            //{
            //    existingChar.Name = updatedChar.Name;
            //    existingChar.HitPoint = updatedChar.HitPoint;
            //    existingChar.Strength = updatedChar.Strength;
            //    existingChar.Defense = updatedChar.Defense;
            //    existingChar.Intelligence = updatedChar.Intelligence;

            //    NewServiceResponse.Data = _mapper.Map<GetCharacterDto>(existingChar);
            //    NewServiceResponse.Success = true;
            //    NewServiceResponse.Message = "Character Updated!";

            //    return NewServiceResponse;
            //}

            //NewServiceResponse.Data = null;
            //NewServiceResponse.Success = false;
            //NewServiceResponse.Message = "User not found";

            return NewServiceResponse;
        }

        // DELETE CHARACTER

        public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int characterId)
        {
            var NewServiceResponse = new ServiceResponse<GetCharacterDto>();

            var charaterToDelete = Characters.FirstOrDefault(c => c.characterId == characterId);

            if (charaterToDelete != null)
            {
                Characters.Remove(charaterToDelete);

                NewServiceResponse.Success = true;
                NewServiceResponse.Message = "Character Deleted Successfully!";

                return NewServiceResponse;

            }

            NewServiceResponse.Success = false;
            NewServiceResponse.Message = "Character Not Found!";

            return NewServiceResponse;

        }
    }
}

