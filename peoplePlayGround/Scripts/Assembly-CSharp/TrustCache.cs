using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Ceras;
using UnityEngine;
using UnityEngine.UI;

internal static class TrustCache
{
	private static readonly HashSet<uint> trusted;

	private static readonly CerasSerializer serialiser;

	private static readonly string path;

	private static readonly Assembly unityAssembly;

	private static readonly Assembly[] allowedAssemblies;

	static TrustCache()
	{
		trusted = new HashSet<uint>();
		serialiser = new CerasSerializer();
		unityAssembly = Assembly.GetAssembly(typeof(GameObject));
		allowedAssemblies = new Assembly[4]
		{
			unityAssembly,
			Assembly.GetAssembly(typeof(Button)),
			Assembly.GetAssembly(typeof(LimbBehaviour)),
			Assembly.GetAssembly(typeof(int))
		};
		path = Hash128.Compute(Environment.MachineName + Environment.UserName + Environment.UserDomainName + Environment.OSVersion?.ToString() + Environment.ProcessorCount).ToString();
	}

	internal static void Load()
	{
		VerifyOrigin(isUi: false);
		try
		{
			byte[] buffer = File.ReadAllBytes(path);
			HashSet<uint> hashSet = serialiser.Deserialize<HashSet<uint>>(buffer);
			trusted.Clear();
			trusted.AddRange(hashSet);
			UnityEngine.Debug.LogFormat("Trust cache of length {0} read", hashSet.Count);
		}
		catch (FileNotFoundException)
		{
			UnityEngine.Debug.Log("Trust cache does not exist yet :)");
		}
		catch (Exception ex2)
		{
			UnityEngine.Debug.LogErrorFormat("Failed to read trust cache: {0}", ex2.Message);
		}
	}

	internal static void Save()
	{
		VerifyOrigin(isUi: false);
		HashSet<uint> hashSet = new HashSet<uint>(trusted);
		byte[] bytes = serialiser.Serialize(hashSet);
		File.WriteAllBytes(path, bytes);
		UnityEngine.Debug.LogFormat("Trust cache of length {0} written", hashSet.Count);
	}

	internal static void Trust(ModMetaData modMeta)
	{
		VerifyOrigin(isUi: false);
		if (modMeta.ContentHash == 0)
		{
			throw new Exception("Failed to trust mod because it has no content hash");
		}
		trusted.Add(modMeta.ContentHash);
	}

	internal static void Distrust(ModMetaData modMeta)
	{
		VerifyOrigin(isUi: false);
		if (modMeta.ContentHash == 0)
		{
			throw new Exception("Failed to distrust mod because it has no content hash");
		}
		modMeta.GetUniqueName();
		trusted.Remove(modMeta.ContentHash);
	}

	internal static bool IsTrusted(ModMetaData modMeta)
	{
		if (modMeta.ContentHash != 0)
		{
			return trusted.Contains(modMeta.ContentHash);
		}
		return false;
	}

	public static void VerifyOrigin(bool isUi)
	{
		StackFrame[] frames = new StackTrace().GetFrames();
		bool flag = frames.All(delegate(StackFrame frame)
		{
			Assembly assembly = frame.GetMethod().DeclaringType.Assembly;
			if (!allowedAssemblies.Contains(assembly))
			{
				UnityEngine.Debug.LogWarningFormat("VerifyOrigin: {0} is not allowed in the stack trace", assembly.GetName().Name);
				return false;
			}
			return true;
		});
		if (isUi)
		{
			flag &= frames.Any((StackFrame frame) => frame.GetMethod().DeclaringType.Assembly == unityAssembly);
		}
		if (!flag)
		{
			throw new Exception("Shady stack trace :S");
		}
	}
}
