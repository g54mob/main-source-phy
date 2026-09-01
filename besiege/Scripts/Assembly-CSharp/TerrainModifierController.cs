using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainModifierController : MonoBehaviour
{
	private struct BrushIdentifier
	{
		public int BrushIndex;

		public int BrushSize;
	}

	[Serializable]
	private struct IslandStampTemplate
	{
		public string Name;

		public float MaxHeight;
	}

	private struct ClearArea
	{
		public Vector3 Position;

		public int BrushSize;
	}

	public const int DefaultIslandStampSize = 512;

	[SerializeField]
	[Header("References")]
	private Terrain terrain;

	[SerializeField]
	private TerrainCollider terrainCollider;

	[SerializeField]
	[Header("Brush settings")]
	private int currentBrushIndex = 3;

	[SerializeField]
	private AnimationCurve stampCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[SerializeField]
	[Header("General settings")]
	private int smoothIterations = 3;

	[SerializeField]
	private float defaultFloorHeight;

	[SerializeField]
	private IslandStampTemplate[] islandStamps;

	private List<ClearArea> clearAreas = new List<ClearArea>();

	private List<TerrainModifierEntity> additiveEntities = new List<TerrainModifierEntity>();

	private List<TerrainModifierEntity> subtractiveEntities = new List<TerrainModifierEntity>();

	private List<TerrainModifierEntity> smoothEntities = new List<TerrainModifierEntity>();

	private Dictionary<BrushIdentifier, Brush> cachedBrushes = new Dictionary<BrushIdentifier, Brush>();

	private Dictionary<BrushIdentifier, Brush> cachedStamps = new Dictionary<BrushIdentifier, Brush>();

	private float[,] emptyTerrainHeights;

	private Texture2D[] brushTextures;

	private Brush[] islandSourceBrushes;

	private HeightmapPainter heightmapPainter;

	private HeightmapPainter.TerrainTool DefaultTool = HeightmapPainter.TerrainTool.StampHeight;

	private GameObject floorBigObject;

	private float terrainMaxYPosition;

	private bool isDirty;

	public float MaxTerrainHeight
	{
		get
		{
			return terrain.terrainData.size.y;
		}
	}

	public float TerrainYPosition
	{
		get
		{
			return terrain.transform.position.y;
		}
	}

	public int DefaultBrushIndex
	{
		get
		{
			return currentBrushIndex;
		}
	}

	public Brush CurrentBrush
	{
		get
		{
			return heightmapPainter.brush;
		}
	}

	public Texture2D StampTexture
	{
		get
		{
			return (heightmapPainter == null) ? null : heightmapPainter.stampTexture;
		}
	}

	public Texture2D StampHeightmapTexture
	{
		get
		{
			return (heightmapPainter == null) ? null : heightmapPainter.heightmapStampTexture;
		}
	}

	private void Awake()
	{
		InitializeTerrain();
		LoadBrushTextures();
		LoadIslandStamps();
		InitializeHeightmapPainter();
		ResetTerrain();
	}

	private void InitializeTerrain()
	{
		CreateEmptyTerrainHeights();
		TerrainData terrainData = new TerrainData();
		terrain.terrainData.CopyProperties(terrainData);
		terrain.terrainData = terrainData;
		TerrainCollider component = terrain.GetComponent<TerrainCollider>();
		component.terrainData = terrainData;
	}

	private void CreateEmptyTerrainHeights()
	{
		TerrainData terrainData = terrain.terrainData;
		int heightmapHeight = terrainData.heightmapHeight;
		int heightmapWidth = terrainData.heightmapWidth;
		emptyTerrainHeights = new float[terrainData.heightmapHeight, terrainData.heightmapWidth];
		for (int i = 0; i < heightmapHeight; i++)
		{
			for (int j = 0; j < heightmapWidth; j++)
			{
				emptyTerrainHeights[i, j] = defaultFloorHeight;
			}
		}
	}

	private void OnEnable()
	{
		floorBigObject = GameObject.Find("FloorBig");
		if (floorBigObject != null)
		{
			floorBigObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		if (floorBigObject != null)
		{
			floorBigObject.SetActive(true);
		}
	}

	private void Update()
	{
		if (isDirty)
		{
			RebuildTerrain();
			isDirty = false;
		}
	}

	public void ResetTerrain()
	{
		FlattenTerrain();
		FlushTerrain();
	}

	public void RegisterModifier(TerrainModifierEntity entity)
	{
		AddEntity(entity, entity.BlendMode);
		entity.SetDirty(true);
		MarkTerrainDirty();
	}

	public void UnregisterModifier(TerrainModifierEntity entity)
	{
		RemoveEntity(entity, entity.BlendMode);
		clearAreas.Add(new ClearArea
		{
			Position = entity.Position,
			BrushSize = entity.BrushSize
		});
		MarkTerrainDirty();
	}

	public void EntityUpdated(TerrainModifierEntity entity)
	{
		MarkTerrainDirty();
		UpdateNearModifications(entity);
		clearAreas.Add(new ClearArea
		{
			Position = entity.Position,
			BrushSize = entity.BrushSize
		});
	}

	public void EntityBlendModeUpdate(TerrainModifierEntity entity)
	{
		RemoveEntity(entity, entity.LastBlendMode);
		AddEntity(entity, entity.BlendMode);
		MarkTerrainDirty();
	}

	private void InitializeHeightmapPainter()
	{
		heightmapPainter = new HeightmapPainter(DefaultTool, null, terrain.terrainData);
		terrainMaxYPosition = terrain.GetPosition().y + terrain.terrainData.size.y;
	}

	private void LoadBrushTextures()
	{
		brushTextures = new Texture2D[21];
		for (int i = 0; i < 21; i++)
		{
			string path = string.Format("LevelEditor/TerrainBrushes/builtin_brush_{0}", i + 1);
			Texture2D texture2D = Resources.Load<Texture2D>(path);
			brushTextures[i] = texture2D;
		}
	}

	private void AddEntity(TerrainModifierEntity entity, ModifierBlendMode blendMode)
	{
		switch (blendMode)
		{
		case ModifierBlendMode.Additive:
			additiveEntities.Add(entity);
			break;
		case ModifierBlendMode.Subtractive:
			subtractiveEntities.Add(entity);
			break;
		default:
			smoothEntities.Add(entity);
			break;
		}
	}

	private void RemoveEntity(TerrainModifierEntity entity, ModifierBlendMode blendMode)
	{
		switch (blendMode)
		{
		case ModifierBlendMode.Additive:
			additiveEntities.Remove(entity);
			break;
		case ModifierBlendMode.Subtractive:
			subtractiveEntities.Remove(entity);
			break;
		default:
			smoothEntities.Remove(entity);
			break;
		}
	}

	private void MarkTerrainDirty()
	{
		isDirty = true;
	}

	private void RebuildTerrain()
	{
		Debug.Log("Rebuilding terrain...");
		DateTime now = DateTime.Now;
		heightmapPainter.PreparePainter();
		ClearDirtyAreas();
		ApplyModifications();
		SmoothTerrain();
		FlushTerrain();
		Debug.Log("Rebuilding terrain took: " + (DateTime.Now - now).TotalMilliseconds + " ms.");
	}

	private void ApplyModifications()
	{
		IEnumerable<TerrainModifierEntity> enumerable = from x in additiveEntities.Concat(subtractiveEntities).Concat(smoothEntities)
			where x.IsDirty
			select x;
		foreach (TerrainModifierEntity item in enumerable)
		{
			ApplyModification(item.Position, item.BrushSize, item.BlendMode, item.ModifierType, item.BrushIndex);
		}
	}

	private void ClearDirtyAreas()
	{
		foreach (ClearArea clearArea in clearAreas)
		{
			ClearEntityArea(clearArea.Position, clearArea.BrushSize);
		}
		clearAreas.Clear();
		IEnumerable<TerrainModifierEntity> enumerable = from x in additiveEntities.Concat(subtractiveEntities).Concat(smoothEntities)
			where x.IsDirty
			select x;
		foreach (TerrainModifierEntity item in enumerable)
		{
			ClearEntityArea(item.LastPosition, item.LastBrushSize);
		}
	}

	private void ClearEntityArea(Vector3 worldPosition, int brushSize)
	{
		Vector2 uv;
		Vector3 pos;
		if (Raycast(worldPosition, out uv, out pos))
		{
			heightmapPainter.ClearArea(uv, brushSize);
		}
	}

	private void ApplyModification(Vector3 worldPosition, int brushSize, ModifierBlendMode blendMode, TerrainModifierType modifierType, int brushIndex)
	{
		Vector2 uv;
		Vector3 pos;
		if (Raycast(worldPosition, out uv, out pos))
		{
			brushSize = ((modifierType != TerrainModifierType.Brush) ? Mathf.CeilToInt((float)brushSize / Mathf.Max(terrain.terrainData.heightmapScale.x, terrain.terrainData.heightmapScale.z)) : brushSize);
			heightmapPainter.targetHeight = ((modifierType != TerrainModifierType.Brush) ? 1f : CalculateTargetHeight(worldPosition));
			heightmapPainter.brushSize = brushSize;
			heightmapPainter.strength = 1f;
			heightmapPainter.blendMode = blendMode;
			UpdateBrush(brushIndex, brushSize, modifierType);
			if (modifierType == TerrainModifierType.Brush)
			{
				StampBrushHeight(uv);
			}
			else
			{
				StampIsland(uv);
			}
		}
	}

	private void UpdateNearModifications(TerrainModifierEntity modificationEntity)
	{
		IEnumerable<TerrainModifierEntity> enumerable = from x in additiveEntities.Concat(subtractiveEntities).Concat(smoothEntities)
			where !x.IsDirty && x != modificationEntity
			select x;
		modificationEntity.SetDirty(true);
		foreach (TerrainModifierEntity item in enumerable)
		{
			float num = Mathf.Abs(modificationEntity.LastPosition.x - item.LastPosition.x) + Mathf.Abs(modificationEntity.LastPosition.z - item.LastPosition.z);
			if (num < (float)(modificationEntity.LastBrushSize + item.LastBrushSize))
			{
				item.SetDirty(true);
			}
		}
	}

	private void SmoothTerrain()
	{
		for (int i = 0; i < smoothIterations; i++)
		{
			TerrainModifierTools.Smooth(terrain.terrainData);
		}
	}

	public Texture2D GetIslandStampTexture(int islandStampIndex)
	{
		Brush brush = islandSourceBrushes[islandStampIndex];
		return brush.PreviewTexture;
	}

	private void UpdateBrush(int brushIndex, int brushSize, TerrainModifierType modifierType)
	{
		BrushIdentifier key = new BrushIdentifier
		{
			BrushIndex = brushIndex,
			BrushSize = brushSize
		};
		Brush value;
		if (modifierType == TerrainModifierType.Brush)
		{
			if (!cachedBrushes.TryGetValue(key, out value))
			{
				value = new Brush();
				value.Load(brushTextures[brushIndex], brushSize);
				cachedBrushes.Add(key, value);
			}
		}
		else if (!cachedStamps.TryGetValue(key, out value))
		{
			Brush brush = islandSourceBrushes[brushIndex];
			IslandStampTemplate islandStamp = islandStamps[brushIndex];
			float stampHeightMultiplier = GetStampHeightMultiplier(islandStamp);
			value = brush.ResizeBrush(brushSize, stampHeightMultiplier);
			cachedStamps.Add(key, value);
		}
		heightmapPainter.brush = value;
	}

	private float GetStampHeightMultiplier(IslandStampTemplate islandStamp)
	{
		return islandStamp.MaxHeight / terrain.terrainData.size.y;
	}

	private void LoadIslandStamps()
	{
		List<Brush> list = new List<Brush>();
		IslandStampTemplate[] array = islandStamps;
		for (int i = 0; i < array.Length; i++)
		{
			IslandStampTemplate islandStamp = array[i];
			Brush brush = new Brush();
			float stampHeightMultiplier = GetStampHeightMultiplier(islandStamp);
			float[,] rawHeightmap = LoadRawIslandStamp(islandStamp.Name);
			brush.LoadFromRaw(rawHeightmap, 512, stampHeightMultiplier);
			list.Add(brush);
		}
		islandSourceBrushes = list.ToArray();
	}

	private float[,] LoadRawIslandStamp(string islandStampName)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(string.Format("LevelEditor/IslandStamps/{0}.raw", islandStampName));
		byte[] bytes = textAsset.bytes;
		RawTerrainEncoder rawTerrainEncoder = new RawTerrainEncoder();
		return rawTerrainEncoder.DecodeTerrain(bytes);
	}

	private void StampBrushHeight(Vector2 uv)
	{
		heightmapPainter.StampBrushHeight(uv.x, uv.y, stampCurve);
	}

	private void StampIsland(Vector2 uv)
	{
		heightmapPainter.PaintIslandStamp(uv.x, uv.y);
	}

	private bool Raycast(Vector3 worldPosition, out Vector2 uv, out Vector3 pos)
	{
		Ray downwardsProjectionRay = TerrainModifierTools.GetDownwardsProjectionRay(worldPosition, terrainMaxYPosition);
		RaycastHit hitInfo;
		if (terrainCollider.Raycast(downwardsProjectionRay, out hitInfo, float.PositiveInfinity))
		{
			uv = hitInfo.textureCoord;
			pos = hitInfo.point;
			return true;
		}
		uv = Vector2.zero;
		pos = Vector3.zero;
		return false;
	}

	private void FlattenTerrain()
	{
		terrain.terrainData.SetHeightsDelayLOD(0, 0, emptyTerrainHeights);
	}

	private void FlushTerrain()
	{
		terrain.ApplyDelayedHeightmapModification();
	}

	private void OnDestroy()
	{
		foreach (Brush value in cachedBrushes.Values)
		{
			value.Dispose();
		}
	}

	private float CalculateTargetHeight(Vector3 worldPosition)
	{
		float num = Mathf.Clamp(terrain.transform.InverseTransformPoint(worldPosition).y, 0f, terrain.terrainData.size.y);
		return num / terrain.terrainData.size.y;
	}
}
