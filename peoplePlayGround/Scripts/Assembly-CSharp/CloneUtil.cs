using System;
using System.Reflection;

public static class CloneUtil<T>
{
	private static readonly Func<T, object> clone;

	static CloneUtil()
	{
		clone = (Func<T, object>)typeof(T).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic).CreateDelegate(typeof(Func<T, object>));
	}

	public static T ShallowClone(T obj)
	{
		return (T)clone(obj);
	}
}
public static class CloneUtil
{
	public static T ShallowClone<T>(this T obj)
	{
		return CloneUtil<T>.ShallowClone(obj);
	}
}
