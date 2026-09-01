using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Water/Controllers/Calm Zone Controller")]
[ExecuteInEditMode]
public class CalmZoneController : MonoBehaviour
{
	public const int MAX_ZONES = 96;

	public const float squareSize = 40f;

	private const uint d = 64u;

	public static int calmCount;

	public Material m;

	public Material[] secondaryMaterials = new Material[0];

	[HideInInspector]
	public Material causticsMaterial;

	public CalmZone[] calmZones = new CalmZone[0];

	public bool updateInUpdate;

	[HideInInspector]
	public Vector4[] CellsContains = new Vector4[64];

	private int baseID;

	private int zoneID;

	private int arrayCount;

	private int cellsContains;

	private Vector4[] gridArray;

	[HideInInspector]
	public Vector4[] v;

	[HideInInspector]
	public float[] b;

	[HideInInspector]
	public int numerOfZones;

	protected int arraySize = 96;

	private bool initialized;

	private bool initializing;

	private bool causticsAdded;

	public static CalmZoneController lastInstance;

	private WaterCausticsCamera waterCausticsCamera;

	private float n;

	private bool usingExtra;

	protected void OnEnable()
	{
		if (!initializing && !initialized)
		{
			Init();
		}
		calmCount = calmZones.Length;
	}

	public uint GetCellKey(Vector2 pos)
	{
		pos.x /= 40f;
		pos.y /= 40f;
		if (pos.x < 0f)
		{
			n = (int)(pos.x - 0.99f) * 3;
		}
		else
		{
			n = (int)pos.x * 3;
		}
		if (pos.y < 0f)
		{
			pos.y = (int)(pos.y - 0.99f) * 7;
		}
		else
		{
			pos.y = (int)pos.y * 7;
		}
		n += pos.y;
		n = ((!(n < 0f)) ? n : (n * -1f)) * 64f + n;
		return (uint)n & 0x3F;
	}

	private void Init()
	{
		if (calmZones.Length > 96)
		{
			Debug.LogError("too many calm zones assigned compared to shader array size in WaterInclude");
			return;
		}
		initializing = true;
		lastInstance = this;
		baseID = Shader.PropertyToID("CalmBaseMultiplier");
		zoneID = Shader.PropertyToID("CalmZones");
		arrayCount = Shader.PropertyToID("CalmArrayCount");
		cellsContains = Shader.PropertyToID("CellsContains");
		b = new float[96];
		v = new Vector4[96];
		gridArray = new Vector4[64];
		for (int i = 0; i < 96; i++)
		{
			b[i] = (v[i].w = 1f);
		}
		numerOfZones = calmZones.Length + WaterZone.primaryZones.Count;
		arraySize = calmZones.Length;
		m.SetInt(arrayCount, arraySize);
		for (int j = 0; j < secondaryMaterials.Length; j++)
		{
			secondaryMaterials[j].SetInt(arrayCount, arraySize);
			secondaryMaterials[j].SetFloat("_WaterY", WaterController.waterTransformHeight + 0.05f);
		}
		if (!causticsAdded)
		{
			waterCausticsCamera = Object.FindObjectOfType<WaterCausticsCamera>();
			if (waterCausticsCamera != null)
			{
				causticsMaterial = waterCausticsCamera.Mat;
				causticsMaterial.SetInt(arrayCount, arraySize);
				causticsMaterial.SetFloat("_WaterY", WaterController.waterTransformHeight + 0.05f);
				causticsAdded = true;
			}
		}
		InitializeGrid();
		AssignGridArray();
		AssignArrays();
		UpdateMaterial();
		initialized = true;
		initializing = false;
	}

