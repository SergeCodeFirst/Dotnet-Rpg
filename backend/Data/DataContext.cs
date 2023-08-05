using System;
using Microsoft.EntityFrameworkCore;
using backend.Models;


namespace backend.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		// Represent the table in Db
		public DbSet<Character> Characters { get; set; }
		//public DbSet<Character> Characters => Set<Character>();
    }
}

