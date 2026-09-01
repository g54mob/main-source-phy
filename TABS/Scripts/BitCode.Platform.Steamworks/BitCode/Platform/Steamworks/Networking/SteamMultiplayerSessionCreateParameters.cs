using System.Runtime.CompilerServices;
using BitCode.Networking;
using BitCode.Users;

namespace BitCode.Platform.Steamworks.Networking
{
	public class SteamMultiplayerSessionCreateParameters : IMultiplayerSessionCreateParameters
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

		public SteamMultiplayerSessionCreateParameters(SteamLocalAccount user)
		{
			gWOCaLhldbLPoAPoAgJDpynnXzhy = user;
		}
	}
}
