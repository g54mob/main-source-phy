using System.Text;

public static class SaveSystemEncryption
{
	private static StringBuilder _sb = new StringBuilder();

	public static string EncryptionSecret = "KnightOwl_MS";

	public static string DataScrambler(string data)
	{
		_sb.Clear();
		for (int i = 0; i < data.Length; i++)
		{
			_sb.Append((char)(data[i] ^ EncryptionSecret[i % EncryptionSecret.Length]));
		}
		return _sb.ToString();
	}
}
