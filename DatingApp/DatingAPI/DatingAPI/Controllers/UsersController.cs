using DatingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.Controllers
{
	public class UsersController : BaseApiController
	{
		private readonly DatingAppContext _context;

		public UsersController(DatingAppContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users.ToListAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
			return Ok(user);
		}

	}
}