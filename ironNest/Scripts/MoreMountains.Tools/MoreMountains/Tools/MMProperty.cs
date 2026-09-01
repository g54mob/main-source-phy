using System;
using System.Reflection;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMProperty
	{
		public enum MemberTypes
		{
			Property = 0,
			Field = 1
		}

		public Component TargetComponent;

		public ScriptableObject TargetScriptableObject;

		public MemberTypes MemberType;

		public PropertyInfo MemberPropertyInfo;

		public FieldInfo MemberFieldInfo;

		public Type PropertyType;

		public string MemberName;

		public MMProperty(Component targetComponent, MemberTypes type, PropertyInfo propertyInfo, FieldInfo fieldInfo, string memberName, ScriptableObject targetScriptable)
		{
		}

		public static MMProperty FindProperty(string propertyName, Component targetComponent, GameObject source, ScriptableObject scriptable)
		{
			return null;
		}
	}
}
