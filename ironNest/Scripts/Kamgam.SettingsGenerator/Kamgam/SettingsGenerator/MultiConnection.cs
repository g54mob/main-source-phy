using System;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class MultiConnection<T> : IConnection<T>, IConnection, IQualityChangeReceiver
	{
		public IConnection<T> DefaultConnection;

		protected List<IConnection<T>> _connections;

		protected List<Action<T>> _changeListeners;

		public void AddConnection(IConnection<T> connection)
		{
		}

		public void RemoveConnection(IConnection<T> connection)
		{
		}

		public void ClearConnections()
		{
		}

		public IConnection<T> GetDefaultConnection()
		{
			return null;
		}

		public T Get()
		{
			return default(T);
		}

		public T GetDefault()
		{
			return default(T);
		}

		public void Set(T value)
		{
		}

		public void AddChangeListener(Action<T> listener)
		{
		}

		public void RemoveChangeListener(Action<T> listener)
		{
		}

		public void OnQualityChanged(int qualityLevel)
		{
		}

		public int GetOrder()
		{
			return 0;
		}

		public void SetOrder(int order)
		{
		}

		public void Destroy()
		{
		}
	}
}
