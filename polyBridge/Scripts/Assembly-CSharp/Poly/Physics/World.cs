using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Collide;
using Poly.Determinism;
using Poly.Draw;
using Poly.Extension;
using Poly.Game;
using Poly.Math;
using Poly.Physics.Gameplay;
using Poly.Physics.Unity;
using Poly.Physics.Viewers;
using Poly.Solver;
using Poly.Timers;
using UnityEngine;

namespace Poly.Physics
{
	public class World : SingletonBehaviour<World>, ISerializationCallbackReceiver
	{
		internal List<NodeHandle> nodeHandles = new List<NodeHandle>();

		internal List<EdgeHandle> edgeHandles = new List<EdgeHandle>();

		internal List<FastAabbTrigger> fastTriggers = new List<FastAabbTrigger>();

		public Node splitNodePartPrefab;

		public static bool debug_useBurstJobs = true;

		public static readonly int m_PhysicsEngineVersion = 2;

		public SolverSettings settings;

		public bool areEdgesBreakable = true;

		private const float default_broadphaseHalfSize = 307.67f;

		public Bounds2 bounds = new Bounds2(Vec2.zero, Vec2.one * 2f * 307.67f);

		[Header("Show Physics Elements")]
		[Tooltip("Only effective for newly-added Nodes.")]
		public bool showNodes = true;

		[Tooltip("Only effective for newly-added Edges.")]
		public bool showEdges = true;

		[Header("Show Debug Info")]
		public bool showNodeIndices;

		public bool showEdgeIndices;

		public bool showStressNumbers;

		public bool showStressBar;

		public float stressLabelZ;

		public Font handlesFont;

		public int handlesFontSize;

		[Header("Other Debug")]
		public bool runValidationChecksEveryFrame;

		public bool updateDataImagesInUnityComponents = true;

		public bool updateNodePositionsFromEditor = true;

		public bool updateBodyTransformFromEditor = true;

		[Header("Debug Framerate")]
		public bool forceOneSimulationStepPerFrame;

		public int fastForwardToFrame;

		[NonSerialized]
		[Header("Experimental settings")]
		public bool onlyDisableShapesInsteadOfRemovingThem = true;

		[NonSerialized]
		public bool fixEdgeDirectionality = true;

		[NonSerialized]
		public List<INodeListener> nodeListeners = new List<INodeListener>();

		[NonSerialized]
		public List<IEdgeListener> edgeListeners = new List<IEdgeListener>();

		[NonSerialized]
		public List<IEdgeBreakListener> edgeBreakListeners = new List<IEdgeBreakListener>();

		[NonSerialized]
		public List<IShapeListener> shapeListeners = new List<IShapeListener>();

		[NonSerialized]
		public List<IActionListener> actionListeners = new List<IActionListener>();

		[NonSerialized]
		public List<IWorldListener> worldListeners = new List<IWorldListener>();

		[NonSerialized]
		public List<IHydraulicListener> hydraulicListeners = new List<IHydraulicListener>();

		[NonSerialized]
		public CollisionLayerManager layerManager;

		public static ShapeHandle[] shapeHandleArray;

		internal List<Rigidbody> bodies = new List<Rigidbody>();

		internal List<DynamicAnchorJoint> dynamicAnchorJoints = new List<DynamicAnchorJoint>();

		internal List<Joint> joints = new List<Joint>();

		internal List<Joint> customShapeJoints = new List<Joint>();

		internal List<Action> actions = new List<Action>();

		internal Collide collide;

		internal PersistentCollisionCache persistentCache;

		internal HashSet<EdgeHandle> dirtyEdges = new HashSet<EdgeHandle>();

		private OneAxisSweepAndPrune broadphase;

		[NonSerialized]
		public bool autoPlay = true;

		internal List<EdgeHandle> edgesWithMotions = new List<EdgeHandle>();

		internal List<short> solverRun_segmentMotionIndices = new List<short>();

		private FastList<Poly.Solver.Motion> solverMotionsAL = new FastList<Poly.Solver.Motion>(16);

		private FastList<SolverNode> solverNodesAL = new FastList<SolverNode>(16);

		private SolverEdge[] solverEdges = new SolverEdge[0];

		private FastList<int> broadphasePairs = new FastList<int>(16);

		private FastList<int> aabbTriggerPairs = new FastList<int>(16);

		private WorldCollisionInput collisionInput;

		private WorldCollisionOutput collisionOutputStruct;

		private List<EdgeHandle> brokenEdges = new List<EdgeHandle>();

		public float maxMomentaryStressNormalized { get; private set; }

		public float maxMomentaryStressNormalizedSmoothed { get; private set; }

		public bool areStuckRoadsBroken { get; set; }

		public float timeElapsed { get; private set; }

		public int frameCount { get; set; }

		private float lastFixedUpdateTime { get; set; }

		internal float currentFractionOfFixedFrame { get; private set; }

		public static bool isQuitting { get; private set; }

		public static float timeElapsedSafe
		{
			get
			{
				if ((bool)SingletonBehaviour<World>._instance)
				{
					return SingletonBehaviour<World>.instance.timeElapsed;
				}
				return 0f;
			}
		}

		private void AddRemoveWorldObjects()
		{
			AddRemoveNodes();
			if (fixEdgeDirectionality)
			{
				FixDirectionOfNewEdges();
			}
			AddRemoveEdges();
			AddRemoveBodies();
			AddRemoveDynamicAnchorJoints();
			AddRemoveJoints();
			AddRemoveActions();
		}

		private void AddRemoveNodes()
		{
			if (0 >= Registry<Node>.EventCount)
			{
				return;
			}
			Registry<Node>.Event[] array = Registry<Node>.SortAndGetEvents().ToArray();
			Registry<Node>.Clear();
			Registry<Node>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Node>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleNode(obj.item);
			}
		}

		private void FixDirectionOfNewEdges()
		{
			foreach (Registry<Edge>.Event item2 in Registry<Edge>.GetAllEvents_DontModify())
			{
				if (item2.type == RegistryOp.Add)
				{
					Edge item = item2.item;
					if (item.node1.CompareTo(item.node0) < 0 && !item.handle)
					{
						Values.Swap(ref item.node0, ref item.node1);
						Values.Swap(ref item.partOn0, ref item.partOn1);
						item.areNodesReversedInPhysics = true;
						item.nodeDirectionMultiplier = -1f;
					}
				}
			}
		}

