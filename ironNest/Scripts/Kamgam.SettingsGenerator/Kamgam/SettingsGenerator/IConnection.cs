using System;

namespace Kamgam.SettingsGenerator
{
	public interface IConnection : IQualityChangeReceiver
	{
		int GetOrder();

		void SetOrder(int order);
	}
	public interface IConnection<TValue> : IConnection, IQualityChangeReceiver
	{
		TValue Get();

		TValue GetDefault();

		void Set(TValue value);

		void AddChangeListener(Action<TValue> listener);

		void RemoveChangeListener(Action<TValue> listener);

		void Destroy();
	}
}
