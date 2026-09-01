using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kamgam.SettingsGenerator
{
	public abstract class Connection<TValue> : IConnection<TValue>, IConnection, IQualityChangeReceiver
	{
		protected List<Action<TValue>> _onChangedListeners;

		protected TValue lastKnownValue;

		public int Order;

		public event Action<int> QualityChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public abstract TValue Get();

		public virtual TValue GetDefault()
		{
			return default(TValue);
		}

		public virtual int GetOrder()
		{
			return 0;
		}

		public virtual void SetOrder(int order)
		{
		}

		public abstract void Set(TValue value);

		public virtual void NotifyListenersIfChanged(TValue value)
		{
		}

		public void AddChangeListener(Action<TValue> listener)
		{
		}

		public void RemoveChangeListener(Action<TValue> listener)
		{
		}

		public virtual void OnQualityChanged(int qualityLevel)
		{
		}

		public virtual void Destroy()
		{
		}
	}
}
