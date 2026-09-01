using System;
using System.Threading.Tasks;

namespace TFBGames
{
	public interface IInternetStatusService : IService
	{
		event Action InternetDisconnected;

		Task<bool> IsConnected(bool connectIfNotConnected);

		bool IsConnectedWithCache(bool connectIfNotConnected);
	}
}
