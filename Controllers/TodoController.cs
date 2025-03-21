using Microsoft.AspNetCore.Mvc;
using TodoApi.Model;
using TodoApi.Respositories;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")] //Ez az attribútum mondja meg az API-nak, hogy milyen URL alatt lesz elérhető az adott Controller. A [controller] helyére az osztály neve kerül "Controller" nélkül. pl: /api/todo
	[ApiController] //Ha nincs rajta, akkor a validációkat és a request kezelést neked kellene egyenként megírnod.
	public class TodoController : ControllerBase
	{
		private readonly TodoRepository repository;

		public TodoController(TodoRepository todoRepository) 
		{
			repository= todoRepository;
		}

		[HttpGet] //ez a metódus a GET HTTP kérésre fog reagálni.Ha valaki meghívja a GET /api/todo végpontot, akkor lefut a GetTodos metódus, amely visszaadja az összes feladatot.
		public ActionResult<IEnumerable<TodoItem>> GetTodos()
		{
			return Ok(repository.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult<TodoItem> GetTodoById(int id)
		{
			var todo = repository.GetById(id);
			return todo == null ? NotFound() : Ok(todo);
		}

		[HttpPost]//A POST HTTP kérés arra szolgál, hogy új adatot hozzunk létre az API-n keresztül.
		public IActionResult CreateTodo([FromBody] TodoItem todo)
		{
			repository.Add(todo);
			return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
		{
			return repository.Update(id, updatedTodo) ? NoContent() : NotFound();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteTodo(int id)
		{
			return repository.Delete(id) ? NoContent() : NotFound();
		}
	}
}
