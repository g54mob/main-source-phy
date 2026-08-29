using UnityEngine;

public class ToolContext
{
	public ToolState state;

	public Interactive target;

	public Interactive currentTarget;

	public Ray ray;

	public Ray currentRay;

	public Vector3 targetHitPoint;

	public Vector3 targetHitNormal;

	public Vector3 currentTargetHitPoint;

	public Vector3 currentTargetHitNormal;

	public float deltaTime;
}
