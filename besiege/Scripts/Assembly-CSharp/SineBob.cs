using UnityEngine;

public class SineBob : SimBehaviour
{
	public Transform myTransform;

	public float startHeight;

	public float bobSpeed = 2f;

	public float amplitude = 2f;

	public float timingOffset;

	public bool randomiseOffset = true;

	public bool onlyInSimulation = true;

	public bool useWorldUp;

	public float timeCounter;

	public bool useFixedUpdate;

	private float startVal;

	private Vector3 zero;

	private Vector3 p;

	private Vector3 v = Vector3.zero;

	protected override void Start()
	{
		base.Start();
		myTransform = base.transform;
		startHeight = ((!useWorldUp) ? myTransform.localPosition.y : myTransform.position.y);
		if (randomiseOffset)
		{
			timingOffset = Random.Range(0f, 10f);
		}
		startVal = startHeight + Mathf.Sin(timingOffset) * amplitude;
	}

	private void Update()
	{
		if (!useFixedUpdate)
		{
			Bob(Time.deltaTime);
		}
	}

	private void FixedUpdate()
	{
		if (useFixedUpdate)
		{
			Bob(Time.fixedDeltaTime);
		}
	}

	private void Bob(float delta)
	{
		if (!onlyInSimulation || (base.isSimulating && base.SimPhysics))
		{
			timeCounter += delta;
			p = ((!useWorldUp) ? myTransform.localPosition : myTransform.position);
			v = zero;
			if (bobSpeed > 0f)
			{
				v.x = p.x;
				v.y = startHeight + Mathf.Sin(timeCounter * bobSpeed + timingOffset) * amplitude;
				v.z = p.z;
			}
			else if (p.y != startVal)
			{
				v.x = p.x;
				v.y = startVal;
				v.z = p.z;
			}
			if (useWorldUp)
			{
				myTransform.position = v;
			}
			else
			{
				myTransform.localPosition = v;
			}
		}
	}
}
