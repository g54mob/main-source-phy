using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_AUDIO_FORMAT
	{
		public uint SamplesPerSecond { get; set; }

		public uint ChannelMask { get; set; }

		public ushort ChannelCount { get; set; }

		public ushort BitsPerSample { get; set; }

		public PARTY_AUDIO_SAMPLE_TYPE SampleType { get; set; }

		public byte Interleaved { get; set; }

		internal PARTY_AUDIO_FORMAT(PartyCSharpSDK.Interop.PARTY_AUDIO_FORMAT interopStruct)
		{
			SamplesPerSecond = interopStruct.samplesPerSecond;
			ChannelMask = interopStruct.channelMask;
			ChannelCount = interopStruct.channelCount;
			BitsPerSample = interopStruct.bitsPerSample;
			SampleType = interopStruct.sampleType;
			Interleaved = interopStruct.interleaved;
		}
	}
}
