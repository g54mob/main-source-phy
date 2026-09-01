using System;

namespace BitCode.Users
{
	public class UserAccountSyncProperty<T> : UserAccountPropertyBase<T>
	{
		internal UserAccountSyncProperty(string P_0, IUserAccount P_1, Action P_2 = null, Action P_3 = null)
			: base(P_0, P_1, P_2, P_3)
		{
		}

		internal UserAccountSyncProperty(string P_0, IUserAccount P_1, T P_2, Action P_3 = null, Action P_4 = null)
			: base(P_0, P_1, P_3, P_4)
		{
			SetTracked(track: true);
			SetValue(P_2);
		}
	}
}
