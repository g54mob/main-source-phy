using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Besiege;
using Mono.CSharp;
using UnityEngine;

public class BlockSelectionTool : SelectionTool
{
	private class MirrorEntry
	{
		public int Index;

		public Vector3 Position;

		public BlockType Type;
	}

	private AdvancedBlockEditor blockEditor;

	public static bool Duplicating;

	public Transform lastClickedTransformInfo;

	protected bool lastIsClicked;

	protected List<BlockBehaviour> _machineSelection = new List<BlockBehaviour>();

	private List<UndoAction> undoList = new List<UndoAction>();

	public BlockBehaviour FirstBlock
	{
		get
		{
			for (int i = 0; i < _machineSelection.Count; i++)
			{
				if (!_machineSelection[i].IsSelectedExtra)
				{
					return _machineSelection[i];
				}
			}
			return null;
		}
	}

	public BlockBehaviour LastBlock
	{
		get
		{
			for (int num = _machineSelection.Count - 1; num >= 0; num--)
			{
				if (!_machineSelection[num].IsSelectedExtra)
				{
					return _machineSelection[num];
				}
			}
			return null;
		}
	}

	public Transform LastClickedTransformInfo
	{
		get
		{
			return (!lastIsClicked) ? lastClickedTransformInfo : LastBlock.transform;
		}
	}

	public List<BlockBehaviour> MachineSelection
	{
		get
		{
			return new List<BlockBehaviour>(_machineSelection);
		}
	}

	private bool SelectionReady()
	{
		Machine machine = Machine.Active();
		if (machine != null && !machine.isSimulating && !StatMaster.Mode.selectSymmetryPivot)
		{
			return true;
		}
		return false;
	}

	public override bool CanSelect()
	{
		if (SelectionReady())
		{
			switch (StatMaster.Mode.selectedTool)
			{
			case StatMaster.Tool.Translate:
			case StatMaster.Tool.Rotate:
			case StatMaster.Tool.Scale:
			case StatMaster.Tool.Mirror:
			case StatMaster.Tool.Modify:
				return true;
			}
		}
		return false;
	}

	public override bool CanDrag()
	{
		return CanSelect() || (StatMaster.advancedBuilding && (StatMaster.Mode.selectedTool == StatMaster.Tool.Erase || StatMaster.Mode.selectedTool == StatMaster.Tool.Paint));
	}

	public void Init(AdvancedBlockEditor editor)
	{
		if (!isInitialized)
		{
			blockEditor = editor;
			SymmetryController symmetryController = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
			symmetryController.OnAxisChanged = (Action)System.Delegate.Combine(symmetryController.OnAxisChanged, new Action(OnSymmetryChanged));
			Init();
		}
	}

	public void OnSymmetryChanged()
	{
		if (SelectedObjects.Count != 0)
		{
			Select(new List<ISelectable>(SelectedObjects), SelectedObjects);
			AdvancedBlockEditor.Instance.UpdateTool();
		}
	}

	public virtual void Select(List<ISelectable> before, List<ISelectable> entities)
	{
		if (entities.Count == 0)
		{
			return;
		}
		List<ISelectable> selectList = entities.Where((ISelectable s) => !s.IsSelectedExtra).ToList();
		List<AdditionalData> list = (from s in GetAdditionalSelection(selectList)
			where !selectList.Contains(s.selectable)
			select s).ToList();
		list.ForEach(delegate(AdditionalData x)
		{
			if (!selectList.Contains(x.selectable) || x.selectable.IsSelectedExtra)
			{
				selectList.Add(x.selectable);
			}
		});
		DeselectAll(true, false);
		if (StatMaster.Mode.displayDrag && AeroDynamicDisplay.IsSelected)
		{
			AeroDynamicDisplay.Select(false);
		}
		bool flag = false;
		foreach (ISelectable item in selectList)
		{
			if (!item.IsDestroyed)
			{
				BeforeSelectionChanged();
				if (item.IsSelected)
				{
					AddSelectionChangeUndo(item, false);
					item.Select(false);
				}
				else
				{
					flag = true;
				}
				int num = AdditionalIndex(list, item);
				if (num != -1)
				{
					AdditionalData additionalData = list[num];
					item.IsSelectedExtra = true;
					item.SymmetryIndex = additionalData.symmetryIndex;
					item.TransformMultiplier = additionalData.transformMultiplier;
				}
				else
				{
					item.IsSelectedExtra = false;
					item.SymmetryIndex = 0;
					item.TransformMultiplier = 1f;
				}
				if (!item.IsSelected)
				{
					AddSelectionChangeUndo(item, true);
				}
				AddToSelection(item);
			}
		}
		bool flag2 = SelectedObjects.Count != before.Count;
		if (!flag2)
		{
			flag2 = !SelectedObjects.All(before.Contains) || !before.All(SelectedObjects.Contains);
		}
		if (flag2)
		{
			StoreSelectionChangeUndos();
		}
		else
		{
			undoList.Clear();
		}
		if (flag)
		{
			SelectionChanged();
		}
	}

