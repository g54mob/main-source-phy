using System.Collections.Generic;
using UnityEngine;

public class WaterWave
{
	private float size = 0.01f;

	private float accumulator;

	private List<WaterNode> affectedNodes;

	public Vector2 origin;

	public bool firstTime = true;

	public float forceMultiplier = 1f;

	private readonly float waveSpeed = 4f;

	private readonly float waveLifeTime = 1f;

	public WaterWave(float force)
	{
		affectedNodes = new List<WaterNode>();
	}

	public void Update(float dt)
	{
		size += dt * waveSpeed;
		accumulator += dt;
	}

	public bool HasAffectedNode(WaterNode node)
	{
		return affectedNodes.Contains(node);
	}

	public void AddAffectedNode(WaterNode node)
	{
		affectedNodes.Add(node);
		firstTime = false;
	}

	public float GetSize()
	{
		return size;
	}

	public float GetForce()
	{
		return (1f - accumulator / waveLifeTime) * forceMultiplier;
	}

	public void ClearForce()
	{
		affectedNodes.Clear();
	}
}
