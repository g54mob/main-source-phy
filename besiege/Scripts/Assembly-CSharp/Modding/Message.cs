using Modding.Common;

namespace Modding
{
	public class Message
	{
		internal object[] objects;

		public MessageType Type { get; internal set; }

		public Player Sender { get; internal set; }

		internal ushort Destination { get; set; }

		internal Message()
		{
		}

		public object GetData(int index)
		{
			return objects[index];
		}
	}
}
