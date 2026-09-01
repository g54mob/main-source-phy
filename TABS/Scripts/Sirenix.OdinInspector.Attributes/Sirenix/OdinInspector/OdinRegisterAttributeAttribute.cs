using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class OdinRegisterAttributeAttribute : Attribute
	{
		public Type AttributeType;

		public string Categories;

		public string Description;

		public string DocumentationUrl;

		public OdinRegisterAttributeAttribute(Type attributeType, string category, string description)
		{
			AttributeType = attributeType;
			Categories = category;
			Description = description;
		}

		public OdinRegisterAttributeAttribute(Type attributeType, string category, string description, string url)
		{
			AttributeType = attributeType;
			Categories = category;
			Description = description;
			DocumentationUrl = url;
		}
	}
}
