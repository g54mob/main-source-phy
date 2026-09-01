using System.Collections;
using Pathfinding;
using UnityEngine;

public class RandomizeTerrain : MonoBehaviour
{
	[Range(0f, 5f)]
	public float s1 = 0.5f;

	[Range(0f, 5f)]
	public float s2 = 0.5f;

	[Range(0f, 5f)]
	public float s4 = 0.5f;

	[Range(0f, 5f)]
	public float s8 = 0.5f;

	[Range(0f, 5f)]
	public float s16 = 0.5f;

	[Range(0f, 5f)]
	public float s32 = 0.5f;

	[Range(0f, 5f)]
	public float s64 = 0.5f;

	[Range(0f, 5f)]
	public float s128 = 0.5f;

	[Range(0f, 5f)]
	public float s256 = 0.5f;

	private float x1 = 0.5f;

	private float x2 = 0.5f;

	private float x4 = 0.5f;

	private float x8 = 0.5f;

	private float x16 = 0.5f;

	private float x32 = 0.5f;

	private float x64 = 0.5f;

	private float x128 = 0.5f;

	private float x256 = 0.5f;

	public AnimationCurve curve;

	public AnimationCurve clampMinCurve = AnimationCurve.Constant(0f, 1f, 0f);

	public AnimationCurve curveMaxCurve = AnimationCurve.Constant(0f, 1f, 1f);

	private float clampMin;

	private float clampMax = 1f;

	public AstarPath path;

	private TerrainData terrain;

	private float offset;

	private float allRandom = 1f;

	private float bigRandom = 1f;

	private float normalRandom = 1f;

	private float smallRandom = 1f;

	public void Go(bool recursive = false)
	{
		offset = Random.Range(-1000000, 1000000);
		allRandom = curve.Evaluate(Random.value);
		bigRandom = curve.Evaluate(Random.value);
		normalRandom = curve.Evaluate(Random.value);
		smallRandom = curve.Evaluate(Random.value);
		x1 = s1 * curve.Evaluate(Random.value) * bigRandom;
		x2 = s2 * curve.Evaluate(Random.value) * bigRandom;
		x4 = s4 * curve.Evaluate(Random.value) * bigRandom;
		x8 = s8 * curve.Evaluate(Random.value) * normalRandom;
		x16 = s16 * curve.Evaluate(Random.value) * normalRandom;
		x32 = s32 * curve.Evaluate(Random.value) * normalRandom;
		x64 = s64 * curve.Evaluate(Random.value) * smallRandom;
		x128 = s128 * curve.Evaluate(Random.value) * smallRandom;
		x256 = s256 * curve.Evaluate(Random.value) * smallRandom;
		if (recursive)
		{
			clampMin = 0f;
			clampMax = 1f;
			allRandom *= 0.2f;
		}
		else
		{
			clampMin = clampMinCurve.Evaluate(Random.value);
			clampMax = curveMaxCurve.Evaluate(Random.value);
		}
		terrain = GetComponent<Terrain>().terrainData;
		float[,] heights = terrain.GetHeights(0, 0, terrain.heightmapResolution, terrain.heightmapResolution);
		for (int i = 0; i < heights.GetLength(0); i++)
		{
			for (int j = 0; j < heights.GetLength(1); j++)
			{
				if (!recursive)
				{
					heights[i, j] = 0f;
				}
				heights[i, j] += GetNoise(i, j);
			}
		}
		terrain.SetHeights(0, 0, heights);
		if (!recursive)
		{
			Clamp();
			Go(recursive: true);
		}
		else
		{
			RecenterTerrain();
			StartCoroutine(Scan());
		}
	}

	private IEnumerator Scan()
	{
		foreach (Progress item in AstarPath.active.ScanAsync())
		{
			_ = item;
			yield return null;
		}
	}

	private void Clamp()
	{
		float num = terrain.GetHeight((int)((float)terrain.heightmapResolution * 0.5f), (int)((float)terrain.heightmapResolution * 0.5f)) - 300f;
		terrain = GetComponent<Terrain>().terrainData;
		float[,] array = new float[terrain.heightmapResolution, terrain.heightmapResolution];
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array.GetLength(1); j++)
			{
				array[i, j] = Mathf.Clamp((terrain.GetHeight(i, j) - num) / 600f, clampMin, clampMax);
			}
		}
		terrain.SetHeights(0, 0, array);
	}

	private void RecenterTerrain()
	{
		float num = terrain.GetHeight((int)((float)terrain.heightmapResolution * 0.5f), (int)((float)terrain.heightmapResolution * 0.5f)) - 300f;
		terrain = GetComponent<Terrain>().terrainData;
		float[,] array = new float[terrain.heightmapResolution, terrain.heightmapResolution];
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array.GetLength(1); j++)
			{
				array[i, j] = (terrain.GetHeight(i, j) - num) / 600f;
			}
		}
		terrain.SetHeights(0, 0, array);
	}

	private float GetNoise(float x, float y)
	{
		x += offset;
		y += offset;
		float num = 0.002f * allRandom;
		float num2 = Mathf.PerlinNoise(x * num * 1f, y * num * 1f);
		float num3 = Mathf.PerlinNoise(x * num * 2f, y * num * 2f);
		float num4 = Mathf.PerlinNoise(x * num * 4f, y * num * 4f);
		float num5 = Mathf.PerlinNoise(x * num * 8f, y * num * 8f);
		float num6 = Mathf.PerlinNoise(x * num * 16f, y * num * 16f);
		float num7 = Mathf.PerlinNoise(x * num * 32f, y * num * 32f);
		float num8 = Mathf.PerlinNoise(x * num * 64f, y * num * 64f);
		float num9 = Mathf.PerlinNoise(x * num * 128f, y * num * 128f);
		float num10 = Mathf.PerlinNoise(x * num * 256f, y * num * 256f);
		return (num2 * x1 + num3 * x2 + num4 * x4 + num5 * x8 + num6 * x16 + num7 * x32 + num8 * x64 + num9 * x128 + num10 * x256) * 0.1f;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			Go();
		}
	}
}
