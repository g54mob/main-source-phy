using System;
using System.Runtime.CompilerServices;
using Poly.Base;
using Poly.Collide;
using Poly.Determinism;
using Poly.Math;
using Poly.UI;
using UnityEngine;

namespace Poly.Physics
{
	public class Edge : WorldObject, IComparable
	{
		public enum Type
		{
			Obsidian = 0,
			Road = 1,
			Wood = 2,
			Steel = 3,
			Hydraulic_unused = 4,
			Rope = 5,
			Cable = 6,
			RoadWithWood = 7,
			RoadWithSteel = 8,
			Test_One = 9,
			Test_Two = 10,
			Test_Ten = 11
		}

		public bool enableHydraulics;

		[ShowIf("enableHydraulics", false, false, "")]
		public HydraulicsDefinition hydraulicsDefine;

		public float freeLengthOverride = -1f;

		[NonSerialized]
		public EdgeHandle handle;

		[NonSerialized]
		[Header("Debug View")]
		public EdgeHandle handleImage;

		[NonSerialized]
		public Shape shapeImage;

		public Node node0;

		public Part partOn0;

		public Node node1;

		public Part partOn1;

		public CollisionGroup collisionGroup = CollisionGroup.Bridge;

		public Layer layer;

		public EdgeMaterial material;

		public bool excludeFromMaxStressCalculation;

		public float nodeDirectionMultiplier = 1f;

		public bool isTemporaryPin { get; set; }

		public bool enableCollision => material.enableCollision;

		public bool isAddedToWorld
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if ((bool)handle)
				{
					return LoggingBehaviour.Exists(handle.world);
				}
				return false;
			}
		}

		public bool areNodesReversedInPhysics { get; internal set; }

		public Vec2 smoothPos
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.5f * (node0.smoothPos + node1.smoothPos);
			}
		}

		internal float smoothedStressSigned { get; set; }

		internal float smoothedStressNormalized { get; set; }

		public Vec2 direction_slow_unused => ((Vec2)(node1.transform.position - node0.transform.position)).normalized;

		public float collisionRadius => material.collisionRadius;

		private new void Awake()
		{
			base.Awake();
		}

		private new void OnDestroy()
		{
			base.OnDestroy();
		}

		private new void OnEnable()
		{
			base.OnEnable();
			Registry<Edge>.Add(this);
			RegisterWithConnectedUnityNodes();
		}

		private new void OnDisable()
		{
			base.OnEnable();
			Registry<Edge>.Remove(this);
			UnregisterWithConnectedUnityNodes();
		}

		public void InitBeforeStart(Node node0, Node node1)
		{
			UnregisterWithConnectedUnityNodes();
			this.node0 = node0;
			this.node1 = node1;
			RegisterWithConnectedUnityNodes();
		}

		private new void Start()
		{
			base.Start();
			CheckOrCreateQuadMesh();
			LineRenderer component = GetComponent<LineRenderer>();
			if ((bool)component)
			{
				UnityEngine.Object.Destroy(component);
			}
		}

		private void RegisterWithConnectedUnityNodes()
		{
			if ((bool)node0)
			{
				node0.edges.Add(this);
			}
			if ((bool)node1)
			{
				node1.edges.Add(this);
			}
		}

		public void UnregisterWithConnectedUnityNodes()
		{
			if ((bool)node0)
			{
				node0.edges.Remove(this);
			}
			if ((bool)node1)
			{
				node1.edges.Remove(this);
			}
		}

		private void RegisterWithConnectedNodes()
		{
			if ((bool)handle && handle.isAddedToWorld)
			{
				if ((bool)handle.node0)
				{
					handle.node0.edges.Add(handle);
				}
				if ((bool)handle.node1)
				{
					handle.node1.edges.Add(handle);
				}
			}
		}

		public void UnregisterWithConnectedNodes()
		{
			if ((bool)handle && handle.isAddedToWorld)
			{
				if ((bool)handle.node0)
				{
					handle.node0.edges.Remove(handle);
				}
				if ((bool)handle.node1)
				{
					handle.node1.edges.Remove(handle);
				}
			}
		}

		public void UpdateBaseColor()
		{
		}

		public ShapeDefinition CreateShapeDefinition(EdgeHandle hack_handle)
		{
			return new ShapeDefinition
			{
				enableCollision = enableCollision,
				type = Shape.Type.Segment,
				radius = material.collisionRadius,
				vertices = null,
				physicsMaterial = material.physicsMaterial,
				collisionGroup = (short)collisionGroup,
				layer = layer,
				recollisionType = RecollisionType.Full_RoadSegment,
				tmpSurfaceVelocity = 0f,
				lengthX = hack_handle.solverEdge.length
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LateUpdate_Manual(bool showEdges, Camera mainCamera)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PostFixedUpdate_Manual(float fixedDeltaTime)
		{
			if (isAddedToWorld)
			{
				float stressNormalizedSigned = handle.stressNormalizedSigned;
				float num = System.Math.Abs(stressNormalizedSigned);
				float smoothing = 1f;
				smoothedStressSigned = Smoothing.Smooth(smoothedStressSigned, stressNormalizedSigned, smoothing, fixedDeltaTime);
				float num2 = 1f;
				float num3 = smoothedStressNormalized - num2 * fixedDeltaTime;
				float num4 = num;
				smoothedStressNormalized = ((num3 < num4) ? num4 : num3);
			}
		}

		public Part GetPartOnNode(Node n)
		{
			if (!(n == node0))
			{
				return partOn1;
			}
			return partOn0;
		}

		public void SetPartOnNode_Once(Node n, Part newPart)
		{
			if (n == node0)
			{
				partOn0 = newPart;
			}
			else
			{
				partOn1 = newPart;
			}
			if (newPart == Part.B)
			{
				n.willSplit = true;
			}
		}

		public void ReplaceNodePart(Node oldNode, NodePart newNodePart)
		{
			UnregisterWithConnectedNodes();
			UnregisterWithConnectedUnityNodes();
			if (oldNode == node0)
			{
				node0 = newNodePart.node;
				partOn0 = newNodePart.part;
			}
			else
			{
				node1 = newNodePart.node;
				partOn1 = newNodePart.part;
			}
			if ((bool)handle)
			{
				handle.node0 = node0.handle;
				handle.node1 = node1.handle;
				handle.ResetNodeIndices();
			}
			RegisterWithConnectedNodes();
			RegisterWithConnectedUnityNodes();
		}

		public new void OnValidate()
		{
			base.OnValidate();
		}

		public bool Update_ShouldDestroySelf()
		{
			if ((bool)node0)
			{
				return !node1;
			}
			return true;
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			Edge edge = obj as Edge;
			if ((bool)this && (bool)edge)
			{
				if (node0 == null || node1 == null)
				{
					if (!(edge.node0 != null) || !(edge.node1 != null))
					{
						return 0;
					}
					return -1;
				}
				Node a = node0;
				Node b = node1;
				Node a2 = edge.node0;
				Node b2 = edge.node1;
				if (a.CompareTo(b) < 0)
				{
					Values.Swap(ref a, ref b);
				}
				if (a2.CompareTo(b2) < 0)
				{
					Values.Swap(ref a2, ref b2);
				}
				int num = a.CompareTo(a2);
				if (num == 0)
				{
					num = b.CompareTo(b2);
				}
				return num;
			}
			return 0;
		}

		private void CheckOrCreateQuadMesh()
		{
		}
	}
}
