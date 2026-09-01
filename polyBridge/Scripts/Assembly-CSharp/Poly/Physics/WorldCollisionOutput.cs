using System.Collections.Generic;
using Poly.Base;
using Poly.Solver;

namespace Poly.Physics
{
	public struct WorldCollisionOutput
	{
		private FastList<CollisionInfo> collisionInfos;

		public FastList<CollisionEvent> collisionEvents;

		public FastList<short> edgesWithCollisions;

		public FastList<CollisionInfo> bodyContact;

		public FastList<CollisionInfo> bridgeContact;

		public FastList<int> fullFrequencyBridgeContactIndices;

		private int lastRequestedCapacity;

		public WorldCollisionOutput(bool unused)
		{
			collisionInfos = new FastList<CollisionInfo>(32);
			collisionEvents = new FastList<CollisionEvent>(16);
			edgesWithCollisions = new FastList<short>(32);
			bodyContact = new FastList<CollisionInfo>(32);
			bridgeContact = new FastList<CollisionInfo>(32);
			fullFrequencyBridgeContactIndices = new FastList<int>(16);
			lastRequestedCapacity = 32;
		}

		public void Reserve(int newCapacity)
		{
			if (collisionInfos.Capacity < newCapacity)
			{
				int capacity = ((newCapacity - 1) / 32 + 1) * 32;
				collisionInfos.Capacity = capacity;
				bodyContact.Capacity = capacity;
				bridgeContact.Capacity = capacity;
				lastRequestedCapacity = capacity;
			}
		}

		public void AssertCapacityUnchanged()
		{
		}

		public void Clear(List<EdgeHandle> edgeHandles)
		{
			collisionInfos.Clear();
			collisionEvents.Clear();
			for (int i = 0; i < edgesWithCollisions.Count; i++)
			{
				short index = edgesWithCollisions[i];
				edgeHandles[index].runtime_isMarkedAsColliding = false;
			}
			edgesWithCollisions.Clear();
			bodyContact.Clear();
			bridgeContact.Clear();
			fullFrequencyBridgeContactIndices.Clear();
		}
	}
}
