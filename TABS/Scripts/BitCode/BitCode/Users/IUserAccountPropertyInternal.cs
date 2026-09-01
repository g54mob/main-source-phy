using System;

namespace BitCode.Users
{
	internal interface IUserAccountPropertyInternal<in T>
	{
		void SetValue(T val);

		void SetError(Exception e);
	}
}
