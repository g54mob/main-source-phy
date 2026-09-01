using UnityEngine;

public class BrushTerrainModifierEntity : TerrainModifierEntity
{
	[SerializeField]
	[Header("Brush Modifier Settings")]
	private float radiusMultiplier = 18f;

	public override TerrainModifierType ModifierType
	{
		get
		{
			return TerrainModifierType.Brush;
		}
	}

	public override void Init()
	{
		base.Init();
		BrushIndex = modifierController.DefaultBrushIndex;
	}

	protected override void UpdateEntityTransform()
	{
		base.Position = base.transform.position;
		Vector3 scale = entity.Scale;
		base.BrushSize = Mathf.CeilToInt(Mathf.Max(scale.x, scale.y, scale.z) * radiusMultiplier);
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}
}
