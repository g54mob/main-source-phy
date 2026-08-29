using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Battlehub
{
	public static class Reflection
	{
		public static string GetAssemblyQualifiedName(Type type)
		{
			return CleanAssemblyName(type.AssemblyQualifiedName);
		}

		public static string CleanAssemblyName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			name = Regex.Replace(name, ", Version=\\d+.\\d+.\\d+.\\d+", string.Empty);
			name = Regex.Replace(name, ", Culture=\\w+", string.Empty);
			name = Regex.Replace(name, ", PublicKeyToken=\\w+", string.Empty);
			return name;
		}

		public static Type GetUnderlyingType(this MemberInfo member)
		{
			return member.MemberType switch
			{
				MemberTypes.Event => ((EventInfo)member).EventHandlerType, 
				MemberTypes.Field => ((FieldInfo)member).FieldType, 
				MemberTypes.Method => ((MethodInfo)member).ReturnType, 
				MemberTypes.Property => ((PropertyInfo)member).PropertyType, 
				_ => throw new ArgumentException("Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"), 
			};
		}

		public static IEnumerable<KeyValuePair<Type, object>> GetTypesWithAttribute(this Assembly assembly, Type attribute)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				object[] customAttributes = type.GetCustomAttributes(attribute, inherit: true);
				if (customAttributes.Length != 0)
				{
					yield return new KeyValuePair<Type, object>(type, customAttributes[0]);
				}
			}
		}

		public static T GetCustomAttribute<T>(Type type, out Type typeWithAttribute) where T : Attribute
		{
			typeWithAttribute = null;
			while (type != null)
			{
				T customAttribute = type.GetCustomAttribute<T>();
				if (customAttribute != null)
				{
					typeWithAttribute = type;
					return customAttribute;
				}
				type = type.BaseType;
			}
			return null;
		}

		public static bool TryConvert(this string input, Type type, out object result)
		{
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null)
				{
					result = converter.ConvertFromString(null, CultureInfo.InvariantCulture, input);
					return true;
				}
				result = GetDefault(type);
				return false;
			}
			catch (NotSupportedException)
			{
				result = GetDefault(type);
				return false;
			}
		}

		public static object GetDefault(Type type)
		{
			if (type == typeof(string))
			{
				return string.Empty;
			}
			if (type.IsValueType())
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		public static bool IsDelegate(Type type)
		{
			return typeof(MulticastDelegate).IsAssignableFrom(type.BaseType);
		}

		public static bool HasParameterlessConstructor(Type type)
		{
			if (!type.IsValueType)
			{
				return type.GetConstructor(Type.EmptyTypes) != null;
			}
			return true;
		}

		public static bool IsScript(this Type type)
		{
			return type.IsSubclassOf(typeof(MonoBehaviour));
		}

		public static PropertyInfo PropertyInfo<T>(string name)
		{
			return typeof(T).GetProperty(name);
		}

		public static FieldInfo FieldInfo<T>(string name)
		{
			return typeof(T).GetField(name);
		}

		public static PropertyInfo[] GetSerializableProperties(this Type type)
		{
			return (from p in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where p.IsSerializable()
				select p).ToArray();
		}

		public static FieldInfo[] GetSerializableFields(this Type type, bool declaredOnly = true)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			if (declaredOnly)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
			}
			return (from f in type.GetFields(bindingFlags)
				where f.IsSerializable()
				select f).ToArray();
		}

		private static bool IsSerializable(this FieldInfo field)
		{
			if (field.IsPublic || field.IsDefined(typeof(SerializeField), inherit: false))
			{
				return !field.IsDefined(typeof(SerializeIgnore), inherit: false);
			}
			return false;
		}

		private static bool IsSerializable(this PropertyInfo property)
		{
			if (property.CanWrite && property.GetSetMethod(nonPublic: true).IsPublic && property.CanRead && property.GetGetMethod(nonPublic: true).IsPublic && !property.IsDefined(typeof(SerializeIgnore), inherit: false))
			{
				return property.GetIndexParameters().Length == 0;
			}
			return false;
		}

		public static Type[] GetAllFromCurrentAssembly()
		{
			return typeof(Reflection).Assembly.GetTypes().ToArray();
		}

		public static Type[] GetAssignableFromTypes(Type type)
		{
			return (from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
				where type.IsAssignableFrom(p) && p.IsClass
				select p).ToArray();
		}

		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		public static bool IsArray(this Type type)
		{
			return type.IsArray;
		}

		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		public static T GetValue<T>(this FieldInfo fieldInfo, object obj)
		{
			return (T)fieldInfo.GetValue(obj);
		}
	}
}
