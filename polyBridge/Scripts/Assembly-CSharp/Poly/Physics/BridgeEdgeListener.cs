using Poly.Collide;
using Poly.Determinism;
using UnityEngine;

namespace Poly.Physics
{
	public class BridgeEdgeListener : ListenerBase, IEdgeBreakListener
	{
		[Tooltip("Allow breaking or prevent it.")]
		public bool allowBreaking = true;

		public bool collideDebrisEdges = true;

		[Range(0.05f, 0.5f)]
		[Tooltip("Length of each debris edge, as a fraction of original edge length.")]
		public float debrisSize = 0.5f;

		public bool collideDebrisNodes;

		public float debrisNodeRadius = 0.12f;

		public PhysicsMaterial2D debrisNodePhysicsMaterial;

		public float debrisNodeBaseMass = 0.1f;

		public EdgeMaterial collidingMaterialForDebris;

		public EdgeMaterial nonCollidingMaterialForDebris;

		public EdgeMaterial springMaterialForDebris;

		public EdgeMaterial ropeOrCableMaterialForDebris;

		public bool reuseOriginalEdge;

		private NodeShapeDefinition templateNodeShapeDefinition;

		private ShapeDefinition templateEdgeShapeDefinition;

		private int m_FramecountLastBreak;

		private bool debug_notifyOnFirstBreak;

		public static bool debug_edgeBroke;

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
			if (!allowBreaking)
			{
				return false;
			}
			BridgeEdge bridgeEdge = BridgeEdges.FindByPhysicsEdge(e.unityEdgeComponent);
			if (!bridgeEdge)
			{
				return false;
			}
			if (GameStateManager.GetState() == GameState.SIM && bridgeEdge.transform.position.y > -200f)
			{
				if (Profiles.m_ActiveProfile.m_PauseOnBreak && (bool)GameUI.m_Instance && Time.frameCount != m_FramecountLastBreak)
				{
					global::Game.MaybeShowPauseOnBreakPopup();
					GameUI.m_Instance.m_TopBar.OnPauseSim();
					BridgeEffects.LoopErrorEffectAtPosition(bridgeEdge.m_PhysicsEdge.smoothPos);
					m_FramecountLastBreak = Time.frameCount;
				}
				if (!GameStateBuild.FirstBreakHasBeenSet())
				{
					BridgeEdgeProxy bridgeEdgeProxy = new BridgeEdgeProxy(bridgeEdge);
					bridgeEdgeProxy.m_NodeA_Guid = ((bridgeEdge.m_StartSimJointA != null) ? bridgeEdge.m_StartSimJointA.m_Guid : bridgeEdgeProxy.m_NodeA_Guid);
					bridgeEdgeProxy.m_NodeB_Guid = ((bridgeEdge.m_StartSimJointB != null) ? bridgeEdge.m_StartSimJointB.m_Guid : bridgeEdgeProxy.m_NodeB_Guid);
					GameStateBuild.SetFirstBreakEdge(bridgeEdgeProxy);
				}
			}
			DeterminismLog.LogEvent(e.unityEdgeComponent, Poly.Determinism.EventType.EdgeBreak);
			CreateDebris(e, bridgeEdge);
			if (GameStateSim.IsSimulatingWithoutPassOrFail())
			{
				GameStateSim.m_NumBridgeBreaks++;
			}
			if (!reuseOriginalEdge)
			{
				return true;
			}
			return false;
		}

