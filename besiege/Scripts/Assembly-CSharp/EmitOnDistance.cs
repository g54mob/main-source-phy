using UnityEngine;

public class EmitOnDistance : MonoBehaviour
{
	public float distance = 100f;

	public float dot;

	public ParticleSystem toDisable;

	private float sqrDistance;

	private Transform cam;

	private bool col;

	private bool visible = true;

	private void Awake()
	{
		sqrDistance = distance * distance;
		cam = Camera.main.transform;
		col = toDisable.collision.enabled;
	}

	private void LateUpdate()
	{
		ParticleSystem.EmissionModule emission = toDisable.emission;
		ParticleSystem.CollisionModule collision = toDisable.collision;
		if (visible && (cam.position - base.transform.position).sqrMagnitude < sqrDistance && Vector3.Dot(-cam.forward, base.transform.up) > dot)
		{
			if (!emission.enabled)
			{
				emission.enabled = true;
				collision.enabled = col;
			}
		}
		else if (emission.enabled)
		{
			emission.enabled = false;
			collision.enabled = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		DebugExtension.DebugWireSphere(base.transform.position, toDisable.emission.enabled ? ((Color.yellow + Color.cyan) * 0.5f) : ((!visible) ? Color.red : Color.yellow), distance, 0f);
	}

	private void OnBecameVisible()
	{
		visible = true;
	}

	private void OnBecameInvisible()
	{
		visible = false;
	}
}
