namespace Shapes;

public static class DashTypeExtensions
{
	public static bool HasModifier(DashType type)
	{
		//IL_0033: Expected O, but got I4
		if (type == DashType.Angled)
		{
			return (byte)type != 0;
		}
		object obj = type - 3;
		return obj == null;
	}
}
