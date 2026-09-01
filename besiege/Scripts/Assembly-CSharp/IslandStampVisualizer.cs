using UnityEngine;

public class IslandStampVisualizer : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer stampRenderer;

	private Material stampMaterial;

	private Vector3 initialTextureSize;

	private void Awake()
	{
		stampMaterial = stampRenderer.material;
		IslandStampEntity componentInParent = GetComponentInParent<IslandStampEntity>();
		if (!(componentInParent == null))
		{
			TerrainModifierController controller = GetController();
			Texture2D islandStampTexture = controller.GetIslandStampTexture(componentInParent.BrushIndex);
			if (!(islandStampTexture == null))
			{
				Initialize(islandStampTexture, controller.MaxTerrainHeight, controller.TerrainYPosition);
			}
		}
	}

	private TerrainModifierController GetController()
	{
		LevelEnvironment env = LevelEditor.Instance.environmentManager.GetEnv(LevelSettings.LevelEnvironment.Water);
		if (env == null)
		{
			return null;
		}
		return env.envParent.GetComponentInChildren<TerrainModifierController>();
	}

	public void Initialize(Texture2D stampTexture, float maxTerrainHeight, float terrainYPosition)
	{
		stampMaterial.SetTexture("_MainTex", stampTexture);
		stampMaterial.SetFloat("_TerrainYPosition", terrainYPosition);
		initialTextureSize = new Vector3(stampTexture.width, maxTerrainHeight, stampTexture.height);
		base.transform.localScale = initialTextureSize;
	}
}
