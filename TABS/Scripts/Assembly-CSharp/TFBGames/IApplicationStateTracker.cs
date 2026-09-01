using System;

namespace TFBGames
{
	public interface IApplicationStateTracker : IService
	{
		event Action OnApplicationSuspended;
	}
}