	public void Remove(BlockBehaviour entity)
	{
		Remove((ISelectable)entity);
	}

	public void Select(BlockBehaviour entity, bool multiSelect, bool addToUndo, bool checkAdditional)
	{
		Select((ISelectable)entity, multiSelect, addToUndo, checkAdditional);
	}

	public void Select(List<BlockBehaviour> entities, bool multiSelect, bool addToUndo)
	{
		Select(entities.Cast<ISelectable>().ToList(), multiSelect, addToUndo);
	}

	private void AddMirrorEntries(List<Tuple<MirrorEntry, float>> entries, Matrix4x4 globalToLocal, float transformMultiplier, BlockBehaviour block, Vector3 pos)
	{
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		List<SymmetryController.MirrorInfo> mirrorInfo = instance.symmetryController.GetMirrorInfo(pos, Quaternion.identity);
		foreach (SymmetryController.MirrorInfo item in mirrorInfo)
		{
			Vector3 mirrorPos = globalToLocal.MultiplyPoint3x4(item.Position);
			if (entries.FindIndex((Tuple<MirrorEntry, float> x) => x.Item1.Type == block.Prefab.Type && x.Item1.Position == mirrorPos) == -1)
			{
				entries.Add(new Tuple<MirrorEntry, float>(new MirrorEntry
				{
					Index = item.Index + 1,
					Type = block.Prefab.Type,
					Position = mirrorPos
				}, transformMultiplier));
			}
		}
	}

	protected override List<AdditionalData> GetAdditionalSelection(IEnumerable<ISelectable> entities)
	{
		List<AdditionalData> additionals = new List<AdditionalData>();
		Machine machine = Machine.Active();
		Transform buildingMachine = machine.BuildingMachine;
		Matrix4x4 localToWorldMatrix = buildingMachine.localToWorldMatrix;
		Matrix4x4 worldToLocalMatrix = buildingMachine.worldToLocalMatrix;
		List<Tuple<MirrorEntry, float>> list = new List<Tuple<MirrorEntry, float>>();
		foreach (ISelectable entity in entities)
		{
			BlockBehaviour blockBehaviour = (BlockBehaviour)entity;
			if (blockBehaviour.Prefab.Type != BlockType.BuildSurface)
			{
				AddMirrorEntries(list, worldToLocalMatrix, 1f, blockBehaviour, localToWorldMatrix.MultiplyPoint3x4(blockBehaviour.Position));
			}
			switch (blockBehaviour.Prefab.Type)
			{
			case BlockType.BuildSurface:
			{
				BuildSurface buildSurface = (BuildSurface)blockBehaviour;
				if (!buildSurface.isValid)
				{
					break;
				}
				for (int num = 0; num < buildSurface.nodes.Length; num++)
				{
					if (!entities.Contains(buildSurface.nodes[num]))
					{
						AddAdditional(additionals, new AdditionalData
						{
							selectable = buildSurface.nodes[num]
						});
					}
				}
				for (int num = 0; num < buildSurface.edges.Length; num++)
				{
					if (!entities.Contains(buildSurface.edges[num]))
					{
						AddAdditional(additionals, new AdditionalData
						{
							selectable = buildSurface.edges[num]
						});
					}
				}
				break;
			}
			case BlockType.BuildNode:
			{
				BuildNodeBlock buildNodeBlock = (BuildNodeBlock)blockBehaviour;
				List<BuildEdgeBlock> edges = buildNodeBlock.ParentMachine.nodeController.GetEdges(buildNodeBlock);
				edges.ForEach(delegate(BuildEdgeBlock x)
				{
					if (!entities.Contains(x))
					{
						bool flag2 = entities.Contains(x.startNode);
						bool flag3 = entities.Contains(x.endNode);
						AddAdditional(additionals, new AdditionalData
						{
							selectable = x,
							transformMultiplier = ((!flag2 || !flag3) ? StatMaster.SurfaceEdgeMovement : 1f)
						});
					}
				});
				break;
			}
			}
		}
		for (int num = 0; num < additionals.Count; num++)
		{
			BlockBehaviour blockBehaviour2 = additionals[num].selectable as BlockBehaviour;
			AddMirrorEntries(list, worldToLocalMatrix, additionals[num].transformMultiplier, blockBehaviour2, localToWorldMatrix.MultiplyPoint3x4(blockBehaviour2.Position));
		}
		if (list.Count > 0)
		{
			BlockBehaviour b;
			for (int num = 0; num < machine.BlockCount; num++)
			{
				if (!machine.GetBlockFromIndex(num, out b) || entities.Contains(b))
				{
					continue;
				}
				int num2 = list.FindIndex((Tuple<MirrorEntry, float> x) => x.Item1.Type == b.Prefab.Type && x.Item1.Position == b.Position);
				if (num2 != -1)
				{
					AddAdditional(additionals, new AdditionalData
					{
						selectable = b,
						symmetryIndex = list[num2].Item1.Index,
						transformMultiplier = list[num2].Item2
					});
				}
				else
				{
					if (b.Prefab.Type != BlockType.BuildSurface)
					{
						continue;
					}
					BuildSurface buildSurface = b as BuildSurface;
					if (!buildSurface.isValid)
					{
						continue;
					}
					int num3 = -1;
					bool flag = true;
					for (int num4 = 0; num4 < buildSurface.edges.Length; num4++)
					{
						if (entities.Contains(buildSurface.edges[num4]))
						{
							num3 = Mathf.Max(num3, 0);
							continue;
						}
						int num5 = AdditionalIndex(additionals, buildSurface.edges[num4]);
						if (num5 != -1)
						{
							num3 = Mathf.Min(additionals[num5].symmetryIndex, num3);
							continue;
						}
						flag = false;
						break;
					}
					if (flag)
					{
						AddAdditional(additionals, new AdditionalData
						{
							selectable = buildSurface,
							symmetryIndex = num3
						});
					}
				}
			}
		}
		return additionals;
	}

