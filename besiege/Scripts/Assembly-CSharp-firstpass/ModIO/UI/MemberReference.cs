using System;
using System.Reflection;
using UnityEngine;

namespace ModIO.UI
{
	[Serializable]
	public struct MemberReference
	{
		[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
		public class DropdownDisplayAttribute : PropertyAttribute
		{
			public Type objectType;

			public bool displayEnumerables;

			public bool displayNested;

			public string[] membersToIgnore;

			public DropdownDisplayAttribute(Type objectType, bool displayEnumerables = false, bool displayNested = false, string[] membersToIgnore = null)
			{
				this.objectType = objectType;
				this.displayEnumerables = displayEnumerables;
				this.displayNested = displayNested;
				this.membersToIgnore = membersToIgnore;
			}
		}

		[SerializeField]
		private string m_memberPath;

		private Func<object, object>[] m_delegateSequence;

		public string MemberPath
		{
			get
			{
				return m_memberPath;
			}
		}

		public MemberReference(string memberPath = null)
		{
			m_memberPath = memberPath;
			m_delegateSequence = null;
		}

		public object GetValue(object objectInstance)
		{
			if (objectInstance == null)
			{
				return null;
			}
			if (m_delegateSequence == null)
			{
				m_delegateSequence = BuildDelegateSequence(objectInstance.GetType(), m_memberPath);
			}
			if (m_delegateSequence.Length > 0)
			{
				object obj = objectInstance;
				for (int i = 0; i < m_delegateSequence.Length; i++)
				{
					if (obj == null)
					{
						break;
					}
					obj = m_delegateSequence[i](obj);
				}
				return obj;
			}
			return null;
		}

		private static Func<object, object>[] BuildDelegateSequence(Type objectType, string memberPath)
		{
			if (string.IsNullOrEmpty(memberPath))
			{
				return new Func<object, object>[0];
			}
			string[] array = memberPath.Split('.');
			Func<object, object>[] array2 = new Func<object, object>[array.Length];
			Type type = objectType;
			for (int i = 0; i < array2.Length; i++)
			{
				if (type == null)
				{
					break;
				}
				MemberInfo[] member = type.GetMember(array[i], BindingFlags.Instance | BindingFlags.Public);
				type = null;
				if (member.Length <= 0 || member[0] == null)
				{
					continue;
				}
				if (member[0] is FieldInfo)
				{
					FieldInfo fieldInfo = (FieldInfo)member[0];
					array2[i] = fieldInfo.GetValue;
					type = fieldInfo.FieldType;
				}
				else if (member[0] is PropertyInfo)
				{
					PropertyInfo pi = (PropertyInfo)member[0];
					array2[i] = (object o) => GetPropertyValue(pi, o);
					type = pi.PropertyType;
				}
			}
			if (type != null)
			{
				return array2;
			}
			return new Func<object, object>[0];
		}

		private static object GetPropertyValue(PropertyInfo info, object objectInstance)
		{
			return info.GetValue(objectInstance, null);
		}
	}
}