		private void AddRemoveEdges()
		{
			if (0 >= Registry<Edge>.EventCount)
			{
				return;
			}
			Registry<Edge>.Event[] array = Registry<Edge>.SortAndGetEvents().ToArray();
			Registry<Edge>.Clear();
			Registry<Edge>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Edge>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleEdge(obj.item);
			}
		}

		private void AddRemoveBodies()
		{
			if (0 >= Registry<Rigidbody>.EventCount)
			{
				return;
			}
			Registry<Rigidbody>.Event[] array = Registry<Rigidbody>.SortAndGetEvents().ToArray();
			Registry<Rigidbody>.Clear();
			Registry<Rigidbody>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Rigidbody>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleBody(obj.item);
			}
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Rigidbody>.Event obj2 = array2[i];
				if (obj2.type == RegistryOp.Add && obj2.item._shapeHandleIndices != null)
				{
					ShapeHandleIndex[] shapeHandleIndices = obj2.item._shapeHandleIndices;
					foreach (ShapeHandleIndex shapeHandleIndex in shapeHandleIndices)
					{
						shapeHandleIndex.Get().collisionGroup = obj2.item.collisionGroup;
					}
				}
			}
		}

		private void AddRemoveDynamicAnchorJoints()
		{
			if (0 >= Registry<DynamicAnchorJoint>.EventCount)
			{
				return;
			}
			Registry<DynamicAnchorJoint>.Event[] array = Registry<DynamicAnchorJoint>.SortAndGetEvents().ToArray();
			Registry<DynamicAnchorJoint>.Clear();
			Registry<DynamicAnchorJoint>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<DynamicAnchorJoint>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleDynamicAnchorJoint(obj.item);
			}
		}

		private void AddRemoveJoints()
		{
			if (0 >= Registry<Joint>.EventCount)
			{
				return;
			}
			Registry<Joint>.Event[] array = Registry<Joint>.SortAndGetEvents().ToArray();
			Registry<Joint>.Clear();
			Registry<Joint>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Joint>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleJoint(obj.item);
			}
		}

		private void AddRemoveActions()
		{
			if (0 >= Registry<Action>.EventCount)
			{
				return;
			}
			Registry<Action>.Event[] array = Registry<Action>.SortAndGetEvents().ToArray();
			Registry<Action>.Clear();
			Registry<Action>.Event[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Registry<Action>.Event obj = array2[i];
				switch (obj.type)
				{
				case RegistryOp.Add:
					obj.item.isRegistered = true;
					break;
				case RegistryOp.Remove:
					obj.item.isRegistered = false;
					break;
				}
				AddRemoveSingleAction(obj.item);
			}
		}

		private void AddRemoveSingleNode(Node n)
		{
			bool isRegistered = n.isRegistered;
			if (isRegistered != n.isAddedToWorld)
			{
				if (isRegistered)
				{
					AddNodeImmediately(n);
				}
				else
				{
					RemoveNode(n);
				}
			}
		}

		private void AddRemoveSingleEdge(Edge e)
		{
			bool flag = e.isRegistered && (bool)e.node0 && e.node0.isAddedToWorld && (bool)e.node1 && e.node1.isAddedToWorld;
			if (flag != e.isAddedToWorld)
			{
				if (flag)
				{
					AddEdge(e);
				}
				else
				{
					RemoveEdge(e);
				}
			}
		}

		private void AddRemoveSingleBody(Rigidbody b)
		{
			bool isRegistered = b.isRegistered;
			if (isRegistered != b.isAddedToWorld)
			{
				if (isRegistered)
				{
					AddBody(b);
				}
				else
				{
					RemoveBody(b);
				}
			}
		}

		private void AddRemoveSingleDynamicAnchorJoint(DynamicAnchorJoint j)
		{
			bool flag = true;
			flag &= j.body.isAddedToWorld;
			if ((bool)j.connectedNode)
			{
				flag &= j.connectedNode.isAddedToWorld;
			}
			bool flag2 = flag;
			if (flag2 != j.isAddedToWorld)
			{
				if (flag2)
				{
					AddDynamicAnchorJoint(j);
				}
				else
				{
					RemoveDynamicAnchorJoint(j);
				}
			}
		}

		private void AddRemoveSingleJoint(Joint j)
		{
			bool flag = true;
			flag &= j.body.isAddedToWorld;
			if ((bool)j.connectedBody)
			{
				flag &= j.connectedBody.isAddedToWorld;
			}
			bool flag2 = flag;
			if (flag2 != j.isAddedToWorld)
			{
				if (flag2)
				{
					AddJoint(j);
				}
				else
				{
					RemoveJoint(j);
				}
			}
		}

		private void AddRemoveSingleAction(Action a)
		{
			bool isRegistered = a.isRegistered;
			if (isRegistered != a.isAddedToWorld)
			{
				if (isRegistered)
				{
					AddAction(a);
				}
				else
				{
					RemoveAction(a);
				}
			}
		}

		internal void AddNodeHandle_Full(Node node, bool bindToNode)
		{
			AddNode(node.handle);
			if (node.willSplit && (bool)layerManager)
			{
				layerManager.RegisterSplittableNode(node.handle);
			}
		}

		internal void AddNodeImmediately(Node node)
		{
			node.handle.SetKinematic(node.define.isKinematic);
			node.handle.SetMass(node.define.mass);
			SplitMultiPartNode_IfNeeded(node, this, HydraulicController.instance, hydraulicListeners);
			if (!node.isAddedToWorld)
			{
				AddNodeHandle_Full(node, bindToNode: false);
			}
			foreach (Edge edge in node.edges)
			{
				if (edge.isRegistered)
				{
					AddRemoveSingleEdge(edge);
				}
			}
			node.GetComponentsInChildren<Renderer>().ToList().ForEach(delegate(Renderer r)
			{
				r.enabled = showNodes;
			});
			DeterminismLog.LogEvent(node, Poly.Determinism.EventType.AddToWorld);
		}

		internal void RemoveNode(Node node)
		{
			if ((bool)node.handle)
			{
				node.doNotDestroy = true;
				_ = node.handle;
				RemoveNode(node.handle);
			}
		}

		internal static EdgeHandle CreateEdgeHandle_Full(Edge edge)
		{
			EdgeDefinition edgeDefinition = new EdgeDefinition();
			edgeDefinition.material = edge.material;
			edgeDefinition.lengthOverride = edge.freeLengthOverride;
			edge.node0.InitOnce();
			edge.node1.InitOnce();
			EdgeHandle edgeHandle = CreateEdge_Inner(edge.node0.handle, edge.node1.handle, edgeDefinition);
			edgeHandle.solverEdge.excludeFromMaxStressCalculation = edge.excludeFromMaxStressCalculation;
			if (edge.enableCollision)
			{
				ShapeHandle newShapeHandle = Shape.CreateShapeAndHandle(edge.CreateShapeDefinition(edgeHandle));
				edgeHandle.SetShape(ref newShapeHandle);
			}
			return edgeHandle;
		}

		internal void AddEdge(Edge edge)
		{
			if (!edge.handle)
			{
				EdgeHandle edgeHandle = CreateEdgeHandle_Full(edge);
				WorldSyncUtilListener.existingEdgeBeingProcessed = edge;
				edgeHandle.userData = edge.userData;
				AddEdge(edgeHandle);
				edge.handle = edgeHandle;
				if (edge.enableHydraulics)
				{
					Hydraulics h = Hydraulics.Create(edge.handle, edge.hydraulicsDefine);
					HydraulicController.instance.RegisterHydro(h, edge.handle);
				}
				edge.UpdateBaseColor();
			}
			edge.GetComponentsInChildren<Renderer>().ToList().ForEach(delegate(Renderer r)
			{
				r.enabled = showNodes;
			});
			DeterminismLog.LogEvent(edge, Poly.Determinism.EventType.AddToWorld);
		}

		internal void RemoveEdge(Edge edge)
		{
			if ((bool)edge.handle)
			{
				WorldSyncUtilListener.existingEdgeBeingProcessed = edge;
				EdgeHandle handle = edge.handle;
				RemoveEdge(edge.handle);
				DestroyEdge(handle);
				edge.handle = null;
			}
		}

		public bool AddNode(NodeHandle node)
		{
			if (nodeHandles.Count <= 32767)
			{
				node.SetWorldAndIndex(this, nodeHandles.Count);
				nodeHandles.Add(node);
				AddShapeOfNode(node);
				foreach (INodeListener nodeListener in nodeListeners)
				{
					nodeListener.OnNodeAdded(node);
				}
				return true;
			}
			return false;
		}

		public void RemoveNode(NodeHandle node)
		{
			foreach (INodeListener nodeListener in nodeListeners)
			{
				nodeListener.OnNodeRemoved(node);
			}
			for (int num = node.edges.Count - 1; num >= 0; num--)
			{
				RemoveEdge(node.edges[num]);
			}
			if (node.shapeHandleIndex.isValid)
			{
				RemoveShapeOfNode(node);
			}
			NodeHandle nodeHandle = nodeHandles.RemoveAtAndSwap(node.worldIdx);
			nodeHandle.SetWorldAndIndex(this, node.worldIdx);
			node.SetWorldAndIndex(null, -1);
			foreach (EdgeHandle edge in nodeHandle.edges)
			{
				edge.ResetNodeIndices();
			}
		}

		public static EdgeHandle CreateEdge_Inner(NodeHandle node0, NodeHandle node1, EdgeDefinition define)
		{
			EdgeHandle edgeHandle = null;
			edgeHandle = new EdgeHandle(node0, node1);
			edgeHandle.solverEdge.InitDefaults();
			if (define.lengthOverride >= 0f)
			{
				edgeHandle.solverEdge.length = define.lengthOverride;
			}
			else
			{
				edgeHandle.solverEdge.length = Vec2.Distance(node0.pos, node1.pos);
			}
			edgeHandle.originalLength = edgeHandle.solverEdge.length;
			edgeHandle.material = define.material;
			edgeHandle.originalMaterial = edgeHandle.material;
			edgeHandle.maxForce = float.PositiveInfinity;
			return edgeHandle;
		}

		public bool AddEdge(EdgeHandle edge, bool recalcLength = false)
		{
			if (edgeHandles.Count <= 32767)
			{
				edge.ResetNodeIndices();
				if (recalcLength)
				{
					edge.solverEdge.length = Vec2.Distance(edge.node0.pos, edge.node1.pos);
					edge.originalLength = edge.solverEdge.length;
				}
				if ((bool)edge.material)
				{
					edge.maxForce = edge.material.strength * settings.strengthMultiplier;
					edge.solverEdge.isRope = edge.material.isRope;
					edge.solverEdge.maxTensionImpulseFactor = edge.material.tensionStrengthFactor;
				}
				else
				{
					edge.solverEdge.maxTensionImpulseFactor = 1f;
				}
				edge.solverEdge.maxImpulsePerIntegration = edge.maxForce * settings.deltaTimeForVelocityEdge * settings.deltaTimeForVelocityEdge * edge.maxForce_ActualFraction;
				float num = edge.node0.solverNode.invMass + edge.node1.solverNode.invMass;
				edge.solverEdge.virtualMass = ((num != 0f) ? (1f / num) : 0f);
				edge.solverEdge.invMassA = edge.node0.solverNode.invMass;
				edge.solverEdge.invMassB = edge.node1.solverNode.invMass;
				edge.SetWorldAndIndex(this, edgeHandles.Count);
				edgeHandles.Add(edge);
				edge.node0.edges.Add(edge);
				edge.node1.edges.Add(edge);
				dirtyEdges.Add(edge);
				AddShapeOfEdge(edge);
				foreach (IEdgeListener edgeListener in edgeListeners)
				{
					edgeListener.OnEdgeAdded(edge);
				}
				return true;
			}
			return false;
		}

		public EdgeHandle ReCreateEdge_InWorld_CopyPasted(EdgeHandle edge, NodeHandle node0, NodeHandle node1, EdgeDefinition define)
		{
			edge.solverEdge.InitDefaults();
			edge.solverEdge.nodeIdxA = node0.worldIdx;
			edge.solverEdge.nodeIdxB = node1.worldIdx;
			if (define.lengthOverride >= 0f)
			{
				edge.solverEdge.length = define.lengthOverride;
			}
			else
			{
				edge.solverEdge.length = Vec2.Distance(node0.pos, node1.pos);
			}
			edge.originalLength = edge.solverEdge.length;
			edge.material = define.material;
			edge.originalMaterial = edge.material;
			edge.maxForce = float.PositiveInfinity;
			if ((bool)edge.material)
			{
				edge.maxForce = edge.material.strength * settings.strengthMultiplier;
				edge.solverEdge.isRope = edge.material.isRope;
				edge.solverEdge.maxTensionImpulseFactor = edge.material.tensionStrengthFactor;
			}
			else
			{
				edge.solverEdge.maxTensionImpulseFactor = 1f;
			}
			edge.solverEdge.maxImpulsePerIntegration = edge.maxForce * settings.deltaTimeForVelocityEdge * settings.deltaTimeForVelocityEdge * edge.maxForce_ActualFraction;
			float num = node0.solverNode.invMass + node1.solverNode.invMass;
			edge.solverEdge.virtualMass = ((num != 0f) ? (1f / num) : 0f);
			edge.solverEdge.invMassA = node0.solverNode.invMass;
			edge.solverEdge.invMassB = node1.solverNode.invMass;
			edge.solverEdge.sumVelImpulses = 0f;
			edge.solverEdge.sumFullImpulses = 0f;
			edge.solverEdge.sumFullImpulsesInFrame = 0f;
			if (edge.solverEdge.pin_isUsing2d)
			{
				edge.solverEdge.pin_isUsing2d = false;
				edge.solverEdge.lengthVelocity = 0f;
			}
			dirtyEdges.Add(edge);
			if ((bool)edge.unityEdgeComponent)
			{
				edge.unityEdgeComponent.material = edge.material;
				edge.unityEdgeComponent.UpdateBaseColor();
			}
			return edge;
		}

		public void RemoveEdge(EdgeHandle edge)
		{
			foreach (IEdgeListener edgeListener in edgeListeners)
			{
				edgeListener.OnEdgeRemoved(edge);
			}
			edge.node0.edges.Remove(edge);
			edge.node1.edges.Remove(edge);
			if (edge.shapeHandleIndex.isValid)
			{
				RemoveShapeOfEdge(edge);
			}
			edgeHandles.RemoveAtAndSwap(edge.worldIdx).SetWorldAndIndex(this, edge.worldIdx);
			edge.SetWorldAndIndex(null, -1);
		}

		public static void DestroyEdge(EdgeHandle edge)
		{
			edge.node0 = null;
			edge.node1 = null;
			edge.ReleaseShape();
		}

		public bool AddFastTrigger(FastAabbTrigger t)
		{
			if (nodeHandles.Count <= 32767)
			{
				AddShapeOfFastTrigger(t);
				t.SetWorldAndIndex(this, fastTriggers.Count);
				fastTriggers.Add(t);
				return true;
			}
			return false;
		}

		public void RemoveFastTrigger(FastAabbTrigger t)
		{
			fastTriggers.RemoveAtAndSwap(t.worldIdx).SetWorldAndIndex(this, t.worldIdx);
			t.SetWorldAndIndex(null, -1);
			if (t.shapeHandleIndex.isValid)
			{
				RemoveShapeOfFastTrigger(t);
			}
		}

		internal static void SplitMultiPartNode_IfNeeded(Node node, World world, HydraulicController hydraulicController, List<IHydraulicListener> hydraulicListeners)
		{
			Part[] nodeParts = GetNodeParts(node);
			Array.Sort(nodeParts);
			if (!node.isInitialized)
			{
				node.willSplit |= nodeParts.Length > 1;
				node.handle.isSplittableAnchor |= node.willSplit && node.define.isKinematic && nodeParts[0] == Part.A;
				node.handle.isAnchor |= node.handle.isSplittableAnchor;
				node.handle.isAnchor |= !node.willSplit && node.define.isKinematic;
			}
			if (!node.isInitialized && nodeParts.Length > 1)
			{
				node.willSplit = true;
				Dictionary<Part, Node> dictionary = new Dictionary<Part, Node>();
				dictionary.Add(nodeParts[0], node);
				for (int i = 1; i < nodeParts.Length; i++)
				{
					Node newSplitNode = UnityEngine.Object.Instantiate(node, node.transform.parent, worldPositionStays: true);
					if ((bool)world.splitNodePartPrefab)
					{
						newSplitNode.define.mass = world.splitNodePartPrefab.define.mass;
					}
					newSplitNode.isInitialized = true;
					Part part = nodeParts[i];
					dictionary.Add(part, newSplitNode);
					hydraulicController.RegisterNode_Part_Split(node, newSplitNode, part);
					hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
					{
						hl.OnNodeSplit(node, newSplitNode);
					});
				}
				hydraulicController.ExecuteNode_Part_Verify(node, nodeParts[0]);
				Edge[] array = node.edges.ToArray();
				for (int num = 0; num < array.Length; num++)
				{
					ReattachEdgeToNewSplitNodes(array[num], node, dictionary, hydraulicListeners);
				}
				node.newSplitParts_forHydraulicsOnly = dictionary.Values.ToArray();
				node.is3WaySplit = dictionary.ContainsKey(Part.C);
				List<EdgeHandle> list = new List<EdgeHandle>();
				NodeHandle[] array2 = dictionary.Values.Select((Node sp) => sp.handle).ToArray();
				EdgeDefinition edgeDefinition = new EdgeDefinition();
				edgeDefinition.InitDefaults();
				edgeDefinition.lengthOverride = 0f;
				edgeDefinition.material = SingletonBehaviour<VerletEditor>.instance.pinMaterial;
				for (int num2 = 0; num2 < array2.Length - 1; num2++)
				{
					for (int num3 = num2 + 1; num3 < array2.Length; num3++)
					{
						EdgeHandle edgeHandle = CreateEdge_Inner(array2[num2], array2[num3], edgeDefinition);
						edgeHandle.solverEdge.pin_isUnbreakable = true;
						list.Add(edgeHandle);
						array2[num2].pins.Add(edgeHandle);
						array2[num3].pins.Add(edgeHandle);
						if ((bool)edgeHandle.unityEdgeComponent)
						{
							edgeHandle.unityEdgeComponent.isTemporaryPin = true;
						}
					}
				}
				for (int num4 = 1; num4 < nodeParts.Length; num4++)
				{
					world.AddNodeImmediately(dictionary[nodeParts[num4]]);
				}
				world.AddNodeHandle_Full(node, bindToNode: false);
				foreach (EdgeHandle item in list)
				{
					world.AddEdge(item);
					if ((bool)item.unityEdgeComponent)
					{
						item.unityEdgeComponent.isTemporaryPin = true;
					}
				}
			}
			else if (!node.isInitialized)
			{
				hydraulicController.ExecuteNode_Part_Verify(node, nodeParts[0]);
			}
			node.isInitialized = true;
		}

		internal static void ReattachEdgeToNewSplitNodes(Edge edge, Node originalNode, Dictionary<Part, Node> newSplitNodes, List<IHydraulicListener> hydraulicListeners)
		{
			Node oldNode = edge.node0;
			Part part = edge.partOn0;
			Node newNode = ((oldNode == originalNode) ? newSplitNodes[edge.partOn0] : null);
			for (int i = 0; i < 2; i++)
			{
				if (oldNode == originalNode && originalNode != newNode)
				{
					edge.ReplaceNodePart(oldNode, new NodePart(newNode, part));
					oldNode.edges.Remove(edge);
					newNode.edges.Add(edge);
					hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
					{
						hl.OnEdgeReattached(edge, oldNode, newNode);
					});
					break;
				}
				oldNode = edge.node1;
				part = edge.partOn1;
				newNode = ((oldNode == originalNode) ? newSplitNodes[edge.partOn1] : null);
			}
		}

		internal static Part[] GetNodeParts(Node node)
		{
			List<Part> list = new List<Part>();
			bool[] array = new bool[3];
			if (node.define.isKinematic || ((bool)node.handle && node.handle.isSplittableAnchor) || node.edges.Count == 0)
			{
				array[0] = true;
				list.Add(Part.A);
			}
			foreach (Edge edge in node.edges)
			{
				if (!edge.isTemporaryPin)
				{
					Part partOnNode = edge.GetPartOnNode(node);
					if (!array[(int)partOnNode])
					{
						array[(int)partOnNode] = true;
						list.Add(partOnNode);
					}
				}
			}
			list.Sort();
			return list.ToArray();
		}

		public void AddShapeOfNode(NodeHandle nodeHandle)
		{
			if (!nodeHandle.shape || !nodeHandle.shapeHandle.HasValue)
			{
				return;
			}
			ShapeHandle h = nodeHandle.shapeHandle.Value;
			h.nodeIdx = nodeHandle.worldIdx;
			h.CacheTransform2(settings.nodeToMotionVelocityMultiplier);
			short num = collide.AddShapeHandle(ref h);
			nodeHandle.UpdateShapeHandleIndex(-1, num);
			nodeHandle.shape = null;
			nodeHandle.shapeHandle = null;
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeAdded(num);
			}
		}

		public void AddShapeOfFastTrigger(FastAabbTrigger trigger)
		{
			if (!trigger.shapeHandleIndex.isValid)
			{
				Shape shape = new AabbShape(trigger.bounds);
				ShapeHandle newShapeHandle = ShapeHandle.Create();
				newShapeHandle.shape = shape;
				newShapeHandle.collisionGroup = 0;
				newShapeHandle.layer = trigger.layer;
				newShapeHandle.isTrigger = true;
				trigger.SetShape(ref newShapeHandle);
				short newIndex = collide.AddShapeHandle(ref newShapeHandle);
				trigger.UpdateShapeHandleIndex(-1, newIndex);
			}
		}

		public void AddShapeOfEdge(EdgeHandle edgeHandle)
		{
			if (!edgeHandle.shape || !edgeHandle.shapeHandle.HasValue)
			{
				return;
			}
			ShapeHandle h = edgeHandle.shapeHandle.Value;
			if (edgeHandle.optional_motion.segment == null)
			{
				edgeHandle.optional_motion.segment = new SegmentMotionRef();
				edgeHandle.optional_motion.segment.worldIdx0 = edgeHandle.solverEdge.nodeIdxA;
				edgeHandle.optional_motion.segment.worldIdx1 = edgeHandle.solverEdge.nodeIdxB;
				edgeHandle.optional_motion.segment.currentStretchedLength = edgeHandle.length;
			}
			h.CacheTransform2(settings.nodeToMotionVelocityMultiplier);
			collide.numSegments++;
			short num = collide.AddShapeHandle(ref h);
			edgeHandle.UpdateShapeHandleIndex(-1, num);
			edgesWithMotions.Add(edgeHandle);
			dirtyEdges.Add(edgeHandle);
			edgeHandle.shape = null;
			edgeHandle.shapeHandle = null;
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeAdded(num);
			}
		}

		public void ModifyShapeOfNode(NodeHandle nodeHandle)
		{
			if (!nodeHandle.shapeHandleIndex.isValid)
			{
				return;
			}
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeModified(nodeHandle.shapeHandleIndex);
			}
		}

		public void ModifyShapeOfEdge(EdgeHandle edgeHandle)
		{
			if (!edgeHandle.shapeHandleIndex.isValid)
			{
				return;
			}
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeModified(edgeHandle.shapeHandleIndex);
			}
		}

		public void RemoveShapeOfNode(NodeHandle nodeHandle)
		{
			ref ShapeHandle reference = ref nodeHandle.shapeHandleIndex.Get();
			nodeHandle.shape = reference.shape;
			nodeHandle.shapeHandle = reference;
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeRemoved(nodeHandle.shapeHandleIndex);
			}
			if (collide != null)
			{
				collide.RemoveShapeHandle(nodeHandle.shapeHandleIndex);
			}
			nodeHandle.UpdateShapeHandleIndex(nodeHandle.shapeHandleIndex, -1);
		}

		public void RemoveShapeOfFastTrigger(FastAabbTrigger trigger)
		{
			trigger.shapeHandleIndex.Get();
			if (collide != null)
			{
				collide.RemoveShapeHandle(trigger.shapeHandleIndex);
			}
			trigger.UpdateShapeHandleIndex(trigger.shapeHandleIndex, -1);
		}

		public void RemoveShapeOfEdge(EdgeHandle edgeHandle)
		{
			ref ShapeHandle reference = ref edgeHandle.shapeHandleIndex.Get();
			edgeHandle.shape = reference.shape;
			edgeHandle.shapeHandle = reference;
			foreach (IShapeListener shapeListener in shapeListeners)
			{
				shapeListener.OnShapeRemoved(edgeHandle.shapeHandleIndex);
			}
			if (collide != null)
			{
				collide.RemoveShapeHandle(edgeHandle.shapeHandleIndex);
			}
			edgeHandle.UpdateShapeHandleIndex(edgeHandle.shapeHandleIndex, -1);
			if (collide != null)
			{
				SingletonBehaviour<World>.instance.collide.numSegments--;
			}
			edgesWithMotions.Remove(edgeHandle);
		}

		public void Clear()
		{
			collisionOutputStruct.Clear(edgeHandles);
			persistentCache.Clear();
			if ((bool)UberCollisionListener.instance)
			{
				UberCollisionListener.instance.VerifyReset();
			}
			Registry<Node>.Clear();
			Registry<Edge>.Clear();
			Registry<Rigidbody>.Clear();
			Registry<Joint>.Clear();
			Registry<Action>.Clear();
			Registry<DynamicAnchorJoint>.Clear();
			for (int num = edgeHandles.Count - 1; num >= 0; num--)
			{
				EdgeHandle edge = edgeHandles[num];
				RemoveEdge(edge);
				DestroyEdge(edge);
			}
			for (int num2 = nodeHandles.Count - 1; num2 >= 0; num2--)
			{
				NodeHandle node = nodeHandles[num2];
				RemoveNode(node);
				NodeHandle.DestroyNode(node);
			}
			bodies.ForEach(delegate(Rigidbody body)
			{
				if ((bool)body && (bool)body.gameObject)
				{
					UnityEngine.Object.Destroy(body.gameObject);
				}
			});
			actions.ForEach(delegate(Action action)
			{
				if ((bool)action)
				{
					UnityEngine.Object.Destroy(action.gameObject);
				}
			});
			AddRemoveWorldObjects();
			for (int num3 = fastTriggers.Count - 1; num3 >= 0; num3--)
			{
				FastAabbTrigger t = fastTriggers[num3];
				RemoveFastTrigger(t);
			}
			if ((bool)HydraulicController.instance)
			{
				HydraulicController.instance.Clear();
			}
			timeElapsed = 0f;
			frameCount = 0;
			lastFixedUpdateTime = Time.fixedTime;
			foreach (IWorldListener worldListener in worldListeners)
			{
				worldListener.AfterWorldCleared();
			}
			collide.Clear();
			GlDrawer.Clear();
			PerformanceTimerDisplay.Clear();
			ContactSolver.debugOnce_01 = false;
			maxMomentaryStressNormalizedSmoothed = 0f;
			Singleton<TriggerManager, int>.instance.Clear();
			splitNodePartPrefab = null;
		}

		private new void Awake()
		{
			base.Awake();
			collisionOutputStruct = new WorldCollisionOutput(unused: true);
			Collide.collisionTolerance = settings.collisionTolerance;
			Collide.maxContactPointDistance = settings.maxContactPointDistance;
			collide = new Collide();
			collide.world = this;
			collide.dispatcher = new CollisionDispatcherImpl();
			collide.dispatcher.Init();
			persistentCache = new PersistentCollisionCache();
			broadphase = new OneAxisSweepAndPrune();
			broadphase.collisionTolerance = settings.collisionTolerance;
		}

		private static void ClearRegistries()
		{
			Registry<Node>.Clear();
			Registry<Edge>.Clear();
			Registry<Rigidbody>.Clear();
			Registry<Joint>.Clear();
			Registry<DynamicAnchorJoint>.Clear();
			Registry<Action>.Clear();
		}

		private void Start()
		{
		}

		private new void OnDestroy()
		{
			collide = null;
			persistentCache.Clear();
			persistentCache = null;
			base.OnDestroy();
		}

		public void OnAfterDeserialize()
		{
			nodeHandles.Clear();
			edgeHandles.Clear();
			bodies.Clear();
			joints.Clear();
			customShapeJoints.Clear();
			dynamicAnchorJoints.Clear();
			dirtyEdges.Clear();
		}

		public void OnBeforeSerialize()
		{
		}

		public void Update()
		{
			UpdateCurrentFractionOfFixedFrame();
			if (forceOneSimulationStepPerFrame && autoPlay)
			{
				FixedUpdate_Manual();
			}
			if (showNodes || showEdges || showNodeIndices || showStressNumbers)
			{
				WorldSyncUtil.NonFixed_UpdateUnityNodes(nodeHandles, base.transform.position);
				WorldSyncUtil.LateUpdateUnityEdges(edgeHandles, updateDataImagesInUnityComponents);
			}
			foreach (IWorldListener worldListener in worldListeners)
			{
				worldListener.AfterWorldFrameUpdate();
			}
		}

		public void UpdateCurrentFractionOfFixedFrame()
		{
			currentFractionOfFixedFrame = (Time.time - lastFixedUpdateTime) / Time.fixedDeltaTime;
		}

		private void RemoveNanNodes()
		{
			for (int num = nodeHandles.Count - 1; num >= 0; num--)
			{
				NodeHandle nodeHandle = nodeHandles[num];
				if (float.IsNaN(nodeHandle.solverNode.pos.sqrMagnitude))
				{
					string text = (nodeHandle.unityNodeComponent ? nodeHandle.unityNodeComponent.gameObject.name : "(internal)");
					Debug.Log("Node exploded to infinity: " + text);
					RemoveNode(nodeHandle);
					NodeHandle.DestroyNode(nodeHandle);
				}
			}
		}

		private void FreezeNodesAndRigidbodiesOutsideOfBroadphase()
		{
			for (int num = nodeHandles.Count - 1; num >= 0; num--)
			{
				NodeHandle nodeHandle = nodeHandles[num];
				if (nodeHandle.solverNode.invMass != 0f && !bounds.Contains(in nodeHandle.solverNode.pos))
				{
					nodeHandle.SetKinematic(isKinematic: true);
					nodeHandle.isEnabled = false;
					foreach (EdgeHandle edge in nodeHandle.edges)
					{
						edge.solverEdge.stiffness = 0f;
						edge.solverEdge.damping = 0f;
						edge.isEnabled = false;
						if (edge.shapeHandleIndex.isValid)
						{
							edge.shapeHandleIndex.Get().layer = Layer.CollideNothing;
						}
					}
					foreach (EdgeHandle pin in nodeHandle.pins)
					{
						pin.solverEdge.stiffness = 0f;
						pin.solverEdge.damping = 0f;
						pin.isEnabled = false;
					}
				}
			}
			for (int num2 = bodies.Count - 1; num2 >= 0; num2--)
			{
				Rigidbody rigidbody = bodies[num2];
				if (rigidbody.mass != 0f && !bounds.Contains(in rigidbody.motion.com))
				{
					rigidbody.mass = 0f;
					rigidbody.inertia = 0f;
					rigidbody.motion.linVel = Vector2.zero;
					rigidbody.motion.angVel = 0f;
					ShapeHandleIndex[] shapeHandleIndices = rigidbody._shapeHandleIndices;
					foreach (ShapeHandleIndex shapeHandleIndex in shapeHandleIndices)
					{
						shapeHandleIndex.Get().layer = Layer.CollideNothing;
					}
				}
			}
		}

		private void FixedUpdate()
		{
			UpdateCurrentFractionOfFixedFrame();
			if (!forceOneSimulationStepPerFrame && autoPlay)
			{
				FixedUpdate_Manual();
				while (frameCount < fastForwardToFrame)
				{
					FixedUpdate_Manual();
				}
			}
		}

		public void FixedUpdate_Manual()
		{
			Poly.Solver.Solver.info.frameCount = frameCount;
			UpdateCurrentFractionOfFixedFrame();
			BridgeUnderWater.instance.Enable();
			HideVehiclesOutsideGameplayAreaListener.instance.Enable();
			GlDrawer.offset = base.transform.position;
			settings.CacheValuesForFrame(timeElapsed, areEdgesBreakable);
			if (!settings.debug_useCollisionCaches)
			{
				persistentCache.Clear();
			}
			GlDrawer.Clear();
			foreach (IWorldListener worldListener in worldListeners)
			{
				worldListener.BeforeStep();
			}
			Singleton<TriggerManager, int>.instance.UpdateOverlapChecks(this);
			Collide.collisionTolerance = settings.collisionTolerance;
			Collide.maxContactPointDistance = settings.maxContactPointDistance;
			broadphase.collisionTolerance = settings.collisionTolerance;
			AddRemoveWorldObjects();
			ProcessDirtyEdges();
			Validate();
			if ((bool)HydraulicController.instance)
			{
				List<EdgeHandle> other = HydraulicController.instance.FixedUpdate_Manual(settings.frameDeltaTime);
				dirtyEdges.AddRange(other);
				ProcessDirtyEdges();
			}
			foreach (Action action in actions)
			{
				action.Execute();
			}
			Validate();
			WorldValidator.ValidateWorldIdxOnly(this);
			if (updateNodePositionsFromEditor && 5 < frameCount)
			{
				WorldSyncUtil.FixedUpdateFromUnityNodes(nodeHandles, base.transform.position);
			}
			if (updateBodyTransformFromEditor && 5 < frameCount)
			{
				WorldSyncUtil.FixedUpdateFromUnityBodies(bodies, base.transform.position);
			}
			Step_ResizeAndInitNodeEdgeBody_AndMapEdgesToMotions();
			_ = nodeHandles.Count;
			_ = edgeHandles.Count;
			int count = bodies.Count;
			int numSegments = collide.numSegments;
			int numSolverBodies = count + numSegments;
			_ = solverNodesAL.array;
			Poly.Solver.Motion[] array = solverMotionsAL.array;
			for (int i = 0; i < joints.Count; i++)
			{
				joints[i].PrepForSolving(settings);
			}
			for (int j = 0; j < customShapeJoints.Count; j++)
			{
				customShapeJoints[j].PrepForSolving(settings);
			}
			for (int k = 0; k < dynamicAnchorJoints.Count; k++)
			{
				dynamicAnchorJoints[k].PrepForSolving(settings);
			}
			broadphasePairs.Clear();
			aabbTriggerPairs.Clear();
			Bounds2 bpBounds = bounds;
			bpBounds.Expand(100f);
			float velocityToDisplacement = settings.frameDeltaTime / settings.deltaTimeForVelocity;
			broadphase.FindPotentialPairs(collide.shapeHandles.array, collide.shapeHandles.Count, collide.filter, in bpBounds, ref broadphasePairs, ref aabbTriggerPairs, velocityToDisplacement);
			persistentCache.InvalidateCaches(collide.invalidateShapeIndices, collide.notifyShapeIndices_CorrectFrictionAnglesOnly, collide.bodyIdxToAngleCorrection);
			collide.invalidateShapeIndices.Clear();
			collide.notifyShapeIndices_CorrectFrictionAnglesOnly.Clear();
			collide.bodyIdxToAngleCorrection.Clear();
			collide.wereShapesRemovedLastFrame = false;
			if (settings.debug_useCollisionCaches)
			{
				PersistentCollisionCache.SortPairs(broadphasePairs);
			}
			persistentCache.UdpateCachesForPairs(broadphasePairs);
			collisionInput.shapeHandles = collide.shapeHandles.array;
			collisionInput.broadphasePairs = broadphasePairs;
			collisionInput.caches = persistentCache.caches;
			collisionInput.nodesPtr = solverNodesAL.array;
			collisionInput.solverMotionsPtr = array;
			collisionOutputStruct.Clear(edgeHandles);
			int newCapacity = 2 * broadphasePairs.Count;
			collisionOutputStruct.Reserve(newCapacity);
			FastTriggerManager.DetectOverlaps(collisionInput, aabbTriggerPairs);
			collide.DetectCollisions(collisionInput, collisionOutputStruct, settings);
			collisionOutputStruct.AssertCapacityUnchanged();
			Poly.Solver.Solver.Solve(solverNodesAL, solverEdges, bodies, numSolverBodies, solverRun_segmentMotionIndices, solverMotionsAL, dynamicAnchorJoints, joints, customShapeJoints, collisionOutputStruct.bodyContact.array, collisionOutputStruct.bodyContact.Count, collisionOutputStruct.bridgeContact.array, collisionOutputStruct.bridgeContact.Count, collisionOutputStruct.fullFrequencyBridgeContactIndices, settings, HydraulicController.instance, areEdgesBreakable);
			persistentCache.UpdateCachesFromCollisionInfos_Rigidbodies(collisionOutputStruct.bodyContact, collisionOutputStruct.collisionEvents);
			persistentCache.UpdateCachesFromCollisionInfos_Rigidbodies(collisionOutputStruct.bridgeContact, collisionOutputStruct.collisionEvents);
			maxMomentaryStressNormalized = Poly.Solver.Solver.CheckImpulseAccumulatorsForBreakage(solverEdges, settings);
			if (Singleton<TriggerManager, int>.instance.areNewEdgesBroken)
			{
				maxMomentaryStressNormalized = 1f;
			}
			Step_ExportNodeEdgeBody_AndBreakEdges();
			RemoveNanNodes();
			FreezeNodesAndRigidbodiesOutsideOfBroadphase();
			Step_ExportTransformsToShapes();
			WorldSyncUtil.FixedUpdateUnityEdges(edgeHandles);
			foreach (Action action2 in actions)
			{
				action2.LateExecute();
			}
			Validate();
			timeElapsed += Time.fixedDeltaTime;
			frameCount++;
			lastFixedUpdateTime = Time.fixedTime;
			foreach (IWorldListener worldListener2 in worldListeners)
			{
				worldListener2.AfterWorldFixedUpdate();
			}
			ContactPointViewer.Draw(collisionOutputStruct.bodyContact);
			ContactPointViewer.Draw(collisionOutputStruct.bridgeContact);
			Validate();
			if (areStuckRoadsBroken)
			{
				maxMomentaryStressNormalized = 1f;
			}
			float fixedDeltaTime = Time.fixedDeltaTime;
			float num = 1f;
			float num2 = 0f;
			float num3 = maxMomentaryStressNormalized - maxMomentaryStressNormalizedSmoothed;
			maxMomentaryStressNormalizedSmoothed = Mathf.Max(maxMomentaryStressNormalizedSmoothed - num * fixedDeltaTime + num3 * num2 * fixedDeltaTime, maxMomentaryStressNormalized);
			if (BridgeEdgeListener.debug_edgeBroke)
			{
				BridgeEdgeListener.debug_edgeBroke = false;
				UnityEngine.Object.FindObjectOfType<Panel_TopBar>().OnExitSim();
			}
		}

		internal void Validate()
		{
			if (runValidationChecksEveryFrame)
			{
				WorldValidator.ValidateNodesAndEdges(this);
			}
		}

		private void Step_ResizeAndInitNodeEdgeBody_AndMapEdgesToMotions()
		{
			int count = nodeHandles.Count;
			int count2 = edgeHandles.Count;
			int count3 = bodies.Count;
			int numSegments = collide.numSegments;
			int newSize = count3 + numSegments;
			solverNodesAL.Clear();
			solverNodesAL.SetSize(count);
			SolverNode[] array = solverNodesAL.array;
			if (solverEdges.Length != count2)
			{
				solverEdges = new SolverEdge[count2];
			}
			solverMotionsAL.Clear();
			solverMotionsAL.SetSize(newSize);
			solverMotionsAL.SetSize(count3);
			Poly.Solver.Motion[] array2 = solverMotionsAL.array;
			for (int i = 0; i < count; i++)
			{
				array[i] = nodeHandles[i].solverNode;
			}
			float num = settings.deltaTimeForVelocityEdge * settings.deltaTimeForVelocityEdge;
			for (int j = 0; j < count2; j++)
			{
				solverEdges[j] = edgeHandles[j].solverEdge;
				solverEdges[j].maxImpulsePerIntegration = edgeHandles[j].maxForce * num * edgeHandles[j].maxForce_ActualFraction;
			}
			for (int k = 0; k < count3; k++)
			{
				array2[k] = bodies[k].motion;
			}
			solverRun_segmentMotionIndices.Clear();
			int num2 = 0;
			int num3 = count3;
			while (num2 < edgesWithMotions.Count)
			{
				array2[num3] = edgesWithMotions[num2].optional_motion;
				solverRun_segmentMotionIndices.Add((short)num3);
				shapeHandleArray[(short)edgesWithMotions[num2].shapeHandleIndex].motionIdx = (short)num3;
				num2++;
				num3++;
			}
		}

		private void Step_ExportNodeEdgeBody_AndBreakEdges()
		{
			int count = nodeHandles.Count;
			int count2 = edgeHandles.Count;
			int count3 = bodies.Count;
			int numSegments = collide.numSegments;
			SolverNode[] array = solverNodesAL.array;
			Poly.Solver.Motion[] array2 = solverMotionsAL.array;
			for (int i = 0; i < count; i++)
			{
				NodeHandle nodeHandle = nodeHandles[i];
				nodeHandle.oldPos = nodeHandle.solverNode.pos;
				nodeHandle.solverNode = array[i];
			}
			int num = 0;
			int num2 = count3;
			while (num < edgesWithMotions.Count)
			{
				edgesWithMotions[num].optional_motion = array2[num2];
				num++;
				num2++;
			}
			brokenEdges.Clear();
			if (areEdgesBreakable)
			{
				for (int j = 0; j < count2; j++)
				{
					edgeHandles[j].solverEdge = solverEdges[j];
					if (edgeHandles[j].solverEdge.isBroken)
					{
						brokenEdges.Add(edgeHandles[j]);
					}
				}
				for (int k = 0; k < brokenEdges.Count; k++)
				{
					EdgeHandle edgeHandle = brokenEdges[k];
					bool flag = true;
					for (int l = 0; l < edgeBreakListeners.Count; l++)
					{
						IEdgeBreakListener edgeBreakListener = edgeBreakListeners[l];
						flag &= edgeBreakListener.OnEdgeBroken(edgeHandle);
					}
					if (flag)
					{
						ModifyShapeOfNode(edgeHandle.node0);
						ModifyShapeOfNode(edgeHandle.node1);
						RemoveEdge(edgeHandle);
						DestroyEdge(edgeHandle);
					}
					else
					{
						edgeHandle.solverEdge.isBroken = false;
					}
				}
			}
			else
			{
				for (int m = 0; m < count2; m++)
				{
					edgeHandles[m].solverEdge = solverEdges[m];
				}
			}
			float maxAngularVelocity_radPerSec_perIntegrationIteration = settings.maxAngularVelocity_radPerSec_perIntegrationIteration;
			if (settings.integrateInSolverIterations)
			{
				_ = settings.numIterations;
				_ = settings.numIterations;
			}
			for (int n = 0; n < count3; n++)
			{
				Rigidbody rigidbody = bodies[n];
				rigidbody.oldCom = rigidbody.motion.com;
				rigidbody.oldAngle = rigidbody.motion.angle;
				rigidbody.motion = array2[n];
				ref Poly.Solver.Motion motion = ref rigidbody.motion;
				if (motion.angle < MathF.PI * -20f || MathF.PI * 20f < motion.angle)
				{
					float num3 = Mathf.Floor(motion.angle / (MathF.PI * 2f)) * (MathF.PI * 2f);
					motion.angle -= num3;
					rigidbody.oldAngle -= num3;
					ShapeHandleIndex[] shapeHandleIndices = rigidbody._shapeHandleIndices;
					foreach (short num5 in shapeHandleIndices)
					{
						_ = ref shapeHandleArray[num5];
						collide.notifyShapeIndices_CorrectFrictionAnglesOnly.Add(num5);
					}
					collide.bodyIdxToAngleCorrection.Add(n, num3);
				}
			}
		}

		private void Step_ExportTransformsToShapes()
		{
			int count = nodeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				nodeHandles[i].CacheTransform2InShapeHandles_Util();
			}
			int count2 = edgeHandles.Count;
			for (int j = 0; j < count2; j++)
			{
				edgeHandles[j].CacheTransform2InShapeHandles_Util();
			}
			int count3 = bodies.Count;
			for (int k = 0; k < count3; k++)
			{
				Rigidbody rigidbody = bodies[k];
				rigidbody.CacheTransform2();
				rigidbody.PostFixedUpdate_Manual();
				rigidbody.CacheTransform2InShapeHandles_Util();
			}
		}

		[Obsolete]
		private void Step_ExportTransformsToShapes_original_unused_kept_for_perf_comparision()
		{
			int count = nodeHandles.Count;
			int count2 = edgeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				if ((short)nodeHandles[i].shapeHandleIndex >= 0)
				{
					int num = (short)nodeHandles[i].shapeHandleIndex;
					ref ShapeHandle reference = ref shapeHandleArray[num];
					reference.t2.position = nodeHandles[i].pos;
					reference.fastLinearVel = nodeHandles[i].solverNode.vel * settings.nodeToMotionVelocityMultiplier;
				}
			}
			for (int j = 0; j < count2; j++)
			{
				if ((short)edgeHandles[j].shapeHandleIndex >= 0)
				{
					int num2 = (short)edgeHandles[j].shapeHandleIndex;
					shapeHandleArray[num2].CacheTransform2(settings.nodeToMotionVelocityMultiplier);
				}
			}
			foreach (Rigidbody body in bodies)
			{
				body.CacheTransform2();
				body.PostFixedUpdate_Manual();
				if ((short)body._singleShapeHandleIndex >= 0)
				{
					ref ShapeHandle reference2 = ref shapeHandleArray[(short)body._singleShapeHandleIndex];
					reference2.t2 = body.t2;
					reference2.fastLinearVel = body.motion.linVel;
					continue;
				}
				ShapeHandleIndex[] shapeHandleIndices = body._shapeHandleIndices;
				foreach (ShapeHandleIndex shapeHandleIndex in shapeHandleIndices)
				{
					ref ShapeHandle reference3 = ref shapeHandleArray[(short)shapeHandleIndex];
					reference3.t2 = body.t2;
					reference3.fastLinearVel = body.motion.linVel;
				}
			}
		}

		internal static void TriggerCollisionCallbacks_Internal_Process(ref CollisionInfo info, ref CollisionEvent e)
		{
			if (e.a.Value.entity != null)
			{
				e.receivingHandle = ReceivingHandle.A;
				foreach (ICollisionListener collisionListener in ((Rigidbody)e.a.Value.entity).collisionListeners)
				{
					collisionListener.OnPolyCollisionProcess_Internal(in e, ref info);
				}
			}
			if (e.b.Value.entity == null)
			{
				return;
			}
			e.receivingHandle = ReceivingHandle.B;
			foreach (ICollisionListener collisionListener2 in ((Rigidbody)e.b.Value.entity).collisionListeners)
			{
				collisionListener2.OnPolyCollisionProcess_Internal(in e, ref info);
			}
		}

		internal static void TriggerCollisionCallbacks_Enter(ref CollisionInfo info, ref CollisionEvent collisionEvent)
		{
			if (collisionEvent.a.Value.entity != null)
			{
				collisionEvent.receivingHandle = ReceivingHandle.A;
				foreach (ICollisionListener collisionListener in ((Rigidbody)collisionEvent.a.Value.entity).collisionListeners)
				{
					collisionListener.OnPolyCollisionEnter(in collisionEvent);
				}
			}
			if (collisionEvent.b.Value.entity == null)
			{
				return;
			}
			collisionEvent.receivingHandle = ReceivingHandle.B;
			foreach (ICollisionListener collisionListener2 in ((Rigidbody)collisionEvent.b.Value.entity).collisionListeners)
			{
				collisionListener2.OnPolyCollisionEnter(in collisionEvent);
			}
		}

		internal static void TriggerCollisionCallbacks_Stay(ref CollisionInfo info, ref CollisionEvent collisionEvent)
		{
			if (collisionEvent.a.Value.entity != null)
			{
				collisionEvent.receivingHandle = ReceivingHandle.A;
				foreach (ICollisionListener collisionListener in ((Rigidbody)collisionEvent.a.Value.entity).collisionListeners)
				{
					collisionListener.OnPolyCollisionStay(in collisionEvent);
				}
			}
			if (collisionEvent.b.Value.entity == null)
			{
				return;
			}
			collisionEvent.receivingHandle = ReceivingHandle.B;
			foreach (ICollisionListener collisionListener2 in ((Rigidbody)collisionEvent.b.Value.entity).collisionListeners)
			{
				collisionListener2.OnPolyCollisionStay(in collisionEvent);
			}
		}

		private void ProcessDirtyEdges()
		{
			foreach (EdgeHandle dirtyEdge in dirtyEdges)
			{
				if ((bool)dirtyEdge.world)
				{
					dirtyEdge.CacheVirtualMassAndSolverStiffness();
					dirtyEdge.CacheCollisionDataForEdge(settings.nodeToMotionVelocityMultiplier);
					if ((short)dirtyEdge.shapeHandleIndex >= 0)
					{
						shapeHandleArray[(short)dirtyEdge.shapeHandleIndex].CacheTransform2(settings.nodeToMotionVelocityMultiplier);
						dirtyEdge.world.collide.invalidateShapeIndices.Add(dirtyEdge.shapeHandleIndex);
					}
				}
				if (!dirtyEdge.isEnabled)
				{
					dirtyEdge.solverEdge.stiffness = 0f;
					dirtyEdge.solverEdge.damping = 0f;
				}
			}
			dirtyEdges.Clear();
		}

		private void AddBody(Rigidbody body)
		{
			if (body != null)
			{
				body.CreateSimulationBody(this);
			}
			body.SetWorldAndIndex(this, bodies.Count);
			bodies.Add(body);
		}

		private void RemoveBody(Rigidbody body)
		{
			_ = body != null;
			body.DestroySimulationBody();
			bodies.RemoveAtAndSwap(body.worldIdx).SetWorldAndIndex(this, body.worldIdx);
			body.SetWorldAndIndex(null, -1);
		}

		private void AddDynamicAnchorJoint(DynamicAnchorJoint joint)
		{
			joint.SetWorldAndIndex(this, dynamicAnchorJoints.Count);
			dynamicAnchorJoints.Add(joint);
			joint.CalcAnchor();
			joint.CalcPivot();
		}

		private void RemoveDynamicAnchorJoint(DynamicAnchorJoint joint)
		{
			dynamicAnchorJoints.RemoveAtAndSwap(joint.worldIdx).SetWorldAndIndex(this, joint.worldIdx);
			joint.SetWorldAndIndex(null, -1);
		}

		private void AddJoint(Joint joint)
		{
			if (!joint.isCustomShapeJoint)
			{
				joint.SetWorldAndIndex(this, joints.Count);
				joints.Add(joint);
			}
			else
			{
				joint.SetWorldAndIndex(this, customShapeJoints.Count);
				customShapeJoints.Add(joint);
			}
			if (joint.autoConfigureThisAnchor)
			{
				joint.CalcConnectedAnchor(reverse: true);
			}
			else if (joint.autoConfigureConnectedAnchor)
			{
				joint.CalcConnectedAnchor(reverse: false);
			}
			joint.CalcPivots();
		}

		private void RemoveJoint(Joint joint)
		{
			if (!joint.isCustomShapeJoint)
			{
				joints.RemoveAtAndSwap(joint.worldIdx).SetWorldAndIndex(this, joint.worldIdx);
				joint.SetWorldAndIndex(null, -1);
			}
			else
			{
				customShapeJoints.RemoveAtAndSwap(joint.worldIdx).SetWorldAndIndex(this, joint.worldIdx);
				joint.SetWorldAndIndex(null, -1);
			}
		}

		private void AddAction(Action a)
		{
			a.SetWorldAndIndex(this, actions.Count);
			actions.Add(a);
			a.OnAddedToWorld();
			foreach (IActionListener actionListener in actionListeners)
			{
				actionListener.OnActionAdded(a);
			}
		}

		private void RemoveAction(Action a)
		{
			foreach (IActionListener actionListener in actionListeners)
			{
				actionListener.OnActionRemoved(a);
			}
			actions.RemoveAtAndSwap(a.worldIdx).SetWorldAndIndex(this, a.worldIdx);
			a.SetWorldAndIndex(null, -1);
		}

		internal void OnValidate()
		{
			if ((bool)settings)
			{
				settings.frameDeltaTime = Time.fixedDeltaTime;
			}
			foreach (EdgeHandle edgeHandle in edgeHandles)
			{
				edgeHandle.UpdateCachedStrength();
			}
		}

		private void OnDrawGizmos()
		{
			if (showNodes || showEdges)
			{
				Gizmos.color = ColorEx.amber;
				Gizmos.DrawWireCube(bounds.center, bounds.size);
				Gizmos.DrawWireCube(bounds.center, bounds.size + Vec2.one);
			}
		}

		private void OnApplicationQuit()
		{
			isQuitting = true;
			CleanupAllCollisionListenersOnVehicles_Slow();
		}

		private void CleanupAllCollisionListenersOnVehicles_Slow()
		{
			(from l in actions.Where((Action a) => a is Vehicle).SelectMany((Action v) => v.GetComponents<ICollisionListener>())
				where l is MonoBehaviour
				select l as MonoBehaviour).ToList().ForEach(delegate(MonoBehaviour b)
			{
				b.enabled = false;
			});
		}
	}
}
