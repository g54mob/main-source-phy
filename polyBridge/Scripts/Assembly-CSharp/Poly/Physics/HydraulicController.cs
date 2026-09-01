using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Extension;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class HydraulicController : ListenerBase, INodeListener, IEdgeListener
	{
		public enum Stage
		{
			Inactive = 0,
			DisconnectingNodes = 1,
			MovingHydraulics = 2,
			ConnectingNodes = 3
		}

		[Serializable]
		public class Set
		{
			public List<Edge> hydraulicEdges;

			public List<NodePart> nodeParts;
		}

		public struct SplitCommand
		{
			public Node src;

			public Node duplicate;

			public Part duplicatePart;
		}

		private static HydraulicController _instance;

		public float nodeMergeTolerance = 0.1f;

		public float minDurationHydraulicsMovement = 1f;

		[NonSerialized]
		public bool earlyMergePinsAreRopes = true;

		private List<EdgeHandle> earlyMergeRopePins = new List<EdgeHandle>();

		[HideInInspector]
		public bool weldNodesOnMerge;

		private bool didExpandNodePartsInSets;

		private float timeElapsedInHydraulicsMovement;

		public NodeMergeConfig mergeConfig;

		public NodeMergeMonitor mergeMonitor;

		[Tooltip("Optionally override hydraulics material during its operation.")]
		public EdgeMaterial activeHydraulicsMaterialOverride;

		public Set[] sets;

		[Header("Smoothing strength of pin-edges")]
		public bool smoothViaImpulseLimit = true;

		[Range(0.001f, 10f)]
		public float smoothingTime = 0.5f;

		public float maxExpectedDisplacement = 0.25f;

		public float loadMultiplier = 10f;

		public float finalSmoothingMultiplier = 1f;

		[Tooltip("When enabled, initial easing is slower, and accelerates at larger expected distance. This enables better first snap-change from max stiffness to smoothed.")]
		public bool useSqrt = true;

		[Header("Debug")]
		public bool spacebarActivates = true;

		public Stage currentStage;

		public int currentSetIndex;

		private Dictionary<EdgeHandle, Hydraulics> edgeToHydraulics = new Dictionary<EdgeHandle, Hydraulics>();

		private List<Hydraulics> movingHydraulics = new List<Hydraulics>();

		private List<EdgeHandle> pinEdges = new List<EdgeHandle>();

		private float pinStrengthFactor;

		private float pinStrengthDelta;

		public byte sVersion;

		private HashSet<NodeHandle> splittableNodes = new HashSet<NodeHandle>();

		private List<EdgeHandle> pinsRemovedImmediatelyOnActivate = new List<EdgeHandle>();

		public const bool doTreat2WayAnchorSpecially = false;

		public const bool doTreat3WayAnchorSpecially = false;

		private bool _activationRequested;

		private float tmp_pinStrengthRatio;

		private List<EdgeHandle> _dirtyEdges = new List<EdgeHandle>();

		public List<System.Action> splitCommands = new List<System.Action>();

		public static HydraulicController instance => _instance ?? (_instance = UnityEngine.Object.FindObjectOfType<HydraulicController>());

		private Set currentSet => sets[currentSetIndex];

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialize()
		{
			_instance = null;
		}

		private void SelectNextSet()
		{
			currentSetIndex = (currentSetIndex + 1) % sets.Length;
		}

		public void RegisterSplittableNode(NodeHandle n)
		{
			splittableNodes.Add(n);
		}

		public void RegisterHydro(Hydraulics h, EdgeHandle e)
		{
			edgeToHydraulics.Add(e, h);
			h.edge = e;
		}

		public Hydraulics GetHydro(EdgeHandle e)
		{
			if (!edgeToHydraulics.TryGetValue(e, out var value))
			{
				return null;
			}
			return value;
		}

		public void OnNodeAdded(NodeHandle n)
		{
		}

		public void OnNodeRemoved(NodeHandle n)
		{
			splittableNodes.Remove(n);
		}

		public void OnEdgeAdded(EdgeHandle e)
		{
		}

		public void OnEdgeRemoved(EdgeHandle e)
		{
			if (edgeToHydraulics.Keys.Contains(e))
			{
				edgeToHydraulics[e].Dispose();
				edgeToHydraulics.Remove(e);
			}
		}

		public void OnEdgeDetachedFromNode(EdgeHandle e, NodeHandle oldNode)
		{
		}

		public void OnEdgeAttachedToNode(EdgeHandle e, NodeHandle newNode)
		{
		}

		private void Expand_All_PartsInSets()
		{
			foreach (System.Action splitCommand in splitCommands)
			{
				splitCommand();
			}
			splitCommands.Clear();
			Set[] array = sets;
			foreach (Set set in array)
			{
				List<NodePart> list = new List<NodePart>();
				foreach (NodePart nodePart in set.nodeParts)
				{
					if (!nodePart.node)
					{
						continue;
					}
					if (nodePart.part == Part.All)
					{
						Part[] nodeParts = World.GetNodeParts(nodePart.node);
						foreach (Part part in nodeParts)
						{
							list.Add(new NodePart(nodePart.node, part));
						}
					}
					else if (World.GetNodeParts(nodePart.node).Contains(nodePart.part))
					{
						list.Add(nodePart);
					}
				}
				set.nodeParts = list;
			}
		}

		private void Update()
		{
			if (spacebarActivates && Input.GetKeyDown(KeyCode.Space) && !IsMoving())
			{
				Activate();
			}
		}

		public void Clear()
		{
			sets = new Set[0];
			currentSetIndex = 0;
			movingHydraulics.Clear();
			pinEdges.Clear();
			pinStrengthFactor = 0f;
			pinStrengthDelta = 0f;
			splitCommands.Clear();
			splittableNodes.Clear();
			edgeToHydraulics.Values.ToList().ForEach(delegate(Hydraulics h)
			{
				h.Dispose();
			});
			edgeToHydraulics.Clear();
			currentStage = Stage.Inactive;
			mergeMonitor.EndMonitoringAndClear();
			earlyMergeRopePins.Clear();
			SingletonBehaviour<World>.instance.hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
			{
				hl.ClearAndReset();
			});
			didExpandNodePartsInSets = false;
		}

		private new void OnEnable()
		{
			base.OnEnable();
			mergeConfig.maxMergeDistance = nodeMergeTolerance;
			mergeConfig.mergeCallback = AsyncMergeNodes;
			mergeMonitor = new NodeMergeMonitor();
			mergeMonitor.config = mergeConfig;
		}

		private void MergeNodesWithSinglePinOnly(NodeHandle a, NodeHandle b, float distance, bool allowAnyDistance = false)
		{
			EdgeHandle edgeHandle = AddPinEdge(a, b, distance, allowAnyDistance);
			edgeHandle.node0.pins.Add(edgeHandle);
			edgeHandle.node1.pins.Add(edgeHandle);
			if (earlyMergePinsAreRopes && currentStage == Stage.MovingHydraulics)
			{
				edgeHandle.solverEdge.isRope = true;
				earlyMergeRopePins.Add(edgeHandle);
			}
			else
			{
				FinalizeMergeOfBodiesByConnectedPin(edgeHandle);
			}
			SingletonBehaviour<World>.instance.hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
			{
				hl.OnNodesMergedEarly(a.unityNodeComponent, b.unityNodeComponent);
			});
		}

		public void AsyncMergeNodes(NodeHandle a, NodeHandle b, float distance, HashSet<NodeHandle> additionalMergedNodes)
		{
			MergeNodesWithSinglePinOnly(a, b, distance);
			List<NodeHandle> list = VerletEditor.FindAllNodesPinnedToCluster(new List<NodeHandle> { a, b });
			for (int i = 0; i < list.Count - 1; i++)
			{
				for (int j = i + 1; j < list.Count; j++)
				{
					NodeHandle nodeHandle = list[i];
					NodeHandle nodeHandle2 = list[j];
					if (!NodeMergeMonitor.IsPairConnected(nodeHandle, nodeHandle2) && !NodeMergeMonitor.NodePair.IsDoubleAnchor(nodeHandle, nodeHandle2))
					{
						MergeNodesWithSinglePinOnly(nodeHandle, nodeHandle2, -1f, allowAnyDistance: true);
						if (additionalMergedNodes != null)
						{
							additionalMergedNodes.Add(nodeHandle);
							additionalMergedNodes.Add(nodeHandle2);
						}
					}
				}
			}
		}

		private void ShrinkLengthOfRopePins()
		{
			foreach (EdgeHandle earlyMergeRopePin in earlyMergeRopePins)
			{
				float num = Vec2.Distance(earlyMergeRopePin.node0.pos, earlyMergeRopePin.node1.pos);
				if (num < earlyMergeRopePin.length)
				{
					earlyMergeRopePin.solverEdge.length = num;
				}
			}
		}

		private void FinalizeRopePins()
		{
			foreach (EdgeHandle earlyMergeRopePin in earlyMergeRopePins)
			{
				float num = Vec2.Distance(earlyMergeRopePin.node0.pos, earlyMergeRopePin.node1.pos);
				if (num < earlyMergeRopePin.length)
				{
					earlyMergeRopePin.solverEdge.length = num;
				}
				earlyMergeRopePin.solverEdge.isRope = false;
				FinalizeMergeOfBodiesByConnectedPin(earlyMergeRopePin);
			}
			earlyMergeRopePins.Clear();
		}

		public static EdgeHandle AddPinEdge(NodeHandle a, NodeHandle b, float distance, bool allowAnyDistance = false)
		{
			EdgeDefinition edgeDefinition = new EdgeDefinition();
			edgeDefinition.InitDefaults();
			edgeDefinition.material = SingletonBehaviour<VerletEditor>.instance.pinMaterial;
			edgeDefinition.lengthOverride = distance;
			EdgeHandle edgeHandle = World.CreateEdge_Inner(a, b, edgeDefinition);
			edgeHandle.solverEdge.pin_isUnbreakable = true;
			a.world.AddEdge(edgeHandle);
			if ((bool)edgeHandle.unityEdgeComponent)
			{
				edgeHandle.unityEdgeComponent.isTemporaryPin = true;
			}
			return edgeHandle;
		}

		private void FinalizeMergeOfBodiesByConnectedPin(EdgeHandle pin)
		{
			if (!pin.node0.isKinematic && !pin.node1.isKinematic)
			{
				return;
			}
			if (!pin.node0.isKinematic && pin.node1.isAnchor)
			{
				pin.node0.SetKinematic(isKinematic: true);
				if ((bool)pin.node0.unityNodeComponent)
				{
					pin.node0.unityNodeComponent.UpdateRendererMaterial();
				}
			}
			if (!pin.node1.isKinematic && pin.node0.isAnchor)
			{
				pin.node1.SetKinematic(isKinematic: true);
				if ((bool)pin.node1.unityNodeComponent)
				{
					pin.node1.unityNodeComponent.UpdateRendererMaterial();
				}
			}
		}

		public void Activate(int setIndex = -1)
		{
			if (setIndex >= 0)
			{
				currentSetIndex = setIndex;
			}
			if (sets.Length != 0)
			{
				_activationRequested = true;
				currentStage = Stage.DisconnectingNodes;
			}
		}

		private void _ReallyActivate()
		{
			if (!didExpandNodePartsInSets)
			{
				Expand_All_PartsInSets();
				didExpandNodePartsInSets = true;
			}
			List<List<NodeHandle>> list = new List<List<NodeHandle>>();
			foreach (NodePart nodePart in currentSet.nodeParts)
			{
				List<NodeHandle> list2 = new List<NodeHandle>();
				NodeHandle handle = nodePart.node.handle;
				if (!handle.isSplittableAnchor || nodePart.part != Part.A || nodePart.node.is3WaySplit || !nodePart.node.is3WaySplit)
				{
					list2.Add(handle);
				}
				else
				{
					list2.AddRange(nodePart.node.newSplitParts_forHydraulicsOnly.Select((Node n) => n.handle));
					list2.Remove(handle);
				}
				list.Add(list2);
			}
			pinsRemovedImmediatelyOnActivate.Clear();
			List<EdgeHandle> list3 = VerletEditor.SeparateSplitNodeParts(list, SingletonBehaviour<VerletEditor>.instance.world, pinsRemovedImmediatelyOnActivate);
			foreach (EdgeHandle item2 in list3)
			{
				earlyMergeRopePins.Remove(item2);
			}
			foreach (EdgeHandle item3 in pinsRemovedImmediatelyOnActivate)
			{
				earlyMergeRopePins.Remove(item3);
			}
			int num = list3.Count - 1;
			while (0 <= num)
			{
				EdgeHandle item = list3[num];
				if (pinEdges.Contains(item))
				{
					list3.RemoveAt(num);
				}
				num--;
			}
			pinEdges.AddRange(list3);
			pinStrengthFactor = 1f;
			pinStrengthDelta = -1f / smoothingTime;
			if ((bool)activeHydraulicsMaterialOverride)
			{
				foreach (Edge hydraulicEdge in currentSet.hydraulicEdges)
				{
					if ((bool)hydraulicEdge && (bool)hydraulicEdge.handle)
					{
						hydraulicEdge.handle.OverrideMaterial(activeHydraulicsMaterialOverride);
					}
				}
			}
			currentStage = Stage.DisconnectingNodes;
		}

		public bool IsMoving()
		{
			return currentStage != Stage.Inactive;
		}

		public List<EdgeHandle> FixedUpdate_Manual(float frameDeltaTime)
		{
			mergeConfig.maxMergeDistance = nodeMergeTolerance;
			if (_activationRequested)
			{
				_activationRequested = false;
				_ReallyActivate();
			}
			switch (currentStage)
			{
			case Stage.DisconnectingNodes:
				UpdateStiffnessOfPinEdges();
				break;
			case Stage.MovingHydraulics:
				timeElapsedInHydraulicsMovement += frameDeltaTime;
				ShrinkLengthOfRopePins();
				MonitorHydraulicsAndCreateNewPinEdges();
				break;
			case Stage.ConnectingNodes:
				FinalizeRopePins();
				UpdateStiffnessOfPinEdges();
				break;
			}
			return UpdateHydraulics(frameDeltaTime);
		}

		private void MonitorHydraulicsAndCreateNewPinEdges()
		{
			mergeMonitor.FixedUpdate_Manual();
			for (int num = movingHydraulics.Count - 1; num >= 0; num--)
			{
				if (!movingHydraulics[num].isMoving)
				{
					movingHydraulics.RemoveAtAndSwap(num);
				}
			}
			if (movingHydraulics.Count == 0 && minDurationHydraulicsMovement <= timeElapsedInHydraulicsMovement)
			{
				mergeMonitor.MergeAllInProximity(nodeMergeTolerance);
				pinEdges.Clear();
				pinStrengthFactor = 1f;
				pinStrengthDelta = 1f / smoothingTime;
				mergeMonitor.EndMonitoringAndClear();
				RemoveMissingNodes();
				currentStage = Stage.ConnectingNodes;
				UpdateStiffnessOfPinEdges();
			}
		}

		private void UpdateStiffnessOfPinEdges()
		{
			float num = pinStrengthFactor;
			pinStrengthFactor += pinStrengthDelta * Time.fixedDeltaTime;
			if (pinEdges.Count == 0)
			{
				pinStrengthFactor += pinStrengthDelta * smoothingTime;
			}
			float num2 = Mathf.Clamp01(pinStrengthFactor);
			if (num2 != pinStrengthFactor)
			{
				pinStrengthFactor = num2;
				pinStrengthDelta = 0f;
			}
			float newStiffness = CalcStiffnessFromStrengthFactor_deprecated(pinStrengthFactor);
			if (smoothViaImpulseLimit && pinStrengthFactor < num)
			{
				newStiffness = 0f;
				tmp_pinStrengthRatio = pinStrengthFactor / num;
				float num3 = tmp_pinStrengthRatio;
				tmp_pinStrengthRatio = Mathf.Pow(tmp_pinStrengthRatio, 1f / (float)SingletonBehaviour<World>.instance.settings.numEdgeIntegrationsPerFrame);
				if (pinStrengthFactor > 0.9f)
				{
					tmp_pinStrengthRatio = -1f;
					pinEdges.ForEach(delegate(EdgeHandle edge)
					{
						edge.solverEdge.stiffness = 0f;
					});
				}
				else
				{
					float deltaTimeForVelocityEdge = SingletonBehaviour<World>.instance.settings.deltaTimeForVelocityEdge;
					foreach (EdgeHandle pinEdge in pinEdges)
					{
						if (pinEdge.maxForce == float.PositiveInfinity)
						{
							float maxForce = (pinEdge.solverEdge.pin_isUsing2d ? Mathf.Sqrt(pinEdge.solverEdge.sumVelImpulses2d_X * pinEdge.solverEdge.sumVelImpulses2d_X + pinEdge.solverEdge.sumVelImpulses2d_Y * pinEdge.solverEdge.sumVelImpulses2d_Y) : Mathf.Abs(pinEdge.solverEdge.sumVelImpulses)) / deltaTimeForVelocityEdge / deltaTimeForVelocityEdge;
							pinEdge.maxForce = maxForce;
							pinEdge.solverEdge.pin_isUnbreakable = true;
						}
						else
						{
							pinEdge.maxForce *= num3;
						}
					}
				}
			}
			_ = tmp_pinStrengthRatio;
			_ = 0f;
			if (pinStrengthDelta != 0f)
			{
				return;
			}
			switch (currentStage)
			{
			case Stage.DisconnectingNodes:
				pinEdges.ForEach(delegate(EdgeHandle edge)
				{
					if ((bool)edge)
					{
						SingletonBehaviour<World>.instance.RemoveEdge(edge);
						World.DestroyEdge(edge);
					}
				});
				pinEdges.Clear();
				foreach (Edge hydraulicEdge in currentSet.hydraulicEdges)
				{
					Hydraulics hydraulics = (((bool)hydraulicEdge && (bool)hydraulicEdge.handle) ? edgeToHydraulics[hydraulicEdge.handle] : null);
					if ((bool)hydraulics)
					{
						hydraulics.Activate();
						if (!movingHydraulics.Contains(hydraulics))
						{
							movingHydraulics.Add(hydraulics);
						}
					}
				}
				SingletonBehaviour<World>.instance.hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
				{
					hl.OnPhaseStart();
				});
				mergeMonitor.StartMonitoring(movingHydraulics.ToArray(), SingletonBehaviour<World>.instance.layerManager.splittableNodesCopy);
				currentStage = Stage.MovingHydraulics;
				timeElapsedInHydraulicsMovement = 0f;
				break;
			case Stage.ConnectingNodes:
			{
				foreach (Edge hydraulicEdge2 in currentSet.hydraulicEdges)
				{
					if ((bool)hydraulicEdge2)
					{
						hydraulicEdge2.handle.RestoreMaterial();
					}
				}
				movingHydraulics.Clear();
				SelectNextSet();
				FinalizeRopePins();
				currentStage = Stage.Inactive;
				Node[] mergedNodes = (from handle in mergeMonitor.GetMergedNodesCopy()
					select handle.unityNodeComponent).ToArray();
				SingletonBehaviour<World>.instance.hydraulicListeners.ForEach(delegate(IHydraulicListener hl)
				{
					hl.OnPhaseComplete(mergedNodes);
				});
				break;
			}
			}
		}

		[Obsolete]
		public float CalcStiffnessFromStrengthFactor_deprecated(float strengthFactor)
		{
			float num = Mathf.LerpUnclamped(0f, maxExpectedDisplacement, 1f - strengthFactor);
			if (useSqrt)
			{
				num = Mathf.LerpUnclamped(0f, Mathf.Sqrt(maxExpectedDisplacement), 1f - strengthFactor);
				num *= num;
			}
			float num2 = SingletonBehaviour<World>.instance.settings.gravityMagnitude * SingletonBehaviour<World>.instance.settings.deltaTimeForVelocity * SingletonBehaviour<World>.instance.settings.deltaTimeForVelocity;
			float num3 = num2 / (num / loadMultiplier + num2);
			num3 = 1f - Mathf.Pow(1f - num3, 1f / (float)(SingletonBehaviour<World>.instance.settings.numIterations * SingletonBehaviour<World>.instance.settings.numEdgeSubIterations));
			return Mathf.Clamp01(num3 / SingletonBehaviour<World>.instance.settings.edgeTau * finalSmoothingMultiplier);
		}

		public List<EdgeHandle> UpdateHydraulics(float frameDeltaTime)
		{
			List<EdgeHandle> dirtyEdges = _dirtyEdges;
			dirtyEdges.Clear();
			foreach (Hydraulics movingHydraulic in movingHydraulics)
			{
				movingHydraulic.UpdateHydraulics(frameDeltaTime);
				if (movingHydraulic.currentSpeed != 0f)
				{
					dirtyEdges.Add(movingHydraulic.edge);
				}
			}
			return dirtyEdges;
		}

		public void UpdateInSolverOnIntegration(SolverEdge[] solverEdges)
		{
			for (int i = 0; i < movingHydraulics.Count; i++)
			{
				movingHydraulics[i].UpdateInSolverOnIntegration(solverEdges);
			}
			if (currentStage != Stage.DisconnectingNodes || !smoothViaImpulseLimit || !(tmp_pinStrengthRatio >= 0f))
			{
				return;
			}
			foreach (EdgeHandle pinEdge in pinEdges)
			{
				solverEdges[pinEdge.worldIdx].maxImpulsePerIntegration *= tmp_pinStrengthRatio;
			}
		}

		public void RegisterNode_Part_Split(Node src, Node duplicate, Part duplicatePart)
		{
			ExecuteNode_Part_Split(src, duplicate, duplicatePart);
		}

		public void ExecuteNode_Part_Verify(Node src, Part part)
		{
			Set[] array = sets;
			foreach (Set set in array)
			{
				for (int num = set.nodeParts.Count - 1; num >= 0; num--)
				{
					NodePart nodePart = set.nodeParts[num];
					if (nodePart.node == src && nodePart.part != Part.All && nodePart.part != part)
					{
						set.nodeParts.RemoveAt(num);
					}
				}
			}
		}

		public void ExecuteNode_Part_Split(Node src, Node duplicate, Part duplicatePart)
		{
			Set[] array = sets;
			NodePart item = default(NodePart);
			foreach (Set set in array)
			{
				int count = set.nodeParts.Count;
				for (int j = 0; j < count; j++)
				{
					NodePart value = set.nodeParts[j];
					if (value.node == src && value.part == duplicatePart)
					{
						value.node = duplicate;
						set.nodeParts[j] = value;
					}
					else if (value.node == src && value.part == Part.All)
					{
						item.node = duplicate;
						item.part = duplicatePart;
						set.nodeParts.Add(item);
					}
				}
			}
		}

		public void RemoveMissingNodes()
		{
			Set[] array = sets;
			foreach (Set set in array)
			{
				List<NodePart> list = new List<NodePart>();
				for (int j = 0; j < set.nodeParts.Count; j++)
				{
					NodePart item = set.nodeParts[j];
					if (item.node != null && item.node.name != "null")
					{
						list.Add(item);
					}
				}
				set.nodeParts = list;
			}
		}

		public void RemoveFromSets(Hydraulics h)
		{
			Set[] array = sets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].hydraulicEdges.Remove(h.edge.unityEdgeComponent);
			}
		}
	}
}
