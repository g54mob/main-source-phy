using System;
using System.Collections.Generic;
using UnityEngine;

public class SymmetryController : MonoBehaviour
{
	public class SymmetryPoint
	{
		public Vector3 checkPos;

		public GameObject gameObject;

		public Transform transform;

		public bool isUsed;

		public BlockBehaviour block;

		public Transform dirArrow;

		public GhostTrigger ghostTrigger;
	}

	private enum Axes
	{
		x = 0,
		y = 1,
		z = 2
	}

	private class PivotInfo
	{
		public Transform Pivot;

		public Quaternion pivotRotation;

		public Quaternion invPivotRotation;
	}

	public class MirrorInfo
	{
		public int Index;

		public Vector3 Position;

		public Quaternion Rotation;
	}

	public class checkForBlock
	{
		public static BlockBehaviour custom(Vector3 blockpos, Vector3 raypos)
		{
			Machine machine = Machine.Active();
			RaycastHit hitInfo;
			if (Physics.Raycast(blockpos + (raypos - blockpos).normalized * 0.1f, (blockpos - raypos).normalized, out hitInfo, 0.2f))
			{
				BlockBehaviour block = machine.GetBlock(hitInfo.rigidbody.transform);
				if (block != null)
				{
					return block;
				}
			}
			return null;
		}

		public static Collider standard(Vector3 blockpos)
		{
			Machine machine = Machine.Active();
			RaycastHit hitInfo;
			if (Physics.Raycast(blockpos + Vector3.up * 2f, blockpos, out hitInfo, 0.2f) && hitInfo.collider.attachedRigidbody != null)
			{
				BlockBehaviour block = machine.GetBlock(hitInfo.collider.attachedRigidbody.transform);
				if (block != null)
				{
					return hitInfo.collider;
				}
			}
			return null;
		}
	}

	public Vector3 axis = Vector3.zero;

	public Action OnAxisChanged;

	private AddPiece addPiece;

	private Transform startingBlock;

	private BlockBehaviour firstBlock;

	[HideInInspector]
	public Transform symmetryTransform;

	private Collider target;

	private bool isDraggedBlock;

	private bool isNode;

	private SymmetryPoint[] mirroredGhosts = new SymmetryPoint[7];

	private BlockType currentBlockType = BlockType.Unused;

	private BlockSkinLoader.SkinPack.Skin currentSkin;

	private Machine symmetryMachine;

	private bool hasSymMachine;

	private List<Vector3> braceStartPoints = new List<Vector3>();

	private List<Vector3> braceEndPoints = new List<Vector3>();

