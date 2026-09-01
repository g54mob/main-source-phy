using System;

namespace BitCode.Users
{
	public static class UserAccountPropertyExtensions
	{
		public static bool NeedsLoading<T>(this IUserAccountProperty<T> property)
		{
			if (property.Tracked)
			{
				while (true)
				{
					int num = -440039056;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1275871265)) % 4)
						{
						case 2u:
							break;
						case 3u:
						{
							int num3;
							int num4;
							if (property.Status == UserAccountPropertyStatus.Loaded)
							{
								num3 = -82744206;
								num4 = num3;
							}
							else
							{
								num3 = -1211021809;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -453820612);
							continue;
						}
						case 0u:
							return property.Status != UserAccountPropertyStatus.Loading;
						default:
							goto end_IL_0008;
						}
						break;
					}
					continue;
					end_IL_0008:
					break;
				}
			}
			return false;
		}

		public static void StartTracking<T>(this IUserAccountProperty<T> property, Action<IUserAccount> callback = null)
		{
			property.ValueChanged += callback;
			property.SetTracked(track: true);
		}

		internal static void SetValue<T>(this IUserAccountProperty<T> property, T value)
		{
			((IUserAccountPropertyInternal<T>)property).SetValue(value);
		}

		internal static void SetError<T>(this IUserAccountProperty<T> property, Exception e)
		{
			((IUserAccountPropertyInternal<T>)property).SetError(e);
		}
	}
}
