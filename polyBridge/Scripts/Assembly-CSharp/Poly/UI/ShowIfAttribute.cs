using System;
using UnityEngine;

namespace Poly.UI
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class ShowIfAttribute : PropertyAttribute
	{
		public string referenceField = "";

		public bool hideInInspector;

		public bool reverse;

		public string runProperty = "";

		public ShowIfAttribute(string conditionalSourceField = "", bool hideInInspector = false, bool reverse = false, string runProperty = "")
		{
			referenceField = conditionalSourceField;
			this.hideInInspector = hideInInspector;
			this.reverse = reverse;
			this.runProperty = runProperty;
		}
	}
}