	protected override void AddToSelection(ISelectable entity)
	{
		base.AddToSelection(entity);
		BlockBehaviour blockBehaviour = entity as BlockBehaviour;
		_machineSelection.Add(blockBehaviour);
		SetLastTransformInfo(blockBehaviour);
		AddtoSelection(blockBehaviour);
	}

	protected virtual void SetLastTransformInfo(BlockBehaviour block)
	{
		lastClickedTransformInfo.position = block.transform.position;
		lastClickedTransformInfo.rotation = block.transform.rotation;
		lastIsClicked = true;
	}

	protected override void RemoveFromSelection(ISelectable entity)
	{
		BlockBehaviour blockBehaviour = entity as BlockBehaviour;
		if (blockBehaviour == LastBlock)
		{
			lastIsClicked = false;
		}
		base.RemoveFromSelection(entity);
		_machineSelection.Remove(blockBehaviour);
		RemoveFromSelection(blockBehaviour);
	}

	protected override void RemoveSelectionAt(int index)
	{
		BlockBehaviour blockBehaviour = SelectedObjects[index] as BlockBehaviour;
		if (blockBehaviour == LastBlock)
		{
			lastIsClicked = false;
		}
		base.RemoveSelectionAt(index);
		_machineSelection.RemoveAt(index);
		RemoveFromSelection(blockBehaviour);
	}

	public override void CleanupSelection()
	{
		if (!UndoSystem.processing)
		{
			return;
		}
		List<ISelectable> list = new List<ISelectable>(SelectedObjects);
		bool flag = true;
		foreach (ISelectable item in list)
		{
			if (!item.IsSelected || item.IsDestroyed || item.IsSelectedExtra)
			{
				continue;
			}
			flag = false;
			break;
		}
		if (!flag)
		{
			return;
		}
		foreach (ISelectable item2 in list)
		{
			if (item2.IsSelected)
			{
				RemoveFromSelection(item2);
			}
		}
		SelectedObjects.Clear();
		_machineSelection.Clear();
	}

	protected virtual void AddtoSelection(BlockBehaviour block)
	{
		BlockVisualController visualController = block.VisualController;
		visualController.SetSelected();
		SetGizmo();
	}

	protected virtual void RemoveFromSelection(BlockBehaviour block)
	{
		BlockVisualController visualController = block.VisualController;
		if (block == SingleInstanceFindOnly<AddPiece>.Instance.HoveredBlock)
		{
			visualController.SetHighlighted(true);
		}
		else
		{
			visualController.SetNoOutline();
		}
		SetGizmo();
	}

	public virtual void RecolorSelection()
	{
		for (int i = 0; i < base.Count; i++)
		{
			_machineSelection[i].VisualController.SetSelected();
		}
	}

	protected override void BeforeSelectionChanged()
	{
		if (StatMaster.Mode.BeforeSelectionChanged != null)
		{
			StatMaster.Mode.BeforeSelectionChanged();
		}
	}

	protected override void SelectionChanged()
	{
		SpatialKeyHUDController.BlockSelectionChanged();
		if (StatMaster.Mode.SelectionChanged != null)
		{
			StatMaster.Mode.SelectionChanged();
		}
		SetGizmo();
	}

	private void Automerge()
	{
		if (StatMaster.mergeSurfaceTypesOnDeselect)
		{
			Machine machine = Machine.Active();
			Dictionary<BlockBehaviour, BlockBehaviour> mergeDict;
			HashSet<BlockBehaviour> removeList;
			undoList.AddRange(machine.nodeController.Merge(out mergeDict, out removeList));
			if (machine.onBatchOperationComplete != null)
			{
				machine.onBatchOperationComplete();
			}
		}
	}

	public override void Deselect(List<ISelectable> entities, bool addToUndo)
	{
		if (addToUndo && entities.Count > 0)
		{
			Automerge();
		}
		base.Deselect(entities, addToUndo);
	}

