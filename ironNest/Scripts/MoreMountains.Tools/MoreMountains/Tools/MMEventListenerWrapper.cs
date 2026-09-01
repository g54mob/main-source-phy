using System;

namespace MoreMountains.Tools
{
	public class MMEventListenerWrapper<TOwner, TTarget, TEvent> : MMEventListener<TEvent>, MMEventListenerBase, IDisposable where TEvent : struct
	{
		private Action<TTarget> _callback;

		private TOwner _owner;

		public MMEventListenerWrapper(TOwner owner, Action<TTarget> callback)
		{
		}

		public void Dispose()
		{
		}

		protected virtual TTarget OnEvent(TEvent eventType)
		{
			return default(TTarget);
		}

		public void OnMMEvent(TEvent eventType)
		{
		}

		private void RegisterCallbacks(bool b)
		{
		}
	}
}
