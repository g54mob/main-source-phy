using System;
using System.Runtime.CompilerServices;
using BitCode.Networking;

namespace BitCode.Platform.Steamworks.Networking
{
	public class SteamGameInvitation : IGameInvitation
	{
		[CompilerGenerated]
		private readonly IMultiplayerSessionInfo mFXmcVhJtiECdWNLhfFdsZAsClsS;

		[CompilerGenerated]
		private readonly byte[] xgSSxpYnsgUOvAiKBPXcfFXnaxuc;

		public IMultiplayerSessionInfo SessionInfo
		{
			[CompilerGenerated]
			get
			{
				return mFXmcVhJtiECdWNLhfFdsZAsClsS;
			}
		}

		public byte[] ApplicationData
		{
			[CompilerGenerated]
			get
			{
				return xgSSxpYnsgUOvAiKBPXcfFXnaxuc;
			}
		}

		public SteamGameInvitation(SteamMultiplayerSessionInfo sessionInfo, byte[] applicationData = null)
		{
			mFXmcVhJtiECdWNLhfFdsZAsClsS = sessionInfo;
			xgSSxpYnsgUOvAiKBPXcfFXnaxuc = applicationData;
		}

		public override string ToString()
		{
			string text = string.Empty;
			if (this.HasApplicationData())
			{
				while (true)
				{
					int num = -332831056;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1756482366)) % 3)
						{
						case 0u:
							break;
						case 2u:
							text += Convert.ToBase64String(ApplicationData);
							num = ((int)num2 * -1021997841) ^ -1585618242;
							continue;
						default:
							goto end_IL_000e;
						}
						break;
					}
					continue;
					end_IL_000e:
					break;
				}
			}
			return text;
		}

		public static SteamGameInvitation FromString(string str)
		{
			byte[] applicationData = Convert.FromBase64String(str);
			return new SteamGameInvitation(new SteamMultiplayerSessionInfo(), applicationData);
		}
	}
}
