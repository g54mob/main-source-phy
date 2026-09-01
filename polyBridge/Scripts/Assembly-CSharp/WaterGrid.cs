using System.Collections.Generic;
using UnityEngine;

public class WaterGrid
{
	public List<WaterNode> nodes = new List<WaterNode>();

	private List<WaterWave> waves = new List<WaterWave>();

	public int width;

	public int height;

	public bool sleeping = true;

	public static WaterGrid CreateGridOfSize(int _width, int _height)
	{
		WaterGrid waterGrid = new WaterGrid();
		waterGrid.width = _width;
		waterGrid.height = _height;
		int num = 0;
		for (int i = 0; i < _height; i++)
		{
			for (int j = 0; j < _width; j++)
			{
				WaterNode waterNode = new WaterNode();
				waterNode.origin = new Vector2(j, i);
				waterGrid.AddNode(waterNode);
				num++;
			}
		}
		return waterGrid;
	}

	public void AddNode(WaterNode node)
	{
		nodes.Add(node);
	}

	public void Update(float dt)
	{
		WaterWave waterWave = null;
		float num = 0.1f;
		foreach (WaterWave wave in waves)
		{
			wave.Update(Time.deltaTime);
		}
		foreach (WaterNode node in nodes)
		{
			foreach (WaterWave wave2 in waves)
			{
				if (!wave2.HasAffectedNode(node) && Mathf.Abs((node.origin - wave2.origin).magnitude - wave2.GetSize()) < num)
				{
					node.ApplyForce(wave2.GetForce(), wave2.firstTime);
					wave2.AddAffectedNode(node);
				}
				if (wave2.GetForce() < 0.01f)
				{
					waterWave = wave2;
				}
			}
			node.UpdateManual(dt);
		}
		if (waterWave != null)
		{
			waves.Remove(waterWave);
		}
	}

	public void CreateWaveAtPosition(Vector2 pos, float force)
	{
		WaterWave waterWave = new WaterWave(force);
		waterWave.origin = pos;
		sleeping = false;
		waterWave.forceMultiplier = force;
		waves.Add(waterWave);
	}

	public bool CanCreateWaves()
	{
		return waves.Count < 5;
	}

	public float GetHeightAtPos(int x, int y)
	{
		return nodes[x + y * width].GetPointHeight();
	}

	public void ClearForce()
	{
		foreach (WaterNode node in nodes)
		{
			node.ClearForce();
		}
		waves.Clear();
	}
}
