using System;

namespace BitCode.Users
{
	public class UserAccountAsyncProperty<T> : UserAccountPropertyBase<T>
	{
		internal UserAccountAsyncProperty(string P_0, IUserAccount P_1, Action P_2 = null, Action P_3 = null)
			: base(P_0, P_1, P_2, P_3)
		{
		}
	}
}
