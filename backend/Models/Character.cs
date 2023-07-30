﻿using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
	public class Character
	{
		public int characterId { get; set; }
		public string Name { get; set; } = "Frodo"; // string? mean the Name can be nulable.
		public int HitPoint { get; set; } = 100;
		public int Strength { get; set; } = 10;
		public int Defense { get; set; } = 10;
		public int Intelligence { get; set; } = 10;
		public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}

