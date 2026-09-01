using System;
using System.Reflection;

public abstract class MutableWrapper
{
	public virtual object Value { get; set; }
}
public class MutableWrapper<T> : MutableWrapper
{
	protected Func<T> _get;

	protected Action<T> _set;

	public override object Value
	{
		get
		{
			return TypedValue;
		}
		set
		{
			string value2 = (string)value;
			if (typeof(T) == typeof(string))
			{
				TypedValue = (T)Convert.ChangeType(value2, typeof(T));
			}
			else
			{
				TypedValue = ParseOrDefault(value2);
			}
		}
	}

	public T TypedValue
	{
		get
		{
			return _get();
		}
		set
		{
			_set(value);
		}
	}

	public MutableWrapper(Func<T> get, Action<T> set)
	{
		_get = get;
		_set = set;
	}

	private T ParseOrDefault(string value)
	{
		Type typeFromHandle = typeof(T);
		MethodInfo method = typeFromHandle.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, null, new Type[2]
		{
			typeof(string),
			typeFromHandle.MakeByRefType()
		}, null);
		if (method == null)
		{
			return default(T);
		}
		object[] array = new object[2] { value, null };
		if ((bool)method.Invoke(null, array))
		{
			return (T)array[1];
		}
		return default(T);
	}
}
