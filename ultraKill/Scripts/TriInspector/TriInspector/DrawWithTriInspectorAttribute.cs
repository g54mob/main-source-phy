using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
	[Conditional("UNITY_EDITOR")]
	public class DrawWithTriInspectorAttribute : Attribute
	{
	}
}