	public override void DeselectAll(bool addToUndo, bool autoFlush = true)
	{
		if (SelectedObjects.Count == 0)
		{
			return;
		}
		if (StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling)
		{
			Debug.LogError("tried to deselect all blocks during a block transformation");
			return;
		}
		BeforeSelectionChanged();
		if (addToUndo)
		{
			Automerge();
		}
		if (SelectedObjects.Count == 0)
		{
			if (addToUndo && autoFlush)
			{
				StoreSelectionChangeUndos();
			}
			SelectionChanged();
		}
		else
		{
			base.DeselectAll(addToUndo, autoFlush);
		}
	}

	protected override void AddSelectionChangeUndo(ISelectable entity, bool selected)
	{
		BlockBehaviour blockBehaviour = entity as BlockBehaviour;
		if (selected)
		{
			undoList.Add(new UndoActionSelect(blockBehaviour.ParentMachine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
		}
		else
		{
			undoList.Add(new UndoActionDeselect(blockBehaviour.ParentMachine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
		}
	}

	protected override void StoreSelectionChangeUndos()
	{
		if (undoList.Count != 0)
		{
			Machine machine = Machine.Active();
			if (machine != null)
			{
				machine.UndoSystem.AddActions(undoList);
			}
			undoList.Clear();
		}
	}

	public void Deselect(BlockBehaviour entity, bool addToUndo, bool updateAdditional)
	{
		Deselect((ISelectable)entity, addToUndo, updateAdditional);
	}

	public void Deselect(List<BlockBehaviour> entities, bool addToUndo)
	{
		Deselect(entities.Cast<ISelectable>().ToList(), addToUndo);
	}

	public override void SelectAll(bool addToUndo)
	{
		Select(blockEditor.Blocks.Cast<ISelectable>().ToList(), false, addToUndo);
		AdvancedBlockEditor.Instance.CheckShowBlockMapper();
	}

	protected override void RecoverMissingDragSelection()
	{
		blockEditor.SetActiveTool(StatMaster.Mode.selectedTool);
		blockEditor.UpdatePlayerSelection(base.Last as BlockBehaviour);
	}

	protected override void Update()
	{
		if (!isInitialized || !active)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (!AddPiece.isEditingLevel && (!machine || !machine.CanModify || machine.isSimulating))
		{
			if (image.enabled)
			{
				image.enabled = false;
				_startDragging = false;
			}
			return;
		}
		ProcessDragSelecting(StatMaster.Mode.selectedTool != StatMaster.Tool.Paint && StatMaster.Mode.selectedTool != StatMaster.Tool.Erase);
		if (FocusKey())
		{
			FocusOnSelection();
		}
		else if (InputManager.CloseKey() && InputManager.ToCloseCount <= 0 && !StatMaster.inMenu)
		{
			DeselectAll(true);
		}
	}

	protected override bool MultiSelect()
	{
		return InputManager.AdvancedBuilding.LeftShiftKey();
	}

	protected virtual void LateUpdate()
	{
		if (StatMaster.inMenu)
		{
			return;
		}
		if (CanSelect())
		{
			if (InputManager.AdvancedBuilding.SelectAllKeys())
			{
				SelectAll(true);
			}
			else if (InputManager.AdvancedBuilding.SelectInverseKeys())
			{
				InverseSelection(true);
			}
			else if (InputManager.AdvancedBuilding.DuplicateKeys())
			{
				if (LastBlock != null)
				{
					List<UndoAction> actions = DuplicateSelection();
					Machine.Active().UndoSystem.AddActions(actions);
				}
			}
			else if (InputManager.AdvancedBuilding.BreakSurface())
			{
				BreakSurface();
			}
			else if (InputManager.DeleteKey())
			{
				RemoveSelection();
			}
			else if (InputManager.AdvancedBuilding.ExportObj() && StatMaster.advancedBuilding)
			{
				StopAllCoroutines();
				StartCoroutine(ExportMachineAsObj());
			}
		}
		else if (SelectionReady() && StatMaster.Mode.LevelEditor.selectedTool == StatMaster.Tool.None && InputManager.AdvancedBuilding.SelectAllKeys())
		{
			AdvancedBlockEditor.Instance.SetActiveTool(StatMaster.Tool.Translate, false);
			SelectAll(true);
		}
	}

	public IEnumerator ExportMachineAsObj()
	{
		List<BlockBehaviour> sel = _machineSelection;
		if (sel.Count == 0)
		{
			sel = ((!Machine.Active().isSimulating) ? Machine.Active().SimulationBlocks : Machine.Active().BuildingBlocks);
			if (sel.Count == 0)
			{
				Debug.LogWarning("[Selection] Can't export no selection");
				yield break;
			}
		}
		Debug.Log("[Selection] Starting OBJ export");
		if (!AssetImporter.readableMeshes)
		{
			AssetImporter.readableMeshes = true;
			foreach (BlockSkinLoader.SkinPack.Skin skin in BlockSkinLoader.loadedSkins)
			{
				skin.ResetMesh();
			}
			yield return null;
			while (AssetImporter.LoadingObject.queue.Count > 0)
			{
				yield return null;
			}
		}
		foreach (BlockBehaviour block in sel)
		{
			block.VisualController.UpdateVis(block.VisualController.selectedSkin);
		}
		ObjExporter.Export(sel, Machine.Active().name, true);
		Debug.Log("[Selection] finished OBJ export");
	}

	public void RemoveSelection(bool playSound = true)
	{
		List<UndoAction> actions = RemoveBlocks(MachineSelection);
		Machine.Active().UndoSystem.AddActions(actions);
		SelectedObjects.Clear();
		_machineSelection.Clear();
		blockEditor.UpdateGizmo();
	}

	private bool GetMirrorBlock(List<BlockBehaviour> blockList, BlockType type, Vector3 pos, out BlockBehaviour block)
	{
		for (int i = 0; i < blockList.Count; i++)
		{
			BlockBehaviour blockBehaviour = blockList[i];
			if (blockBehaviour.Prefab.Type == type && blockBehaviour.Position == pos)
			{
				block = blockBehaviour;
				return true;
			}
		}
		block = null;
		return false;
	}

	public List<UndoAction> RemoveBlocks(List<BlockBehaviour> selection, bool playSound = true)
	{
		SelectionTool.BatchChange = true;
		Machine machine = Machine.Active();
		List<UndoAction> list = new List<UndoAction>();
		if (machine == null || machine.isLoadingInfo)
		{
			SelectionTool.BatchChange = false;
			return list;
		}
		if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		bool removedAnything = false;
		List<BlockBehaviour> list2 = new List<BlockBehaviour>();
		Matrix4x4 localToWorldMatrix = machine.BuildingMachine.localToWorldMatrix;
		Matrix4x4 worldToLocalMatrix = machine.BuildingMachine.worldToLocalMatrix;
		for (int i = 0; i < selection.Count; i++)
		{
			BlockBehaviour blockBehaviour = selection[i];
			if (blockBehaviour.IsDestroyed)
			{
				selection.Remove(blockBehaviour);
				i--;
				continue;
			}
			bool flag = false;
			if (blockBehaviour.IsSelected && blockBehaviour.IsSelectedExtra && blockBehaviour.SymmetryIndex > 0)
			{
				Vector3 pos = worldToLocalMatrix.MultiplyPoint3x4(SingleInstanceFindOnly<AddPiece>.Instance.symmetryController.MirrorVector(blockBehaviour.SymmetryIndex - 1, localToWorldMatrix.MultiplyPoint3x4(blockBehaviour.Position)));
				BlockBehaviour block;
				if ((GetMirrorBlock(selection, blockBehaviour.Prefab.Type, pos, out block) || GetMirrorBlock(list2, blockBehaviour.Prefab.Type, pos, out block)) && !block.IsSelectedExtra)
				{
					flag = true;
				}
			}
			bool flag2 = false;
			if (blockBehaviour.SurfaceType)
			{
				bool flag3 = true;
				if (blockBehaviour.IsSelectedExtra && !flag && !machine.nodeController.GetSurfaces(blockBehaviour).TrueForAll((BuildSurface x) => selection.Contains(x)))
				{
					list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, true, 0, blockBehaviour.TransformMultiplier));
					AdvancedBlockEditor.Instance.selectionController.Deselect(blockBehaviour, false, false);
					flag3 = false;
				}
				if (flag3)
				{
					list2.Add(blockBehaviour);
				}
				flag2 = true;
			}
			else if (blockBehaviour.IsSelectedExtra && !flag)
			{
				list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
				AdvancedBlockEditor.Instance.selectionController.Deselect(blockBehaviour, false, false);
				flag2 = true;
			}
			if (flag2)
			{
				selection.Remove(blockBehaviour);
				i--;
			}
		}
		if (list2.Count > 0)
		{
			machine.nodeController.AddDependencies(list2);
			for (int num = list2.Count - 1; num >= 0; num--)
			{
				BlockBehaviour blockBehaviour = list2[num];
				if (!blockBehaviour.IsDestroyed)
				{
					BlockInfo blockInfo = BlockInfo.FromBlockBehaviour(blockBehaviour);
					if (blockBehaviour.IsSelected)
					{
						list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
					}
					machine.RemoveBlock(blockBehaviour);
					list.Add(new UndoActionRemove(machine, blockInfo));
				}
			}
		}
		for (int num2 = selection.Count - 1; num2 >= 0; num2--)
		{
			BlockBehaviour blockBehaviour = selection[num2];
			if (blockBehaviour.IsSelected)
			{
				list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
			}
			BlockInfo blockInfo2 = BlockInfo.FromBlockBehaviour(blockBehaviour);
			if (RemoveBlock(machine, blockBehaviour, ref removedAnything))
			{
				list.Add(new UndoActionRemove(machine, blockInfo2));
			}
			else if (blockBehaviour.IsSelected)
			{
				Deselect(blockBehaviour, false, false);
			}
		}
		SingleInstanceFindOnly<AddPiece>.Instance.PostRemoveBlock(machine, playSound && removedAnything);
		if (machine.onBatchOperationComplete != null)
		{
			machine.onBatchOperationComplete();
		}
		if (StatMaster.cachingTransformActions)
		{
			(machine as ServerMachine).FlushAndBan();
		}
		SelectionTool.BatchChange = false;
		machine.Analyze();
		return list;
	}

	public bool RemoveBlock(Machine activeMachine, BlockBehaviour block, ref bool removedAnything)
	{
		if (block == null)
		{
			return false;
		}
		if (block.Prefab.Type == BlockType.StartingBlock && activeMachine.GetBlocks(BlockType.StartingBlock).Count <= 1)
		{
			return false;
		}
		Machine componentInParent = block.GetComponentInParent<Machine>();
		if (!componentInParent || activeMachine != componentInParent)
		{
			Debug.LogWarning("Invalid machine!");
			return false;
		}
		removedAnything = true;
		componentInParent.RemoveBlock(block);
		return true;
	}

	public virtual void InverseSelection(bool addToUndo)
	{
		SelectionTool.BatchChange = true;
		List<UndoActionReplaceSelection.ReplaceEntry> list = new List<UndoActionReplaceSelection.ReplaceEntry>();
		List<BlockBehaviour> list2 = new List<BlockBehaviour>();
		Machine machine = Machine.Active();
		List<UndoAction> list3 = new List<UndoAction>();
		if (StatMaster.mergeSurfaceTypesOnDeselect && addToUndo)
		{
			Dictionary<BlockBehaviour, BlockBehaviour> mergeDict;
			HashSet<BlockBehaviour> removeList;
			list3.AddRange(machine.nodeController.Merge(out mergeDict, out removeList));
		}
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(i, out block) && !block.IsSelected && !block.IsSelectedExtra)
			{
				list.Add(new UndoActionReplaceSelection.ReplaceEntry
				{
					guid = block.Guid,
					isExtra = false,
					symmetryIndex = 0,
					transformMultiplier = block.TransformMultiplier
				});
				list2.Add(block);
			}
		}
		List<AdditionalData> additionalSelection = GetAdditionalSelection(list2.Cast<ISelectable>());
		for (int i = 0; i < additionalSelection.Count; i++)
		{
			AdditionalData additionalData = additionalSelection[i];
			BlockBehaviour block = additionalData.selectable as BlockBehaviour;
			if (!list2.Contains(block))
			{
				list.Add(new UndoActionReplaceSelection.ReplaceEntry
				{
					guid = block.Guid,
					isExtra = true,
					symmetryIndex = additionalData.symmetryIndex,
					transformMultiplier = additionalData.transformMultiplier
				});
				list2.Add(block);
			}
		}
		if (addToUndo)
		{
			List<UndoActionReplaceSelection.ReplaceEntry> list4 = new List<UndoActionReplaceSelection.ReplaceEntry>();
			for (int i = 0; i < _machineSelection.Count; i++)
			{
				BlockBehaviour block = _machineSelection[i];
				list4.Add(new UndoActionReplaceSelection.ReplaceEntry
				{
					guid = block.Guid,
					isExtra = block.IsSelectedExtra,
					symmetryIndex = block.SymmetryIndex,
					transformMultiplier = block.TransformMultiplier
				});
			}
			list3.Add(new UndoActionReplaceSelection(machine, list4, list));
			machine.UndoSystem.AddActions(list3);
		}
		Select(list2.Cast<ISelectable>().ToList(), false, false);
		SelectionTool.BatchChange = false;
		machine.Analyze();
		AdvancedBlockEditor.Instance.CheckShowBlockMapper();
	}

