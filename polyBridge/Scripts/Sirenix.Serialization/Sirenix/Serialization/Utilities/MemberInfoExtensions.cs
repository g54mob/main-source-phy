using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Sirenix.Serialization.Utilities
{
	internal static class MemberInfoExtensions
	{
		public static bool IsDefined<T>(this ICustomAttributeProvider member, bool inherit) where T : Attribute
		{
			try
			{
				return member.IsDefined(typeof(T), inherit);
			}
			catch
			{
				return false;
			}
		}

		public static bool IsDefined<T>(this ICustomAttributeProvider member) where T : Attribute
		{
			return member.IsDefined<T>(inherit: false);
		}

		public static T GetAttribute<T>(this ICustomAttributeProvider member, bool inherit) where T : Attribute
		{
			T[] array = member.GetAttributes<T>(inherit).ToArray();
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		public static T GetAttribute<T>(this ICustomAttributeProvider member) where T : Attribute
		{
			return member.GetAttribute<T>(inherit: false);
		}

		public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider member) where T : Attribute
		{
			return member.GetAttributes<T>(inherit: false);
		}

		public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider member, bool inherit) where T : Attribute
		{
			try
			{
				return member.GetCustomAttributes(typeof(T), inherit).Cast<T>();
			}
			catch
			{
				return new T[0];
			}
		}

		public static Attribute[] GetAttributes(this ICustomAttributeProvider member)
		{
			try
			{
				return member.GetAttributes<Attribute>().ToArray();
			}
			catch
			{
				return new Attribute[0];
			}
		}

		public static Attribute[] GetAttributes(this ICustomAttributeProvider member, bool inherit)
		{
			try
			{
				return member.GetAttributes<Attribute>(inherit).ToArray();
			}
			catch
			{
				return new Attribute[0];
			}
		}

		public static string GetNiceName(this MemberInfo member)
		{
			string input = ((!(member is MethodBase method)) ? member.Name : method.GetFullName());
			return input.ToTitleCase();
		}

		public static bool IsStatic(this MemberInfo member)
		{
			if (member is FieldInfo fieldInfo)
			{
				return fieldInfo.IsStatic;
			}
			if (member is PropertyInfo propertyInfo)
			{
				if (!propertyInfo.CanRead)
				{
					return propertyInfo.GetSetMethod(nonPublic: true).IsStatic;
				}
				return propertyInfo.GetGetMethod(nonPublic: true).IsStatic;
			}
			if (member is MethodBase methodBase)
			{
				return methodBase.IsStatic;
			}
			if (member is EventInfo eventInfo)
			{
				return eventInfo.GetRaiseMethod(nonPublic: true).IsStatic;
			}
			if (member is Type type)
			{
				if (type.IsSealed)
				{
					return type.IsAbstract;
				}
				return false;
			}
			string message = string.Format(CultureInfo.InvariantCulture, "Unable to determine IsStatic for member {0}.{1}MemberType was {2} but only fields, properties, methods, events and types are supported.", new object[3]
			{
				member.DeclaringType.FullName,
				member.Name,
				member.GetType().FullName
			});
			throw new NotSupportedException(message);
		}

		public static bool IsAlias(this MemberInfo memberInfo)
		{
			if (!(memberInfo is MemberAliasFieldInfo) && !(memberInfo is MemberAliasPropertyInfo))
			{
				return memberInfo is MemberAliasMethodInfo;
			}
			return true;
		}

		public static MemberInfo DeAlias(this MemberInfo memberInfo, bool throwOnNotAliased = false)
		{
			if (memberInfo is MemberAliasFieldInfo memberAliasFieldInfo)
			{
				return memberAliasFieldInfo.AliasedField;
			}
			if (memberInfo is MemberAliasPropertyInfo memberAliasPropertyInfo)
			{
				return memberAliasPropertyInfo.AliasedProperty;
			}
			if (memberInfo is MemberAliasMethodInfo memberAliasMethodInfo)
			{
				return memberAliasMethodInfo.AliasedMethod;
			}
			if (throwOnNotAliased)
			{
				throw new ArgumentException("The member " + memberInfo.GetNiceName() + " was not aliased.");
			}
			return memberInfo;
		}
	}
}
