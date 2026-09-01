using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Landfall.TABS.Utils
{
	public static class TypeUtils
	{
		public static List<FieldInfo> GetUnitySerializableFields(Type type)
		{
			List<FieldInfo> list = new List<FieldInfo>();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.IsPublic)
				{
					list.Add(fieldInfo);
				}
				else if (fieldInfo.IsPrivate && HasAttribute<SerializeField>(fieldInfo))
				{
					list.Add(fieldInfo);
				}
			}
			return list;
		}

		public static bool HasAttribute<T>(FieldInfo field) where T : Attribute
		{
			if (field.GetCustomAttributes<T>().Any())
			{
				return true;
			}
			return false;
		}
	}
}
