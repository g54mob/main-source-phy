using System;

namespace Oculus.Platform.Models
{
	public class CowatchingState
	{
		public readonly bool InSession;

		public CowatchingState(IntPtr o)
		{
			InSession = CAPI.ovr_CowatchingState_GetInSession(o);
		}
	}
}
