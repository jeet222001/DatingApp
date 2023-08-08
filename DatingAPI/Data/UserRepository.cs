using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingAPI.DTOs;
using DatingAPI.Interfaces;
using DatingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly DatingAppContext _context;
		private readonly IMapper _mapper;
        public UserRepository(DatingAppContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _context.Users
				.Include(p=>p.Photos)
				.ToListAsync();
		}

		public async Task<MemberDTO> GetMemberAsync(string username)
		{
			return await _context.Users
				.Where(x => x.UserName == username)
				.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
		{
			var users= await _context.Users
				.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
			return users;
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<User> GetUserByNameAsync(string name)
		{
			return await _context.Users
				.Include(p=>p.Photos)
				.SingleOrDefaultAsync(u => u.UserName == name);
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync()>0;
		}

		public void Update(User user)
		{
			 _context.Entry(user).State = EntityState.Modified;
		}
	}
}
