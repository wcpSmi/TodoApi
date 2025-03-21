using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApi.Data;
using TodoApi.Model;

namespace TodoApi.Respositories
{
	public class TodoRepository
	{
		private readonly TodoDbContext context;

		public TodoRepository(TodoDbContext context)
		{
			this.context = context;
		}

		//Ez azt mondja: töltsd be a TodoItem listát úgy, hogy a kapcsolódó user elemeket is hozd vele együtt.
		public IEnumerable<TodoItem> GetAll() =>  this.context.Todos.Include(t => t.User).ToList();

		public TodoItem? GetById(int id) => this.context.Todos.Include(t=>t.User).FirstOrDefault(t=>t.Id== id);


		public void Add(TodoItem item)
		{
			this.context.Todos.Add(item);
			this.context.SaveChanges();
		}

		public bool Update(int id, TodoItem updateItem)
		{
			var existingTodo =this.context.Todos.Find(id);
			if(existingTodo ==null)
			{
				return false;
			}

			existingTodo.Title = updateItem.Title;
			existingTodo.IsCompeted = updateItem.IsCompeted;
			existingTodo.UserId = updateItem.UserId;

			return true;
		}

		public bool Delete(int id)
		{
			var todo = this.context.Todos.Find(id);
			if (todo == null)
			{
				return false;
			}
			this.context.Todos.Remove(todo);
			this.context.SaveChanges();
			return true;
		}
	}
}
