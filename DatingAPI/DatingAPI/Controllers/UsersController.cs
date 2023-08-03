using AutoMapper;
using DatingAPI.DTOs;
using DatingAPI.Interfaces;
using DatingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatingAPI.Controllers
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UsersController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
		{
			var users = await _userRepository.GetMembersAsync();

			return Ok(users);
		}

		[HttpGet("{username}")]
		public async Task<ActionResult<MemberDTO>> GetUser(string username)
		{
			return await _userRepository.GetMemberAsync(username);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
		{
			var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var user = await _userRepository.GetUserByNameAsync(username);
			if (user == null) return NotFound();

			_mapper.Map(memberUpdateDTO, user);

			if (await _userRepository.SaveAllAsync()) return NoContent();

			return BadRequest("failed to update the user");
		}

	}
}