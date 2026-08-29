using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

public static class SharedWriter
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Lease : IDisposable
	{
		public void Dispose()
		{
			isBorrowed = false;
		}
	}

	private static FastBinaryWriter writerInstance;

	private static bool isBorrowed;

	private static string lastBorrowStackTrace = string.Empty;

	public static Lease borrow(out FastBinaryWriter writer)
	{
		if (isBorrowed)
		{
			throw new Exception("[SharedWriter] Writer is already borrowed by another source.\n\n" + lastBorrowStackTrace);
		}
		writer = default(FastBinaryWriter);
		initWriterIfNeeded();
		if (!writerInstance.IsInitialized)
		{
			return default(Lease);
		}
		writerInstance.Reset();
		writer = writerInstance;
		isBorrowed = true;
		return default(Lease);
	}

	private static void initWriterIfNeeded()
	{
		if (!writerInstance.IsInitialized)
		{
			writerInstance = new FastBinaryWriter(65536, 1048576, Allocator.Persistent);
			if (writerInstance.IsInitialized)
			{
				Debug.Log(string.Format("[{0}] Initialized writer with {1} bytes.", "SharedWriter", 65536));
			}
			else
			{
				Debug.LogError("[SharedWriter] Writer was not initialized properly.");
			}
		}
	}

	public static void dispose()
	{
		writerInstance.Dispose();
	}
}
