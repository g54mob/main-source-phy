using System;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class RequiredAttribute : Attribute
	{
		public string Message { get; set; }

		public string FixAction { get; set; }

		public string FixActionName { get; set; }
	}
}
