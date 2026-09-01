using System.Text;

public static class ConnectionHelper
{
	public static string DebugMessage(byte[] message, int offset = 0)
	{
		StringBuilder stringBuilder = new StringBuilder("[");
		for (int i = offset; i < message.Length; i++)
		{
			if (i == offset)
			{
				stringBuilder.Append(message[i]);
				continue;
			}
			stringBuilder.Append(", ");
			stringBuilder.Append(message[i]);
		}
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}
}
