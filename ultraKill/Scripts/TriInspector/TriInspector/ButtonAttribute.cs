using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("UNITY_EDITOR")]
	public class ButtonAttribute : Attribute
	{
		public string Name { get; set; }

		public int ButtonSize { get; }

		public ButtonAttribute()
		{
		}

		public ButtonAttribute(string name)
		{
			Name = name;
		}

		public ButtonAttribute(ButtonSizes buttonSize, string name = null)
		{
			ButtonSize = (int)buttonSize;
			Name = name;
		}
	}
}
