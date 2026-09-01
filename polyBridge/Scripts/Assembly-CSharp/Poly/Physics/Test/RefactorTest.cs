using Poly.Base;
using UnityEngine;

namespace Poly.Physics.Test
{
	public class RefactorTest : MonoBehaviour
	{
		public bool justStrings;

		private void OnEnable()
		{
			if (!justStrings)
			{
				CreateRope(new Vec2(0f, -5f), 10);
				CreateRope(new Vec2(0f, -10f), 18);
				CreateRope(new Vec2(0f, -15f), 25);
				CreateRope(new Vec2(0f, -20f), 50);
				CreateRope(new Vec2(0f, -25f), 100);
			}
			else
			{
				for (int i = 0; i < 20; i++)
				{
					CreateRope(new Vec2(-50f, (float)i * 2.5f), 50);
				}
			}
		}

		private static void CreateRope(Vec2 offset, int numSegments)
		{
			World instance = SingletonBehaviour<World>.instance;
			EdgeDefinition define = new EdgeDefinition();
			NodeHandle nodeHandle = null;
			for (int i = 0; i <= numSegments; i++)
			{
				NodeHandle nodeHandle2 = NodeHandle.Create((i != 0 && i != numSegments) ? MotionType.Dynamic : MotionType.Kinematic, 1f, offset + 2 * i * Vec2.right);
				instance.AddNode(nodeHandle2);
				if ((bool)nodeHandle)
				{
					EdgeHandle edge = World.CreateEdge_Inner(nodeHandle, nodeHandle2, define);
					instance.AddEdge(edge);
				}
				nodeHandle = nodeHandle2;
			}
		}
	}
}
