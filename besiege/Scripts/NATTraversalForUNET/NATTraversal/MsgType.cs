using UnityEngine.Networking;

namespace NATTraversal
{
	public class MsgType : UnityEngine.Networking.MsgType
	{
		public static short ReplaceConnection = 32762;

		public static short SetConnectionInfo = 32763;

		public static short ExtraPeerInfo = 32764;
	}
}
