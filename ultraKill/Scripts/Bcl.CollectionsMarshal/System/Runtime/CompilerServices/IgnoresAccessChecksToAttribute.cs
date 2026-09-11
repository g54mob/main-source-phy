namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class IgnoresAccessChecksToAttribute : Attribute
	{
		private readonly string _003CAssemblyName_003Ek__BackingField;

		public string AssemblyName => _003CAssemblyName_003Ek__BackingField;

		public IgnoresAccessChecksToAttribute(string assemblyName)
		{
			_003CAssemblyName_003Ek__BackingField = assemblyName;
		}
	}
}
