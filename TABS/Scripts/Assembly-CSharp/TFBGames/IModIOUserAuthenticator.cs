using System.Threading.Tasks;

namespace TFBGames
{
	public interface IModIOUserAuthenticator
	{
		Task AuthenticateUserAsync();
	}
}
