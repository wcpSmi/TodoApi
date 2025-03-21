using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data
{
	public class TodoDbContext: DbContext
	{
		public TodoDbContext(DbContextOptions<TodoDbContext> options) :base(options) { }

		//DbSet<TodoItem> azt jelenti, hogy lesz egy Todos nevű táblánk az adatbázisban.
		//Így kapcsolhatunk egy osztályt a DbSet hez , hogy az létrehozza a áblát
		public DbSet<TodoItem> Todos { get; set; }
		public DbSet<User> Users { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	// Egy User-nek több Todo-ja lehet
		//	modelBuilder.Entity<User>()
		//		.HasMany(u => u.Todos)
		//		.WithOne(t => t.User)
		//		.HasForeignKey(t => t.UserId);
		//}
	}
}
