using System;
using UnityEngine;

namespace VLB;

public class PlatformHelper
{
	public unsafe static string GetCurrentPlatformSuffix()
	{
		//IL_0021: Expected O, but got Ref
		RuntimePlatform platform = Application.platform;
		IntPtr intPtr = default(IntPtr);
		return ((Enum)(&intPtr)).ToString();
	}

	private unsafe static string GetPlatformSuffix(RuntimePlatform platform)
	{
		//IL_000e: Expected O, but got Ref
		object obj = default(object);
		return ((Enum)(&obj)).ToString();
	}
}
