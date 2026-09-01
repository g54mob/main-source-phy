using System;

namespace Shapes;

internal class OvldDefault : Attribute
{
	public string @default;

	public OvldDefault(string @default)
	{
		this.@default = @default;
	}
}
