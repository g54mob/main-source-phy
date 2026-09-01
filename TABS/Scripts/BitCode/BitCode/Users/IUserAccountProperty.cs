using System;

namespace BitCode.Users
{
	public interface IUserAccountProperty<T>
	{
		string Name { get; }

		T Value { get; }

		bool Tracked { get; }

		UserAccountPropertyStatus Status { get; set; }

		Exception LastException { get; }

		event Action<IUserAccount> ValueChanged;

		event Action TrackingStarted;

		event Action TrackingStopped;

		void SetTracked(bool track);
	}
}
