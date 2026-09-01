using System;
using System.Reflection;

public static class Reflection
{
	public static void CopyProperties(this object source, object destination)
	{
		if (source == null || destination == null)
		{
			throw new Exception("Source or/and Destination Objects are null");
		}
		Type type = destination.GetType();
		Type type2 = source.GetType();
		PropertyInfo[] properties = type2.GetProperties();
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			if (propertyInfo.CanRead)
			{
				PropertyInfo property = type.GetProperty(propertyInfo.Name);
				if (property != null && property.CanWrite && (property.GetSetMethod(true) == null || !property.GetSetMethod(true).IsPrivate) && (property.GetSetMethod() == null || (property.GetSetMethod().Attributes & MethodAttributes.Static) == 0) && property.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
				{
					property.SetValue(destination, propertyInfo.GetValue(source, null), null);
				}
			}
		}
	}
}
