using System;
using System.Collections;
using System.Collections.Generic;
using BesiegeDlc;
using InternalModding.Blocks;
using Modding;
using Mono.CSharp;
using Steamworks;
using UnityEngine;

[AddComponentMenu("Core/Machine")]
public class Machine : MonoBehaviour
{
	public class InputGroupEntry
	{
		public MKey Key;

		public int State;

		public string Name;
	}

	public class LinkedBlock
	{
		public BlockBehaviour Other;

		public List<TriggerSetJointBase> TriggerJoints;

		public LinkedBlock(BlockBehaviour other, TriggerSetJointBase trigger)
		{
			Other = other;
			TriggerJoints = new List<TriggerSetJointBase>();
			AddTrigger(trigger);
		}

		public void AddTrigger(TriggerSetJointBase trigger)
		{
			TriggerJoints.Add(trigger);
			if (TriggerJoints.Count > 1)
			{
				TriggerJoints.Sort(CompareTriggerPath);
			}
		}
	}

	protected class BlockJointTrigger
	{
		public BlockBehaviour block;

		public TriggerSetJointBase trigger;

		public Vector3 pos;

		public float radius;

		public bool usesRay;

		public BlockJointTrigger(BlockBehaviour b, TriggerSetJointBase triggerJoint)
		{
			trigger = triggerJoint;
			block = b;
			usesRay = false;
		}

		public BlockJointTrigger(BlockBehaviour b, Vector3 p, float r)
		{
			block = b;
			pos = p;
			radius = r;
			usesRay = true;
		}
	}

	public class SimCluster
	{
		public BlockBehaviour Base;

		public Transform BaseTransform;

		public BlockBehaviour[] Blocks;

		public Vector3 CenterOffset;

		public Vector3 SimOffset;

		public float Weight;

		public int count;

		public bool alwaysIncludeInCenter;

		public SimCluster(BlockBehaviour baseBlock, Vector3 centerOffset, float weight, int count)
		{
			Base = baseBlock;
			BaseTransform = baseBlock.transform;
			CenterOffset = centerOffset;
			Weight = weight;
			this.count = count;
		}
	}

	public bool hasIntactBlocks = true;

	public Action onBatchOperationComplete;

	public string LoadedMachinePath = string.Empty;

	public bool ghostMode;

	public bool curtainMode;

	public bool isSimulating;

	public bool isLocalMachine = true;

	public bool isLocalSim;

	public bool isRespawning;

	public SimCluster[] simClusters;

	protected bool clusterSurplus;

	public bool ignoreDisconnectedBlocks = true;

	public bool useEndPointWeights = true;

	public bool UnbreakableMode;

	public bool InfiniteAmmoMode;

	public bool ExplodingCannonballs;

	internal bool hasFiredProjectiles;

	public bool isReady = true;

	public bool analyzing;

	public bool isDestroyed;

	public float notifyDelay = 0.2f;

	public bool isLoadingInfo;

	public BoundingBoxController boundingBoxController;

	public bool spawningMachine;

	public bool isLoadingDifference;

	public Action OnBeforeClone;

	public Action OnAfterClone;

	protected bool isActiveMachine = true;

	protected List<BlockBehaviour> buildingBlocks;

	protected Dictionary<Guid, BlockBehaviour> guidToBlock = new Dictionary<Guid, BlockBehaviour>();

	protected List<BlockBehaviour> simBlocks;

	protected BlockLinkManager linkManager;

	protected MachineAnalyzer analyzer;

	protected Bounds machineBounds;

	protected float boundsSqrSize = 100f;

	protected UndoSystem undoSystem;

	protected Vector3 _basePosition = Vector3.up * 5.05f;

	protected Quaternion _baseRotation = Quaternion.identity;

	protected Vector3 lastMachinePosition;

	protected Transform tempSim;

	protected XDataHolder machineData = new XDataHolder();

	protected Vector3 _centerPosOffsetToCenter;

	protected KeyInputController inputController;

	private static readonly Quaternion negativeIdentity = new Quaternion(0f, 0f, 0f, -1f);

	protected Transform buildingMachine;

	protected Transform spawner;

	protected Transform simulationClone;

	protected BlockBehaviour[] simulationArray;

	protected Quaternion machineRotation;

	protected bool hasTempSim;

	protected Vector3 _machineMiddle = Vector3.zero;

	protected Vector3 _lastMiddle = Vector3.zero;

	protected Vector3 lastCenterPos;

	public List<GenericDraggedBlock> currentDragged = new List<GenericDraggedBlock>();

	public NodeController nodeController;

	private Vector3 smoothFollow = Vector3.zero;

	public bool resetNeeded;

	private float machineMass;

	private string _name = "Machine";

	public bool finishedPhysics;

	private float linkRayLength = 0.25f;

	private float linkRayNormalOffset = 0.05f;

	private Vector3 cameraOffset;

	private Transform display;

	private GameObject tempVis;

	private Transform tempVisTransform;

	private GameObject tempSkinnedVis;

	private Transform tempSkinnedTransform;

	private MeshFilter tempFilter;

	private MeshRenderer tempRenderer;

	private SkinnedMeshRenderer tempSkinned;

	private Collider[] ignoredInPhys;

	private List<BlockBehaviour> visibleSurfaceBlocks = new List<BlockBehaviour>();

	private List<BlockBehaviour> buildUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> buildFixedUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> buildLateUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> simUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> simFixedUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> simSendEmulationUpdate = new List<BlockBehaviour>();

	private List<BlockBehaviour> simLateUpdate = new List<BlockBehaviour>();

	private List<BlockVisualController> buildBVC = new List<BlockVisualController>();

	private List<BlockVisualController> simBVC = new List<BlockVisualController>();

	private List<BlockBehaviour> physPhaseEarly = new List<BlockBehaviour>();

	private List<BlockBehaviour> physPhaseNormal = new List<BlockBehaviour>();

	private List<BlockBehaviour> physFinishBlocks = new List<BlockBehaviour>();

	private List<BlockBehaviour> physFragmentBlocks = new List<BlockBehaviour>();

	public bool HasEmulationBlocks;

	protected int _blockCount;

	protected int _blocksCost;

	private int LoadedBlockScore;

	protected Transform centerBlockTransform;

	protected BlockBehaviour centerBlock;

	protected bool hasCenterBlock;

	private Dictionary<int, LinkedBlock> linkedBlocks = new Dictionary<int, LinkedBlock>();

	private bool hasAddPiece;

	protected AddPiece addPiece;

	private bool setColliderIterations;

	protected Vector3 _simMiddle = Vector3.zero;

	private int finishPhysicsFrameCount;

	private bool finishedNormalPhysics;

	private int finishNormalFrameCount;

	private Dictionary<Rigidbody, RigidbodyInterpolation> oldInterpolation = new Dictionary<Rigidbody, RigidbodyInterpolation>();

	private bool hasChangedInterpolation;

	public static bool ignoreRocket = false;

	internal List<DlcManager.DlcType> containsDLCs = new List<DlcManager.DlcType>();

	protected BlockBehaviour refBlock;

	protected Transform refBlockTransform;

	protected Vector3 refOffset = Vector3.zero;

	protected Vector3 refCoM = Vector3.zero;

	protected Vector3 refUp = Vector3.up;

	protected Vector3 refFwd = Vector3.forward;

	protected int refCount;

	protected int everyOther = 1;

	protected int fixedTime;

	public bool IsLoadingMachine
	{
		get
		{
			return isLoadingInfo;
		}
	}

	public bool SimPhysics
	{
		get
		{
			return !StatMaster.isMP || (StatMaster.isHosting && !RemoteLocal) || (StatMaster.isClient && LocalSim);
		}
	}

	public virtual bool LocalSim
	{
		get
		{
			return isLocalMachine && isLocalSim;
		}
	}

	public bool RemoteLocal
	{
		get
		{
			return !isLocalMachine && isLocalSim;
		}
	}

	public Vector3 Size
	{
		get
		{
			return linkManager.Size;
		}
	}

	internal KeyInputController InputController
	{
		get
		{
			return inputController;
		}
	}

	public Transform SimClone
	{
		get
		{
			return simulationClone;
		}
	}

	public int InitialSimCount { get; private set; }

	public Quaternion MachineSpawnRotation
	{
		get
		{
			return machineRotation;
		}
	}

	public bool IsDraggingBlocks
	{
		get
		{
			return isLocalMachine && currentDragged.Count > 0;
		}
	}

	public virtual ushort PlayerID
	{
		get
		{
			return 0;
		}
	}

	public float Mass
	{
		get
		{
			UpdateMass();
			return machineMass;
		}
	}

	public BlockBehaviour FirstBlock
	{
		get
		{
			return (isSimulating && simBlocks.Count > 0) ? simBlocks[0] : ((buildingBlocks.Count <= 0) ? null : buildingBlocks[0]);
		}
	}

	public int BlockCount
	{
		get
		{
			return buildingBlocks.Count;
		}
	}

	public int DisplayBlockCount
	{
		get
		{
			return _blockCount;
		}
	}

	public int BlocksCost
	{
		get
		{
			return _blocksCost;
		}
	}

	public int ClusterCount
	{
		get
		{
			return linkManager.Clusters.Count;
		}
	}

	public BlockLinkManager LinkManager
	{
		get
		{
			return linkManager;
		}
	}

	public List<BlockBehaviour> BuildingBlocks
	{
		get
		{
			return new List<BlockBehaviour>(buildingBlocks);
		}
	}

	public List<BlockBehaviour> SimulationBlocks
	{
		get
		{
			return new List<BlockBehaviour>(simBlocks);
		}
	}

	public UndoSystem UndoSystem
	{
		get
		{
			return undoSystem;
		}
	}

	public Vector3 SmoothFollowPosition
	{
		get
		{
			return smoothFollow;
		}
	}

	public virtual Vector3 MiddlePosition
	{
		get
		{
			return (!isSimulating) ? buildingMachine.TransformPoint(_machineMiddle) : _simMiddle;
		}
	}

	public Vector3 MachineCenterPos
	{
		get
		{
			if (isReady && !analyzing && hasCenterBlock && (!isSimulating || centerBlock.hasSimBlock))
			{
				lastCenterPos = centerBlockTransform.position;
			}
			return lastCenterPos;
		}
	}

	public XDataHolder MachineData
	{
		get
		{
			return machineData;
		}
	}

	public Transform BuildingMachine
	{
		get
		{
			return buildingMachine;
		}
	}

	public Transform SimulationMachine
	{
		get
		{
			return (!hasTempSim) ? simulationClone : tempSim;
		}
	}

	public Transform SpawnTransform
	{
		get
		{
			return spawner;
		}
	}

	public virtual bool CanModify
	{
		get
		{
			return !StatMaster.waitingForSim;
		}
	}

	public virtual bool BuildingLocked
	{
		get
		{
			return false;
		}
	}

	public virtual bool ReadyForSim
	{
		get
		{
			return isReady && !analyzing;
		}
	}

	public float maxStress { get; set; }

	public Vector3 Position
	{
		get
		{
			return buildingMachine.position;
		}
		set
		{
			buildingMachine.position = value;
		}
	}

	public Quaternion Rotation
	{
		get
		{
			return buildingMachine.rotation;
		}
		set
		{
			buildingMachine.rotation = value;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			base.name = "Machine " + PlayerID + " - " + value;
			_name = value;
		}
	}

	public string Author { get; protected set; }

	public MachineInfo.MachineType MachineType { get; protected set; }

	public BlockBehaviour RefBlock
	{
		get
		{
			return refBlock;
		}
	}

	public Vector3 RefUp
	{
		get
		{
			return (!refBlockTransform) ? Vector3.zero : refBlockTransform.TransformDirection(refUp);
		}
	}

	public Vector3 RefForward
	{
		get
		{
			return (!refBlockTransform) ? Vector3.zero : refBlockTransform.TransformDirection(refFwd);
		}
	}

	public Vector3 RefCenter
	{
		get
		{
			return (!refBlockTransform) ? buildingBlocks[0].transform.position : refBlockTransform.TransformPoint(refOffset);
		}
	}

	public int LocalTime
	{
		get
		{
			return fixedTime;
		}
	}

	public bool OddFrame
	{
		get
		{
			return everyOther == 0;
		}
	}

	public void SetHasEmulation(bool b)
	{
		HasEmulationBlocks = b;
		if ((bool)inputController)
		{
			inputController.SetHasAnyEmulation(HasEmulationBlocks);
		}
	}

	internal void UpdateMachineDLCStatus()
	{
		if ((float)Mathf.Abs(LoadedBlockScore - _blocksCost) > (float)LoadedBlockScore * 0.5f || _blocksCost == 1)
		{
			MachineType = MachineInfo.MachineType.Built;
			Author = ((!SteamManager.Initialized) ? string.Empty : SteamUser.GetSteamID().m_SteamID.ToString());
		}
		DlcManager.Instance.GetMachineBlockDlc(buildingBlocks, out containsDLCs);
		if (ReferenceMaster.onMachineDLCStateChanged != null)
		{
			ReferenceMaster.onMachineDLCStateChanged();
		}
	}

