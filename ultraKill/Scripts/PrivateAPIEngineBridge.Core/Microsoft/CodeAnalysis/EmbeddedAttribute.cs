using System;

namespace Microsoft.CodeAnalysis
{
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
