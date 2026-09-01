using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Landfall.TABS.WinConditions;

namespace TABS.Security
{
	public class TargetTypeSerializationBinder<T> : SerializationBinder
	{
		private static readonly List<Type> m_allTypes = new List<Type>();

		private static readonly List<Type> m_peekedBaseTypes = new List<Type>();

		private static readonly int MAX_RECURSION = 5;

		private static Type[] NULL_EXCEPTIONS = new Type[4]
		{
			typeof(SerializedRuntimeReference),
			typeof(SerializedEvaluator),
			typeof(SerializedWinCondition),
			typeof(List<SerializedWinCondition>)
		};

		public override Type BindToType(string assemblyName, string typeName)
		{
			Type typeFromHandle = typeof(T);
			if (!m_peekedBaseTypes.Contains(typeFromHandle))
			{
				m_allTypes.AddRange(RetrieveAllowedTypes());
				m_peekedBaseTypes.Add(typeFromHandle);
			}
			Type type = m_allTypes.Find((Type t) => t.FullName == typeName);
			if (type == null && !IsExempt(typeName))
			{
				return typeof(object);
			}
			return type;
		}

		private bool IsExempt(string typename)
		{
			for (int i = 0; i < NULL_EXCEPTIONS.Length; i++)
			{
				if (typename == NULL_EXCEPTIONS[i].FullName)
				{
					return true;
				}
			}
			return false;
		}

		private List<Type> RetrieveAllowedTypes()
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = typeof(T);
			list.Add(typeFromHandle);
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int depth = 0;
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				GetTypesRecursiveFromField(list, fieldInfo.FieldType, depth);
			}
			return list;
		}

		private void GetTypesRecursiveFromField(List<Type> types, Type fieldType, int depth)
		{
			if (depth >= MAX_RECURSION)
			{
				return;
			}
			if (!types.Contains(fieldType))
			{
				types.Add(fieldType);
			}
			if (fieldType.IsGenericType)
			{
				Type[] genericArguments = fieldType.GetGenericArguments();
				Type[] array = genericArguments;
				foreach (Type fieldType2 in array)
				{
					GetTypesRecursiveFromField(types, fieldType2, depth++);
				}
				if (!fieldType.IsGenericType)
				{
					return;
				}
				Type genericTypeDefinition = fieldType.GetGenericTypeDefinition();
				if (!(genericTypeDefinition == typeof(Dictionary<, >)))
				{
					return;
				}
				Type item = genericTypeDefinition.MakeGenericType(genericArguments);
				Type item2 = typeof(KeyValuePair<, >).MakeGenericType(genericArguments);
				array = Assembly.GetAssembly(typeof(EqualityComparer<>)).GetTypes();
				foreach (Type type in array)
				{
					if (type.FullName.Contains("System.Collections.Generic.GenericEqualityComparer"))
					{
						Type item3 = type.MakeGenericType(genericArguments[0]);
						types.Add(item3);
					}
					else if (type.FullName.Contains("System.Collections.Generic.InternalStringComparer"))
					{
						types.Add(type);
					}
				}
				types.Add(item);
				types.Add(item2);
			}
			else if (fieldType.IsArray)
			{
				Type elementType = fieldType.GetElementType();
				if (!types.Contains(elementType))
				{
					types.Add(elementType);
					GetTypesRecursiveFromField(types, elementType, depth++);
				}
			}
			else if (!fieldType.IsPrimitive)
			{
				FieldInfo[] fields = fieldType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					GetTypesRecursiveFromField(types, fieldInfo.FieldType, depth++);
				}
			}
		}
	}
}
