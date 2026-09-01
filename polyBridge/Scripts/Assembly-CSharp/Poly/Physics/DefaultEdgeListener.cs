using Poly.Collide;
using UnityEngine;

namespace Poly.Physics
{
	public class DefaultEdgeListener : ListenerBase, IEdgeBreakListener
	{
		[Tooltip("Allow breaking or prevent it.")]
		public bool allowBreaking = true;

		[Header("Creating 'debris' edges")]
		public bool createDebrisEdges = true;

		[Range(0.05f, 0.5f)]
		[Tooltip("Length of each debris edge, as a fraction of original edge length.")]
		public float debrisLengthFraction = 0.5f;

		public bool collideDebrisNodes;

		public float debrisNodeRadius = 0.12f;

		public PhysicsMaterial2D debrisNodePhysicsMaterial;

		public float debrisNodeBaseMass = 0.1f;

		public bool collideDebrisEdges;

		public EdgeMaterial collidingMaterialForDebris;

		public EdgeMaterial nonCollidingMaterialForDebris;

		public EdgeMaterial springMaterialForDebris;

		public bool reuseOriginalEdge;

		[Header("Debug")]
		public bool logBrekage;

		private NodeShapeDefinition templateNodeShapeDefinition;

		private ShapeDefinition templateEdgeShapeDefinition;

		private void Awake()
		{
			NodeShapeDefinition nodeShapeDefinition = new NodeShapeDefinition();
			nodeShapeDefinition.enableCollision = true;
			nodeShapeDefinition.layer = Layer.CollideEverything;
			nodeShapeDefinition.collisionRadius = debrisNodeRadius;
			nodeShapeDefinition.physicsMaterial = debrisNodePhysicsMaterial;
			nodeShapeDefinition.surfaceVelocity = 0f;
			nodeShapeDefinition.collisionGroup = CollisionGroup.Bridge;
			templateNodeShapeDefinition = nodeShapeDefinition;
			ShapeDefinition shapeDefinition = new ShapeDefinition();
			shapeDefinition.enableCollision = true;
			shapeDefinition.type = Shape.Type.Segment;
			shapeDefinition.radius = collidingMaterialForDebris.collisionRadius;
			shapeDefinition.vertices = null;
			shapeDefinition.physicsMaterial = collidingMaterialForDebris.physicsMaterial;
			shapeDefinition.collisionGroup = 1;
			shapeDefinition.layer = Layer.DebrisRoadEdge;
			shapeDefinition.recollisionType = RecollisionType.DistanceOnly;
			shapeDefinition.tmpSurfaceVelocity = 0f;
			shapeDefinition.lengthX = 1f;
			templateEdgeShapeDefinition = shapeDefinition;
		}

