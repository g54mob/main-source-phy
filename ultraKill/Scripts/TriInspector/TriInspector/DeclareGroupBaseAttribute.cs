using System;

namespace TriInspector
{
	public abstract class DeclareGroupBaseAttribute : Attribute
	{
		public string Path { get; }

		protected DeclareGroupBaseAttribute(string path)
		{
			Path = path ?? "None";
		}
	}
}
