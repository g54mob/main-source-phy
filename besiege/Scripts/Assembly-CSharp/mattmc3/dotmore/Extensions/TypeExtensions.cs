using System;

namespace mattmc3.dotmore.Extensions
{
	public static class TypeExtensions
	{
		public static bool IsStructure(this Type theType)
		{
			return theType.IsValueType && !theType.IsEnum && !theType.IsPrimitive;
		}

		public static bool IsNullableType(this Type theType)
		{
			if (theType == null)
			{
				return true;
			}
			return theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
		}
	}
}
