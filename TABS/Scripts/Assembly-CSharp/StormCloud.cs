using UnityEngine;
using UnityEngine.Events;

public class StormCloud : MonoBehaviour
{
	public LayerMask mask;

	public bool useSpherecast;

	public float radius;

	public float interval;

	public float chechRange;

	public float cloudRadius;

	public float stormHeight;

	public bool useLineEffect = true;

	private float counter;

	private LineEffects lineEffect;

	private Explosion expl;

	public Transform t1;

	public Transform t2;

	public ParticleSystem particle;

	public bool spawnKillboxesForParticles;

	public GameObject killbox;

	public UnityEvent hitEvent;

	private int colliderIndex;

	private void Start()
	{
		lineEffect = GetComponentInChildren<LineEffects>();
		expl = GetComponentInChildren<Explosion>();
	}

	private void Update()
	{
		counter += Time.deltaTime;
		if (counter > interval)
		{
			counter = Random.Range((0f - interval) * 0.5f, interval * 0.5f);
			Zap();
		}
	}

	private void Zap()
	{
		Vector2 vector = Random.insideUnitCircle * cloudRadius;
		Vector3 vector2 = base.transform.position + stormHeight * Vector3.up + vector.x * Vector3.forward + vector.y * Vector3.right;
		Ray ray = new Ray(vector2, Vector3.down);
		RaycastHit hitInfo;
		if (useSpherecast && radius > 0f)
		{
			Physics.SphereCast(ray, radius, out hitInfo, 50f + stormHeight, mask);
		}
		else
		{
			Physics.Raycast(ray, out hitInfo, 50f + stormHeight, mask);
		}
		if ((bool)hitInfo.transform)
		{
			float num = Vector3.Distance(hitInfo.point, vector2);
			t1.position = hitInfo.point + num * Vector3.up;
			t2.position = hitInfo.point;
			if (useLineEffect)
			{
				lineEffect.Play(t1, t2);
			}
			if ((bool)particle)
			{
				particle.Play();
			}
			if (spawnKillboxesForParticles)
			{
				GameObject gameObject = Object.Instantiate(position: new Vector3(t2.position.x, t2.position.y - 0.5f, t2.position.z), original: killbox, rotation: t2.rotation);
				particle.trigger.SetCollider(colliderIndex, gameObject.GetComponent<BoxCollider>());
				colliderIndex++;
			}
			expl.transform.position = hitInfo.point;
			expl.Explode();
			hitEvent.Invoke();
		}
	}
}
