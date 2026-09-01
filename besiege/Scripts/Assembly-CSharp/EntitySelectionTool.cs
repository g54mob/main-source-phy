using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitySelectionTool : SelectionTool
{
	private LevelEditor levelEditor;

	protected List<LevelEntity> _levelSelection = new List<LevelEntity>();

	private List<LevelUndoAction> undoList = new List<LevelUndoAction>();

	public LevelEntity FirstEntity
	{
		get
		{
			return (base.Count <= 0) ? null : _levelSelection[0];
		}
	}

	public LevelEntity LastEntity
	{
		get
		{
			return (base.Count <= 0) ? null : _levelSelection[base.Count - 1];
		}
	}

	public List<LevelEntity> LevelSelection
	{
		get
		{
			return new List<LevelEntity>(_levelSelection);
		}
	}

	public override bool CanSelect()
	{
		return levelEditor.isActive && AddPiece.isEditingLevel && levelEditor.IsTransformTool(levelEditor.CurrentState) && !StatMaster.ToolActive;
	}

	public void Init(LevelEditor editor)
	{
		if (!isInitialized)
		{
			levelEditor = editor;
			Init();
		}
	}

	public void Remove(LevelEntity entity)
	{
		Remove((ISelectable)entity);
	}

	public void Select(LevelEntity entity, bool multiSelect, bool addToUndo)
	{
		Select(entity, multiSelect, addToUndo, false);
	}

	public void Select(List<LevelEntity> entities, bool multiSelect, bool addToUndo)
	{
		Select(entities.Cast<ISelectable>().ToList(), multiSelect, addToUndo);
	}

	protected override void AddToSelection(ISelectable entity)
	{
		base.AddToSelection(entity);
		_levelSelection.Add(entity as LevelEntity);
	}

	protected override void RemoveFromSelection(ISelectable entity)
	{
		base.RemoveFromSelection(entity);
		_levelSelection.Remove(entity as LevelEntity);
	}

	protected override void RemoveSelectionAt(int index)
	{
		base.RemoveSelectionAt(index);
		_levelSelection.RemoveAt(index);
	}

	protected override void SelectionChanged()
	{
		levelEditor.OnSelectionUpdate();
	}

	protected override void AddSelectionChangeUndo(ISelectable entity, bool selected)
	{
		if (selected)
		{
			undoList.Add(new LUASelectEntity(entity as LevelEntity));
		}
		else
		{
			undoList.Add(new LUADeselectEntity(entity as LevelEntity));
		}
	}

	protected override void StoreSelectionChangeUndos()
	{
		if (undoList.Count != 0)
		{
			if (undoList.Count == 1)
			{
				LevelUndoSystem.Add(undoList[0], false);
			}
			else
			{
				LevelUndoSystem.Add(undoList, false);
			}
			undoList.Clear();
		}
	}

	public void Deselect(LevelEntity entity, bool addToUndo)
	{
		Deselect(entity, addToUndo, false);
	}

	public void Deselect(List<LevelEntity> entities, bool addToUndo)
	{
		Deselect(entities.Cast<ISelectable>().ToList(), addToUndo);
	}

	public override void SelectAll(bool addToUndo)
	{
		Select(levelEditor.Entities.Cast<ISelectable>().ToList(), false, addToUndo);
	}

	public override void DeselectAll(bool addToUndo, bool autoFlush = true)
	{
		base.DeselectAll(addToUndo, autoFlush);
		_levelSelection.Clear();
	}

	protected override void RecoverMissingDragSelection()
	{
		levelEditor.SetActiveTool(levelEditor.CurrentState);
		levelEditor.UpdatePlayerSelection(LastEntity);
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
		foreach (LevelEntity entity in levelEditor.Entities)
		{
			Vector3 center = entity.GetCenter();
			float num = Vector3.Dot(forward, center - position);
			if (!(num <= 0f))
			{
				Vector2 vector = main.WorldToScreenPoint(center);
				if (bounds.Contains(vector))
				{
					dictionary.Add(entity.identifier, entity);
				}
			}
		}
		return dictionary;
	}

	protected void LateUpdate()
	{
		if (levelEditor.CurrentState != StatMaster.Tool.None)
		{
			if (InputManager.AdvancedBuilding.LeftShiftKey())
			{
				ToolTransform.gameObject.SetActive(false);
			}
			else
			{
				ToolTransform.gameObject.SetActive(true);
			}
		}
	}
}
