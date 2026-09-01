using System;

namespace MoreMountains.Tools
{
	public struct MMObservable<T>
	{
		public Action OnValueChanged;

		public Action<T> OnValueChangedTo;

		public Action<T, T> OnValueChangedFromTo;

		private T _value;

		public T Value
		{
			get
			{
				return default(T);
			}
			set
			{
			}
		}
	}
}
