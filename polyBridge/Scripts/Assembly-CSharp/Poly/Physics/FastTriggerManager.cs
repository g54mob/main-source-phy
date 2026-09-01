using Poly.Base;
using Poly.Extension;

namespace Poly.Physics
{
	public static class FastTriggerManager
	{
		public static void DetectOverlaps(WorldCollisionInput input, FastList<int> triggerAabbPairs)
		{
			for (int i = 0; i < triggerAabbPairs.Count; i++)
			{
				Vec2Short vec2Short = Vec2Short.FromKey(triggerAabbPairs[i]);
				ref ShapeHandle reference = ref input.shapeHandles[vec2Short.x];
				ref ShapeHandle reference2 = ref input.shapeHandles[vec2Short.y];
				if (reference.isTrigger)
				{
					HandleOverlap((FastAabbTrigger)reference.entityHandle, ref reference2);
				}
				if (reference2.isTrigger)
				{
					HandleOverlap((FastAabbTrigger)reference2.entityHandle, ref reference);
				}
			}
		}

		private static void HandleOverlap(FastAabbTrigger trigger, ref ShapeHandle other)
		{
			if (other.entityHandle is NodeHandle)
			{
				NodeHandle node = (NodeHandle)other.entityHandle;
				trigger.nodeOverlapCallback(node);
			}
			else if (other.entityHandle is EdgeHandle)
			{
				_ = (EdgeHandle)other.entityHandle;
			}
			else if (other.entity != null)
			{
				Rigidbody body = (Rigidbody)other.entity;
				trigger.bodyOverlapCallback(body);
			}
			else if (other.entityHandle is FastAabbTrigger)
			{
				_ = (FastAabbTrigger)other.entityHandle;
			}
		}
	}
}
