using System.Collections.Generic;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PacketPool
	{
		private static readonly Stack<Packet> Pool = new Stack<Packet>();

		public static Packet Acquire()
		{
			if (Pool.Count != 0)
			{
				return Pool.Pop();
			}
			return new Packet();
		}

		public static void ReturnToPool(Packet packet)
		{
			Pool.Push(packet);
		}
	}
}
