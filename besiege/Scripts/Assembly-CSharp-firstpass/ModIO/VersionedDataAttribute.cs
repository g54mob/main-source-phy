using System;
using System.Reflection;

namespace ModIO
{
	public class VersionedDataAttribute : Attribute
	{
		public int version;

		public object defaultValue;

		public VersionedDataAttribute(int version, object defaultValue)
		{
			this.version = version;
			this.defaultValue = defaultValue;
		}

		public static T UpdateStructFields<T>(int dataVersion, T dataValues) where T : struct
		{
			object obj = dataValues;
			FieldInfo[] fields = typeof(T).GetFields();
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(VersionedDataAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					VersionedDataAttribute versionedDataAttribute = (VersionedDataAttribute)customAttributes[0];
					if (versionedDataAttribute.version > dataVersion)
					{
						fieldInfo.SetValue(obj, versionedDataAttribute.defaultValue);
					}
				}
			}
			return (T)obj;
		}
	}
}
