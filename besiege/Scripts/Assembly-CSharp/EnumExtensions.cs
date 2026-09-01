using System;

public static class EnumExtensions
{
	private class Value
	{
		public readonly long? Signed;

		private static readonly Type _uInt64 = typeof(ulong);

		private static readonly Type _uInt32 = typeof(long);

		public ulong? Unsigned { get; set; }

		public Value(object value, Type type)
		{
			if (!type.IsEnum)
			{
				throw new ArgumentException("Value provided is not an enumerated type!");
			}
			Type underlyingType = Enum.GetUnderlyingType(type);
			if (underlyingType == _uInt32 || underlyingType == _uInt64)
			{
				Unsigned = Convert.ToUInt64(value);
			}
			else
			{
				Signed = Convert.ToInt64(value);
			}
		}
	}

	public static T Include<T>(this Enum value, T append)
	{
		Type type = value.GetType();
		object obj = value;
		Value value2 = new Value(append, type);
		if (value2.Signed.HasValue)
		{
			obj = Convert.ToInt64(value) | value2.Signed.Value;
		}
		return (T)Enum.Parse(type, obj.ToString());
	}

	public static T Remove<T>(this Enum value, T remove)
	{
		Type type = value.GetType();
		object obj = value;
		Value value2 = new Value(remove, type);
		if (value2.Signed.HasValue)
		{
			obj = Convert.ToInt64(value) & ~value2.Signed.Value;
		}
		return (T)Enum.Parse(type, obj.ToString());
	}

	public static bool Has<T>(this Enum value, T check)
	{
		Type type = value.GetType();
		Value value2 = new Value(check, type);
		return value2.Signed.HasValue && (Convert.ToInt64(value) & value2.Signed.Value) == value2.Signed.Value;
	}

	public static bool Missing<T>(this Enum obj, T value)
	{
		return !obj.Has(value);
	}
}
