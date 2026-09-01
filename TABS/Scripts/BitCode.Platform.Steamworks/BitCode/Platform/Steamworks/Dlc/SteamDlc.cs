using System.Runtime.CompilerServices;
using BitCode.Dlc;
using Steamworks;

namespace BitCode.Platform.Steamworks.Dlc
{
	public class SteamDlc : IDlc
	{
		[CompilerGenerated]
		private readonly string JfDRGcTUweSQnKEwNOlIIqqHLdEf;

		[CompilerGenerated]
		private readonly AppId_t? pilNdYnNybeQAUJWXcOXyqxxYdnM;

		public string Id
		{
			[CompilerGenerated]
			get
			{
				return JfDRGcTUweSQnKEwNOlIIqqHLdEf;
			}
		}

		public AppId_t? SteamApiId
		{
			[CompilerGenerated]
			get
			{
				return pilNdYnNybeQAUJWXcOXyqxxYdnM;
			}
		}

		public SteamDlc(AppId_t dlcId)
		{
			pilNdYnNybeQAUJWXcOXyqxxYdnM = dlcId;
			JfDRGcTUweSQnKEwNOlIIqqHLdEf = SteamApiId.Value.ToString();
		}
	}
}
