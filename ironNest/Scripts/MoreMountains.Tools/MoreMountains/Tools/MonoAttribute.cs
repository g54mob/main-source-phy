using System.Reflection;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MonoAttribute
	{
		public enum MemberTypes
		{
			Property = 0,
			Field = 1
		}

		public MonoBehaviour TargetObject;

		public MemberTypes MemberType;

		public PropertyInfo MemberPropertyInfo;

		public FieldInfo MemberFieldInfo;

		public string MemberName;

		public MonoAttribute(MonoBehaviour targetObject, MemberTypes type, PropertyInfo propertyInfo, FieldInfo fieldInfo, string memberName)
		{
		}

		public virtual float GetValue()
		{
			return 0f;
		}

		public virtual void SetValue(float newValue)
		{
		}
	}
}