	internal void UpdateMachineDLCStatus(BlockBehaviour block)
	{
		if ((float)Mathf.Abs(LoadedBlockScore - _blocksCost) > (float)LoadedBlockScore * 0.5f || _blocksCost == 1)
		{
			MachineType = MachineInfo.MachineType.Built;
			Author = ((!SteamManager.Initialized) ? string.Empty : SteamUser.GetSteamID().m_SteamID.ToString());
		}
		List<DlcManager.DlcType> dlcTypes = new List<DlcManager.DlcType>(containsDLCs);
		if (DlcManager.Instance.GetMachineBlockDlc(new List<BlockBehaviour> { block }, out dlcTypes) && !containsDLCs.Contains(dlcTypes[0]))
		{
			containsDLCs.Add(dlcTypes[0]);
		}
		if (ReferenceMaster.onMachineDLCStateChanged != null)
		{
			ReferenceMaster.onMachineDLCStateChanged();
		}
	}

	public static bool IsStartMachine(MachineInfo machineInfo)
	{
		return machineInfo != null && machineInfo.Blocks.Count == 1 && machineInfo.Blocks[0].ID == BlockType.StartingBlock;
	}

	public void SetMachineCenter(Vector3 centerPos)
	{
		lastCenterPos = centerPos;
	}

	public static void RemoveBody(Transform t)
	{
		Joint[] components = t.GetComponents<Joint>();
		Joint[] array = components;
		foreach (Joint obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		UnityEngine.Object.Destroy(t.GetComponent<Rigidbody>());
	}

	public List<Tuple<BlockBehaviour, int>> GetMirroredBlocks(BlockBehaviour block)
	{
		List<Tuple<BlockBehaviour, int>> list = new List<Tuple<BlockBehaviour, int>>();
		SymmetryController symmetryController = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
		Matrix4x4 localToWorldMatrix = BuildingMachine.localToWorldMatrix;
		List<SymmetryController.MirrorInfo> mirrorInfo;
		BlockBehaviour b;
		if (block.Prefab.Type == BlockType.BuildSurface)
		{
			BuildSurface surface = block as BuildSurface;
			if (surface.isValid)
			{
				Quaternion rotation = BuildingMachine.rotation;
				mirrorInfo = new List<SymmetryController.MirrorInfo>();
				BlockType type = surface.edges[0].Prefab.Type;
				List<Tuple<BuildEdgeBlock, int>> list2 = new List<Tuple<BuildEdgeBlock, int>>();
				for (int i = 0; i < surface.edges.Length; i++)
				{
					BuildEdgeBlock buildEdgeBlock = surface.edges[i];
					if (!buildEdgeBlock.isValid)
					{
						continue;
					}
					list2.Add(new Tuple<BuildEdgeBlock, int>(buildEdgeBlock, 0));
					List<SymmetryController.MirrorInfo> mirrorInfo2 = symmetryController.GetMirrorInfo(BuildingMachine.localToWorldMatrix.MultiplyPoint3x4(buildEdgeBlock.Position), rotation * buildEdgeBlock.Rotation);
					mirrorInfo2.ForEach(delegate(SymmetryController.MirrorInfo x)
					{
						if (mirrorInfo.FindIndex((SymmetryController.MirrorInfo y) => x.Position == y.Position) == -1)
						{
							mirrorInfo.Add(x);
						}
					});
				}
				for (int num = 0; num < buildingBlocks.Count; num++)
				{
					b = buildingBlocks[num];
					if (b.Prefab.Type != type)
					{
						continue;
					}
					Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(b.Position);
					for (int num2 = 0; num2 < mirrorInfo.Count; num2++)
					{
						SymmetryController.MirrorInfo mirrorInfo3 = mirrorInfo[num2];
						if (vector == mirrorInfo3.Position && list2.FindIndex((Tuple<BuildEdgeBlock, int> x) => x.Item1 == b) == -1)
						{
							list2.Add(new Tuple<BuildEdgeBlock, int>(b as BuildEdgeBlock, mirrorInfo3.Index));
						}
					}
				}
				if (list2.Count > surface.edges.Length)
				{
					for (int num3 = 0; num3 < buildingBlocks.Count; num3++)
					{
						if (buildingBlocks[num3].Prefab.Type != BlockType.BuildSurface)
						{
							continue;
						}
						surface = buildingBlocks[num3] as BuildSurface;
						if (!surface.isValid || surface == block)
						{
							continue;
						}
						int num4 = -1;
						bool flag = true;
						for (int j = 0; j < surface.edges.Length; j++)
						{
							int num5 = list2.FindIndex((Tuple<BuildEdgeBlock, int> x) => x.Item1 == surface.edges[j]);
							if (surface.edges[j].isValid && num5 != -1)
							{
								num4 = Mathf.Max(list2[num5].Item2, num4);
								continue;
							}
							flag = false;
							break;
						}
						if (flag)
						{
							list.Add(new Tuple<BlockBehaviour, int>(surface, num4));
						}
					}
				}
			}
		}
		else
		{
			mirrorInfo = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController.GetMirrorInfo(BuildingMachine.localToWorldMatrix.MultiplyPoint3x4(block.Position), BuildingMachine.rotation * block.Rotation);
			if (mirrorInfo.Count > 0)
			{
				for (int num6 = 0; num6 < buildingBlocks.Count; num6++)
				{
					b = buildingBlocks[num6];
					BlockType type = b.Prefab.Type;
					Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(b.Position);
					for (int num7 = 0; num7 < mirrorInfo.Count; num7++)
					{
						SymmetryController.MirrorInfo mirrorInfo4 = mirrorInfo[num7];
						if (b != block && type == block.Prefab.Type && vector == mirrorInfo4.Position && list.FindIndex((Tuple<BlockBehaviour, int> x) => x.Item1 == b) == -1)
						{
							list.Add(new Tuple<BlockBehaviour, int>(b, mirrorInfo4.Index));
						}
					}
				}
			}
		}
		return list;
	}

	public void RegisterBVCUpdate(BlockVisualController block, bool isBuild)
	{
		if (isBuild)
		{
			buildBVC.Add(block);
		}
		else if (!simBVC.Contains(block))
		{
			simBVC.Add(block);
		}
	}

	public bool SortEdge(BuildEdgeBlock edge)
	{
		int a = buildingBlocks.IndexOf(edge.startNode);
		int b = buildingBlocks.IndexOf(edge.endNode);
		int num = Mathf.Max(a, b);
		int num2 = buildingBlocks.IndexOf(edge);
		if (num2 < num)
		{
			int siblingIndex = buildingBlocks[num].transform.GetSiblingIndex();
			buildingBlocks.RemoveAt(num2);
			buildingBlocks.Insert(num, edge);
			edge.transform.SetSiblingIndex(siblingIndex);
			return true;
		}
		return false;
	}

	public bool SortSurface(BuildSurface surface)
	{
		bool result = false;
		if (surface.isValid)
		{
			for (int i = 0; i < surface.edges.Length; i++)
			{
				BuildEdgeBlock buildEdgeBlock = surface.edges[i];
				if (buildEdgeBlock.isValid)
				{
					int num = buildingBlocks.IndexOf(surface);
					int num2 = buildingBlocks.IndexOf(buildEdgeBlock);
					if (num2 > num)
					{
						int siblingIndex = buildEdgeBlock.transform.GetSiblingIndex();
						buildingBlocks.RemoveAt(num);
						buildingBlocks.Insert(num2, surface);
						surface.transform.SetSiblingIndex(siblingIndex);
						result = true;
					}
				}
			}
		}
		return result;
	}

	public void ToggleUndo(bool toggle)
	{
		isLoadingDifference = (isLoadingInfo = toggle);
	}

	public void RegisterSurfaceBlock(BlockBehaviour block)
	{
		if (!visibleSurfaceBlocks.Contains(block))
		{
			visibleSurfaceBlocks.Add(block);
		}
	}

	public void UnregisterSurfaceBlock(BlockBehaviour block)
	{
		visibleSurfaceBlocks.Remove(block);
	}

	public bool OverrideHover(Ray ray, bool mouseHasHit, RaycastHit hitInfo, BlockBehaviour currentBlock, out BlockBehaviour block)
	{
		List<Tuple<float, BlockBehaviour>> list = OverlapSurfaceBlocks(ray, mouseHasHit, hitInfo, currentBlock);
		if (list.Count == 0)
		{
			block = null;
			return false;
		}
		float item = list[0].Item1;
		block = list[0].Item2;
		return !mouseHasHit || item < hitInfo.distance;
	}

	public List<Tuple<float, BlockBehaviour>> OverlapSurfaceBlocks(Ray ray, bool mouseHasHit, RaycastHit hitInfo, BlockBehaviour currentBlock)
	{
		float num = 300f;
		List<Tuple<float, BlockBehaviour>> list = new List<Tuple<float, BlockBehaviour>>();
		BlockBehaviour b;
		for (int i = 0; i < visibleSurfaceBlocks.Count; i++)
		{
			b = visibleSurfaceBlocks[i];
			float dist;
			switch (b.Prefab.Type)
			{
			case BlockType.BuildNode:
			{
				BuildNodeBlock buildNodeBlock = b as BuildNodeBlock;
				if (buildNodeBlock.RayHit(ray, out dist) && dist < num)
				{
					list.Add(new Tuple<float, BlockBehaviour>(Mathf.Max(0f, dist - buildNodeBlock.Radius * 1.5f), b));
				}
				break;
			}
			case BlockType.BuildEdge:
			{
				BuildEdgeBlock buildEdgeBlock = b as BuildEdgeBlock;
				if (buildEdgeBlock.RayHit(ray, out dist) && dist < num)
				{
					list.Add(new Tuple<float, BlockBehaviour>(Mathf.Max(0f, dist - buildEdgeBlock.Radius * 1.5f), b));
				}
				break;
			}
			}
		}
		if (mouseHasHit && hitInfo.distance < num)
		{
			if (hitInfo.collider != null)
			{
				b = hitInfo.collider.GetComponentInParent<BlockBehaviour>();
			}
			else
			{
				b = currentBlock;
			}
			if (b != null && list.FindIndex((Tuple<float, BlockBehaviour> x) => x.Item2 == b) == -1)
			{
				list.Add(new Tuple<float, BlockBehaviour>(hitInfo.distance, b));
			}
		}
		list.Sort((Tuple<float, BlockBehaviour> x, Tuple<float, BlockBehaviour> y) => x.Item1.CompareTo(y.Item1));
		return list;
	}

	public void SetRigidInterpolation(RigidbodyInterpolation interp, List<BlockBehaviour> blockList = null)
	{
		if (blockList == null)
		{
			blockList = buildingBlocks;
		}
		oldInterpolation.Clear();
		foreach (BlockBehaviour block in blockList)
		{
			if (!block.noRigidbody && !oldInterpolation.ContainsKey(block.Rigidbody))
			{
				oldInterpolation.Add(block.Rigidbody, block.Rigidbody.interpolation);
				block.Rigidbody.interpolation = interp;
			}
		}
		hasChangedInterpolation = true;
	}

	public void RestoreRigidInterpolation()
	{
		if (hasChangedInterpolation)
		{
			Dictionary<Rigidbody, RigidbodyInterpolation>.Enumerator enumerator = oldInterpolation.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Rigidbody, RigidbodyInterpolation> current = enumerator.Current;
				current.Key.interpolation = current.Value;
			}
			hasChangedInterpolation = false;
		}
	}

	public void UnregisterBVCUpdate(BlockVisualController bvc, bool isBuild)
	{
		if (isBuild)
		{
			if (buildBVC.Contains(bvc))
			{
				buildBVC.Remove(bvc);
			}
		}
		else if (simBVC.Contains(bvc))
		{
			simBVC.Remove(bvc);
		}
	}