		public bool OnEdgeBroken(EdgeHandle e)
		{
			if (allowBreaking)
			{
				if (logBrekage)
				{
					Debug.Log("Breaking");
				}
				e.world.ModifyShapeOfNode(e.node0);
				e.world.ModifyShapeOfNode(e.node1);
				if (createDebrisEdges)
				{
					float num = debrisLengthFraction;
					float num2 = 1f - debrisLengthFraction;
					Vec2 position = Vec2.LerpUnclamped(e.node0.pos, e.node1.pos, num);
					Vec2 position2 = Vec2.LerpUnclamped(e.node0.pos, e.node1.pos, num2);
					Vec2 velocityDisplacement = Vec2.LerpUnclamped(in e.node0.solverNode.vel, in e.node1.solverNode.vel, num);
					Vec2 velocityDisplacement2 = Vec2.LerpUnclamped(in e.node0.solverNode.vel, in e.node1.solverNode.vel, num2);
					NodeHandle nodeHandle = NodeHandle.Create(MotionType.Dynamic, debrisNodeBaseMass, position, velocityDisplacement);
					NodeHandle nodeHandle2 = NodeHandle.Create(MotionType.Dynamic, debrisNodeBaseMass, position2, velocityDisplacement2);
					bool flag = (bool)e.material && e.material.enableCollision;
					if (collideDebrisNodes)
					{
						ShapeDefinition shapeDefinition = templateNodeShapeDefinition;
						shapeDefinition.layer = ((flag && collideDebrisEdges) ? Layer.DebrisRoadNode : Layer.DebrisNonRoadNode);
						ShapeHandle newShapeHandle = Shape.CreateShapeAndHandle(shapeDefinition);
						nodeHandle.SetShape(ref newShapeHandle);
						newShapeHandle = Shape.CreateShapeAndHandle(shapeDefinition);
						nodeHandle2.SetShape(ref newShapeHandle);
					}
					e.world.AddNode(nodeHandle);
					e.world.AddNode(nodeHandle2);
					EdgeDefinition edgeDefinition = new EdgeDefinition();
					edgeDefinition.InitDefaults();
					edgeDefinition.lengthOverride = debrisLengthFraction * e.solverEdge.length;
					edgeDefinition.material = ((flag && collideDebrisEdges) ? collidingMaterialForDebris : nonCollidingMaterialForDebris);
					if (e.material.isSpring)
					{
						edgeDefinition.material = springMaterialForDebris;
					}
					EdgeHandle edgeHandle = World.CreateEdge_Inner(e.node1, nodeHandle2, edgeDefinition);
					EdgeHandle edgeHandle2;
					if (reuseOriginalEdge)
					{
						edgeHandle2 = e;
						if (edgeHandle2.shapeHandleIndex.isValid)
						{
							if (true)
							{
								((Segment)edgeHandle2.shapeHandleIndex.Get().shape).halfLengthX = 0.5f * edgeDefinition.lengthOverride;
								edgeHandle2.shapeHandleIndex.Get().layer = Layer.CollideNothing;
								edgeHandle2.world.ModifyShapeOfEdge(edgeHandle2);
							}
							else
							{
								edgeHandle2.world.RemoveShapeOfEdge(edgeHandle2);
								edgeHandle2.ReleaseShape();
							}
						}
						foreach (IEdgeListener edgeListener in edgeHandle2.world.edgeListeners)
						{
							edgeListener.OnEdgeDetachedFromNode(edgeHandle2, e.node0);
							edgeListener.OnEdgeDetachedFromNode(edgeHandle2, e.node1);
						}
						edgeHandle2.unityEdgeComponent.ReplaceNodePart(e.node1.unityNodeComponent, new NodePart(nodeHandle.unityNodeComponent, Part.A));
						edgeHandle2.world.ReCreateEdge_InWorld_CopyPasted(edgeHandle2, e.node0, nodeHandle, edgeDefinition);
						foreach (IEdgeListener edgeListener2 in edgeHandle2.world.edgeListeners)
						{
							edgeListener2.OnEdgeAttachedToNode(edgeHandle2, e.node0);
							edgeListener2.OnEdgeAttachedToNode(edgeHandle2, nodeHandle);
						}
					}
					else
					{
						edgeHandle2 = World.CreateEdge_Inner(e.node0, nodeHandle, edgeDefinition);
					}
					if (collideDebrisEdges && flag && edgeDefinition.material.enableCollision)
					{
						ShapeDefinition shapeDefinition2 = templateEdgeShapeDefinition;
						shapeDefinition2.layer = Layer.DebrisRoadEdge;
						shapeDefinition2.lengthX = edgeHandle2.solverEdge.length;
						if (edgeHandle2.shapeHandleIndex.isValid)
						{
							Shape.ReuseShapeAndHandle(ref edgeHandle2.shapeHandleIndex.Get(), shapeDefinition2);
							edgeHandle2.world.collide.invalidateShapeIndices.Add(edgeHandle2.shapeHandleIndex);
						}
						else
						{
							ShapeHandle newShapeHandle2 = Shape.CreateShapeAndHandle(shapeDefinition2);
							edgeHandle2.SetShape(ref newShapeHandle2);
						}
						shapeDefinition2.lengthX = edgeHandle.solverEdge.length;
						ShapeHandle newShapeHandle3 = Shape.CreateShapeAndHandle(shapeDefinition2);
						edgeHandle.SetShape(ref newShapeHandle3);
					}
					bool flag2 = e.world.AddEdge(edgeHandle);
					if (!reuseOriginalEdge)
					{
						flag2 &= e.world.AddEdge(edgeHandle2);
					}
					if ((bool)e.unityEdgeComponent)
					{
						MaybeSplitRope(e.unityEdgeComponent, num, num2, edgeHandle2.unityEdgeComponent, edgeHandle.unityEdgeComponent, out var _);
					}
				}
				if (!reuseOriginalEdge)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public static void MaybeSplitRope(Edge brokenEdge, float t0, float t1, Edge debrisOne, Edge debrisTwo, out Rope[] newRopes, RopeNode[] srcNodes = null)
		{
			Rope componentInChildren = brokenEdge.GetComponentInChildren<Rope>();
			if ((bool)componentInChildren)
			{
				newRopes = new Rope[2];
				newRopes[0] = componentInChildren.CopyRopeFromTill(0f, t0, debrisOne, srcNodes);
				newRopes[1] = componentInChildren.CopyRopeFromTill(1f, t1, debrisTwo, srcNodes);
				if (brokenEdge == debrisOne || brokenEdge == debrisTwo)
				{
					Object.DestroyImmediate(componentInChildren);
				}
			}
			else
			{
				newRopes = new Rope[0];
			}
		}
	}
}
