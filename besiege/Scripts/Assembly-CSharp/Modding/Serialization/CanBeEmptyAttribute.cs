using System;

namespace Modding.Serialization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CanBeEmptyAttribute : Attribute
	{
	}
}