	public List<UndoAction> DuplicateSelection()
	{
		SelectionTool.BatchChange = true;
		List<UndoAction> undoActions = new List<UndoAction>();
		Machine machine = Machine.Active();
		HashSet<BlockBehaviour> removeList = new HashSet<BlockBehaviour>();
		BlockBehaviour lastBlock = LastBlock;
		Vector3 position = lastBlock.transform.position;
		int blockID = lastBlock.BlockID;
		List<BlockBehaviour> machineSelection = MachineSelection;
		if (machineSelection.Any((BlockBehaviour x) => x.SurfaceType))
		{
			List<UndoAction> list = new List<UndoAction>();
			if (StatMaster.mergeSurfaceTypesOnDeselect)
			{
				Dictionary<BlockBehaviour, BlockBehaviour> mergeDict;
				undoActions.AddRange(machine.nodeController.Merge(out mergeDict, out removeList));
			}
			machineSelection = MachineSelection;
			for (int num = 0; num < machineSelection.Count; num++)
			{
				if (removeList.Contains(machineSelection[num]))
				{
					RemoveFromSelection((ISelectable)machineSelection[num]);
				}
			}
			machineSelection = _machineSelection;
			List<BlockBehaviour> list2 = new List<BlockBehaviour>(machineSelection.Count);
			HashSet<BlockBehaviour> hashSet = new HashSet<BlockBehaviour>();
			BlockBehaviour hoveredBlock = SingleInstanceFindOnly<AddPiece>.Instance.HoveredBlock;
			List<BlockBehaviour> list3 = new List<BlockBehaviour>();
			List<BlockBehaviour> list4 = new List<BlockBehaviour>();
			List<BlockBehaviour> list5 = new List<BlockBehaviour>();
			for (int num = 0; num < machineSelection.Count; num++)
			{
				BlockBehaviour blockBehaviour = machineSelection[num];
				BlockVisualController visualController = blockBehaviour.VisualController;
				switch (blockBehaviour.Prefab.Type)
				{
				case BlockType.BuildNode:
					if (blockBehaviour == hoveredBlock)
					{
						visualController.SetHighlighted(true);
					}
					else
					{
						visualController.SetNoOutline();
					}
					list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
					blockBehaviour.Select(false);
					machineSelection.RemoveAt(num--);
					break;
				case BlockType.BuildEdge:
					if (blockBehaviour == hoveredBlock)
					{
						visualController.SetHighlighted(true);
					}
					else
					{
						visualController.SetNoOutline();
					}
					list.Add(new UndoActionDeselect(machine, blockBehaviour.Guid, blockBehaviour.IsSelectedExtra, blockBehaviour.SymmetryIndex, blockBehaviour.TransformMultiplier));
					blockBehaviour.Select(false);
					machineSelection.RemoveAt(num--);
					break;
				case BlockType.BuildSurface:
				{
					BuildSurface buildSurface = blockBehaviour as BuildSurface;
					if (buildSurface != null && buildSurface.isValid)
					{
						BuildEdgeBlock[] edges = buildSurface.edges;
						foreach (BuildEdgeBlock buildEdgeBlock in edges)
						{
							if (hashSet.Add(buildEdgeBlock.startNode))
							{
								list3.Add(buildEdgeBlock.startNode);
							}
							if (hashSet.Add(buildEdgeBlock.endNode))
							{
								list3.Add(buildEdgeBlock.endNode);
							}
							if (hashSet.Add(buildEdgeBlock))
							{
								list4.Add(buildEdgeBlock);
							}
						}
						if (hashSet.Add(blockBehaviour))
						{
							list5.Add(blockBehaviour);
						}
					}
					machineSelection.RemoveAt(num--);
					break;
				}
				}
			}
			list2.AddRange(list3);
			list2.AddRange(list4);
			list2.AddRange(list5);
			machineSelection.InsertRange(0, list2);
			SelectedObjects = new List<ISelectable>(machineSelection.ToArray());
			if (machineSelection.Count == 0)
			{
				Duplicating = false;
				SelectionTool.BatchChange = false;
				undoActions.AddRange(list);
				machine.Analyze();
				return undoActions;
			}
		}
		Duplicating = true;
		Machine machine2 = Machine.Active();
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		machine2.isLoadingInfo = true;
		if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		List<BlockInfo> list6 = new List<BlockInfo>(machineSelection.Count);
		for (int num3 = 0; num3 < machineSelection.Count; num3++)
		{
			BlockBehaviour block = machineSelection[num3];
			list6.Add(BlockInfo.FromBlockBehaviour(block));
		}
		Dictionary<Guid, BlockBehaviour> addedBlocks;
		machine2.AddBlocksFromInfo(list6, out addedBlocks, ref undoActions);
		machine2.isLoadingInfo = false;
		if (addedBlocks.Count > 0)
		{
			List<UndoActionReplaceSelection.ReplaceEntry> list7 = new List<UndoActionReplaceSelection.ReplaceEntry>();
			BlockBehaviour blockBehaviour;
			for (int num = 0; num < machineSelection.Count; num++)
			{
				blockBehaviour = machineSelection[num];
				bool isExtra = !blockBehaviour.IsSelected || blockBehaviour.IsSelectedExtra;
				list7.Add(new UndoActionReplaceSelection.ReplaceEntry
				{
					guid = blockBehaviour.Guid,
					isExtra = isExtra,
					symmetryIndex = blockBehaviour.SymmetryIndex,
					transformMultiplier = blockBehaviour.TransformMultiplier
				});
			}
			DeselectAll(false);
			List<UndoActionReplaceSelection.ReplaceEntry> list8 = new List<UndoActionReplaceSelection.ReplaceEntry>();
			blockBehaviour = null;
			KeyValuePair<Guid, BlockBehaviour> added;
			foreach (KeyValuePair<Guid, BlockBehaviour> item in addedBlocks)
			{
				added = item;
				blockBehaviour = added.Value;
				UndoActionReplaceSelection.ReplaceEntry replaceEntry = list7.Find((UndoActionReplaceSelection.ReplaceEntry x) => x.guid == added.Key);
				bool isExtra2 = replaceEntry.isExtra;
				list8.Add(new UndoActionReplaceSelection.ReplaceEntry
				{
					guid = blockBehaviour.Guid,
					isExtra = isExtra2,
					symmetryIndex = replaceEntry.symmetryIndex,
					transformMultiplier = replaceEntry.transformMultiplier
				});
				Select(blockBehaviour, true, false, isExtra2, replaceEntry.symmetryIndex, replaceEntry.transformMultiplier);
			}
			if (machine2.onBatchOperationComplete != null)
			{
				machine2.onBatchOperationComplete();
			}
			undoActions.Add(new UndoActionReplaceSelection(machine2, list7, list8));
			instance.SingleHammerAnimate(blockBehaviour.transform.position, blockBehaviour.transform.position, blockBehaviour.transform.forward);
		}
		if (StatMaster.cachingTransformActions)
		{
			(machine2 as ServerMachine).FlushAndBan();
		}
		switch ((BlockType)blockID)
		{
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
		case BlockType.BuildSurface:
			foreach (BlockBehaviour item2 in MachineSelection)
			{
				if (item2.transform.position == position && blockID == item2.BlockID)
				{
					Machine m = Machine.Active();
					undoActions.Add(new UndoActionSetSelectionPivot(m, lastBlock.Guid, item2.Guid));
					SetBlockAsLast(item2);
					if (machine2.onBatchOperationComplete != null)
					{
						machine2.onBatchOperationComplete();
					}
					break;
				}
			}
			break;
		}
		Duplicating = false;
		SelectionTool.BatchChange = false;
		machine.Analyze();
		AdvancedBlockEditor.Instance.CheckShowBlockMapper();
		return undoActions;
	}

