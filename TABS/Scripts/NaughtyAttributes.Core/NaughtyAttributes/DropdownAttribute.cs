using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DropdownAttribute : DrawerAttribute
	{
		public string ValuesFieldName { get; private set; }

		public DropdownAttribute(string valuesFieldName)
		{
			ValuesFieldName = valuesFieldName;
		}
	}
}
