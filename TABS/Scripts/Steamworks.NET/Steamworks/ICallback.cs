using System;

namespace Steamworks
{
	internal interface ICallback
	{
		void OnRunCallback(IntPtr thisptr, IntPtr pvParam);

		void OnRunCallResult(IntPtr thisptr, IntPtr pvParam, bool bFailed, ulong hSteamAPICall);

		int OnGetCallbackSizeBytes(IntPtr thisptr);
	}
}
