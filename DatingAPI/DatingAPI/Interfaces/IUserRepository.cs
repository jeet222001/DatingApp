using DatingAPI.DTOs;
using DatingAPI.Models;

namespace DatingAPI.Interfaces
{
	public interface IUserRepository
	{
		void Update(User user);
		Task<bool> SaveAllAsync();
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> GetUserByIdAsync(int id);
		Task<User> GetUserByNameAsync(string name);
		Task<IEnumerable<MemberDTO>> GetMembersAsync();
		Task<MemberDTO> GetMemberAsync(string username);
	}
}
