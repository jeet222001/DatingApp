using System.Security.Claims;

namespace DatingAPI.Extensions
{
	public static class ClaimsPrincipleExtensions
	{
		public static string GetUserName(this ClaimsPrincipal User)
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
