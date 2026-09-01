using UnityEngine;

public class IslandStampEntity : TerrainModifierEntity
{
	[SerializeField]
	[Header("Island Stamp Settings")]
	private IslandStampVisualizer visualizer;

	[SerializeField]
	private int stampIndex;

	public override TerrainModifierType ModifierType
	{
		get
		{
			return TerrainModifierType.Stamp;
		}
	}

	public override int BrushIndex
	{
		get
		{
			return stampIndex;
		}
	}

	public override void Init()
	{
		base.Init();
		UpdateEntityTransform();
		DestroyVisualizer();
	}

	private void DestroyVisualizer()
	{
		Object.Destroy(visualizer.gameObject);
	}

	protected override void UpdateEntityTransform()
	{
		base.Position = base.transform.position;
		Vector3 scale = entity.Scale;
		float num = Mathf.Max(scale.x, scale.y, scale.z);
		base.BrushSize = Mathf.CeilToInt(512f * num);
	}
}
