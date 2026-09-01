using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityTransformTool : TransformTool
{
	protected LevelEditor levelEditor;

	protected List<LevelEntity> levelSelection;

	protected List<LevelUndoAction> levelUndoActions = new List<LevelUndoAction>();

	protected override void OnEnable()
	{
		levelEditor = LevelEditor.Instance;
		base.OnEnable();
	}

	protected override void OnGizmoClicked()
	{
		OnGizmoClicked(levelEditor.selectionController.Selection);
	}

	protected override void OnGizmoClicked(List<ISelectable> selection)
	{
		levelSelection = selection.Cast<LevelEntity>().ToList();
		base.OnGizmoClicked(selection);
	}

	protected override bool SnapKeyHeld()
	{
		return InputManager.LevelEditor.LeftCtrlKey();
	}

	protected override bool ReverseKey()
	{
		return InputManager.LevelEditor.LeftAltKey();
	}

	protected override bool MultiSelectKey()
	{
		return InputManager.LevelEditor.LeftShiftKey();
	}

	protected override bool UseSnap()
	{
		bool grid = StatMaster.Mode.LevelEditor.grid;
		return (!SnapKeyHeld()) ? grid : (!grid);
	}

	protected override void OnGizmoReleased()
	{
		base.OnGizmoReleased();
		levelEditor.UpdateTool();
	}

	public override void AddUndo(ISelectable entity, int i)
	{
		LevelUndoAction levelUndoAction = CreateUndoAction(entity as LevelEntity, originalPositions[i], originalRotations[i], originalScales[i]);
		if (levelUndoAction != null)
		{
			levelUndoActions.Add(levelUndoAction);
		}
	}

	protected virtual LevelUndoAction CreateUndoAction(LevelEntity entity, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		return null;
	}

	public override void ProcessUndo()
	{
		if (levelUndoActions.Count != 0)
		{
			if (levelUndoActions.Count == 1)
			{
				LevelUndoSystem.Add(levelUndoActions[0]);
			}
			else
			{
				LevelUndoSystem.Add(levelUndoActions);
			}
			levelUndoActions.Clear();
		}
	}

	protected override Vector3 SnapVector(Vector3 oldPos, Vector3 delta, float snapValue)
	{
		Vector3 result = oldPos + delta;
		if (!StatMaster.Mode.LevelEditor.grid)
		{
			return result;
		}
		return base.SnapVector(oldPos, delta, snapValue);
	}
}