	public void RegisterUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (!block.RegisteredBuildUpdate)
			{
				buildUpdate.Add(block);
				block.RegisteredBuildUpdate = true;
			}
		}
		else if (!block.RegisteredSimUpdate)
		{
			simUpdate.Add(block);
			block.RegisteredSimUpdate = true;
		}
	}

	public void AddToSimUpdate(BlockBehaviour block)
	{
		simUpdate.Add(block);
	}

	public bool ContainedInSimUpdate(BlockBehaviour block)
	{
		return simUpdate.Contains(block);
	}

	public void RemoveFromSimUpdate(BlockBehaviour block)
	{
		simUpdate.Remove(block);
	}

	public void RegisterFixedUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (!block.RegisteredBuildFixedUpdate)
			{
				buildFixedUpdate.Add(block);
				block.RegisteredBuildFixedUpdate = true;
			}
		}
		else if (!block.RegisteredSimFixedUpdate)
		{
			if (!block.RegisteredSimEmulationUpdate)
			{
				simFixedUpdate.Add(block);
			}
			block.RegisteredSimFixedUpdate = true;
		}
	}

	public void RegisterEmulationUpdate(BlockBehaviour block)
	{
		if (!HasEmulationBlocks)
		{
			return;
		}
		if (!block.RegisteredSimEmulationUpdate)
		{
			if (!block.RegisteredSimFixedUpdate)
			{
				simFixedUpdate.Add(block);
			}
			block.RegisteredSimEmulationUpdate = true;
		}
		if (block.Prefab.EmulatesAnyKeys && !block.RegisteredSimSendEmulationUpdate)
		{
			simSendEmulationUpdate.Add(block);
			block.RegisteredSimSendEmulationUpdate = true;
		}
	}

	public void RegisterLateUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (!block.RegisteredBuildLateUpdate)
			{
				buildLateUpdate.Add(block);
				block.RegisteredBuildLateUpdate = true;
			}
		}
		else if (!block.RegisteredSimLateUpdate)
		{
			simLateUpdate.Add(block);
			block.RegisteredSimLateUpdate = true;
		}
	}

	public void UnregisterUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (block.RegisteredBuildUpdate)
			{
				buildUpdate.Remove(block);
				block.RegisteredBuildUpdate = false;
			}
		}
		else if (block.RegisteredSimUpdate)
		{
			simUpdate.Remove(block);
			block.RegisteredSimUpdate = false;
		}
	}

	public void UnregisterFixedUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (block.RegisteredBuildFixedUpdate)
			{
				buildFixedUpdate.Remove(block);
				block.RegisteredBuildFixedUpdate = false;
			}
		}
		else if (block.RegisteredSimFixedUpdate)
		{
			if (!block.RegisteredSimEmulationUpdate)
			{
				simFixedUpdate.Remove(block);
			}
			block.RegisteredSimFixedUpdate = false;
		}
	}

	public void UnregisterEmulationUpdate(BlockBehaviour block)
	{
		if (!HasEmulationBlocks)
		{
			return;
		}
		if (block.RegisteredSimEmulationUpdate)
		{
			if (!block.RegisteredSimFixedUpdate)
			{
				simFixedUpdate.Remove(block);
			}
			block.RegisteredSimEmulationUpdate = false;
		}
		if (block.RegisteredSimSendEmulationUpdate)
		{
			simSendEmulationUpdate.Remove(block);
			block.RegisteredSimSendEmulationUpdate = false;
		}
	}

	public void UnregisterLateUpdate(BlockBehaviour block, bool isBuild)
	{
		if (isBuild)
		{
			if (block.RegisteredBuildLateUpdate)
			{
				buildLateUpdate.Remove(block);
				block.RegisteredBuildLateUpdate = false;
			}
		}
		else if (block.RegisteredSimLateUpdate)
		{
			simLateUpdate.Remove(block);
			block.RegisteredSimLateUpdate = false;
		}
	}

	protected virtual void Awake()
	{
		if (!ReferenceMaster.machineSimulationStates.ContainsKey(base.transform.root))
		{
			ReferenceMaster.machineSimulationStates.Add(base.transform.root, false);
		}
		GetBlocks(PlayerID);
		boundingBoxController = UnityEngine.Object.FindObjectOfType<BoundingBoxController>();
		if (boundingBoxController != null)
		{
			boundingBoxController.machine = this;
			boundingBoxController.Init();
		}
		else
		{
			Debug.LogWarning("Could not find BoundingBoxController for Machine.");
		}
		inputController = base.gameObject.AddComponent<KeyInputController>();
		base.transform.position = Vector3.zero;
		AwakeBase();
		SingleInstance<MachineObjectTracker>.Instance.SetActiveMachine(this);
		isLocalMachine = true;
		nodeController.Initialize();
	}

	public void GetBlocks(uint playerId)
	{
		buildingBlocks = ReferenceMaster.GetBuildingBlocks(playerId);
		simBlocks = ReferenceMaster.GetSimulationBlocks(playerId);
		buildingBlocks.Clear();
		guidToBlock.Clear();
		_blockCount = 0;
		_blocksCost = 0;
		LoadedBlockScore = 0;
		simBlocks.Clear();
	}

	protected virtual void AwakeBase()
	{
		nodeController = new NodeController(this);
		lastMachinePosition = _basePosition;
		buildingMachine = new GameObject("Building Machine").transform;
		buildingMachine.SetParent(base.transform);
		buildingMachine.localPosition = _basePosition;
		buildingMachine.localRotation = _baseRotation;
		spawner = new GameObject("Machine Spawn Position").transform;
		spawner.SetParent(buildingMachine);
		spawner.localPosition = Vector3.zero;
		spawner.localRotation = Quaternion.identity;
		lastCenterPos = buildingMachine.position;
		addPiece = SingleInstanceFindOnly<AddPiece>.Instance;
		linkManager = base.gameObject.GetComponent<BlockLinkManager>();
		analyzer = base.gameObject.GetComponent<MachineAnalyzer>();
		analyzer.Init(linkManager, this);
		undoSystem = base.gameObject.GetComponent<UndoSystem>();
		undoSystem.Machine = this;
		ReferenceMaster.UndoSystemGO = base.gameObject;
	}

	public virtual Vector3 CalculateMiddle()
	{
		if (isSimulating)
		{
			if (!isReady)
			{
				return _simMiddle;
			}
			return _simMiddle = CalculateSimPos();
		}
		_machineMiddle = (_simMiddle = CalculateBuildPos());
		_lastMiddle = (_machineMiddle = buildingMachine.InverseTransformPoint(_machineMiddle));
		return MiddlePosition;
	}

	private Vector3 CalculateBuildPos()
	{
		int count = buildingBlocks.Count;
		Bounds bounds = new Bounds(buildingBlocks[0].GetCenter(), Vector3.zero);
		Vector3 vector = Vector3.zero;
		int num = 0;
		int num2 = (OptionsMaster.BesiegeConfig.UseBoundsCenter ? 1 : 0);
		for (int i = num2; i < count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (object.ReferenceEquals(blockBehaviour, null) || blockBehaviour.IsDestroyed)
			{
				continue;
			}
			BlockType type = blockBehaviour.Prefab.Type;
			BlockType blockType = type;
			if (blockType != BlockType.Pin && blockType != BlockType.CameraBlock && blockType != BlockType.BuildNode && blockType != BlockType.BuildEdge)
			{
				Vector3 center = blockBehaviour.GetCenter();
				if (OptionsMaster.BesiegeConfig.UseBoundsCenter)
				{
					bounds.Encapsulate(center);
				}
				else
				{
					vector = ((num++ != 0) ? (vector + center) : center);
				}
			}
		}
		if (OptionsMaster.BesiegeConfig.UseBoundsCenter)
		{
			return bounds.center;
		}
		return (num != 0) ? (vector / num) : buildingMachine.position;
	}

	protected BlockBehaviour GetRefBlock()
	{
		if (simBlocks.Count == 0)
		{
			Debug.LogError("[Machine]: Missing Reference Block, lack of simulation blocks to pick from.");
			return (!buildingBlocks[0].hasSimBlock) ? buildingBlocks[0] : buildingBlocks[0].SimBlock;
		}
		int num = simBlocks[0].ClusterIndex;
		if (num <= 0 || simClusters[num].count < 10 || !simBlocks[0].Prefab.clusterBaseCandidate)
		{
			int num2 = -1;
			int num3 = 0;
			for (int i = 0; i < simClusters.Length; i++)
			{
				SimCluster simCluster = simClusters[i];
				if (simCluster.count > num3)
				{
					num2 = i;
					num3 = simCluster.count;
				}
			}
			if (num2 > -1 && (num3 > 1 || num < 0))
			{
				BlockBehaviour blockBehaviour = simClusters[num2].Base;
				Vector3 vector = simClusters[num2].BaseTransform.TransformPoint(simClusters[num2].CenterOffset) + simClusters[num2].SimOffset;
				float num4 = float.MaxValue;
				for (int j = 0; j < simClusters[num2].Blocks.Length; j++)
				{
					BlockBehaviour blockBehaviour2 = simClusters[num2].Blocks[j];
					if (blockBehaviour2.Prefab.clusterBaseCandidate)
					{
						float sqrMagnitude = (blockBehaviour2.transform.position - vector).sqrMagnitude;
						if (sqrMagnitude < num4)
						{
							blockBehaviour = blockBehaviour2;
							num4 = sqrMagnitude;
						}
					}
				}
				refOffset = blockBehaviour.transform.InverseTransformPoint(vector);
				refUp = blockBehaviour.transform.InverseTransformDirection(Quaternion.Inverse(machineRotation) * Vector3.up);
				refFwd = blockBehaviour.transform.InverseTransformDirection(Quaternion.Inverse(machineRotation) * Vector3.forward);
				return blockBehaviour;
			}
			if (num < 0)
			{
				num = 0;
			}
		}
		Vector3 position = simBlocks[0].transform.TransformPoint(simClusters[num].CenterOffset) + simClusters[num].SimOffset;
		refOffset = simBlocks[0].transform.InverseTransformPoint(position);
		refUp = simBlocks[0].transform.InverseTransformDirection(Quaternion.Inverse(machineRotation) * Vector3.up);
		refFwd = simBlocks[0].transform.InverseTransformDirection(Quaternion.Inverse(machineRotation) * Vector3.forward);
		return simBlocks[0];
	}

	private Vector3 CalculateSimPos()
	{
		Vector3 vector = refBlockTransform.TransformPoint(refOffset);
		float num = linkManager.GetTotalBlocks();
		float num2 = 1f / num;
		float num3 = vector.x * num2;
		float num4 = vector.y * num2;
		float num5 = vector.z * num2;
		float num6 = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight - 10f;
		float num7 = num2;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < simClusters.Length; i++)
		{
			SimCluster simCluster = simClusters[i];
			if ((simCluster.Base.IsDestroyed && (!simCluster.Base.gameObject.activeInHierarchy || simCluster.Base.noRigidbody || simCluster.Base.Rigidbody.isKinematic)) || (simCluster.count < 2 && !simCluster.alwaysIncludeInCenter && !clusterSurplus))
			{
				continue;
			}
			zero = simCluster.BaseTransform.TransformPoint(simCluster.CenterOffset) + simCluster.BaseTransform.TransformDirection(simCluster.SimOffset);
			if (zero.y >= num6 || !OptionsMaster.clampMachineMiddleBlocksBelowFloor)
			{
				num2 = (zero - vector).sqrMagnitude;
				if (simCluster.alwaysIncludeInCenter || num2 < boundsSqrSize)
				{
					num2 = simCluster.Weight;
					num3 += zero.x * num2;
					num4 += zero.y * num2;
					num5 += zero.z * num2;
					num7 += num2;
				}
				else
				{
					num2 = Mathf.InverseLerp(boundsSqrSize * 2f, boundsSqrSize, num2) * simCluster.Weight;
					num3 += zero.x * num2;
					num4 += zero.y * num2;
					num5 += zero.z * num2;
					num7 += num2;
				}
			}
		}
		if (num7 > 0f)
		{
			float num8 = 1f / num7;
			return new Vector3(num3 * num8, num4 * num8, num5 * num8);
		}
		return vector;
	}

	protected List<BlockBehaviour> FindBlocks(BlockJointTrigger blockTrigger, bool findOtherTriggers)
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		TriggerSetJointBase trigger = blockTrigger.trigger;
		Collider[] array;
		if (!blockTrigger.usesRay && trigger.enabled)
		{
			LayerMask layerMask = ((!findOtherTriggers) ? AddPiece.CreateLayerMask(new int[2] { 12, 14 }) : AddPiece.CreateLayerMask(new int[3] { 12, 14, 22 }));
			Transform transform = trigger.transform;
			Collider component = transform.GetComponent<Collider>();
			if ((bool)component)
			{
				Bounds bounds = component.bounds;
				if (component is SphereCollider)
				{
					Vector3 extents = bounds.extents;
					float radius = Mathf.Min(extents.x, extents.y, extents.z);
					array = Physics.OverlapSphere(bounds.center, radius, layerMask);
				}
				else
				{
					array = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, layerMask);
				}
			}
			else
			{
				float radius2 = 0.26f;
				array = Physics.OverlapSphere(transform.position, radius2, layerMask);
			}
		}
		else
		{
			array = Physics.OverlapSphere(layerMask: (!findOtherTriggers) ? AddPiece.CreateLayerMask(new int[2] { 12, 14 }) : AddPiece.CreateLayerMask(new int[3] { 12, 14, 22 }), position: blockTrigger.pos, radius: blockTrigger.radius);
		}
		foreach (Collider collider in array)
		{
			if (!collider.gameObject.CompareTag("ClusterIgnore"))
			{
				BlockBehaviour componentInParent = collider.GetComponentInParent<BlockBehaviour>();
				if (componentInParent != null && (!StatMaster.isMP || componentInParent.ParentMachine == this) && componentInParent.BuildIndex != blockTrigger.block.BuildIndex && !list.Contains(componentInParent) && (collider.gameObject.layer != 22 || componentInParent.Prefab.Type == BlockType.BuildSurface))
				{
					list.Add(componentInParent);
				}
			}
		}
		return list;
	}

	protected Ray GetRay(Vector3 pos, Vector3 normal, BlockBehaviour block, bool isWorldSpace)
	{
		normal = ((!(pos.sqrMagnitude < linkRayLength)) ? normal : Vector3.back);
		Vector3 vector = pos - normal * linkRayNormalOffset;
		Vector3 direction = normal;
		if (!isWorldSpace)
		{
			vector = block.transform.TransformPoint(vector);
			direction = block.transform.TransformDirection(normal);
		}
		return new Ray(vector, direction);
	}

	protected Ray GetTriggerSetRay(Transform objTransform, BlockBehaviour block, bool isWorldSpace)
	{
		Vector3 localPosition = objTransform.localPosition;
		return GetRay(localPosition, localPosition.normalized, block, isWorldSpace);
	}

	public static string GetObjectPath(GameObject obj)
	{
		string text = "/" + obj.name;
		while (!object.ReferenceEquals(obj.transform.parent, null))
		{
			obj = obj.transform.parent.gameObject;
			text = "/" + obj.name + text;
		}
		return text;
	}

	public static int CompareTriggerPath(TriggerSetJointBase a, TriggerSetJointBase b)
	{
		string objectPath = GetObjectPath(a.gameObject);
		string objectPath2 = GetObjectPath(b.gameObject);
		return objectPath.CompareTo(objectPath2);
	}

	public void FindLinks(BlockBehaviour block, bool findAdjacentBlocks)
	{
		if (!hasAddPiece)
		{
			addPiece = SingleInstanceFindOnly<AddPiece>.Instance;
			if (addPiece == null)
			{
				Debug.LogWarning("Cannot find AddPiece, could not find links");
				return;
			}
			hasAddPiece = true;
		}
		if (BlockLinkManager.IgnoreType(block.Prefab.Type))
		{
			block.ClusterIndex = -2;
			return;
		}
		TriggerSetJointBase[] componentsInChildren = block.gameObject.GetComponentsInChildren<TriggerSetJointBase>();
		linkedBlocks.Clear();
		foreach (TriggerSetJointBase triggerSetJointBase in componentsInChildren)
		{
			if (!triggerSetJointBase.createLinks)
			{
				continue;
			}
			BlockJointTrigger blockTrigger = new BlockJointTrigger(block, triggerSetJointBase);
			List<BlockBehaviour> list = FindBlocks(blockTrigger, true);
			foreach (BlockBehaviour item in list)
			{
				if (item.BuildIndex != block.BuildIndex)
				{
					int buildIndex = item.BuildIndex;
					LinkedBlock value;
					if (!linkedBlocks.TryGetValue(buildIndex, out value))
					{
						linkedBlocks.Add(buildIndex, new LinkedBlock(item, triggerSetJointBase));
					}
					else
					{
						value.AddTrigger(triggerSetJointBase);
					}
				}
			}
		}
		List<int> list2 = new List<int>(linkedBlocks.Keys);
		for (int i = 0; i < list2.Count; i++)
		{
			LinkedBlock value = linkedBlocks[list2[i]];
			BlockBehaviour other = value.Other;
			bool flag = true;
			foreach (TriggerSetJointBase triggerJoint in value.TriggerJoints)
			{
				linkManager.Link(block, other, triggerJoint);
				if (!triggerJoint.canJoinMultiple)
				{
					flag = false;
				}
			}
			if (!flag)
			{
				break;
			}
		}
		if (!findAdjacentBlocks)
		{
			return;
		}
		LayerMask layerMask = AddPiece.CreateLayerMask(new int[1] { 22 });
		HashSet<BlockBehaviour> hashSet = new HashSet<BlockBehaviour>();
		Collider[] array;
		switch (block.Prefab.Type)
		{
		case BlockType.BuildSurface:
		{
			BuildSurface buildSurface = block as BuildSurface;
			for (int j = 0; j < buildSurface.AddingPoints.Length; j++)
			{
				BoxCollider component2 = buildSurface.AddingPoints[j].GetComponent<BoxCollider>();
				array = Physics.OverlapBox(component2.transform.position, component2.size * 0.5f, component2.transform.rotation, layerMask);
				for (int i = 0; i < array.Length; i++)
				{
					BlockBehaviour componentInParent3 = array[i].GetComponentInParent<BlockBehaviour>();
					if (!(componentInParent3 == null) && componentInParent3.BuildIndex != block.BuildIndex && !hashSet.Contains(componentInParent3))
					{
						hashSet.Add(componentInParent3);
						FindLinks(componentInParent3, false);
					}
				}
			}
			return;
		}
		case BlockType.Brace:
		case BlockType.Spring:
		case BlockType.RopeWinch:
		case BlockType.RopeMeasure:
		{
			GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
			SphereCollider component = genericDraggedBlock.startPoint.GetComponent<SphereCollider>();
			float num = Mathf.Max(component.transform.localScale.x, component.transform.localScale.y, component.transform.localScale.z);
			array = Physics.OverlapSphere(component.transform.position, component.radius * num);
			for (int i = 0; i < array.Length; i++)
			{
				BlockBehaviour componentInParent = array[i].GetComponentInParent<BlockBehaviour>();
				if (!(componentInParent == null) && componentInParent.BuildIndex != block.BuildIndex && !hashSet.Contains(componentInParent))
				{
					hashSet.Add(componentInParent);
					FindLinks(componentInParent, false);
				}
			}
			component = genericDraggedBlock.endPoint.GetComponent<SphereCollider>();
			num = Mathf.Max(component.transform.localScale.x, component.transform.localScale.y, component.transform.localScale.z);
			array = Physics.OverlapSphere(component.transform.position, component.radius * num);
			for (int i = 0; i < array.Length; i++)
			{
				BlockBehaviour componentInParent2 = array[i].GetComponentInParent<BlockBehaviour>();
				if (!(componentInParent2 == null) && componentInParent2.BuildIndex != block.BuildIndex && !hashSet.Contains(componentInParent2))
				{
					hashSet.Add(componentInParent2);
					FindLinks(componentInParent2, false);
				}
			}
			return;
		}
		}
		Collider[] array2 = ((!block.Prefab.hasMyBounds) ? block.gameObject.GetComponentsInChildren<Collider>(true) : block.myBounds.childColliders.ToArray());
		Bounds bounds = default(Bounds);
		bool flag2 = false;
		foreach (Collider collider in array2)
		{
			if (collider.gameObject.layer == 12 || collider.gameObject.layer == 14)
			{
				Bounds bounds2 = collider.bounds;
				if (!flag2)
				{
					bounds = new Bounds(bounds2.center, bounds2.size);
					flag2 = true;
				}
				else
				{
					bounds.Encapsulate(bounds2);
				}
			}
		}
		if (!flag2)
		{
			Bounds bounds3 = block.MeshRenderer.bounds;
			bounds = new Bounds(bounds3.center, bounds3.size);
		}
		float num2 = 1f;
		Vector3 halfExtents = new Vector3(bounds.extents.x * 0.5f + num2, bounds.extents.y * 0.5f + num2, bounds.extents.z * 0.5f + num2);
		array = Physics.OverlapBox(bounds.center, halfExtents, Quaternion.identity, layerMask);
		for (int i = 0; i < array.Length; i++)
		{
			BlockBehaviour componentInParent4 = array[i].GetComponentInParent<BlockBehaviour>();
			if (!(componentInParent4 == null) && componentInParent4.BuildIndex != block.BuildIndex && !hashSet.Contains(componentInParent4))
			{
				hashSet.Add(componentInParent4);
				FindLinks(componentInParent4, false);
			}
		}
	}

	public static bool IsDraggedBlock(BlockType id)
	{
		switch (id)
		{
		case BlockType.Brace:
		case BlockType.Spring:
		case BlockType.RopeWinch:
		case BlockType.RopeMeasure:
			return true;
		default:
			return false;
		}
	}

	public void FinishDraggedBlocks(bool forceFail = false)
	{
		List<UndoAction> list = new List<UndoAction>();
		List<GenericDraggedBlock> list2 = new List<GenericDraggedBlock>(currentDragged);
		for (int i = 0; i < list2.Count; i++)
		{
			GenericDraggedBlock genericDraggedBlock = list2[i];
			if (genericDraggedBlock.Set(forceFail))
			{
				AddDraggedBlock(genericDraggedBlock);
				list.Add(new UndoActionAdd(this, BlockInfo.FromBlockBehaviour(genericDraggedBlock)));
			}
		}
		if (currentDragged.Count > 0 && ReferenceMaster.onDraggedBlockPlaced != null)
		{
			ReferenceMaster.onDraggedBlockPlaced(currentDragged[0]);
		}
		currentDragged.Clear();
		list2.Clear();
		if (list.Count > 0)
		{
			if (ReferenceMaster.onMachineModified != null)
			{
				ReferenceMaster.onMachineModified(this);
			}
			UndoSystem.AddActions(list);
		}
	}

	public virtual void AddDraggedBlock(GenericDraggedBlock block)
	{
		BlockNode node;
		linkManager.AddBlock(block, out node);
		if (!isLoadingInfo)
		{
			block.PlacementComplete = true;
			block.SaveInitialData();
			analyzer.FindLinks(OptionsMaster.linkDelayFrames, block, true);
		}
	}

	public bool GetBlockFromIndex(int blockIndex, out BlockBehaviour block)
	{
		if (blockIndex >= buildingBlocks.Count)
		{
			Debug.LogError("Couldn't get block from index " + blockIndex + " (" + buildingBlocks.Count + ")!\n" + Environment.StackTrace);
			block = null;
			return false;
		}
		block = buildingBlocks[blockIndex];
		return true;
	}

	public BlockBehaviour GetSimBlock(BlockBehaviour block)
	{
		if (block.hasSimBlock)
		{
			return block.SimBlock;
		}
		int buildIndex = block.BuildIndex;
		if (buildIndex == -1 || buildIndex >= SimulationMachine.childCount)
		{
			Debug.LogError("Couldn't find sim block for " + block.name, block.gameObject);
			return null;
		}
		Transform child = SimulationMachine.GetChild(buildIndex);
		BlockBehaviour blockBehaviour = (block.SimBlock = child.GetComponent<BlockBehaviour>());
		block.hasSimBlock = !object.ReferenceEquals(blockBehaviour, null);
		if (!block.hasSimBlock)
		{
			Debug.LogError("Couldn't fetch BlockBehaviour on " + child.name + "!");
		}
		return blockBehaviour;
	}

	private void DisplayTemp(Renderer r, bool copyMaterialPropertyBlock)
	{
		Transform transform = r.transform;
		Transform original = tempVisTransform;
		if (r is SkinnedMeshRenderer)
		{
			original = tempSkinnedTransform;
			SkinnedMeshRenderer skinnedMeshRenderer = r as SkinnedMeshRenderer;
			tempSkinned.sharedMesh = skinnedMeshRenderer.sharedMesh;
			tempSkinned.sharedMaterials = skinnedMeshRenderer.sharedMaterials;
		}
		else
		{
			tempFilter.sharedMesh = r.GetComponent<MeshFilter>().sharedMesh;
			tempRenderer.sharedMaterials = r.sharedMaterials;
		}
		Vector3 position = spawner.TransformPoint(buildingMachine.InverseTransformPoint(transform.position));
		Quaternion rotation = spawner.rotation * Quaternion.Inverse(buildingMachine.rotation) * transform.rotation;
		Transform transform2 = UnityEngine.Object.Instantiate(original, position, rotation) as Transform;
		transform2.localScale = transform.lossyScale;
		transform2.parent = display;
		transform2.gameObject.layer = transform.gameObject.layer;
		if (copyMaterialPropertyBlock || StatMaster.aeroCoded || StatMaster.stressCoded)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			r.GetPropertyBlock(materialPropertyBlock);
			transform2.gameObject.GetComponent<Renderer>().SetPropertyBlock(materialPropertyBlock);
		}
	}

	protected void InitSimBlock(BlockBehaviour block, BlockBehaviour oldBlock, uint index)
	{
		bool flag = resetNeeded;
		BlockVisualController visualController = oldBlock.VisualController;
		BlockVisualController visualController2 = block.VisualController;
		block.Prefab = oldBlock.Prefab;
		block.RegisterSimUpdates();
		block.NodeIndex = oldBlock.NodeIndex;
		block.ClusterIndex = oldBlock.ClusterIndex;
		block.Team = MPTeam.None;
		block.ShelterAmount = 1f;
		if (visualController.selectedSkin != null)
		{
			visualController2.selectedSkin = visualController.selectedSkin;
		}
		if (block.Prefab.hasArrow)
		{
			UnityEngine.Object.Destroy(visualController2.arrow);
		}
		if (block.DestroyOnSimulate.Length > 0 || block.DestroyOnClient.Length > 0)
		{
			if (block.DestroyOnSimulate.Length > 0 && block.Prefab.physFinishSetting != PhysFinishSetting.Immediate)
			{
				PhysFinishSetting physFinishSetting = block.Prefab.physFinishSetting;
				if (physFinishSetting == PhysFinishSetting.Early)
				{
					physPhaseEarly.Add(block);
				}
				else
				{
					physPhaseNormal.Add(block);
				}
			}
			else
			{
				block.FinishDestroySimulate();
			}
		}
		if (block.Prefab.hasFragment && (block.VisualController as FragmentVisualController).brokenVis.Length > 1)
		{
			physFragmentBlocks.Add(block);
		}
		bool simPhysics = SimPhysics;
		if (!simPhysics || block.Prefab.SetBreakForce)
		{
			physFinishBlocks.Add(block);
		}
		switch (block.Prefab.Type)
		{
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
			SingleInstance<Events>.Instance.BlockInit(block);
			return;
		case BlockType.CameraBlock:
			visualController2.SetInvisible();
			flag = false;
			break;
		}
		if (flag)
		{
			if (simPhysics && !block.noRigidbody)
			{
				block.Rigidbody.interpolation = RigidbodyInterpolation.None;
			}
			visualController2.SetInvisible();
			if (!StatMaster.isHeadless)
			{
				bool flag2 = false;
				bool copyMaterialPropertyBlock = false;
				BlockType type = block.Prefab.Type;
				if (type == BlockType.BuildSurface || type == BlockType.SqrBalloon)
				{
					copyMaterialPropertyBlock = true;
				}
				if (visualController is BlockSkinnedVisualController)
				{
					flag2 = true;
					DisplayTemp((visualController as BlockSkinnedVisualController).meshRenderer, copyMaterialPropertyBlock);
				}
				else
				{
					MeshRenderer[] renderers = visualController.renderers;
					foreach (Renderer renderer in renderers)
					{
						if (!(renderer == null) && renderer.enabled)
						{
							flag2 = true;
							DisplayTemp(renderer, copyMaterialPropertyBlock);
						}
					}
					Renderer shortVis;
					if (visualController.GetShortRenderer(out shortVis))
					{
						Renderer renderer2 = shortVis;
						if (!flag2 && renderer2.enabled)
						{
							DisplayTemp(renderer2, copyMaterialPropertyBlock);
						}
					}
				}
			}
		}
		SingleInstance<Events>.Instance.BlockInit(block);
		XDataHolder lastState = oldBlock.LastState;
		block.OnLoad(lastState);
		block.SetInputController(inputController);
		if (simPhysics)
		{
			for (int j = 0; j < block.KeyList.Count; j++)
			{
				MKey mKey = block.KeyList[j];
				mKey.SetInputController(inputController);
				for (int k = 0; k < mKey.KeysCount; k++)
				{
					KeyCode key = mKey.GetKey(k);
					inputController.AddMKey(block, mKey, key);
					inputController.Add(key);
				}
			}
			return;
		}
		for (int l = 0; l < oldBlock.KeyList.Count; l++)
		{
			MKey mKey2 = oldBlock.KeyList[l];
			for (int m = 0; m < mKey2.KeysCount; m++)
			{
				inputController.Add(mKey2.GetKey(m));
			}
		}
		for (int l = 0; l < block.KeyList.Count; l++)
		{
			MKey mKey3 = block.KeyList[l];
			mKey3.SetInputController(inputController);
			for (int m = 0; m < mKey3.KeysCount; m++)
			{
				inputController.AddMKey(block, mKey3, mKey3.GetKey(m));
			}
		}
	}

	protected virtual void InitSim()
	{
		inputController.Clear();
		ReferenceMaster.IgnoreBreakCollisions = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("IgnoreBreakCollision"));
	}

	protected virtual void PostSimStart()
	{
		SingleInstance<Events>.Instance.MachineSimulate(this);
		if (ReferenceMaster.onMachinePostSim != null)
		{
			ReferenceMaster.onMachinePostSim(this);
		}
	}

	public virtual void StartSimulation()
	{
		spawner.parent = null;
		if (IsDraggingBlocks)
		{
			FinishDraggedBlocks();
		}
		if (!isRespawning)
		{
			inputController.Toggle(true);
		}
		isReady = false;
		hasFiredProjectiles = false;
		setColliderIterations = false;
		finishedPhysics = (finishedNormalPhysics = false);
		finishPhysicsFrameCount = (finishNormalFrameCount = 0);
		isSimulating = true;
		ReferenceMaster.machineSimulationStates[base.transform.root] = isSimulating;
		undoSystem.enabled = false;
		physPhaseNormal.Clear();
		physPhaseEarly.Clear();
		physFinishBlocks.Clear();
		physFragmentBlocks.Clear();
		if (isLocalMachine)
		{
			addPiece.CloseMappers();
			StatMaster.waitingForSim = false;
			ReferenceMaster.activeMachineSimulating = isSimulating;
			_simMiddle = addPiece.middleOfObject.position;
			ReferenceMaster.onLocalMachineSimulation(true);
		}
		if (!isRespawning)
		{
			boundingBoxController.FadeVis();
		}
		Transform transform = spawner;
		machineRotation = transform.rotation;
		bool simPhysics = SimPhysics;
		resetNeeded = simPhysics && machineRotation != Quaternion.identity && machineRotation != negativeIdentity;
		if (simPhysics)
		{
			buildingMachine.gameObject.SetActive(false);
		}
		if (ReferenceMaster.onMachineSimulation != null)
		{
			ReferenceMaster.onMachineSimulation(this, true);
		}
		simulationArray = new BlockBehaviour[buildingBlocks.Count];
		InitialSimCount = buildingBlocks.Count;
		if (OnBeforeClone != null)
		{
			OnBeforeClone();
		}
		tempSim = UnityEngine.Object.Instantiate(buildingMachine, transform.position, simPhysics ? Quaternion.identity : machineRotation, base.transform) as Transform;
		if (OnAfterClone != null)
		{
			OnAfterClone();
		}
		hasTempSim = true;
		tempSim.gameObject.name = "Simulation Machine";
		if (simPhysics)
		{
			tempSim.gameObject.SetActive(true);
		}
		else
		{
			buildingMachine.gameObject.SetActive(false);
		}
		if (resetNeeded && !StatMaster.isHeadless)
		{
			display = new GameObject("Temporary Display").transform;
			tempVis = new GameObject("Temporary Vis Block");
			tempVisTransform = tempVis.transform;
			tempFilter = tempVis.AddComponent<MeshFilter>();
			tempRenderer = tempVis.AddComponent<MeshRenderer>();
			tempSkinnedVis = new GameObject("Temporary Skinned Block");
			tempSkinnedTransform = tempSkinnedVis.transform;
			tempSkinned = tempSkinnedVis.AddComponent<SkinnedMeshRenderer>();
			tempSkinned.rootBone = tempSkinnedTransform;
		}
		InitSim();
		InitBlocks();
		if (resetNeeded && !StatMaster.isHeadless)
		{
			UnityEngine.Object.Destroy(tempVis);
			UnityEngine.Object.Destroy(tempSkinnedVis);
		}
		PostSimStart();
		spawner.parent = buildingMachine;
		ReferenceMaster.OnMachineBeginSimulation();
	}

	public bool AddSimBlock(BlockBehaviour block)
	{
		if (simBlocks.Count >= simulationArray.Length)
		{
			return false;
		}
		simulationArray[simBlocks.Count] = block;
		simBlocks.Add(block);
		return true;
	}

	protected virtual void InitBlocks()
	{
		int count = linkManager.Clusters.Count;
		simClusters = new SimCluster[count];
		clusterSurplus = BlockCount < simClusters.Length + 3;
		uint num = 0u;
		HasEmulationBlocks = SingleInstanceFindOnly<CinematicCam>.hasInstance() && SingleInstanceFindOnly<CinematicCam>.Instance.emulateKey;
		for (int i = 0; i < simBlocks.Count; i++)
		{
			if (simBlocks[i].BuildingBlock.Prefab.EmulatesAnyKeys)
			{
				HasEmulationBlocks = true;
				break;
			}
		}
		inputController.SetHasAnyEmulation(HasEmulationBlocks);
		for (int i = 0; i < simBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = simBlocks[i];
			InitSimBlock(blockBehaviour, blockBehaviour.BuildingBlock, num++);
		}
		InitSimClusters();
		refBlock = GetRefBlock();
		refBlockTransform = refBlock.transform;
		if (hasCenterBlock)
		{
			Transform transform = centerBlock.SimBlock.transform;
			if (transform != null)
			{
				centerBlockTransform = transform;
			}
		}
	}

	protected virtual void InitSimClusters()
	{
		Vector3 vector = tempSim.TransformPoint(_machineMiddle);
		Vector3 zero = Vector3.zero;
		float num = 0f;
		for (int i = 0; i < linkManager.Clusters.Count; i++)
		{
			BlockCluster blockCluster = linkManager.Clusters[i];
			BlockBehaviour block = blockCluster.Base.Block;
			Transform transform = block.SimBlock.transform;
			SimCluster simCluster = (simClusters[i] = new SimCluster(block.SimBlock, blockCluster.CenterOffset, blockCluster.BlockWeight, blockCluster.Blocks.Count));
			simCluster.alwaysIncludeInCenter = AlwaysIncludeInCenter(block.BlockID);
			int count = blockCluster.Blocks.Count;
			simClusters[i].Blocks = new BlockBehaviour[count - 1];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				BlockBehaviour block2 = blockCluster.Blocks[j].Block;
				if (block2.NodeIndex != block.NodeIndex)
				{
					BlockBehaviour simBlock = GetSimBlock(block2);
					simClusters[i].Blocks[num2++] = simBlock;
				}
			}
			if (simCluster.count >= 2 || simCluster.alwaysIncludeInCenter || clusterSurplus)
			{
				zero += simCluster.Base.transform.TransformPoint(simCluster.CenterOffset) * simCluster.Weight;
				num += simCluster.Weight;
			}
		}
		Vector3 direction = vector - zero / num;
		for (int k = 0; k < linkManager.Clusters.Count; k++)
		{
			SimCluster simCluster = simClusters[k];
			simCluster.SimOffset = simCluster.BaseTransform.InverseTransformDirection(direction);
		}
	}

	protected bool AlwaysIncludeInCenter(int id)
	{
		switch ((BlockType)id)
		{
		case BlockType.Rocket:
			if (!ignoreRocket)
			{
				return true;
			}
			return false;
		case BlockType.BuildSurface:
			return true;
		default:
			return false;
		}
	}

	public void DestroySimMachine()
	{
		if (simulationClone != null)
		{
			MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
			if (instance != null && instance.targetType == MouseOrbit.TargetType.Block && instance.targetInfo.isSimulating && instance.targetInfo.ParentMachine == this)
			{
				instance.target = (instance.targetInfo = (instance.targetInfo as BlockBehaviour).BuildingBlock).transform;
			}
			bool flag = curtainMode && !isLocalMachine;
			for (int i = 0; i < buildingBlocks.Count; i++)
			{
				BlockBehaviour blockBehaviour = buildingBlocks[i];
				if (blockBehaviour.hasSimBlock)
				{
					blockBehaviour.SimBlock = null;
					blockBehaviour.hasSimBlock = false;
				}
				if (flag)
				{
					blockBehaviour.VisualController.SetNormal();
				}
			}
			UnityEngine.Object.Destroy(simulationClone.gameObject);
			simulationClone = null;
		}
		simBlocks.Clear();
		if (!isRespawning || (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim))
		{
			buildingMachine.gameObject.SetActive(true);
		}
	}

	public virtual void EndSimulation()
	{
		if (!isRespawning)
		{
			inputController.Toggle(false);
		}
		SoftEndSimulation();
		spawner.SetParent(buildingMachine);
		spawner.localPosition = Vector3.zero;
		spawner.localRotation = Quaternion.identity;
		hasIntactBlocks = true;
	}

	public virtual void SoftEndSimulation()
	{
		if (!isSimulating)
		{
			return;
		}
		isReady = false;
		isSimulating = false;
		undoSystem.enabled = true;
		ReferenceMaster.machineSimulationStates[base.transform.root] = isSimulating;
		if (hasCenterBlock)
		{
			centerBlockTransform = centerBlock.transform;
			lastCenterPos = centerBlockTransform.position;
		}
		simUpdate.Clear();
		simFixedUpdate.Clear();
		simSendEmulationUpdate.Clear();
		simLateUpdate.Clear();
		simBVC.Clear();
		ReferenceMaster.ClearSimulationBlocks(PlayerID);
		if (isLocalMachine)
		{
			if (!isRespawning)
			{
				addPiece.ReopenMappers(this);
			}
			StatMaster.waitingForSim = false;
			_machineMiddle = _lastMiddle;
			ReferenceMaster.activeMachineSimulating = isSimulating;
			ReferenceMaster.onLocalMachineSimulation(false);
		}
		if (ReferenceMaster.onMachineSimulation != null)
		{
			ReferenceMaster.onMachineSimulation(this, false);
		}
		ReferenceMaster.blocksInSim -= buildingBlocks.Count;
		DestroySimMachine();
		if (!isRespawning)
		{
			boundingBoxController.FadeVis();
		}
		isReady = true;
		SingleInstance<Events>.Instance.MachineStopSimulate(this);
	}

	public virtual void MoveBlock(Guid guid, Vector3 pos)
	{
		BlockBehaviour block;
		if (GetBlock(guid, out block))
		{
			block.SetPosition(pos);
		}
	}

	public virtual void RotateBlock(Guid guid, Quaternion rot)
	{
		BlockBehaviour block;
		if (GetBlock(guid, out block))
		{
			block.SetRotation(rot);
		}
	}

	public virtual void ScaleBlock(Guid guid, Vector3 scale)
	{
		BlockBehaviour block;
		if (GetBlock(guid, out block))
		{
			block.SetScale(scale);
		}
	}

	public virtual bool AddBlock(BlockInfo blockInfo, out BlockBehaviour block)
	{
		return AddBlock(blockInfo, true, out block);
	}

	private bool AddBlock(BlockInfo blockInfo, bool tryAgain, out BlockBehaviour block)
	{
		if (!SpawnBlock(blockInfo, tryAgain, out block))
		{
			block = null;
			return false;
		}
		PostAddBlock(xdataholder: (!AddPiece.usingCopiedBlock || AddPiece.copiedBlockData == null) ? blockInfo.BlockData : AddPiece.copiedBlockData.Clone(), blockClone: block, blockInfo: blockInfo);
		return true;
	}

	private bool SpawnBlock(BlockInfo blockInfo, bool tryAgain, out BlockBehaviour block)
	{
		if (blockInfo.ID == BlockType.Unused)
		{
			Debug.Log("Tried loading block id 8, unused block known to cause issues, and interfere with modding, refrained from loading block.");
			block = null;
			return false;
		}
		PrefabMaster.PrefabType prefabType = PrefabMaster.PrefabType.Normal;
		if (StatMaster.isClient && !isLocalMachine)
		{
			prefabType = PrefabMaster.PrefabType.Stripped;
		}
		else if (StatMaster.isMP)
		{
			prefabType = PrefabMaster.PrefabType.Network;
		}
		if (prefabType != PrefabMaster.PrefabType.Stripped && !DlcManager.Instance.GetBlockDLCStatus(blockInfo.ID))
		{
			Debug.LogWarning("Tried spawning non available DLC block.");
			block = null;
			return false;
		}
		BlockBehaviour block2;
		if (!PrefabMaster.GetBlock(blockInfo.ID, prefabType, out block2))
		{
			if (tryAgain)
			{
				Debug.LogWarning(string.Concat("There is no block with ID #", blockInfo.ID, ", trying again."));
				StartCoroutine(WaitAndTryAddBlockAgain(blockInfo, 0.2f));
			}
			else
			{
				Debug.LogWarning(string.Concat("There is no block with ID #", blockInfo.ID, "."));
			}
			block = null;
			return false;
		}
		if (isLocalMachine && (blockInfo.ID == BlockType.Pin || blockInfo.ID == BlockType.CameraBlock))
		{
			addPiece.checkVirtualBlocks = true;
		}
		BlockBehaviour blockBehaviour = UnityEngine.Object.Instantiate(block2, buildingMachine.TransformPoint(blockInfo.Position), buildingMachine.rotation * blockInfo.Rotation, buildingMachine) as BlockBehaviour;
		GameObject gameObject = blockBehaviour.gameObject;
		blockBehaviour.isBuildBlock = true;
		blockBehaviour.SetParentMachine(this);
		blockBehaviour.Prefab = block2.Prefab;
		if (SingleInstanceFindOnly<BlockLoader>.Instance.IsModBlock((int)blockInfo.ID))
		{
			PrefabMaster.AddNetworkBlock(blockBehaviour.gameObject);
		}
		blockBehaviour.Position = blockInfo.Position;
		blockBehaviour.Rotation = blockInfo.Rotation;
		blockBehaviour.transform.localScale = blockInfo.Scale;
		blockBehaviour.Scale = blockInfo.Scale;
		gameObject.SetActive(true);
		if (block2 == null)
		{
			Debug.LogError("Action Not Allowed");
			block = null;
			return false;
		}
		blockBehaviour.name = block2.name;
		block = blockBehaviour;
		return true;
	}

	public virtual void RebuildClusters()
	{
		linkManager.Reset();
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour block = buildingBlocks[i];
			BlockNode node;
			linkManager.AddBlock(block, out node);
			analyzer.FindLinks(0, block, true);
		}
	}

	public virtual void RebuildExistingClusters(List<BlockBehaviour> blocks)
	{
		BlockNode node;
		for (int i = 0; i < blocks.Count; i++)
		{
			BlockBehaviour block = blocks[i];
			node = linkManager.GetNode(block);
			linkManager.RemoveBlock(block);
		}
		for (int j = 0; j < blocks.Count; j++)
		{
			BlockBehaviour block = blocks[j];
			linkManager.AddBlock(block, out node);
			analyzer.FindLinks(0, block, true);
		}
	}

	protected virtual void PostAddBlock(BlockBehaviour blockClone, BlockInfo blockInfo, XDataHolder xdataholder)
	{
		if (blockClone.Flipped != blockInfo.Flipped)
		{
			blockClone.Flipped = blockInfo.Flipped;
			blockClone.PostFlip(false, false);
		}
		if (BlockSelectionTool.Duplicating)
		{
			blockInfo.Guid = Guid.NewGuid();
		}
		blockClone.Guid = blockInfo.Guid;
		Rigidbody[] componentsInChildren = blockClone.GetComponentsInChildren<Rigidbody>();
		Rigidbody[] array = componentsInChildren;
		foreach (Rigidbody rigidbody in array)
		{
			rigidbody.solverIterations = StaticSettings.BlockSolverIterationCount;
		}
		blockClone.BuildIndex = buildingBlocks.Count;
		SingleInstance<Events>.Instance.BlockInit(blockClone);
		xdataholder.WasCreated = true;
		blockClone.OnLoad(xdataholder);
		buildingBlocks.Add(blockClone);
		guidToBlock.Add(blockClone.Guid, blockClone);
		int blockID = blockClone.BlockID;
		if (blockID != 71 && blockID != 72)
		{
			_blockCount++;
			_blocksCost += WinScreen.GetBlockScore(blockClone);
		}
		if (blockClone.Prefab.RegisterBuildUpdate)
		{
			RegisterUpdate(blockClone, true);
		}
		if (blockClone.Prefab.RegisterBuildLateUpdate)
		{
			RegisterLateUpdate(blockClone, true);
		}
		if (blockClone.Prefab.RegisterBuildFixedUpdate)
		{
			RegisterFixedUpdate(blockClone, true);
		}
		if (isLoadingInfo || blockClone.Prefab.AutoCompletePlacement)
		{
			blockClone.PlacementComplete = true;
			OnAddComplete(blockClone);
		}
		else if (isLocalMachine && IsDraggedBlock(blockClone.Prefab.Type) && !isLoadingDifference && !BlockSelectionTool.Duplicating)
		{
			currentDragged.Add(blockClone as GenericDraggedBlock);
			if (ReferenceMaster.onDraggedBlockPlacement != null)
			{
				ReferenceMaster.onDraggedBlockPlacement(blockClone);
			}
		}
		if (!isLoadingInfo)
		{
			UpdateMachineDLCStatus();
		}
	}

	public void OnAddComplete(BlockBehaviour block)
	{
		block.SaveInitialData();
		bool flag = true;
		switch (block.Prefab.Type)
		{
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
			flag = false;
			break;
		case BlockType.Pin:
		case BlockType.CameraBlock:
			if (!StatMaster.isMP)
			{
				flag = false;
			}
			break;
		}
		if (flag)
		{
			BlockNode node;
			linkManager.AddBlock(block, out node);
			if (isLocalMachine && !UndoSystem.processing)
			{
				analyzer.FindLinks(OptionsMaster.linkDelayFrames, block, !isLoadingInfo || isLoadingDifference);
			}
		}
		block.PlacementComplete = true;
		if (!isLoadingInfo && ReferenceMaster.onBlockPlaced != null)
		{
			ReferenceMaster.onBlockPlaced(block);
		}
		SingleInstance<Events>.Instance.BlockPlaced(block);
	}

	public void UpdateIndices()
	{
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			blockBehaviour.BuildIndex = i;
		}
	}

	public IEnumerator WaitAndTryAddBlockAgain(BlockInfo blockInfo, float waiting)
	{
		yield return new WaitForSeconds(waiting);
		BlockBehaviour block;
		AddBlock(blockInfo, false, out block);
	}

	public bool AddBlock(Vector3 position, Quaternion rotation, BlockType id, bool isFlipped, out BlockBehaviour block)
	{
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.ID = id;
		blockInfo.Position = position;
		blockInfo.Rotation = rotation;
		blockInfo.Scale = PrefabMaster.GetDefaultScale(id);
		blockInfo.Flipped = isFlipped;
		blockInfo.Skin = PrefabMaster.BlockPrefabs[(int)id].DefaultSkin;
		blockInfo.BlockData = new XDataHolder();
		return AddBlock(blockInfo, out block);
	}

	public virtual bool AddBlockGlobal(Vector3 position, Quaternion rotation, BlockType id, bool isFlipped, out BlockBehaviour block)
	{
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.ID = id;
		blockInfo.Position = BuildingMachine.InverseTransformPoint(position);
		blockInfo.Rotation = Quaternion.Inverse(BuildingMachine.rotation) * rotation;
		blockInfo.Scale = PrefabMaster.GetDefaultScale(id);
		blockInfo.Flipped = isFlipped;
		blockInfo.BlockData = new XDataHolder();
		BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[(int)id];
		blockInfo.Skin = blockPrefab.VisualController.selectedSkin;
		bool flag = AddBlock(blockInfo, out block);
		if (flag)
		{
			block.VisualController.PlaceFromPrefab();
		}
		return flag;
	}

	public void UnregisterSimulationBlock(BlockBehaviour block)
	{
		if (simBlocks.Contains(block))
		{
			simBlocks.Remove(block);
			simulationArray = simBlocks.ToArray();
		}
	}

	public void Analyze()
	{
		analyzer.OnReset();
		if (isLocalMachine && !StatMaster.cachingTransformActions && !nodeController.IsMerging && !SelectionTool.BatchChange)
		{
			analyzer.Analyze();
		}
	}

	public virtual void RemoveBlock(BlockBehaviour block)
	{
		if (StatMaster.Mode.placingBlock && block is GenericDraggedBlock)
		{
			GenericDraggedBlock item = block as GenericDraggedBlock;
			if (currentDragged.Contains(item))
			{
				currentDragged.Remove(item);
			}
		}
		if (block.SurfaceType)
		{
			nodeController.CancelRefresh(block);
		}
		if (block.IsSelected)
		{
			AdvancedBlockEditor.Instance.selectionController.Deselect(block, false, false);
		}
		SingleInstance<Events>.Instance.BlockRemoving(block);
		UnregisterBlock(block, !nodeController.IsMerging);
		block.IsDestroyed = true;
		block.IsSelected = false;
		block.IsSelectedExtra = false;
		UnityEngine.Object.DestroyImmediate(block.gameObject);
		Analyze();
		MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (instance != null && instance.targetType == MouseOrbit.TargetType.Block && instance.targetInfo == block)
		{
			instance.SoftResetCamTarget();
		}
		if (!isLoadingInfo)
		{
			UpdateMachineDLCStatus();
		}
	}

	public void RemoveBlock(Transform transform)
	{
		RemoveBlock(GetBlock(transform));
	}

	public void RemoveBlockEndOfFrame(Transform transform)
	{
		BlockBehaviour block = GetBlock(transform);
		if (!object.ReferenceEquals(block, null))
		{
			RemoveBlock(block);
		}
		else
		{
			Debug.LogWarning("Specified transform is not a block.");
		}
	}

	public virtual void UnregisterBlock(BlockBehaviour block, bool updateIndices)
	{
		if (block.BuildIndex != -1)
		{
			UnregisterFixedUpdate(block, true);
			UnregisterLateUpdate(block, true);
			UnregisterUpdate(block, true);
			UnregisterBVCUpdate(block.VisualController, true);
			int num = buildingBlocks.IndexOf(block);
			if (updateIndices && num != block.BuildIndex)
			{
				Debug.LogWarning("Block BuildIndex (" + block.BuildIndex + ") doesn't match building block index (" + num + ")!");
			}
			buildingBlocks.RemoveAt(num);
			guidToBlock.Remove(block.Guid);
			int blockID = block.BlockID;
			if (blockID != 71 && blockID != 72)
			{
				_blockCount--;
				_blocksCost -= WinScreen.GetBlockScore(block);
			}
			if (updateIndices)
			{
				UpdateIndices();
			}
			block.BuildIndex = -1;
			if (block.PlacementComplete)
			{
				linkManager.RemoveBlock(block);
			}
		}
	}

	public void RemoveSimBlock(BlockBehaviour block, bool destroy = false)
	{
		simBlocks.Remove(block);
		if (!finishedPhysics || !finishedNormalPhysics)
		{
			if (!SimPhysics || block.Prefab.SetBreakForce)
			{
				physFinishBlocks.Remove(block);
			}
			PhysFinishSetting physFinishSetting = block.Prefab.physFinishSetting;
			if (physFinishSetting == PhysFinishSetting.Early)
			{
				physPhaseEarly.Remove(block);
			}
			else
			{
				physPhaseNormal.Remove(block);
			}
		}
		block.BuildingBlock.hasSimBlock = false;
		block.BuildingBlock.SimBlock = null;
		GameObject gameObject = block.gameObject;
		if (destroy)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public BlockBehaviour GetRandomBlock()
	{
		if (!isSimulating || simulationClone == null)
		{
			return null;
		}
		return ReferenceMaster.GetRandomBlock(PlayerID);
	}

	protected virtual void Update()
	{
		if (isSimulating)
		{
			if (isLocalMachine)
			{
				inputController.UpdateKeys();
			}
			if (!finishedNormalPhysics)
			{
				if (finishNormalFrameCount == 5)
				{
					for (int i = 0; i < physPhaseNormal.Count; i++)
					{
						physPhaseNormal[i].FinishDestroySimulate();
					}
					finishedNormalPhysics = true;
				}
				else
				{
					finishNormalFrameCount++;
				}
			}
			if (simUpdate.Count <= 0 && simBVC.Count <= 0)
			{
				return;
			}
			if (StatMaster.stressCoded)
			{
				for (int i = 0; i < simUpdate.Count; i++)
				{
					simUpdate[i].UpdateStress();
				}
			}
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < simBVC.Count; i++)
			{
				simBVC[i].UpdateBlockVis(deltaTime);
			}
			if (isReady)
			{
				for (int i = 0; i < simUpdate.Count; i++)
				{
					BlockBehaviour blockBehaviour = simUpdate[i];
					blockBehaviour.UpdateBlock();
				}
			}
			return;
		}
		for (int i = 0; i < currentDragged.Count; i++)
		{
			currentDragged[i].UpdateDragged();
		}
		if (buildUpdate.Count > 0 || buildBVC.Count > 0)
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < buildBVC.Count; i++)
			{
				buildBVC[i].UpdateBlockVis(deltaTime);
			}
			for (int i = 0; i < buildUpdate.Count; i++)
			{
				BlockBehaviour blockBehaviour = buildUpdate[i];
				blockBehaviour.UpdateBlock();
			}
		}
	}

	protected void FixedUpdate()
	{
		if (isSimulating)
		{
			if (SimPhysics && !setColliderIterations)
			{
				for (int i = 0; i < simBlocks.Count; i++)
				{
					BlockBehaviour blockBehaviour = simBlocks[i];
					if (!blockBehaviour.noRigidbody)
					{
						if (blockBehaviour.SetColliderIterations)
						{
							blockBehaviour.SetColIterations();
						}
						if (blockBehaviour.Prefab.SetMaxAngularVelocity)
						{
							blockBehaviour.SetMaxAngularVelocity();
						}
					}
				}
				setColliderIterations = true;
			}
			if (!finishedPhysics)
			{
				if (finishPhysicsFrameCount == 2 && physPhaseEarly.Count > 0)
				{
					int count = physPhaseEarly.Count;
					for (int i = 0; i < count; i++)
					{
						physPhaseEarly[i].FinishDestroySimulate();
					}
				}
				else if (finishPhysicsFrameCount == 3 && physFragmentBlocks.Count > 0)
				{
					int count2 = physFragmentBlocks.Count;
					for (int i = 0; i < count2; i++)
					{
						(physFragmentBlocks[i].VisualController as FragmentVisualController).StartController();
					}
				}
				else if (finishPhysicsFrameCount == 8)
				{
					int count3 = physFinishBlocks.Count;
					for (int i = 0; i < count3; i++)
					{
						physFinishBlocks[i].FinishPhysics();
					}
					finishedPhysics = true;
				}
				finishPhysicsFrameCount++;
			}
			if (HasEmulationBlocks && everyOther == 0)
			{
				for (int i = 0; i < simSendEmulationUpdate.Count; i++)
				{
					BlockBehaviour blockBehaviour = simSendEmulationUpdate[i];
					blockBehaviour.SendEmulationUpdateBlock();
				}
			}
			if (simFixedUpdate.Count > 0)
			{
				for (int i = 0; i < simFixedUpdate.Count; i++)
				{
					BlockBehaviour blockBehaviour = simFixedUpdate[i];
					if (HasEmulationBlocks && everyOther == 0 && blockBehaviour.RegisteredSimEmulationUpdate)
					{
						blockBehaviour.EmulationUpdateBlock();
					}
					if (blockBehaviour.RegisteredSimFixedUpdate)
					{
						blockBehaviour.FixedUpdateBlock();
					}
				}
			}
			everyOther = (everyOther + 1) % 2;
			fixedTime++;
			return;
		}
		if (buildFixedUpdate.Count > 0)
		{
			int count4 = buildFixedUpdate.Count;
			for (int i = 0; i < count4; i++)
			{
				BlockBehaviour blockBehaviour = buildFixedUpdate[i];
				blockBehaviour.FixedUpdateBlock();
			}
			everyOther = 1;
		}
		fixedTime = 0;
	}

	protected void LateUpdate()
	{
		if (isSimulating)
		{
			if (simLateUpdate.Count > 0)
			{
				for (int i = 0; i < simLateUpdate.Count; i++)
				{
					BlockBehaviour blockBehaviour = simLateUpdate[i];
					blockBehaviour.LateUpdateBlock();
				}
			}
		}
		else if (buildLateUpdate.Count > 0)
		{
			for (int i = 0; i < buildLateUpdate.Count; i++)
			{
				BlockBehaviour blockBehaviour = buildLateUpdate[i];
				blockBehaviour.LateUpdateBlock();
			}
		}
	}

	public virtual bool ReverseBlock(BlockBehaviour block, bool playSound, bool isUndo)
	{
		bool flipped = block.Flipped;
		block.SetFlip(!flipped);
		return flipped != block.Flipped && block.PostFlip(playSound, isUndo);
	}

	public virtual bool SpinBlock(BlockBehaviour block, bool playSound, bool forward)
	{
		return block.PostSpin(playSound, forward);
	}

	public virtual void EditBlockData(BlockBehaviour block, XDataHolder data)
	{
		block.OnLoad(data);
		block.OnPostEdit();
	}

	public BlockBehaviour GetBlock(Transform transform)
	{
		return transform.GetComponent<BlockBehaviour>();
	}

	public List<BlockBehaviour> GetBlocks(BlockType targetType)
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		List<BlockBehaviour> list2 = ((hasTempSim || !isSimulating) ? buildingBlocks : simBlocks);
		for (int i = 0; i < list2.Count; i++)
		{
			BlockBehaviour blockBehaviour = list2[i];
			if (blockBehaviour.Prefab.Type == targetType)
			{
				list.Add(blockBehaviour);
			}
		}
		return list;
	}

	public virtual void StartPhysics()
	{
		if (!hasTempSim)
		{
			return;
		}
		if (simulationClone != null)
		{
			Debug.LogError("Spawning sim machine for already spawned machine");
		}
		simulationClone = tempSim;
		if (resetNeeded)
		{
			simulationClone.position = spawner.position;
			simulationClone.rotation = machineRotation;
			if (ignoredInPhys != null)
			{
				for (int i = 0; i < ignoredInPhys.Length; i++)
				{
					ignoredInPhys[i].enabled = true;
				}
				ignoredInPhys = null;
			}
			if (!StatMaster.isHeadless)
			{
				UnityEngine.Object.Destroy(display.gameObject);
				display = null;
				tempVis = null;
				tempFilter = null;
				tempRenderer = null;
				tempSkinnedVis = null;
				tempSkinned = null;
			}
		}
		hasTempSim = false;
		tempSim = null;
		if (SimPhysics)
		{
			SetLayerCollisions(true);
			for (int j = 0; j < buildingBlocks.Count; j++)
			{
				BlockBehaviour blockBehaviour = buildingBlocks[j];
				BlockBehaviour simBlock = blockBehaviour.SimBlock;
				if (!blockBehaviour.hasSimBlock)
				{
					if (BesiegeLogFilter.logDev)
					{
						Debug.LogWarning("Sim block for " + blockBehaviour.name + " doesn't exist anymore!");
					}
					continue;
				}
				if (StatMaster.handleCrossPatternJoints)
				{
					HandleCrossPatternBlock(simBlock);
				}
				if (resetNeeded && !StatMaster.isHeadless)
				{
					simBlock.VisualController.SetVisible();
				}
				WakePhysics(simBlock);
			}
			SetLayerCollisions(false);
		}
		WinCondition.simStarted = Time.fixedTime;
		ReferenceMaster.blocksInSim += buildingBlocks.Count;
		maxStress = 0f;
		isReady = true;
		StartCoroutine(HandleSimToggleEmulation());
		for (int k = 0; k < simBlocks.Count; k++)
		{
			simBlocks[k].ShelterAmount = 0f;
		}
	}

	private IEnumerator HandleSimToggleEmulation()
	{
		if (simBlocks.Count == 0)
		{
			yield break;
		}
		if (HasEmulationBlocks)
		{
			BlockBehaviour block = simBlocks[0];
			block.EmulateSimToggleKey(true);
			for (int i = 0; i < 5; i++)
			{
				yield return new WaitForFixedUpdate();
			}
			if (block != null)
			{
				block.EmulateSimToggleKey(false);
			}
		}
		else
		{
			inputController.SetSimToggleOverride(true, true, false);
			for (int j = 0; j < 3; j++)
			{
				yield return null;
				inputController.SetSimToggleOverride(false, true, false);
			}
			inputController.SetSimToggleOverride(false, false, true);
			yield return null;
			inputController.SetSimToggleOverride(false, false, false);
		}
	}

	private void SetLayerCollisions(bool toggle)
	{
		StatMaster.IgnoreLevelTriggerResults = true;
		Physics.IgnoreLayerCollision(12, 25, toggle);
		Physics.IgnoreLayerCollision(12, 0, toggle);
		Physics.IgnoreLayerCollision(14, 25, toggle);
		Physics.IgnoreLayerCollision(14, 0, toggle);
		Physics.IgnoreLayerCollision(12, 14, toggle);
		if (!toggle)
		{
			StartCoroutine(EnableIgnoreLevelTriggerResults());
		}
	}

	private IEnumerator EnableIgnoreLevelTriggerResults()
	{
		yield return new WaitForFixedUpdate();
		StatMaster.IgnoreLevelTriggerResults = false;
	}

	protected virtual void WakePhysics(BlockBehaviour block)
	{
		if (block.noRigidbody)
		{
			BlockType type = block.Prefab.Type;
			if (type == BlockType.BuildNode || type == BlockType.BuildEdge)
			{
				block.StartPhysics(true);
			}
			return;
		}
		if (block.IsDestroyed)
		{
			block.StartPhysics(true);
			return;
		}
		GameObject gameObject = block.gameObject;
		if (gameObject.CompareTag("StayKinematic"))
		{
			block.StartPhysics(true);
			return;
		}
		block.StartPhysics(false);
		Rigidbody rigidbody = block.Rigidbody;
		if (!gameObject.CompareTag("DontInterpolate"))
		{
			rigidbody.isKinematic = false;
			rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		else
		{
			rigidbody.interpolation = RigidbodyInterpolation.None;
		}
	}

	private void HandleCrossPatternBlock(BlockBehaviour block)
	{
		block.CreateSimLists();
		List<Joint> list = new List<Joint>(block.jointsToMe);
		Joint[] components = block.GetComponents<Joint>();
		Joint[] array = components;
		foreach (Joint item in array)
		{
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		if (list.Count < 4)
		{
			return;
		}
		BlockBehaviour[] array2 = new BlockBehaviour[list.Count];
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Transform transform = block.transform;
		int num = 0;
		int num2 = 0;
		foreach (Joint item3 in list)
		{
			if (item3 == null)
			{
				continue;
			}
			ConfigurableJoint configurableJoint = item3 as ConfigurableJoint;
			if (configurableJoint == null)
			{
				continue;
			}
			BlockBehaviour blockBehaviour = ((!(configurableJoint.gameObject == block.gameObject)) ? item3.GetComponent<BlockBehaviour>() : ((!(configurableJoint.connectedBody != null)) ? null : configurableJoint.connectedBody.GetComponent<BlockBehaviour>()));
			if (blockBehaviour != null)
			{
				array2[num2] = blockBehaviour;
				int blockID = blockBehaviour.BlockID;
				if (!dictionary.ContainsKey(blockID))
				{
					dictionary.Add(blockID, 1);
				}
				int value = 0;
				if (dictionary.TryGetValue(blockID, out value))
				{
					dictionary[blockID] = value + 1;
				}
				num++;
			}
			else
			{
				array2[num2] = null;
			}
			num2++;
		}
		if (num < 4)
		{
			return;
		}
		KeyValuePair<int, int> keyValuePair = default(KeyValuePair<int, int>);
		num2 = 0;
		foreach (KeyValuePair<int, int> item4 in dictionary)
		{
			if (num2++ == 0 || keyValuePair.Value < item4.Value)
			{
				keyValuePair = item4;
			}
		}
		if (keyValuePair.Value < 4)
		{
			return;
		}
		int key = keyValuePair.Key;
		List<BlockBehaviour> list2 = new List<BlockBehaviour>();
		for (int j = 0; j < 3; j++)
		{
			list2.Clear();
			foreach (BlockBehaviour blockBehaviour2 in array2)
			{
				if ((bool)blockBehaviour2 && blockBehaviour2.BlockID == key && Mathf.Approximately(transform.position[j], blockBehaviour2.transform.position[j]))
				{
					list2.Add(blockBehaviour2);
				}
			}
			if (list2.Count != 4)
			{
				continue;
			}
			{
				foreach (Joint item5 in list)
				{
					if (!item5 || !item5.connectedBody)
					{
						break;
					}
					BlockBehaviour item2 = ((!(item5.gameObject == block.gameObject)) ? item5.GetComponent<BlockBehaviour>() : item5.connectedBody.GetComponent<BlockBehaviour>());
					if (list2.Contains(item2))
					{
						ConfigurableJoint configurableJoint = item5 as ConfigurableJoint;
						if (configurableJoint != null)
						{
							configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
						}
					}
				}
				break;
			}
		}
	}

	public virtual Bounds GetBounds(bool renew = true)
	{
		if (!renew || isSimulating)
		{
			return machineBounds;
		}
		bool flag = true;
		bool updateBounding = isLocalMachine;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (!blockBehaviour.Prefab.hasMyBounds)
			{
				continue;
			}
			BlockType type = blockBehaviour.Prefab.Type;
			if (type != BlockType.Pin && type != BlockType.CameraBlock)
			{
				Bounds bounds = blockBehaviour.myBounds.GetBounds(updateBounding, false);
				if (flag)
				{
					machineBounds = bounds;
					flag = false;
				}
				else
				{
					machineBounds.Encapsulate(bounds);
				}
			}
		}
		boundsSqrSize = (machineBounds.size * 2f).sqrMagnitude;
		return machineBounds;
	}

	private Bounds GetBoundsRotated()
	{
		bool flag = false;
		Bounds result = default(Bounds);
		for (int i = 1; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (!blockBehaviour.hasSimBlock)
			{
				continue;
			}
			BlockBehaviour simBlock = blockBehaviour.SimBlock;
			if (!simBlock.Prefab.hasMyBounds)
			{
				continue;
			}
			BlockType type = simBlock.Prefab.Type;
			if (type != BlockType.Pin && type != BlockType.CameraBlock)
			{
				Bounds bounds = simBlock.myBounds.GetBounds(true, false);
				if (!flag)
				{
					result = bounds;
					flag = true;
				}
				else
				{
					result.Encapsulate(bounds);
				}
			}
		}
		result.center += new Vector3(0f, 1f, 0f);
		return result;
	}

	private void UpdateMass()
	{
		machineMass = 0f;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (!blockBehaviour.noRigidbody)
			{
				machineMass += blockBehaviour.Rigidbody.mass;
			}
		}
	}

	public virtual MachineInfo CreateMachineInfo(bool withBlocks = true)
	{
		MachineInfo machineInfo = new MachineInfo();
		machineInfo.Name = Name;
		machineInfo.Author = Author;
		machineInfo.Type = MachineType;
		machineInfo.Position = buildingMachine.position;
		machineInfo.Rotation = buildingMachine.rotation;
		MachineInfo machineInfo2 = machineInfo;
		if (withBlocks)
		{
			machineInfo2.Blocks = new List<BlockInfo>(buildingBlocks.Count);
			foreach (BlockBehaviour buildingBlock in buildingBlocks)
			{
				machineInfo2.Blocks.Add(BlockInfo.FromBlockBehaviour(buildingBlock));
			}
		}
		XDataHolder data = new XDataHolder();
		SaveMachineData(data);
		machineInfo2.MachineData = data;
		return machineInfo2;
	}

	public void AddMachineData(XData data)
	{
		machineData.Write(data.Key, data);
	}

	public XData GetMachineData(string key)
	{
		return machineData.Read(key);
	}

	public void SaveMachineData(XDataHolder data)
	{
		machineData.Write(data);
	}

	public void LoadMachineInfo(MachineInfo info, string machinePath, bool resetUndoActions = false)
	{
		LoadedMachinePath = machinePath;
		LoadMachineInfo(info, resetUndoActions);
	}

	public virtual void LoadMachineInfo(MachineInfo info, bool resetUndoActions = false)
	{
		isLoadingInfo = true;
		spawningMachine = true;
		Reset(resetUndoActions);
		Name = info.Name;
		Author = info.Author;
		MachineType = info.Type;
		machineData = info.MachineData;
		buildingMachine.position = info.Position;
		buildingMachine.rotation = info.Rotation;
		foreach (BlockInfo block2 in info.Blocks)
		{
			BlockBehaviour block;
			if (AddBlock(block2, out block))
			{
				block.VisualController.PlaceFromBlockInfo(block2);
			}
		}
		LoadedBlockScore = _blocksCost;
		isLoadingInfo = false;
		spawningMachine = false;
		PostLoad(resetUndoActions);
		UpdateMachineDLCStatus();
	}

	public void AddBlocksFromInfo(List<BlockInfo> blockInfos, out Dictionary<Guid, BlockBehaviour> addedBlocks, ref List<UndoAction> undoActions)
	{
		addedBlocks = new Dictionary<Guid, BlockBehaviour>(blockInfos.Count);
		bool flag = GetBlocks(BlockType.StartingBlock).Count > 0;
		for (int i = 0; i < blockInfos.Count; i++)
		{
			BlockInfo blockInfo = blockInfos[i];
			Guid guid = blockInfo.Guid;
			blockInfo.Guid = Guid.NewGuid();
			bool flag2 = false;
			if (blockInfo.ID == BlockType.StartingBlock)
			{
				if (flag || StatMaster.KeyMapper.multipleStartingBlocks)
				{
					blockInfo.ID = BlockType.Ballast;
					flag2 = true;
					blockInfo.BlockData.Write("bmt-mass", 0.25f);
				}
				else
				{
					flag = true;
				}
			}
			else if (blockInfo.ID == BlockType.BuildEdge)
			{
				if (!blockInfo.BlockData.HasKey("start") || !blockInfo.BlockData.HasKey("end"))
				{
					Debug.Log(string.Format("BuildEdge {0} has no start/end node!", guid), this);
					continue;
				}
				BlockBehaviour value;
				if (!addedBlocks.TryGetValue(new Guid(blockInfo.BlockData.ReadString("start")), out value))
				{
					Debug.LogError(string.Format("BuildEdge {0} cannot find start node {1}!", guid, blockInfo.BlockData.ReadString("start")), this);
					continue;
				}
				BlockBehaviour value2;
				if (!addedBlocks.TryGetValue(new Guid(blockInfo.BlockData.ReadString("end")), out value2))
				{
					Debug.LogError(string.Format("BuildEdge {0} cannot find end node {1}!", guid, blockInfo.BlockData.ReadString("end")), this);
					continue;
				}
				BuildEdgeBlock.WriteData(blockInfo.BlockData, value as BuildNodeBlock, value2 as BuildNodeBlock);
			}
			else if (blockInfo.ID == BlockType.BuildSurface)
			{
				if (!blockInfo.BlockData.HasKey("edges"))
				{
					Debug.LogError(string.Format("BuildSurface {0} has no edges!", guid), this);
					continue;
				}
				string[] array = blockInfo.BlockData.ReadString("edges").Split('|');
				BuildEdgeBlock[] array2 = new BuildEdgeBlock[array.Length];
				bool flag3 = false;
				for (int j = 0; j < array.Length; j++)
				{
					BlockBehaviour value3;
					if (addedBlocks.TryGetValue(new Guid(array[j]), out value3))
					{
						array2[j] = value3 as BuildEdgeBlock;
						continue;
					}
					Debug.LogError(string.Format("Couldn't find surface edge {0}!", j), this);
					flag3 = true;
					break;
				}
				if (flag3)
				{
					continue;
				}
				BuildSurface.WriteData(blockInfo.BlockData, array2);
			}
			BlockBehaviour block;
			if (AddBlock(blockInfo, out block))
			{
				blockInfo.Guid = block.Guid;
				if (flag2)
				{
					block.SetPosition(block.transform.position - block.transform.forward * (0.5f * block.transform.localScale.z));
				}
				block.VisualController.PlaceFromBlockInfo(blockInfo);
				addedBlocks.Add(guid, block);
				undoActions.Add(new UndoActionAdd(this, blockInfo));
			}
		}
	}

	public void PostLoad()
	{
		PostLoad(true);
	}

	public virtual void PostLoad(bool resetUndoActions)
	{
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		if (instance == null)
		{
			return;
		}
		if (onBatchOperationComplete != null)
		{
			onBatchOperationComplete();
		}
		if (isLocalMachine)
		{
			if (resetUndoActions)
			{
				UndoSystem.Reset();
			}
			instance.UpdateMiddleOfObject(true);
			if (ReferenceMaster.onMachinePostLoad != null)
			{
				ReferenceMaster.onMachinePostLoad(this);
			}
		}
	}

	public void OnAnalysisReset()
	{
		hasCenterBlock = false;
	}

	protected void UpdateCenterBlock()
	{
		if (isLocalMachine || !OptionsMaster.networkClusters)
		{
			centerBlock = linkManager.GetLabelTarget();
			if (centerBlock != null && !StatMaster.isHeadless)
			{
				centerBlockTransform = centerBlock.transform;
				lastCenterPos = centerBlockTransform.position;
				hasCenterBlock = true;
			}
			_centerPosOffsetToCenter = linkManager.Center - lastCenterPos;
		}
	}

	public virtual void OnAnalyzeComplete()
	{
		if (addPiece == null)
		{
			return;
		}
		if (isLocalMachine)
		{
			addPiece.UpdateMiddleOfObject();
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				OverviewBlockMapper.Open(this);
			}
		}
		UpdateCenterBlock();
		if (hasCenterBlock)
		{
			centerBlockTransform = centerBlock.transform;
			lastCenterPos = centerBlockTransform.position;
		}
		if (ReferenceMaster.onMachineModified != null)
		{
			ReferenceMaster.onMachineModified(this);
		}
	}

	public bool GetBlock(Guid guid, out BlockBehaviour block)
	{
		if (!guidToBlock.ContainsKey(guid))
		{
			block = null;
			return false;
		}
		block = guidToBlock[guid];
		return true;
	}

	public void CheckBounds(bool renewBounds = true)
	{
		if (isLocalMachine)
		{
			boundingBoxController.Check(this, renewBounds);
		}
	}

	public virtual void SetTransform(Vector3 pos, Quaternion rot, bool boundCheck = true)
	{
		buildingMachine.position = pos;
		buildingMachine.rotation = rot;
		OnRotationChanged();
		if (boundCheck)
		{
			CheckBounds();
		}
	}

	public virtual void SetPosition(Vector3 pos, bool boundCheck = true)
	{
		buildingMachine.position = pos;
		if (boundCheck)
		{
			CheckBounds();
		}
	}

	public virtual void SetRotation(Quaternion rot, bool boundCheck = true)
	{
		buildingMachine.rotation = rot;
		OnRotationChanged();
		if (boundCheck)
		{
			CheckBounds();
		}
	}

	private void OnRotationChanged()
	{
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (IsDraggedBlock(blockBehaviour.Prefab.Type))
			{
				(blockBehaviour as GenericDraggedBlock).SaveEulerAngles(blockBehaviour.LastState);
			}
		}
		if (StatMaster.Mode.displayDrag)
		{
			StatMaster.Mode.InvokeAeroDisplayChanged();
		}
	}

	protected void ResetCameraTarget()
	{
		MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (instance != null && instance.targetType == MouseOrbit.TargetType.Block && instance.targetInfo.ParentMachine == this)
		{
			instance.ResetCamTarget();
		}
	}

	public virtual void Reset(bool resetUndoActions = true)
	{
		if (isLocalMachine)
		{
			AdvancedBlockEditor.Instance.selectionController.DeselectAll(false);
			FileBrowserView.SetLastSaveEntry(FileBrowserType.LocalMachines, string.Empty);
		}
		nodeController.ResetPlacement();
		ResetCameraTarget();
		while (buildingBlocks.Count > 0)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[0];
			blockBehaviour.BuildIndex = -1;
			blockBehaviour.name = "destroyed";
			blockBehaviour.IsDestroyed = true;
			guidToBlock.Remove(blockBehaviour.Guid);
			UnityEngine.Object.DestroyImmediate(blockBehaviour.gameObject);
			buildingBlocks.RemoveAt(0);
		}
		_blockCount = 0;
		_blocksCost = 0;
		LoadedBlockScore = 0;
		analyzer.Reset();
		linkManager.Reset();
		if (!analyzing)
		{
			analyzing = true;
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(PlayerID + ": Resetting clusters..");
			}
		}
		buildUpdate.Clear();
		buildFixedUpdate.Clear();
		buildLateUpdate.Clear();
		buildBVC.Clear();
		Name = "Unnamed";
		Author = ((!SteamManager.Initialized) ? string.Empty : SteamUser.GetSteamID().m_SteamID.ToString());
		MachineType = MachineInfo.MachineType.Local;
		buildingMachine.position = _basePosition;
		buildingMachine.rotation = _baseRotation;
		hasCenterBlock = false;
		if (isLocalMachine)
		{
			if (resetUndoActions)
			{
				UndoSystem.Reset();
			}
			addPiece.checkVirtualBlocks = false;
		}
	}

	public static Machine Active()
	{
		return MachineObjectTracker.activeMachine;
	}

	protected virtual void OnDestroy()
	{
		nodeController.Dispose();
		MachineObjectTracker.lastBuild = CreateMachineInfo();
	}
}
