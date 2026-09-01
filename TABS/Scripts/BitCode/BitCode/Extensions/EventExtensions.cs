using System;

namespace BitCode.Extensions
{
	public static class EventExtensions
	{
		public static void SafelyInvoke(this Action self)
		{
			try
			{
				self();
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke<T>(this Action<T> self, T obj)
		{
			try
			{
				self(obj);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke<T1, T2>(this Action<T1, T2> self, T1 arg1, T2 arg2)
		{
			try
			{
				self(arg1, arg2);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke<T1, T2, T3>(this Action<T1, T2, T3> self, T1 arg1, T2 arg2, T3 arg3)
		{
			try
			{
				self(arg1, arg2, arg3);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			try
			{
				self(arg1, arg2, arg3, arg4);
			}
			catch (Exception)
			{
			}
		}

		public static TResult SafelyInvoke<TResult>(this Func<TResult> self) where TResult : class
		{
			TResult result = default(TResult);
			try
			{
				result = self();
			}
			catch (Exception)
			{
				while (true)
				{
					IL_000a:
					int num = -817363049;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1406012793)) % 3)
						{
						case 2u:
							break;
						default:
							goto end_IL_000f;
						case 1u:
							goto IL_002c;
						case 0u:
							goto end_IL_000f;
						}
						goto IL_000a;
						IL_002c:
						result = null;
						num = (int)(num2 * 702554303) ^ -1498460384;
						continue;
						end_IL_000f:
						break;
					}
					break;
				}
			}
			return result;
		}

		public static TResult SafelyInvoke<TResult, T>(this Func<T, TResult> self, T obj) where TResult : class
		{
			try
			{
				return self(obj);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static TResult SafelyInvoke<TResult, T1, T2>(this Func<T1, T2, TResult> self, T1 arg1, T2 arg2) where TResult : class
		{
			try
			{
				return self(arg1, arg2);
			}
			catch (Exception)
			{
				TResult result = default(TResult);
				while (true)
				{
					int num = 1851256595;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x7E9D630B)) % 3)
						{
						case 2u:
							break;
						case 1u:
							goto IL_002e;
						default:
							return result;
						}
						break;
						IL_002e:
						result = null;
						num = (int)(num2 * 1554395413) ^ -2058631543;
					}
				}
			}
		}

		public static TResult SafelyInvoke<TResult, T1, T2, T3>(this Func<T1, T2, T3, TResult> self, T1 arg1, T2 arg2, T3 arg3) where TResult : class
		{
			try
			{
				return self(arg1, arg2, arg3);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
