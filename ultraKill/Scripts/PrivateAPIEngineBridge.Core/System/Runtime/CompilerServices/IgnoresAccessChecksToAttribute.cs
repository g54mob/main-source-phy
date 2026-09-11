using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class IgnoresAccessChecksToAttribute : Attribute
	{
		public string AssemblyName { get; }

		public IgnoresAccessChecksToAttribute(string assemblyName)
		{
			AssemblyName = assemblyName;
			base._002Ector();
		}
	}
}
