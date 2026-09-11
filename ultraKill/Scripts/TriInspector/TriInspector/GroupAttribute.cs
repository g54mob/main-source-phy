using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class GroupAttribute : Attribute
	{
		public string Path { get; }

		public GroupAttribute(string path)
		{
			Path = path;
		}
	}
}
