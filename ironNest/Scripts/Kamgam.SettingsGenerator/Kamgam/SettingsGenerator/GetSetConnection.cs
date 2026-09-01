using System;
using System.Runtime.CompilerServices;

namespace Kamgam.SettingsGenerator
{
	public class GetSetConnection<T> : Connection<T>
	{
		protected T _value;

		public event Func<T> Getter
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

		public event Action<T> Setter
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

		public GetSetConnection(Func<T> getter, Action<T> setter)
		{
		}

		public override T Get()
		{
			return default(T);
		}

		public override void Set(T value)
		{
		}

		public T GetLastKnownValue()
		{
			return default(T);
		}

		public void SetLastKnownValue(T value)
		{
		}
	}
}
