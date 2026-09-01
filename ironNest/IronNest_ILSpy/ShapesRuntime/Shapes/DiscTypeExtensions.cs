namespace Shapes;

internal static class DiscTypeExtensions
{
	public static bool HasThickness(DiscType type)
	{
		//IL_0034: Expected O, but got I4
		if (type == DiscType.Ring)
		{
			return true;
		}
		object obj = type - 3;
		return obj == null;
	}

	public static bool HasSector(DiscType type)
	{
		//IL_0033: Expected O, but got I4
		if (type == DiscType.Pie)
		{
			return (byte)type != 0;
		}
		object obj = type - 3;
		return obj == null;
	}
}
