using System;

namespace BitCode
{
	public interface IPreInitializationWorker
	{
		bool Initialized { get; }

		event Action<IPreInitializationWorker> InitializationComplete;
	}
}
