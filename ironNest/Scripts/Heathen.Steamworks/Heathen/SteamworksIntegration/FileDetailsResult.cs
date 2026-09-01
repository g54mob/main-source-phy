using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct FileDetailsResult
	{
		public FileDetailsResult_t Data;

		public EResult Result => default(EResult);

		public ulong FileSize => 0uL;

		public byte[] SHA1Hash => null;

		public uint Flags => 0u;

		public static implicit operator FileDetailsResult(FileDetailsResult_t native)
		{
			return default(FileDetailsResult);
		}

		public static implicit operator FileDetailsResult_t(FileDetailsResult heathen)
		{
			return default(FileDetailsResult_t);
		}
	}
}
