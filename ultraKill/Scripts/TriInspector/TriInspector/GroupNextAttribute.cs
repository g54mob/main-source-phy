using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class GroupNextAttribute : Attribute
	{
		[CanBeNull]
		public string Path { get; }

		public GroupNextAttribute(string path)
		{
			Path = path;
		}
	}
}
