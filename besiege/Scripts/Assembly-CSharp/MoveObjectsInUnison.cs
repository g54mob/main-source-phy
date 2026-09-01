using UnityEngine;

public class MoveObjectsInUnison : MonoBehaviour
{
	public Rigidbody[] bodies = new Rigidbody[0];

	private BasicInfo[] infos = new BasicInfo[0];

	public float effect = 0.5f;

	public float radiusSqr = 150f;

	public bool moveByProximity = true;

	private Vector3 lastCenter = Vector3.zero;

	public LayerMask m;

	private void Start()
	{
		if (bodies.Length == 0)
		{
			base.enabled = false;
			return;
		}
		lastCenter = Vector3.zero;
		infos = new BasicInfo[bodies.Length];
		for (int i = 0; i < bodies.Length; i++)
		{
			bodies[i].sleepThreshold = 0.05f;
			lastCenter += bodies[i].worldCenterOfMass;
			infos[i] = bodies[i].GetComponent<BasicInfo>();
		}
		lastCenter /= (float)bodies.Length;
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			if (moveByProximity)
			{
				MoveProximates();
			}
			else
			{
				MoveAll();
			}
		}
	}

	private void MoveAll()
	{
		int num = 0;
		float e = 0f;
		Vector3 p = Vector3.zero;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		bool[] array = new bool[bodies.Length];
		for (int i = 0; i < bodies.Length; i++)
		{
			if (!CheckProximity(bodies[i], ref p, ref e))
			{
				array[i] = true;
				continue;
			}
			Vector3 velocity = bodies[i].velocity;
			zero += velocity * e;
			zero2 += p;
			num++;
		}
		zero /= (float)num;
		zero2 /= (float)num;
		if (float.IsNaN(zero.x))
		{
			Debug.LogError("sum is NaN");
			return;
		}
		for (int j = 0; j < bodies.Length; j++)
		{
			if (!array[j])
			{
				Vector3 velocity = bodies[j].velocity;
				velocity = zero - velocity;
				velocity.y = 0f;
				if (velocity.sqrMagnitude > 0f)
				{
					bodies[j].AddForce(velocity * effect, ForceMode.VelocityChange);
				}
			}
		}
		lastCenter = zero2;
	}

	private bool CheckProximity(Rigidbody b, ref Vector3 p, ref float e)
	{
		if (b.name.Contains("Completed"))
		{
			return false;
		}
		p = b.worldCenterOfMass;
		e = (p - lastCenter).sqrMagnitude;
		if (e > radiusSqr)
		{
			return false;
		}
		e = 1f;
		return true;
	}

	private void MoveProximates()
	{
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < bodies.Length; i++)
		{
			if (infos[i].grabbed)
			{
				continue;
			}
			int num = 0;
			Vector3 vector = zero;
			Rigidbody rigidbody = bodies[i];
			Vector3 worldCenterOfMass = rigidbody.worldCenterOfMass;
			for (int j = 0; j < bodies.Length; j++)
			{
				Rigidbody rigidbody2 = bodies[j];
				Vector3 worldCenterOfMass2 = rigidbody2.worldCenterOfMass;
				if ((worldCenterOfMass2 - worldCenterOfMass).sqrMagnitude < 25f && !infos[j].grabbed && !rigidbody2.name.Contains("Completed"))
				{
					Vector3 velocity = rigidbody2.velocity;
					vector += velocity;
					num++;
				}
			}
			if (vector.sqrMagnitude > 0f)
			{
				vector /= (float)num;
				Vector3 velocity = rigidbody.velocity;
				velocity = vector - velocity;
				velocity.y = 0f;
				rigidbody.AddForce(velocity * effect, ForceMode.VelocityChange);
			}
		}
	}
}
