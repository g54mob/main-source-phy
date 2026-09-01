using System.Collections.Generic;
using UnityEngine;

public class EntityGroundTool : EntityTransformTool
{
	protected override bool UseDragTool
	{
		get
		{
			return false;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		GameObject gameObject = GameObject.Find("FloorBig");
		if (gameObject == null)
		{
			Debug.LogError("Couldn't find the floor!");
		}
	}

	protected override void OnGizmoReleased()
	{
		bool flag = !reverse;
		float num = float.MaxValue;
		bool flag2 = false;
		List<LevelUndoAction> list = new List<LevelUndoAction>();
		for (int i = 0; i < levelSelection.Count; i++)
		{
			LevelEntity levelEntity = levelSelection[i];
			if (levelEntity == null)
			{
				continue;
			}
			LevelBoundingBox.GroundResult groundResult = levelEntity.behaviour.Ground();
			if (!groundResult.hasHit)
			{
				continue;
			}
			if (flag)
			{
				LevelEntity levelEntity2 = groundResult.hitCollider.GetComponentInParent<LevelEntity>();
				if (levelEntity2 != null)
				{
					if (levelEntity2.hasBase)
					{
						levelEntity2 = levelEntity2.baseEntity as LevelEntity;
					}
					if (levelSelection.Contains(levelEntity2))
					{
						continue;
					}
				}
				if (!flag2 || groundResult.hitDistance < num)
				{
					num = groundResult.hitDistance;
					flag2 = true;
				}
			}
			else if (groundResult.hitDistance > 0f)
			{
				LevelUndoAction item = GroundEntity(levelEntity, i, groundResult.hitDistance);
				list.Add(item);
			}
		}
		if (flag && flag2 && num > 0f)
		{
			for (int i = 0; i < levelSelection.Count; i++)
			{
				LevelEntity levelEntity = levelSelection[i];
				if (levelEntity != null)
				{
					LevelUndoAction item = GroundEntity(levelSelection[i], i, num);
					list.Add(item);
				}
			}
		}
		if (list.Count > 0)
		{
			LevelUndoSystem.Add(list);
		}
		levelEditor.UpdateTool();
		ResetTool();
	}

	private LevelUndoAction GroundEntity(LevelEntity entity, int index, float dist)
	{
		Vector3 position = entity.Position + Vector3.down * (dist - entity.behaviour.prefab.groundOffset);
		entity.SetPosition(position);
		entity.transform.position = position;
		return new LUAMoveEntity(entity, originalPositions[index]);
	}
}
