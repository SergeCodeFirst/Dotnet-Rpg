using System;
using AutoMapper;
using backend.Models;
using backend.Dtos.Character;

namespace backend
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Character, GetCharacterDto>();
			CreateMap<AddCharacterDto, Character>();
        }
	}
}

