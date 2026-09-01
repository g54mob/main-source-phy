using System;
using System.Reflection;

namespace Poly.Base
{
	public static class ReflectionUtil
	{
		public static PropertyInfo GetAnyProperty(this Type type, string propertyName)
		{
			PropertyInfo propertyInfo = null;
			do
			{
				propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				type = type.BaseType;
			}
			while (null == propertyInfo && null != type);
			return propertyInfo;
		}
	}
}
