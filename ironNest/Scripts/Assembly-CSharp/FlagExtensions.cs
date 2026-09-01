using System;

public static class FlagExtensions
{
	public static bool Has<T>(this T value, T flag) where T : Enum
	{
		return false;
	}

	public static T Add<T>(this T value, T flag) where T : Enum
	{
		return default(T);
	}

	public static T Remove<T>(this T value, T flag) where T : Enum
	{
		return default(T);
	}
}
