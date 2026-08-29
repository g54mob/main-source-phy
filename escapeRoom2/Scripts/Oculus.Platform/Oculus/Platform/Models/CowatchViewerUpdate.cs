using System;

namespace Oculus.Platform.Models
{
	public class CowatchViewerUpdate
	{
		public readonly CowatchViewerList DataList;

		public readonly ulong Id;

		public CowatchViewerUpdate(IntPtr o)
		{
			DataList = new CowatchViewerList(CAPI.ovr_CowatchViewerUpdate_GetDataList(o));
			Id = CAPI.ovr_CowatchViewerUpdate_GetId(o);
		}
	}
}
