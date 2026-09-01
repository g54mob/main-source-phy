using UnityEngine;
using UnityEngine.Events;

public class AddCollisionEventToUnit : MonoBehaviour
{
	public enum BodyTarget
	{
		Head = 0,
		RightFoot = 1,
		LeftFoot = 2,
		Torso = 3,
		Hip = 4,
		LeftHand = 5,
		RightHand = 6
	}

	public bool playOnStart = true;

	public BodyTarget bodyTarget;

	public CollisionEvent.AcceptedCollisionTargets acceptedCollisionTargets;

	public float impactMultiplier = 15f;

	public float cd;

	public UnityEvent eventToCall;

	private GameObject target;

	private void Start()
	{
		target = null;
		if (bodyTarget == BodyTarget.Head)
		{
			target = base.transform.root.GetComponentInChildren<Head>().gameObject;
		}
		else if (bodyTarget == BodyTarget.RightFoot)
		{
			target = base.transform.root.GetComponentInChildren<KneeRight>().gameObject;
		}
		else if (bodyTarget == BodyTarget.LeftFoot)
		{
			target = base.transform.root.GetComponentInChildren<KneeLeft>().gameObject;
		}
		else if (bodyTarget == BodyTarget.Torso)
		{
			target = base.transform.root.GetComponentInChildren<Torso>().gameObject;
		}
		else if (bodyTarget == BodyTarget.Hip)
		{
			target = base.transform.root.GetComponentInChildren<Hip>().gameObject;
		}
		else if (bodyTarget == BodyTarget.LeftHand)
		{
			target = base.transform.root.GetComponentInChildren<HandLeft>().gameObject;
		}
		else if (bodyTarget == BodyTarget.RightHand)
		{
			target = base.transform.root.GetComponentInChildren<HandRight>().gameObject;
		}
		if (playOnStart)
		{
			AddCollisionEvent();
		}
	}

	public void AddCollisionEvent()
	{
		CollisionEvent collisionEvent = target.AddComponent<CollisionEvent>();
		collisionEvent.cd = cd;
		collisionEvent.impactMultiplier = impactMultiplier;
		collisionEvent.acceptedCollisionTargets = acceptedCollisionTargets;
		collisionEvent.collisionEvent = eventToCall;
	}
}
