using System;
using System.Security.Cryptography;
using UnityEngine;

public class Checksum
{
	private static MD5 m_MD5;

	public static string Generate(byte[] bytes)
	{
		try
		{
			m_MD5 = MD5.Create();
			m_MD5.TransformFinalBlock(bytes, 0, bytes.Length);
			return BitConverter.ToString(m_MD5.Hash).ToLower().Replace("-", string.Empty);
		}
		catch (Exception arg)
		{
			Debug.LogWarning($"Failed to generate MD5 checksum due to exception: '{arg}'");
			return string.Empty;
		}
	}
}
