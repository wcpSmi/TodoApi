﻿
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Model;

namespace TodoApi.Repositories
{
	public class UserRepository
	{
		private readonly TodoDbContext context;

		public UserRepository(TodoDbContext context)
		{
			this.context = context;
		}

		//Ez azt mondja: töltsd be a Users listát úgy, hogy a kapcsolódó Todos elemeket is hozd vele együtt.
		public IEnumerable<User> GetAll() => this.context.Users.Include(u=>u.Todos).ToList();

		public User? GetById(int id) => this.context.Users.Include(t => t.Todos).FirstOrDefault(t => t.Id == id);

		public void Add(User user)
		{
			this.context.Users.Add(user);
			this.context.SaveChanges(); 
		}

		public bool Update(int id,User user)
		{
			var existingUser = this.context.Users.Find(id);
			if (existingUser == null)
			{
				return false;
			}

			existingUser.Name = user.Name;
			existingUser.Email = user.Email;

			this.context.SaveChanges();

			return true;
		}
	}
}


