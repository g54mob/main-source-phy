using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblUserProfile
	{
		public ulong XboxUserId { get; private set; }

		public string AppDisplayName { get; private set; }

		public string AppDisplayPictureResizeUri { get; private set; }

		public string GameDisplayName { get; private set; }

		public string GameDisplayPictureResizeUri { get; private set; }

		public string Gamerscore { get; private set; }

		public string Gamertag { get; private set; }

		public string ModernGamertag { get; private set; }

		public string ModernGamertagSuffix { get; private set; }

		public string UniqueModernGamertag { get; private set; }

		internal XblUserProfile(XGamingRuntime.Interop.XblUserProfile interopStruct)
		{
			XboxUserId = interopStruct.xboxUserId;
			AppDisplayName = Converters.ByteArrayToString(interopStruct.appDisplayName);
			AppDisplayPictureResizeUri = Converters.ByteArrayToString(interopStruct.appDisplayPictureResizeUri);
			GameDisplayName = Converters.ByteArrayToString(interopStruct.gameDisplayName);
			GameDisplayPictureResizeUri = Converters.ByteArrayToString(interopStruct.gameDisplayPictureResizeUri);
			Gamerscore = Converters.ByteArrayToString(interopStruct.gamerscore);
			Gamertag = Converters.ByteArrayToString(interopStruct.gamertag);
			ModernGamertag = Converters.ByteArrayToString(interopStruct.modernGamertag);
			ModernGamertagSuffix = Converters.ByteArrayToString(interopStruct.modernGamertagSuffix);
			UniqueModernGamertag = Converters.ByteArrayToString(interopStruct.uniqueModernGamertag);
		}
	}
}
