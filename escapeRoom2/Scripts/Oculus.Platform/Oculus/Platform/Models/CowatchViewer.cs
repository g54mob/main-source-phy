using System;

namespace Oculus.Platform.Models
{
	public class CowatchViewer
	{
		public readonly string Data;

		public readonly ulong Id;

		public CowatchViewer(IntPtr o)
		{
			Data = CAPI.ovr_CowatchViewer_GetData(o);
			Id = CAPI.ovr_CowatchViewer_GetId(o);
		}
	}
}
