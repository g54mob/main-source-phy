public struct ProtectedInt
{
	private int encryptedValue;

	private int key;

	private int checksum;

	private bool initialized;

	public bool WasTampered { get; private set; }

	public int Value
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public ProtectedInt(int value)
	{
		encryptedValue = 0;
		key = 0;
		checksum = 0;
		initialized = false;
		WasTampered = false;
	}

	public bool CheckTampered()
	{
		return false;
	}

	private void EnsureInitialized()
	{
	}

	private static int CalculateChecksum(int value, int key)
	{
		return 0;
	}

	public static implicit operator int(ProtectedInt value)
	{
		return 0;
	}

	public static implicit operator ProtectedInt(int value)
	{
		return default(ProtectedInt);
	}
}
