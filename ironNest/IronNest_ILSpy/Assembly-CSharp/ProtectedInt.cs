using System;
using UnityEngine;

public struct ProtectedInt
{
	private int encryptedValue;

	private int key;

	private int checksum;

	private bool initialized;

	private bool _003CWasTampered_003Ek__BackingField;

	public bool WasTampered
	{
		get
		{
			return _003CWasTampered_003Ek__BackingField;
		}
		private set
		{
			_003CWasTampered_003Ek__BackingField = value;
		}
	}

	public int Value
	{
		get
		{
			//IL_00b2: Expected O, but got I4
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_0035: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected I4, but got Unknown
			if (!initialized)
			{
				object obj = (encryptedValue = (key = UnityEngine.Random.Range(-2147483648, 2147483647))) * 31;
				initialized = true;
				int num = obj + 1512014882;
				checksum = num;
			}
			int num2 = key ^ encryptedValue;
			object obj2 = num2 * 31;
			object obj3 = obj2 + key;
			object obj4 = obj3 * 31;
			object obj5 = obj4 + 1512014882;
			if (checksum != (nint)obj5)
			{
				_003CWasTampered_003Ek__BackingField = true;
			}
			return num2;
		}
		set
		{
			//IL_0096: Expected I4, but got I8
			//IL_00d4: Expected O, but got I4
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected I4, but got Unknown
			//IL_0035: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected I4, but got Unknown
			if (!initialized)
			{
				object obj = (encryptedValue = (key = UnityEngine.Random.Range(-2147483648, 2147483647))) * 31;
				initialized = true;
				int num = obj + 1512014882;
				checksum = num;
			}
			int num2 = (key = UnityEngine.Random.Range(-2147483648, 2147483647));
			int num3 = num2 ^ value;
			_003CWasTampered_003Ek__BackingField = false;
			encryptedValue = num3;
			object obj2 = value * 31;
			object obj3 = obj2 + num2;
			object obj4 = obj3 * 31;
			int num4 = obj4 + 1512014882;
			checksum = num4;
		}
	}

	public ProtectedInt(int value)
	{
		//IL_001c: Expected I4, but got I8
		//IL_004f: Expected O, but got I4
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected I4, but got Unknown
		System.Random random = new System.Random();
		int num = random.Next(-2147483648, 2147483647);
		int num2 = num ^ value;
		key = num;
		encryptedValue = num2;
		object obj = value * 31;
		initialized = true;
		object obj2 = obj + num;
		object obj3 = obj2 * 31;
		int num3 = obj3 + 1512014882;
		checksum = num3;
	}

	public bool CheckTampered()
	{
		//IL_00b2: Expected O, but got I4
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_0035: Expected I4, but got I8
		//IL_005b: Expected O, but got I4
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected I4, but got Unknown
		if (!initialized)
		{
			object obj = (encryptedValue = (key = UnityEngine.Random.Range(-2147483648, 2147483647))) * 31;
			initialized = true;
			int num = obj + 1512014882;
			checksum = num;
		}
		int num2 = key ^ encryptedValue;
		object obj2 = num2 * 31;
		object obj3 = obj2 + key;
		object obj4 = obj3 * 31;
		object obj5 = obj4 + 1512014882;
		if (checksum != (nint)obj5)
		{
			_003CWasTampered_003Ek__BackingField = true;
		}
		return _003CWasTampered_003Ek__BackingField;
	}

	private void EnsureInitialized()
	{
		//IL_0035: Expected I4, but got I8
		//IL_005b: Expected O, but got I4
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected I4, but got Unknown
		if (!initialized)
		{
			object obj = (encryptedValue = (key = UnityEngine.Random.Range(-2147483648, 2147483647))) * 31;
			initialized = true;
			int num = obj + 1512014882;
			checksum = num;
		}
	}

	private static int CalculateChecksum(int value, int key)
	{
		//IL_000e: Expected O, but got I4
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected I4, but got Unknown
		object obj = value * 31;
		object obj2 = obj + key;
		object obj3 = obj2 * 31;
		return obj3 + 1512014882;
	}

	public unsafe static implicit operator int(ProtectedInt value)
	{
		//IL_0038: Expected I4, but got I8
		//IL_0044: Expected native int or pointer, but got O
		//IL_0051: Expected native int or pointer, but got O
		if (!value.initialized)
		{
			int num = (((ProtectedInt*)(nint)value)->key = UnityEngine.Random.Range(-2147483648, 2147483647));
			((ProtectedInt*)(nint)value)->encryptedValue = num;
		}
		return value.key ^ value.encryptedValue;
	}

	public unsafe static implicit operator ProtectedInt(int value)
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0016: Expected native int or pointer, but got O
		ProtectedInt protectedInt = default(ProtectedInt);
		((ProtectedInt*)(nint)protectedInt)->encryptedValue = 0;
		*(ProtectedInt*)(nint)protectedInt = new ProtectedInt(value);
		return protectedInt;
	}
}
