using System;
using TFBGames;
using UnityEngine;

public class ProjectileStick : MonoBehaviour, GameObjectPooling.IPoolable
{
	public float minWeight;

	public Transform target;

	[HideInInspector]
	public Rigidbody targetRig;

	public Transform centerOfMass;

	public bool hardStick;

	public float drag = 0.8f;

	public float spring = 1f;

	[HideInInspector]
	public bool stuck;

	private Vector3 lastPosition;

	private Vector3 rot = new Vector3(0f, 0f, 1f);

	private Vector3 pos;

	private Vector3 velocity;

	private Vector3 upVelocity;

	private float originalDrag;

	private float originalSpring;

	private Vector3 deltaPos;

	public Vector3 StickPoint => base.transform.TransformPoint(pos);

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		originalDrag = drag;
		originalSpring = spring;
		SettingsProfileManager service = ServiceLocator.GetService<SettingsProfileManager>();
		if (service?.CurrentSettingsProfile != null && service.CurrentSettingsProfile.UseHardProjectileStick)
		{
			hardStick = true;
		}
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void Update()
	{
		if (!target)
		{
			if (stuck)
			{
				if (IsManagedByPool)
				{
					ReleaseSelf?.Invoke();
				}
				else
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			return;
		}
		base.transform.position = target.TransformPoint(pos);
		if (hardStick)
		{
			base.transform.rotation = Quaternion.LookRotation(target.TransformDirection(rot));
			return;
		}
		velocity += Vector3.Angle(target.TransformDirection(rot), base.transform.forward) * -Vector3.Cross(target.TransformDirection(rot), base.transform.forward).normalized * spring * Time.deltaTime * 60f;
		velocity *= drag;
		base.transform.Rotate(velocity * 10f * Time.deltaTime, Space.World);
		Vector3 vector = target.TransformPoint(pos);
		deltaPos = vector - lastPosition;
		if (deltaPos != Vector3.zero)
		{
			Vector3 vector2 = Vector3.Cross(deltaPos, centerOfMass.position - base.transform.position).normalized * deltaPos.magnitude;
			velocity += vector2 * Time.deltaTime * 10000f;
		}
		lastPosition = vector;
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		stuck = false;
		target = null;
		targetRig = null;
		drag = originalDrag;
		spring = originalSpring;
	}

	public void Stick(Transform stickTarget, Vector3 stickRot, Vector3 stickPos, Rigidbody rig)
	{
		velocity = UnityEngine.Random.insideUnitSphere * 50f;
		target = stickTarget;
		rot = stickTarget.InverseTransformDirection(stickRot);
		pos = stickTarget.InverseTransformPoint(stickPos);
		lastPosition = target.TransformPoint(pos);
		stuck = true;
		if ((bool)rig)
		{
			targetRig = rig;
		}
	}

	private void InitializeOnSpawn()
	{
		drag += UnityEngine.Random.Range(-0.05f, 0f);
		spring += UnityEngine.Random.Range(0f, 0.5f);
	}
}
