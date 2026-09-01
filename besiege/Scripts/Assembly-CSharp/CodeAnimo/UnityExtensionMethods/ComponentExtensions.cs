using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CodeAnimo.UnityExtensionMethods
{
	public static class ComponentExtensions
	{
		private static string OnValidateMethodName = "OnValidate";

		public static void ApplyPrefabSettings(this Component targetComponent, GameObject prefab)
		{
			Type type = targetComponent.GetType();
			Component component = prefab.GetComponent(type);
			if (component == null)
			{
				throw new NullReferenceException(string.Concat("Component of type ", type, " is not available on prefab with the name ", prefab.name));
			}
			List<FieldInfo> displayedFields = targetComponent.GetDisplayedFields();
			for (int i = 0; i < displayedFields.Count; i++)
			{
				FieldInfo fieldInfo = displayedFields[i];
				object value = fieldInfo.GetValue(component);
				if (value != null)
				{
					fieldInfo.SetValue(targetComponent, value);
				}
			}
		}

		public static List<FieldInfo> GetDisplayedFields(this Component targetComponent)
		{
			List<FieldInfo> list = new List<FieldInfo>();
			Type type = targetComponent.GetType();
			do
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					if (isFieldVisibleInInspector(fieldInfo))
					{
						list.Add(fieldInfo);
					}
				}
				type = type.BaseType;
			}
			while (type != null);
			return list;
		}

		private static bool isFieldVisibleInInspector(FieldInfo field)
		{
			object[] customAttributes = field.GetCustomAttributes(false);
			bool flag = field.IsPublic;
			if (field.IsStatic)
			{
				return false;
			}
			foreach (object obj in customAttributes)
			{
				if (obj is HideInInspector)
				{
					return false;
				}
				if (!flag && obj is SerializeField)
				{
					flag = true;
				}
			}
			return flag;
		}

		public static void TriggerValidation(this Component targetComponent)
		{
			throw new InvalidOperationException("This method (" + OnValidateMethodName + ") is only available in the Unity Editor");
		}

		public static Component FindPostAssemblyReloadComponent(this Component originalComponent)
		{
			int instanceID = originalComponent.GetInstanceID();
			Type type = originalComponent.GetType();
			Component[] components;
			try
			{
				components = originalComponent.gameObject.GetComponents(type);
			}
			catch (MissingReferenceException innerException)
			{
				throw new NullReferenceException("The component can not be reconstructed. It probably really IS null, not just Unity's kind of null. It can't access its gameObject reference.", innerException);
			}
			for (int i = 0; i < components.Length; i++)
			{
				int instanceID2 = components[i].GetInstanceID();
				if (instanceID2 == instanceID)
				{
					return components[i];
				}
			}
			throw new MissingComponentException("Can't find a component of same type with matching InstanceID");
		}
	}
}
