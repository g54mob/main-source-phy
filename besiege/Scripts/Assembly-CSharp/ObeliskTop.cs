using UnityEngine;

public class ObeliskTop : MonoBehaviour
{
	public Animation anim;

	public Rigidbody body;

	public GameObject pillar;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void FixedUpdate()
	{
		if (pillar == null || !pillar.activeSelf)
		{
			Release();
			body.AddForce(Vector3.up * 500f);
			body.AddTorque(Random.insideUnitSphere * 500f);
		}
		else if ((base.transform.position - startPos).sqrMagnitude > 2f)
		{
			Release();
		}
	}

	private void Release()
	{
		anim.Stop();
		anim.enabled = false;
		body.useGravity = true;
		Object.Destroy(this);
	}
}
