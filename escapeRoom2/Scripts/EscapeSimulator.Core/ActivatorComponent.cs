using System.Collections.Generic;
using UnityEngine;

public class ActivatorComponent : MonoBehaviour
{
	public enum ActivatorType
	{
		Disable = 0,
		Enable = 1,
		Toggle = 2
	}

	public class DisabledColliderMarker : MonoBehaviour
	{
	}

	public class DisabledObstacleMarker : MonoBehaviour
	{
	}

	public class DisabledTargetableMarker : MonoBehaviour
	{
	}

	public bool activeOnStart;

	public List<GameObject> keys = new List<GameObject>();

	public ActivatorType type;

	public bool targetObject;

	public bool targetRenderer;

	public bool targetCollider;

	public bool targetObstacle;

	public bool targetTargetable;
}