	public void UpdateZones(List<WaterZone> zones)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		usingExtra = true;
		int num = calmZones.Length + zones.Count;
		if (arraySize != num)
		{
			arraySize = num;
			m.SetInt(arrayCount, arraySize);
			for (int i = 0; i < secondaryMaterials.Length; i++)
			{
				secondaryMaterials[i].SetInt(arrayCount, arraySize);
			}
			if (causticsAdded)
			{
				causticsMaterial.SetInt(arrayCount, arraySize);
			}
		}
		if (updateInUpdate)
		{
			AssignArrays();
		}
		WaterController.WaterBoundsExceedsTop = false;
		WaterController.WaterBoundsExceedsBottom = false;
		for (int j = calmZones.Length; j < Mathf.Min(num, 96); j++)
		{
			WaterZone waterZone = zones[j - calmZones.Length];
			float value = waterZone.Value;
			b[j] = value;
			v[j].x = waterZone.Position.x;
			v[j].y = waterZone.Position.z;
			v[j].z = Mathf.Pow(waterZone.Range, 2f);
			v[j].w = waterZone.Exponent;
			if (value > 1.001f)
			{
				WaterController.WaterBoundsExceedsTop = true;
			}
			if (value < -0.001f)
			{
				WaterController.WaterBoundsExceedsBottom = true;
			}
			waterZone.UpdateGrid((uint)j, this);
		}
		AssignGridArray();
		UpdateMaterial();
	}

	public void ClearExtraZones()
	{
		if (Application.isPlaying)
		{
			arraySize = calmZones.Length;
			m.SetInt(arrayCount, arraySize);
			for (int i = 0; i < secondaryMaterials.Length; i++)
			{
				secondaryMaterials[i].SetInt(arrayCount, arraySize);
			}
			if (causticsAdded)
			{
				causticsMaterial.SetInt(arrayCount, arraySize);
			}
			UpdateMaterial();
			usingExtra = false;
			WaterController.WaterBoundsExceedsTop = false;
			WaterController.WaterBoundsExceedsBottom = false;
		}
	}

	protected void Update()
	{
		numerOfZones = calmZones.Length + WaterZone.primaryZones.Count;
		if (WaterZone.primaryZones.Count > 0)
		{
			UpdateZones(WaterZone.primaryZones);
			AssignGridArray();
		}
		else if (usingExtra)
		{
			ClearExtraZones();
		}
		else if (updateInUpdate && calmZones.Length > 0)
		{
			AssignArrays();
			UpdateMaterial();
		}
	}

	protected void AssignGridArray()
	{
		for (int i = 0; i < gridArray.Length; i++)
		{
			gridArray[i] = CellsContains[i];
		}
	}

	private void AssignArrays()
	{
		for (int i = 0; i < calmZones.Length; i++)
		{
			b[i] = calmZones[i].baseValue;
			v[i].x = calmZones[i].transform.position.x;
			v[i].y = calmZones[i].transform.position.z;
			v[i].z = calmZones[i].gradientSize * calmZones[i].gradientSize;
			v[i].w = calmZones[i].exponentialIncrease;
		}
	}

	private void InitializeGrid()
	{
		Vector4 zero = Vector4.zero;
		for (int i = 0; i < CellsContains.Length; i++)
		{
			CellsContains[i] = zero;
		}
		for (int j = 0; j < calmZones.Length; j++)
		{
			calmZones[j].PopulateGrid((uint)j, this);
		}
	}

	private void UpdateMaterial()
	{
		UpdateMat(m);
		for (int i = 0; i < secondaryMaterials.Length; i++)
		{
			UpdateMat(secondaryMaterials[i]);
		}
		if (causticsAdded)
		{
			UpdateMat(causticsMaterial);
		}
	}

	private void UpdateMat(Material m)
	{
		m.SetFloatArray(baseID, b);
		m.SetVectorArray(zoneID, v);
		m.SetVectorArray(cellsContains, gridArray);
	}

	public void SetMat(Material m)
	{
		this.m = m;
		for (int i = 0; i < secondaryMaterials.Length; i++)
		{
			SetMaterialToFollowWater(secondaryMaterials[i]);
		}
	}

	public void SetHeight()
	{
		for (int i = 0; i < secondaryMaterials.Length; i++)
		{
			secondaryMaterials[i].SetFloat("_WaterY", WaterController.waterTransformHeight + 0.05f);
		}
		if (causticsAdded)
		{
			causticsMaterial.SetFloat("_WaterY", WaterController.waterTransformHeight + 0.05f);
		}
	}

	public void SetMaterialToFollowWater(Material m)
	{
		m.SetFloat("_WaterY", WaterController.waterTransformHeight + 0.05f);
		m.SetFloat("_Wave1Scale", this.m.GetFloat("_Wave1Scale"));
		m.SetFloat("_Wave2Scale", this.m.GetFloat("_Wave2Scale"));
		m.SetFloat("_BigWaveScale", this.m.GetFloat("_BigWaveScale"));
		m.SetFloat("_DetailHeight", this.m.GetFloat("_DetailHeight"));
		m.SetFloat("_BigWaveHeight", this.m.GetFloat("_BigWaveHeight"));
		m.SetFloat("_MicroDetailHeight", this.m.GetFloat("_MicroDetailHeight"));
		m.SetFloat("_WaveSpeed", this.m.GetFloat("_WaveSpeed"));
		m.SetFloat("_FoamSize", this.m.GetFloat("_FoamSize"));
	}

	protected void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus && !initialized && !initializing)
		{
			Init();
		}
	}

	protected void OnDestroy()
	{
		if (this == lastInstance)
		{
			m.SetInt(arrayCount, 0);
			for (int i = 0; i < secondaryMaterials.Length; i++)
			{
				secondaryMaterials[i].SetInt(arrayCount, 0);
			}
			if (causticsAdded)
			{
				causticsMaterial.SetInt(arrayCount, 0);
			}
		}
	}

	public void Reset()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			Object.DestroyImmediate(base.transform.GetChild(num).gameObject);
		}
	}
}
