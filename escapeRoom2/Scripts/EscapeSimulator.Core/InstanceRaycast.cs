using UnityEngine;

public class InstanceRaycast
{
	public int hitsPtr;

	public int[] hitCounts;

	public RaycastHit[][] hits;

	public PropInstance[] instances;

	public int instanceCount;

	public int lastInstanceHit;

	public Vector2 mouse;
}
