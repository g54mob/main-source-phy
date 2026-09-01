using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poly.Collide;
using Poly.Extension;
using Poly.Solver;

namespace Poly.Physics
{
	[Serializable]
	[DebuggerDisplay("w#: {worldIdx} mass: {mass} #edges: {edges.Count} #pins: {pins.Count}")]
	public class NodeHandle : WorldObjectImpl
	{
		[NonSerialized]
		public Node unityNodeComponent;

		private bool _isKinematic;

		internal float massWhenDynamic;

		public SolverNode solverNode;

		public Vec2 oldPos;

		[NonSerialized]
		internal List<EdgeHandle> edges = new List<EdgeHandle>();

		[NonSerialized]
		internal List<EdgeHandle> pins = new List<EdgeHandle>();

		public bool isSplittableAnchor { get; set; }

		public bool isAnchor { get; set; }

		public float mass
		{
			get
			{
				if (solverNode.invMass != 0f)
				{
					return 1f / solverNode.invMass;
				}
				return 0f;
			}
		}

		public override bool isDynamic => solverNode.invMass != 0f;

		public bool isKinematic => _isKinematic;

		public Vec2 pos => solverNode.pos;

		public float GetMassWhenDynamic()
		{
			return massWhenDynamic;
		}

		public void SetMass(float mass)
		{
			float invMass = solverNode.invMass;
			massWhenDynamic = mass;
			if (mass >= 1E-06f && !_isKinematic)
			{
				solverNode.invMass = 1f / mass;
				solverNode.gravityScale = 1f;
			}
			else
			{
				solverNode.invMass = 0f;
				solverNode.gravityScale = 0f;
				solverNode.vel = Vec2.zero;
			}
			if ((bool)world && invMass != solverNode.invMass)
			{
				world.dirtyEdges.AddRange(edges);
			}
		}

		public void SetKinematic(bool isKinematic)
		{
			_isKinematic = isKinematic;
			SetMass(massWhenDynamic);
		}

		public override void SetWorldAndIndex(World world, int index)
		{
			base.SetWorldAndIndex(world, index);
			if (shapeHandleIndex.isValid)
			{
				shapeHandleIndex.Get().nodeIdx = worldIdx;
			}
		}

		public static NodeHandle Create(MotionType motionType, float mass, Vec2 position)
		{
			return Create(motionType, mass, position, Vec2.zero);
		}

		public static NodeHandle Create(MotionType motionType, float mass, Vec2 position, Vec2 velocityDisplacement)
		{
			NodeHandle nodeHandle = new NodeHandle();
			nodeHandle.SetKinematic(motionType == MotionType.Kinematic);
			nodeHandle.SetMass(mass);
			nodeHandle.solverNode.pos = position;
			nodeHandle.oldPos = position;
			nodeHandle.solverNode.vel = velocityDisplacement;
			return nodeHandle;
		}

		public static NodeHandle Create(NodeDefinition define, NodeShapeDefinition shapeDefine, Vec2 position)
		{
			NodeHandle nodeHandle = Create((!define.isKinematic) ? MotionType.Dynamic : MotionType.Kinematic, define.mass, position);
			if (shapeDefine.enableCollision)
			{
				ShapeHandle newShapeHandle = Shape.CreateShapeAndHandle(shapeDefine);
				nodeHandle.SetShape(ref newShapeHandle);
			}
			return nodeHandle;
		}

		public static void DestroyNode(NodeHandle node)
		{
			for (int num = node.edges.Count - 1; num >= 0; num--)
			{
				EdgeHandle edgeHandle = node.edges[num];
				if (edgeHandle.isAddedToWorld)
				{
					edgeHandle.world.RemoveEdge(edgeHandle);
				}
				World.DestroyEdge(edgeHandle);
			}
			node.ReleaseShape();
			node.edges = null;
		}

		public void CacheTransform2InShapeHandles_Util()
		{
			if (0 <= (short)shapeHandleIndex)
			{
				ref ShapeHandle reference = ref World.shapeHandleArray[(short)shapeHandleIndex];
				reference.t2.position = pos;
				reference.fastLinearVel = solverNode.vel * world.settings.nodeToMotionVelocityMultiplier;
			}
		}
	}
}
