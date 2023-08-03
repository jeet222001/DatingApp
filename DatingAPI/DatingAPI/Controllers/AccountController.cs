

using DatingAPI.DTOs;
using DatingAPI.Interfaces;
using DatingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingAPI.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly DatingAppContext _context;
		private readonly ITokenService _tokenService;

		public AccountController(DatingAppContext context, ITokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}

		[HttpPost("register")]

		public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
		{
			if (await UserExists(register.Username))
			{
				return BadRequest("Username is Taken");
			}
			using var hmac = new HMACSHA512();

			var user = new User()
			{
				UserName = register.Username.ToLower(),
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
				PasswordSalt = hmac.Key
			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return Ok(new UserDTO
			{
				UserName = user.UserName,
				Token = _tokenService.CreateToken(user)
			});
		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

			if (user == null) return Unauthorized("Invalid Username");

			using var hmac = new HMACSHA512(user.PasswordSalt);

			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
			}
			return Ok(new UserDTO
			{
				UserName = user.UserName,
				Token = _tokenService.CreateToken(user)
			});

		}

		private async Task<bool> UserExists(string username)
		{
			return await _context.Users.AnyAsync(x => x.UserName == username);
		}
	}
}
