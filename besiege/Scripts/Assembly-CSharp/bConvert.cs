using System;
using System.Collections;
using UnityEngine;

public class bConvert
{
	public static string ByteArrayToHexString(byte[] ba)
	{
		string text = BitConverter.ToString(ba);
		return text.Replace("-", string.Empty);
	}

	public static string ByteArrayTo64String(byte[] ba)
	{
		return Convert.ToBase64String(ba);
	}

	public static byte[] ConvertBitsToBytes(BitArray bits)
	{
		byte[] array = new byte[(int)Mathf.Ceil(bits.Count / 8)];
		bits.CopyTo(array, 0);
		return array;
	}
}
