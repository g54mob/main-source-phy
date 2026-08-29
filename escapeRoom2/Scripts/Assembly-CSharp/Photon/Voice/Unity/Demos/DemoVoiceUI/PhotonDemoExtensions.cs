using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
	public static class PhotonDemoExtensions
	{
		internal const string IS_MUTED_PROPERTY_KEY = "mute";

		public static bool Mute(this Player player)
		{
			return player.SetCustomProperties(new Hashtable(1) { { "mute", true } });
		}

		public static bool Unmute(this Player player)
		{
			return player.SetCustomProperties(new Hashtable(1) { { "mute", false } });
		}

		public static bool IsMuted(this Player player)
		{
			if (player.CustomProperties.TryGetValue("mute", out var value))
			{
				return (bool)value;
			}
			return false;
		}
	}
}
