using System;
using System.Reflection;
using UnityEngine;

namespace ModIO.UI
{
	[Serializable]
	public struct GenericTextComponent
	{
		[SerializeField]
		private Component m_textDisplayComponent;

		private Action<Component, string> m_setTextDelegate;

		private Func<Component, string> m_getTextDelegate;

		public Component displayComponent
		{
			get
			{
				return m_textDisplayComponent;
			}
		}

		public string text
		{
			get
			{
				if (m_getTextDelegate == null)
				{
					GenerateDelegates();
				}
				return m_getTextDelegate(m_textDisplayComponent);
			}
			set
			{
				if (m_setTextDelegate == null)
				{
					GenerateDelegates();
				}
				m_setTextDelegate(m_textDisplayComponent, value);
			}
		}

		public static Component FindCompatibleTextComponent(GameObject gameObject)
		{
			Component result = null;
			if (gameObject != null)
			{
				Component[] components = gameObject.GetComponents<Component>();
				Component[] array = components;
				foreach (Component component in array)
				{
					Type type = component.GetType();
					PropertyInfo property = type.GetProperty("text", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
					if (property != null && property.PropertyType == typeof(string) && property.GetGetMethod() != null && property.GetSetMethod() != null)
					{
						result = component;
						break;
					}
				}
			}
			return result;
		}

		public void SetTextDisplayComponent(Component displayComponent)
		{
			if (displayComponent != m_textDisplayComponent)
			{
				m_textDisplayComponent = displayComponent;
				m_setTextDelegate = null;
				m_getTextDelegate = null;
			}
		}

		private void GenerateDelegates()
		{
			PropertyInfo propertyInfo = null;
			if (m_textDisplayComponent != null)
			{
				Type type = m_textDisplayComponent.GetType();
				propertyInfo = type.GetProperty("text", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			}
			if (propertyInfo != null && propertyInfo.PropertyType == typeof(string) && propertyInfo.GetGetMethod() != null && propertyInfo.GetSetMethod() != null)
			{
				m_getTextDelegate = (Component component) => propertyInfo.GetValue(component, null) as string;
				m_setTextDelegate = delegate(Component component, string s)
				{
					propertyInfo.SetValue(component, s, null);
				};
			}
			else
			{
				m_getTextDelegate = (Component component) => (string)null;
				m_setTextDelegate = delegate
				{
				};
			}
		}
	}
}
