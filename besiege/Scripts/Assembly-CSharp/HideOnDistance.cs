using UnityEngine;

public class HideOnDistance : MonoBehaviour
{
	public float distance = 100f;

	public Behaviour toDisable;

	private float sqrDistance;

	private Transform cam;

	private void Awake()
	{
		sqrDistance = distance * distance;
		cam = Camera.main.transform;
	}

	private void LateUpdate()
	{
		if ((cam.position - base.transform.position).sqrMagnitude < sqrDistance)
		{
			if (!toDisable.enabled)
			{
				toDisable.enabled = true;
			}
		}
		else if (toDisable.enabled)
		{
			toDisable.enabled = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		DebugExtension.DebugWireSphere(base.transform.position, (Color.yellow + Color.cyan) * 0.5f, distance, 0f);
	}
}
