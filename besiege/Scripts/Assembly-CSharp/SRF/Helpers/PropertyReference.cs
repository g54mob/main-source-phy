using System;
using System.Linq;
using System.Reflection;

namespace SRF.Helpers
{
	public class PropertyReference
	{
		private readonly PropertyInfo _property;

		private readonly object _target;

		public string PropertyName
		{
			get
			{
				return _property.Name;
			}
		}

		public Type PropertyType
		{
			get
			{
				return _property.PropertyType;
			}
		}

		public bool CanRead
		{
			get
			{
				return _property.GetGetMethod() != null;
			}
		}

		public bool CanWrite
		{
			get
			{
				return _property.GetSetMethod() != null;
			}
		}

		public PropertyReference(object target, PropertyInfo property)
		{
			SRDebugUtil.AssertNotNull(target);
			_target = target;
			_property = property;
		}

		public object GetValue()
		{
			if (_property.CanRead)
			{
				return SRReflection.GetPropertyValue(_target, _property);
			}
			return null;
		}

		public void SetValue(object value)
		{
			if (_property.CanWrite)
			{
				SRReflection.SetPropertyValue(_target, _property, value);
				return;
			}
			throw new InvalidOperationException("Can not write to property");
		}

		public T GetAttribute<T>() where T : Attribute
		{
			object obj = _property.GetCustomAttributes(typeof(T), true).FirstOrDefault();
			return obj as T;
		}
	}
}
