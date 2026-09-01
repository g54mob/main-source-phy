using System;

namespace Shapes
{
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class OvldDefault : Attribute
	{
		public string @default;

		public OvldDefault(string @default)
		{
		}
	}
}
