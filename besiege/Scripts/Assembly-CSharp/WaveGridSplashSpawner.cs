using UnityEngine;

[AddComponentMenu("VFX/WaveGridSplashSpawner")]
[ExecuteInEditMode]
public class WaveGridSplashSpawner : WaveGridMover
{
	public int count = 24;

	public int gap = 10;

	public Vector3 center = Vector3.zero;

	private Vector3[] grid = new Vector3[0];

	private int index;

	private int p;

	private bool even = true;

	private float delta;

	private bool BigWave(float y)
	{
		return y > 4.25f + WaterController.waterTransformHeight;
	}

	protected override void Start()
	{
		base.Start();
		p = index;
		GenerateGrid();
	}

	protected override void LateUpdate()
	{
		if (grid.Length != 0 && !StatMaster.GodTools.GravityDisabled)
		{
			MoveGrid();
			if (delta >= 1f)
			{
				IterateGrid();
				delta = 0f;
			}
			delta += Time.timeScale * Time.unscaledDeltaTime * 60f;
		}
	}

	protected override float Offset()
	{
		return (float)count * cellSize * 0.4f;
	}

	protected override void ResetPos(ref Vector3 pos)
	{
		pos.y = center.y;
	}

	protected override void SetPosition(Vector3 pos)
	{
		center = pos;
	}

	private void IterateGrid()
	{
		Vector3 vector = grid[p] + center;
		vector.y = WaterController.CheckHeightMap(vector.x, vector.z);
		if (BigWave(vector.y) && Application.isPlaying)
		{
			GlobalParticles.EmitParticleBursts(1, vector + Vector3.down);
		}
		p += gap;
		if (p >= grid.Length)
		{
			if (even)
			{
				index += Mathf.CeilToInt((float)gap * 0.5f);
			}
			else
			{
				index -= Mathf.CeilToInt((float)gap * 0.5f) - 1;
			}
			even = !even;
			if (index >= gap)
			{
				index = 0;
				even = true;
			}
			p = index;
		}
	}

	private void GenerateGrid()
	{
		float y = 0f;
		int num = 0;
		grid = new Vector3[count * count];
		for (int i = 0; i < count; i++)
		{
			for (int j = 0; j < count; j++)
			{
				float x = (float)i - (float)count * 0.5f;
				float z = (float)j - (float)count * 0.5f;
				grid[num] = new Vector3(x, y, z) * cellSize;
				num++;
			}
		}
	}
}