		private void CreateDebris(EdgeHandle e, BridgeEdge brokenEdge)
		{
			EdgeMaterial originalMaterial = e.originalMaterial;
			e.world.ModifyShapeOfNode(e.node0);
			e.world.ModifyShapeOfNode(e.node1);
			float num = debrisSize;
			float num2 = 1f - debrisSize;
			if ((bool)brokenEdge.m_HydraulicEdgeVisualization)
			{
				float num3 = Mathf.Clamp(brokenEdge.m_HydraulicEdgeVisualization.basePartLength / e.length, 0.5f, 1f);
				if (brokenEdge.m_HydraulicEdgeVisualization.isReversed ^ brokenEdge.m_PhysicsEdge.areNodesReversedInPhysics)
				{
					num3 = 1f - num3;
				}
				num = num3;
				num2 = num3;
				Hydraulics hydro = HydraulicController.instance.GetHydro(e);
				HydraulicController.instance.RemoveFromSets(hydro);
			}
			RopeNode[] srcNodes = null;
			if ((bool)brokenEdge.m_PhysicsEdge)
			{
				Rope componentInChildren = brokenEdge.m_PhysicsEdge.GetComponentInChildren<Rope>();
				if ((bool)componentInChildren)
				{
					srcNodes = componentInChildren.CalcNodesForCopying();
				}
			}
			Vector2 vector = Vector2.Lerp(e.node0.pos, e.node1.pos, num);
			Vector2 vector2 = Vector2.Lerp(e.node0.pos, e.node1.pos, num2);
			Vector2 vector3 = Vector2.Lerp(e.node0.solverNode.vel, e.node1.solverNode.vel, num);
			Vector2 vector4 = Vector2.Lerp(e.node0.solverNode.vel, e.node1.solverNode.vel, num2);
			NodeHandle nodeHandle = NodeHandle.Create(MotionType.Dynamic, debrisNodeBaseMass, vector, vector3);
			NodeHandle nodeHandle2 = NodeHandle.Create(MotionType.Dynamic, debrisNodeBaseMass, vector2, vector4);
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
			edgeDefinition.lengthOverride = debrisSize * e.solverEdge.length;
			edgeDefinition.material = ((flag && collideDebrisEdges) ? collidingMaterialForDebris : nonCollidingMaterialForDebris);
			if (e.material.isSpring)
			{
				edgeDefinition.material = springMaterialForDebris;
			}
			if (e.material.isRope)
			{
				edgeDefinition.material = ropeOrCableMaterialForDebris;
			}
			NodeHandle node = e.node1;
			edgeDefinition.lengthOverride = (1f - num2) * e.solverEdge.length;
			NodeHandle node2 = ((e.material.isSpring || e.material.isRope) ? node : nodeHandle2);
			NodeHandle node3 = ((e.material.isSpring || e.material.isRope) ? nodeHandle2 : node);
			EdgeHandle edgeHandle = World.CreateEdge_Inner(node2, node3, edgeDefinition);
			edgeDefinition.lengthOverride = num * e.solverEdge.length;
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
			DefaultEdgeListener.MaybeSplitRope(e.unityEdgeComponent, num, num2, edgeHandle2.unityEdgeComponent, edgeHandle.unityEdgeComponent, out var newRopes, srcNodes);
			Rope[] array = newRopes;
			foreach (Rope obj in array)
			{
				obj.userData = brokenEdge;
				obj.visualize = false;
			}
			BridgeJoint bridgeJoint = CreateBridgeJointFromNode(nodeHandle.unityNodeComponent, brokenEdge.transform.position.z);
			BridgeJoint bridgeJoint2 = CreateBridgeJointFromNode(nodeHandle2.unityNodeComponent, brokenEdge.transform.position.z);
			if (!reuseOriginalEdge)
			{
				BridgeEdge bridgeEdge = CreateBridgeEdgeFromEdge(brokenEdge.m_JointA, bridgeJoint, edgeHandle2.unityEdgeComponent, brokenEdge.m_Material.m_MaterialType);
				if ((bool)bridgeEdge)
				{
					bridgeEdge.m_MeshRenderer.gameObject.SetActive(!originalMaterial.isRope && !originalMaterial.isSpring);
					if (newRopes != null && newRopes.Length == 2)
					{
						newRopes[0].userData = bridgeEdge;
					}
				}
			}
			else
			{
				brokenEdge.m_MeshRenderer.gameObject.SetActive(!originalMaterial.isRope && !originalMaterial.isSpring);
				brokenEdge.TryRecreateSpringVisualization();
			}
			BridgeJoint a = ((brokenEdge.m_Material.m_MaterialType == BridgeMaterialType.SPRING) ? brokenEdge.m_JointB : bridgeJoint2);
			BridgeJoint b = ((brokenEdge.m_Material.m_MaterialType == BridgeMaterialType.SPRING) ? bridgeJoint2 : brokenEdge.m_JointB);
			BridgeEdge bridgeEdge2 = CreateBridgeEdgeFromEdge(a, b, edgeHandle.unityEdgeComponent, brokenEdge.m_Material.m_MaterialType);
			if ((bool)bridgeEdge2)
			{
				bridgeEdge2.m_MeshRenderer.gameObject.SetActive(!originalMaterial.isRope && !originalMaterial.isSpring);
				if (newRopes != null && newRopes.Length == 2)
				{
					newRopes[1].userData = bridgeEdge2;
				}
			}
			if ((bool)brokenEdge.m_HydraulicEdgeVisualization)
			{
				if (brokenEdge.m_HydraulicEdgeVisualization.isReversed ^ brokenEdge.m_PhysicsEdge.areNodesReversedInPhysics)
				{
					bridgeEdge2.m_HydraulicEdgeVisualization = bridgeEdge2.GetComponentInChildren<BridgeHydraulicEdgeVisualization>();
					if ((bool)bridgeEdge2.m_HydraulicEdgeVisualization)
					{
						bridgeEdge2.m_HydraulicEdgeVisualization.basePartLength = brokenEdge.m_HydraulicEdgeVisualization.basePartLength;
					}
					Object.Destroy(brokenEdge.m_HydraulicEdgeVisualization.gameObject);
				}
				else
				{
					bridgeEdge2.CreateHydraulicVisualization();
				}
			}
			if ((bool)brokenEdge.m_SpringCoilVisualization && bridgeEdge2.IsSpring())
			{
				BridgeSprings.CreateSpring(bridgeEdge2, 0.5f, brokenEdge.m_SpringCoilVisualization.m_Guid);
			}
			brokenEdge.m_IsBroken = true;
			if (!reuseOriginalEdge)
			{
				brokenEdge.gameObject.SetActive(value: false);
				brokenEdge.m_JointA.HideCapIfNoConnectedEdges();
				brokenEdge.m_JointB.HideCapIfNoConnectedEdges();
			}
			else
			{
				brokenEdge.m_JointB.UnregisterEdgeFromCache(brokenEdge);
				brokenEdge.m_JointB = bridgeJoint;
				brokenEdge.m_JointBPart = SplitJointPart.A;
				brokenEdge.m_JointB.RegisterEdgeInCache(brokenEdge);
			}
			BridgeAudio.PlayBreakEdge(brokenEdge.m_Material.m_MaterialType, brokenEdge.transform.position);
			if (debug_notifyOnFirstBreak)
			{
				debug_notifyOnFirstBreak = false;
				debug_edgeBroke = true;
			}
		}

		private BridgeJoint CreateBridgeJointFromNode(Node node, float z)
		{
			BridgeJoint bridgeJoint = BridgeJoints.CreateDebris(new Vector3(node.transform.position.x, node.transform.position.y, z));
			if ((bool)bridgeJoint)
			{
				bridgeJoint.m_IsDebris = true;
				bridgeJoint.m_PhysicsNode = node;
				bridgeJoint.gameObject.name += " (Debris)";
			}
			return bridgeJoint;
		}

		private BridgeEdge CreateBridgeEdgeFromEdge(BridgeJoint A, BridgeJoint B, Edge physicsEdge, BridgeMaterialType materialType)
		{
			BridgeEdge bridgeEdge = BridgeEdges.CreateEdge(A, B, materialType, string.Empty, physicsEdge);
			if (bridgeEdge != null)
			{
				bridgeEdge.m_IsDebris = true;
				bridgeEdge.m_PhysicsEdge = physicsEdge;
				bridgeEdge.gameObject.name += " (Debris)";
			}
			return bridgeEdge;
		}
	}
}
