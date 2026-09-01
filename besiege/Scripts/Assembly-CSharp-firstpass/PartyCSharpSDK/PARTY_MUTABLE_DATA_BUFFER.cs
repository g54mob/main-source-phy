using System;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_MUTABLE_DATA_BUFFER
	{
		public IntPtr Buffer { get; set; }

		public uint BufferByteCount { get; set; }

		internal PARTY_MUTABLE_DATA_BUFFER(PartyCSharpSDK.Interop.PARTY_MUTABLE_DATA_BUFFER interopStruct)
		{
			Buffer = interopStruct.buffer;
			BufferByteCount = interopStruct.bufferByteCount;
		}
	}
}