	private void BreakSurface()
	{
		SelectionTool.BatchChange = true;
		List<BlockBehaviour> machineSelection = MachineSelection;
		List<UndoAction> list = new List<UndoAction>();
		list.AddRange(DuplicateSelection());
		list.AddRange(RemoveBlocks(machineSelection.Where((BlockBehaviour x) => x.Prefab.Type == BlockType.BuildSurface || !SurfaceComponent(x.Prefab.Type)).ToList()));
		SelectionTool.BatchChange = false;
		Machine.Active().Analyze();
		Machine.Active().UndoSystem.AddActions(list);
	}

	private bool SurfaceComponent(BlockType t)
	{
		switch (t)
		{
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
		case BlockType.BuildSurface:
			return true;
		default:
			return false;
		}
	}

	public void SetBlockAsLast(BlockBehaviour block)
	{
		block.IsSelectedExtra = false;
		block.VisualController.SetSelected();
		SelectedObjects.Remove(block);
		_machineSelection.Remove(block);
		SelectedObjects.Add(block);
		_machineSelection.Add(block);
	}

	protected override void StartDragSelection()
	{
		blockEditor.ShowToolGizmo(StatMaster.Tool.None);
	}

	protected override void FinishDragSelection()
	{
		if (StatMaster.Mode.selectedTool == StatMaster.Tool.Erase)
		{
			List<BlockBehaviour> machineSelection = MachineSelection;
			DeselectAll(false);
			List<UndoAction> actions = RemoveBlocks(machineSelection);
			Machine.Active().UndoSystem.AddActions(actions);
		}
		else if (StatMaster.Mode.selectedTool == StatMaster.Tool.Paint)
		{
			List<BlockBehaviour> machineSelection = MachineSelection;
			DeselectAll(false);
			BlockSkinLoader.SetBlocksToPack(SkinPaintTool.Skin.pack, Machine.Active(), machineSelection);
		}
		else
		{
			SetGizmo();
			AdvancedBlockEditor.Instance.CheckShowBlockMapper();
		}
	}

	protected virtual void SetGizmo()
	{
		if (!base.IsDragging)
		{
			blockEditor.SetGizmo(StatMaster.Mode.selectedTool);
		}
	}

	protected override Dictionary<long, ISelectable> GetSelectedObjects(Vector3 startPos, Vector3 endPos)
	{
		Vector3 min = Vector3.Min(startPos, endPos);
		Vector3 max = Vector3.Max(startPos, endPos);
		Bounds bounds = default(Bounds);
		bounds.SetMinMax(min, max);
		Camera main = Camera.main;
		Vector3 forward = main.transform.forward;
		Vector3 position = main.transform.position;
		Dictionary<long, ISelectable> dictionary = new Dictionary<long, ISelectable>();
		foreach (BlockBehaviour block in blockEditor.Blocks)
		{
			Vector3 center = block.GetCenter();
			float num = Vector3.Dot(forward, center - position);
			if (!(num <= 0f))
			{
				Vector2 vector = main.WorldToScreenPoint(center);
				if (bounds.Contains(vector))
				{
					dictionary.Add(block.identifier, block);
				}
			}
		}
		return dictionary;
	}
}
