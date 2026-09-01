using System.Runtime.CompilerServices;
using BitCode.Networking;
using BitCode.Users;

namespace BitCode.Platform.Steamworks.Networking
{
	public class SteamMultiplayerSessionJoinParameters : IMultiplayerSessionJoinParameters
	{
		[CompilerGenerated]
		private readonly ILocalAccount gWOCaLhldbLPoAPoAgJDpynnXzhy;

		public ILocalAccount User
		{
			[CompilerGenerated]
			get
			{
				return gWOCaLhldbLPoAPoAgJDpynnXzhy;
			}
		}

		public SteamLocalAccount SteamUser => (SteamLocalAccount)User;

		public SteamMultiplayerSessionJoinParameters(SteamLocalAccount user)
		{
			gWOCaLhldbLPoAPoAgJDpynnXzhy = user;
		}
	}
}
