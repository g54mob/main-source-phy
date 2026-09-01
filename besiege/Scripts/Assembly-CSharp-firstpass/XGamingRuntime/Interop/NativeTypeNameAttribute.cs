using System;
using System.Diagnostics;

namespace XGamingRuntime.Interop
{
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
	[Conditional("DEBUG")]
	internal sealed class NativeTypeNameAttribute : Attribute
	{
		public string Name { get; private set; }

		public NativeTypeNameAttribute(string name)
		{
			Name = name;
		}
	}
}
