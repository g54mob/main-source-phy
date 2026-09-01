using System;

namespace XGamingRuntime.Interop
{
	internal struct XPackageChunkSelector
	{
		internal XPackageChunkSelectorType type;

		internal UIntPtr unionData;

		internal unsafe string LanguageOrTagOrFeature()
		{
			IntPtr rawPtr = (IntPtr)unionData.ToPointer();
			return Converters.PtrToStringUTF8(rawPtr);
		}

		internal uint ChunkId()
		{
			return Convert.ToUInt32(unionData.ToUInt64() & 0xFFFFFFFFu);
		}
	}
}
