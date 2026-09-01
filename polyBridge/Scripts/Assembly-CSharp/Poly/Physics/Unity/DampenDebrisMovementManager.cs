using Poly.Base;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics.Unity
{
	public class DampenDebrisMovementManager : ListenerBase, IWorldListener
	{
		[Range(0f, 1f)]
		public float angularDrag;

		public void AfterWorldFixedUpdate()
		{
			World instance = SingletonBehaviour<World>.instance;
			float num = 1f - Mathf.Pow(1f - angularDrag, instance.settings.frameDeltaTime);
			foreach (EdgeHandle edgeHandle in instance.edgeHandles)
			{
				if (edgeHandle.material.isDebris)
				{
					ref SolverNode solverNode = ref edgeHandle.node0.solverNode;
					ref SolverNode solverNode2 = ref edgeHandle.node1.solverNode;
					float mass = edgeHandle.node0.mass;
					float mass2 = edgeHandle.node1.mass;
					Vec2 vec = solverNode2.pos - solverNode.pos;
					if (0f < mass + mass2 && 1E-12f < vec.sqrMagnitude)
					{
						Vec2 b = solverNode2.vel - solverNode.vel;
						Vec2 a = vec.rotated90;
						Vec2 vec2 = Vec2.Dot(in a, in b) / a.sqrMagnitude * a;
						float num2 = ((!(0f < mass * mass2)) ? ((mass == 0f) ? 0f : 1f) : (mass2 / (mass + mass2)));
						solverNode.vel += vec2 * num * num2;
						solverNode2.vel -= vec2 * num * (1f - num2);
					}
				}
			}
		}

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
		}

		public void AfterWorldFrameUpdate()
		{
		}
	}
}
