using System;
using BitCode.Users;

namespace TFBGames
{
	public interface IAccountMonitor : IService
	{
		ILocalAccount InitialAccount { get; }

		event Action<ILocalAccount> AccountChanged;

		void OnSelectedActiveAccount(ILocalAccount activeAccount);
	}
}
