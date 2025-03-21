using Microsoft.AspNetCore.Mvc;
using TodoApi.Model;
using TodoApi.Repositories;


namespace TodoApi.Controllers
{
	[Route("api/[controller]")] //Ez az attribútum mondja meg az API-nak, hogy milyen URL alatt lesz elérhető az adott Controller. A [controller] helyére az osztály neve kerül "Controller" nélkül. pl: /api/todo
	[ApiController] //Ha nincs rajta, akkor a validációkat és a request kezelést neked kellene egyenként megírnod.
	public class UserController: ControllerBase
	{
		private readonly UserRepository repository;

		public UserController(UserRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<User>> GetUsers()
		{
			return Ok(this.repository.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult<IEnumerable<User>> GetUserById(int id)
		{
			var user = repository.GetById(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] User user)
		{
			this.repository.Add(user);
			return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
		}
	}
}
