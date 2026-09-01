using System;

namespace FluffyUnderware.Curvy.Generator
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ResourceLoaderAttribute : Attribute
	{
		public readonly string ResourceName;

		public ResourceLoaderAttribute(string resName)
		{
			ResourceName = resName;
		}
	}
}
