using Steamworks;

namespace Heathen.SteamworksIntegration.API
{
	public static class Voice
	{
		public static class Client
		{
			public static uint OptimalSampleRate => 0u;

			public static EVoiceResult DecompressVoice(byte[] compressedData, byte[] resultBuffer, out uint resultsWrittenSize, uint desiredSampleRate)
			{
				resultsWrittenSize = default(uint);
				return default(EVoiceResult);
			}

			public static EVoiceResult GetAvailableVoice(out uint pcbCompressed)
			{
				pcbCompressed = default(uint);
				return default(EVoiceResult);
			}

			public static EVoiceResult GetVoice(byte[] pDestBuffer, out uint nBytesWritten)
			{
				nBytesWritten = default(uint);
				return default(EVoiceResult);
			}

			public static void StartRecording()
			{
			}

			public static void StopRecording()
			{
			}
		}
	}
}