	protected void Awake()
	{
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onMachinePostLoad = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachinePostLoad, new Action<Machine>(OnMachinePostLoad));
		for (int i = 0; i < mirroredGhosts.Length; i++)
		{
			mirroredGhosts[i] = new SymmetryPoint();
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onMachinePostLoad = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachinePostLoad, new Action<Machine>(OnMachinePostLoad));
	}

	private void OnMachinePostLoad(Machine obj)
	{
		FindSymmetryPoint();
	}

	private void OnMachineChanged(Machine machine)
	{
		if (machine != null)
		{
			SetSymmetryMachine(machine);
		}
		else
		{
			ClearSymmetryMachine();
		}
	}

	public void SetSymmetryMachine(Machine machine)
	{
		if (machine == null || machine.BlockCount == 0)
		{
			ClearSymmetryMachine();
			return;
		}
		symmetryMachine = machine;
		hasSymMachine = true;
		firstBlock = symmetryMachine.FirstBlock;
		startingBlock = ((!(firstBlock != null)) ? null : firstBlock.transform);
		FindSymmetryPoint();
		base.enabled = true;
	}

	public void ClearSymmetryMachine()
	{
		hasSymMachine = false;
		base.enabled = false;
	}

	protected void Start()
	{
		addPiece = SingleInstanceFindOnly<AddPiece>.Instance;
		if (!hasSymMachine)
		{
			base.enabled = false;
		}
	}

	public void Update()
	{
		if (!hasSymMachine)
		{
			return;
		}
		if (startingBlock == null)
		{
			if (!symmetryMachine.ReadyForSim)
			{
				return;
			}
			BlockBehaviour blockBehaviour = symmetryMachine.FirstBlock;
			if (blockBehaviour != null)
			{
				startingBlock = blockBehaviour.transform;
			}
		}
		if (symmetryTransform == null)
		{
			FindSymmetryPoint();
		}
		if (StatMaster.Mode.selectSymmetryPivot && InputManager.LeftMouseButton() && addPiece.HoveredBlock != null)
		{
			target = addPiece.mouseHit.collider;
			StatMaster.Mode.selectSymmetryPivot = false;
		}
		bool flag = axis[0] != 0f;
		bool flag2 = axis[1] != 0f;
		bool flag3 = axis[2] != 0f;
		if ((flag || flag2 || flag3) && UpdatePivotPosition())
		{
			InvokeAxisChange();
		}
	}

	public bool UpdatePivotPosition()
	{
		Vector3 vector = (target ? target.bounds.center : ((!startingBlock) ? symmetryMachine.BuildingMachine.position : startingBlock.position));
		if (symmetryTransform.position != vector)
		{
			if (StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling)
			{
				return false;
			}
			symmetryTransform.position = vector;
			return true;
		}
		return false;
	}

	public void InvokeAxisChange()
	{
		Vector3 vector = (target ? target.bounds.center : ((!startingBlock) ? symmetryMachine.BuildingMachine.position : startingBlock.position));
		if (symmetryTransform.position != vector)
		{
			symmetryTransform.position = vector;
		}
		if (OnAxisChanged != null)
		{
			OnAxisChanged();
		}
	}

	public void UpdateSymmetryTransforms()
	{
		if (addPiece == null)
		{
			Debug.LogError("Missing Addpiece for symmetry");
			return;
		}
		Transform currentGhost = addPiece.CurrentGhost;
		GhostTrigger componentInChildren = currentGhost.GetComponentInChildren<GhostTrigger>();
		BlockType currentType = addPiece.CurrentType;
		if (!componentInChildren.hasMaterialCode)
		{
			componentInChildren.materialCode = componentInChildren.GetComponentInChildren<GhostMaterialController>();
			componentInChildren.hasMaterialCode = true;
		}
		bool isRed = componentInChildren.materialCode.isRed;
		componentInChildren.materialCode.SetNormal();
		isDraggedBlock = Machine.IsDraggedBlock(currentType);
		isNode = currentType == BlockType.BuildNode;
		GetMirrorPoses();
		for (int i = 0; i < mirroredGhosts.Length; i++)
		{
			if (currentType != currentBlockType || mirroredGhosts[i] == null || !mirroredGhosts[i].gameObject || currentSkin != ReferenceMaster.ActiveSkin)
			{
				AssignNewSymmetryGhost(i, currentGhost);
				if ((bool)addPiece.CurrentGhostArrow)
				{
					mirroredGhosts[i].dirArrow = mirroredGhosts[i].gameObject.transform.FindChild("DirectionArrow");
					UpdateFlip(i);
				}
			}
			else
			{
				if ((bool)addPiece.CurrentGhostArrow)
				{
					UpdateFlip(i);
				}
				mirroredGhosts[i].ghostTrigger.touchingCount = 0;
			}
			bool isUsed = mirroredGhosts[i].isUsed;
			mirroredGhosts[i].gameObject.SetActive(isUsed);
			if (isNode)
			{
				symmetryMachine.nodeController.UpdateGhost(i + 1, mirroredGhosts[i].gameObject.transform.position, isUsed);
			}
		}
		currentBlockType = currentType;
		currentSkin = ReferenceMaster.ActiveSkin;
		if (isRed)
		{
			componentInChildren.materialCode.SetRed();
		}
	}

	private void AssignNewSymmetryGhost(int i, Transform currentGhost)
	{
		GameObject obj = mirroredGhosts[i].gameObject;
		Vector3 position = ((!mirroredGhosts[i].gameObject) ? currentGhost.position : mirroredGhosts[i].transform.position);
		Quaternion rotation = ((!mirroredGhosts[i].gameObject) ? currentGhost.rotation : mirroredGhosts[i].transform.rotation);
		mirroredGhosts[i].gameObject = UnityEngine.Object.Instantiate(currentGhost.gameObject, position, rotation) as GameObject;
		mirroredGhosts[i].transform = mirroredGhosts[i].gameObject.transform;
		mirroredGhosts[i].ghostTrigger = mirroredGhosts[i].gameObject.GetComponentInChildren<GhostTrigger>();
		mirroredGhosts[i].ghostTrigger.materialCode.SetHalfOpacity();
		if ((bool)mirroredGhosts[i].ghostTrigger)
		{
			mirroredGhosts[i].ghostTrigger.touchingCount = 0;
		}
		UnityEngine.Object.Destroy(obj);
	}

	private void UpdateFlip(int i)
	{
		if ((i == 1 || i == 2 || i == 5) && mirroredGhosts[i] != null && (bool)mirroredGhosts[i].dirArrow)
		{
			mirroredGhosts[i].dirArrow.localScale = new Vector3(addPiece.CurrentGhostArrow.localScale.x, mirroredGhosts[i].dirArrow.localScale.y, mirroredGhosts[i].dirArrow.localScale.z);
		}
	}

	private PivotInfo GetPivotInfo()
	{
		symmetryTransform.rotation = symmetryMachine.BuildingMachine.rotation;
		Quaternion rotation = symmetryTransform.transform.rotation;
		PivotInfo pivotInfo = new PivotInfo();
		pivotInfo.Pivot = symmetryTransform;
		pivotInfo.pivotRotation = rotation;
		pivotInfo.invPivotRotation = Quaternion.Inverse(rotation);
		return pivotInfo;
	}

	public void GetMirrorPoses()
	{
		if (symmetryTransform == null)
		{
			FindSymmetryPoint();
		}
		Transform currentGhost = addPiece.CurrentGhost;
		if (currentGhost == null || symmetryTransform == null)
		{
			return;
		}
		List<MirrorInfo> mirrorInfo = GetMirrorInfo(currentGhost.position, currentGhost.rotation);
		int i;
		for (i = 0; i < mirroredGhosts.Length; i++)
		{
			SymmetryPoint symmetryPoint = mirroredGhosts[i];
			int num = mirrorInfo.FindIndex((MirrorInfo x) => x.Index == i);
			if (num == -1)
			{
				symmetryPoint.isUsed = false;
				continue;
			}
			MirrorInfo mirrorInfo2 = mirrorInfo[num];
			symmetryPoint.isUsed = true;
			if (symmetryPoint.gameObject != null)
			{
				symmetryPoint.transform.position = mirrorInfo2.Position;
				symmetryPoint.transform.rotation = mirrorInfo2.Rotation;
			}
		}
	}

	private void CreateInfo(List<MirrorInfo> list, PivotInfo info, int index, Vector3 pos, Quaternion rot)
	{
		if (CanMirror(info, pos, index))
		{
			list.Add(new MirrorInfo
			{
				Index = index,
				Position = MirrorVector(info, index, false, pos),
				Rotation = MirrorRotation(info, index, rot)
			});
		}
	}

	private bool IsX(int i)
	{
		return i == 0 || i == 3 || i == 5 || i == 6;
	}

	private bool IsY(int i)
	{
		return i == 1 || i == 3 || i == 4 || i == 6;
	}

	private bool IsZ(int i)
	{
		return i == 2 || i == 4 || i == 5 || i == 6;
	}

	private bool MirrorEnabled(int i)
	{
		if (StatMaster.advancedBuilding)
		{
			switch (StatMaster.Mode.selectedTool)
			{
			case StatMaster.Tool.Translate:
			case StatMaster.Tool.Rotate:
			case StatMaster.Tool.Scale:
			case StatMaster.Tool.Mirror:
			case StatMaster.Tool.Modify:
			case StatMaster.Tool.Paint:
				if (!StatMaster.Mode.Symmetry.selection)
				{
					return false;
				}
				break;
			case StatMaster.Tool.Erase:
				if (!StatMaster.Mode.Symmetry.eraser)
				{
					return false;
				}
				break;
			default:
				if (!StatMaster.Mode.Symmetry.placement)
				{
					return false;
				}
				break;
			}
		}
		if (IsX(i) && axis[0] == 0f)
		{
			return false;
		}
		if (IsY(i) && axis[1] == 0f)
		{
			return false;
		}
		if (IsZ(i) && axis[2] == 0f)
		{
			return false;
		}
		return true;
	}

	private bool CanMirror(PivotInfo info, Vector3 pos, int i)
	{
		if (!MirrorEnabled(i))
		{
			return false;
		}
		Vector3 vector = info.Pivot.InverseTransformPoint(pos);
		if (!isDraggedBlock && !isNode)
		{
			if (IsX(i) && Mathf.Abs(vector.x) < 0.1f)
			{
				return false;
			}
			if (IsY(i) && Mathf.Abs(vector.y) < 0.1f)
			{
				return false;
			}
			if (IsZ(i) && Mathf.Abs(vector.z) < 0.1f)
			{
				return false;
			}
		}
		return true;
	}

	public List<MirrorInfo> GetMirrorInfo(Vector3 pos, Quaternion rot)
	{
		List<MirrorInfo> list = new List<MirrorInfo>();
		bool flag = axis[0] != 0f;
		bool flag2 = axis[1] != 0f;
		bool flag3 = axis[2] != 0f;
		PivotInfo pivotInfo = GetPivotInfo();
		if (flag)
		{
			CreateInfo(list, pivotInfo, 0, pos, rot);
			if (flag3)
			{
				CreateInfo(list, pivotInfo, 5, pos, rot);
			}
		}
		if (flag2)
		{
			CreateInfo(list, pivotInfo, 1, pos, rot);
			if (flag3)
			{
				CreateInfo(list, pivotInfo, 4, pos, rot);
			}
			if (flag)
			{
				CreateInfo(list, pivotInfo, 3, pos, rot);
				if (flag3)
				{
					CreateInfo(list, pivotInfo, 6, pos, rot);
				}
			}
		}
		if (flag3)
		{
			CreateInfo(list, pivotInfo, 2, pos, rot);
		}
		return list;
	}

	public Vector3 MirrorVector(int index, Vector3 vec)
	{
		return MirrorVector(GetPivotInfo(), index, false, vec);
	}

	public Vector3 MirrorDirection(int index, Vector3 dir)
	{
		return MirrorVector(GetPivotInfo(), index, true, dir);
	}

	public Quaternion MirrorRotation(int index, Quaternion rot)
	{
		return MirrorRotation(GetPivotInfo(), index, rot);
	}

	private Vector3 MirrorLocal(int index, Vector3 localDir)
	{
		switch (index)
		{
		case 0:
			return new Vector3(0f - localDir.x, localDir.y, localDir.z);
		case 1:
			return new Vector3(localDir.x, 0f - localDir.y, localDir.z);
		case 2:
			return new Vector3(localDir.x, localDir.y, 0f - localDir.z);
		case 3:
			return new Vector3(0f - localDir.x, 0f - localDir.y, localDir.z);
		case 4:
			return new Vector3(localDir.x, 0f - localDir.y, 0f - localDir.z);
		case 5:
			return new Vector3(0f - localDir.x, localDir.y, 0f - localDir.z);
		case 6:
			return new Vector3(0f - localDir.x, 0f - localDir.y, 0f - localDir.z);
		default:
			return localDir;
		}
	}

	private Vector3 MirrorVector(PivotInfo info, int index, bool isDir, Vector3 vec)
	{
		Transform pivot = info.Pivot;
		Vector3 vector;
		switch (index)
		{
		case 3:
		{
			Vector3 vector2 = MirrorVector(info, 1, isDir, vec);
			vector = MirrorLocal(0, (!isDir) ? pivot.InverseTransformPoint(vector2) : pivot.InverseTransformDirection(vector2));
			break;
		}
		case 4:
		{
			Vector3 vector2 = MirrorVector(info, 1, isDir, vec);
			vector = MirrorLocal(2, (!isDir) ? pivot.InverseTransformPoint(vector2) : pivot.InverseTransformDirection(vector2));
			break;
		}
		case 5:
		{
			Vector3 vector2 = MirrorVector(info, 0, isDir, vec);
			vector = MirrorLocal(2, (!isDir) ? pivot.InverseTransformPoint(vector2) : pivot.InverseTransformDirection(vector2));
			break;
		}
		case 6:
		{
			Vector3 vector2 = MirrorVector(info, 0, isDir, MirrorVector(info, 1, isDir, vec));
			vector = MirrorLocal(2, (!isDir) ? pivot.InverseTransformPoint(vector2) : pivot.InverseTransformDirection(vector2));
			break;
		}
		default:
			vector = MirrorLocal(index, (!isDir) ? pivot.InverseTransformPoint(vec) : pivot.InverseTransformDirection(vec));
			break;
		}
		return (!isDir) ? pivot.TransformPoint(vector) : pivot.TransformDirection(vector);
	}

	private Quaternion MirrorRotation(PivotInfo info, int index, Quaternion rot)
	{
		Quaternion quaternion = info.invPivotRotation * rot;
		Quaternion quaternion2;
		switch (index)
		{
		case 0:
			quaternion2 = new Quaternion(quaternion.x, 0f - quaternion.y, 0f - quaternion.z, quaternion.w);
			break;
		case 1:
			quaternion2 = new Quaternion(0f - quaternion.x, quaternion.y, 0f - quaternion.z, quaternion.w) * Quaternion.Euler(Vector3.forward * 180f);
			break;
		case 2:
			quaternion2 = new Quaternion(0f - quaternion.x, 0f - quaternion.y, quaternion.z, quaternion.w) * Quaternion.Euler(Vector3.right * 180f + Vector3.forward * 180f);
			break;
		case 3:
			quaternion2 = MirrorRotation(1, MirrorRotation(0, rot));
			break;
		case 4:
			quaternion2 = MirrorRotation(1, MirrorRotation(2, rot));
			break;
		case 5:
			quaternion2 = MirrorRotation(0, MirrorRotation(2, rot));
			break;
		case 6:
			quaternion2 = MirrorRotation(1, MirrorRotation(0, MirrorRotation(2, rot)));
			break;
		default:
			quaternion2 = quaternion;
			break;
		}
		return info.pivotRotation * quaternion2;
	}

	public void FindSymmetryPoint()
	{
		if (hasSymMachine)
		{
			if (symmetryTransform == null)
			{
				symmetryTransform = new GameObject("SymmetryPivot").transform;
				symmetryTransform.SetParent(symmetryMachine.transform);
			}
			BlockBehaviour blockBehaviour = symmetryMachine.FirstBlock;
			if (blockBehaviour != null && blockBehaviour.Prefab.Type != BlockType.StartingBlock)
			{
				target = checkForBlock.standard(symmetryMachine.MiddlePosition);
			}
		}
	}

	public void AddSymBlocks()
	{
		if (!hasSymMachine)
		{
			Debug.Log("No sym machine!");
			return;
		}
		List<UndoAction> list = new List<UndoAction>();
		List<UndoAction> list2 = new List<UndoAction>();
		BlockBehaviour lastBlock = addPiece.LastBlock;
		if (lastBlock != null)
		{
			if (addPiece.CreatedBlock)
			{
				list.Add(new UndoActionAdd(symmetryMachine, BlockInfo.FromBlockBehaviour(addPiece.LastBlock)));
			}
			if (isNode)
			{
				list2.AddRange(symmetryMachine.nodeController.Select((BuildNodeBlock)lastBlock, 0));
			}
		}
		for (int i = 0; i < mirroredGhosts.Length; i++)
		{
			if (mirroredGhosts[i] == null || !mirroredGhosts[i].isUsed || mirroredGhosts[i].gameObject == null)
			{
				continue;
			}
			Transform transform = mirroredGhosts[i].gameObject.transform;
			if (!addPiece.AddBlockTypeNoSound(transform, addPiece.CurrentType, true))
			{
				continue;
			}
			lastBlock = addPiece.LastBlock;
			if (lastBlock != null)
			{
				if (addPiece.CreatedBlock)
				{
					list.Add(new UndoActionAdd(symmetryMachine, BlockInfo.FromBlockBehaviour(lastBlock)));
				}
				if (isNode)
				{
					list2.AddRange(symmetryMachine.nodeController.Select((BuildNodeBlock)lastBlock, i + 1));
				}
				else if (isDraggedBlock)
				{
					(lastBlock as GenericDraggedBlock).symEndPos = transform;
				}
			}
		}
		list.AddRange(list2);
		if (isDraggedBlock)
		{
			braceStartPoints.Clear();
			braceEndPoints.Clear();
		}
		else if (list.Count > 0)
		{
			if (isNode && symmetryMachine.onBatchOperationComplete != null)
			{
				symmetryMachine.onBatchOperationComplete();
			}
			symmetryMachine.UndoSystem.AddActions(list);
			if (ReferenceMaster.onMachineModified != null)
			{
				ReferenceMaster.onMachineModified(symmetryMachine);
			}
		}
	}

	public void ClearSymGameObjects()
	{
		SymmetryPoint[] array = mirroredGhosts;
		foreach (SymmetryPoint symmetryPoint in array)
		{
			if (symmetryPoint != null && symmetryPoint.gameObject != null)
			{
				UnityEngine.Object.Destroy(symmetryPoint.gameObject);
			}
		}
	}

	public void DisableSymGameObjects()
	{
		for (int i = 0; i < mirroredGhosts.Length; i++)
		{
			SymmetryPoint symmetryPoint = mirroredGhosts[i];
			if (symmetryPoint != null && symmetryPoint.gameObject != null)
			{
				GameObject gameObject = symmetryPoint.gameObject;
				gameObject.SetActive(false);
				if (isNode)
				{
					symmetryMachine.nodeController.UpdateGhost(i + 1, gameObject.transform.position, false);
				}
			}
		}
	}

	public bool CheckForBraceDouble(Vector3 braceStartPos, Vector3 braceEndPos)
	{
		for (int i = 0; i < braceStartPoints.Count; i++)
		{
			if (braceEndPos == braceStartPoints[i])
			{
				return true;
			}
			if (braceStartPos == braceStartPoints[i] && braceEndPos == braceEndPoints[i])
			{
				return true;
			}
		}
		braceStartPoints.Add(braceStartPos);
		braceEndPoints.Add(braceEndPos);
		return false;
	}
}
