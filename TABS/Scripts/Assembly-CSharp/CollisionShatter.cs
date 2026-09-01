using System;
using UnityEngine;

public class CollisionShatter : CollisionWeaponEffect, GameObjectPooling.IPoolable
{
	public GameObject pieceParent;

	public float screenShake = 1f;

	private Rigidbody rig;

	private Vector3 lerpedVelocity;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
		lerpedVelocity = rig.velocity;
	}

	private void Update()
	{
		lerpedVelocity = Vector3.Lerp(lerpedVelocity, rig.velocity, Time.deltaTime * 15f);
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		Rigidbody[] componentsInChildren = pieceParent.GetComponentsInChildren<Rigidbody>(includeInactive: true);
		pieceParent.transform.position = rig.position;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Rigidbody rigidbody = UnityEngine.Object.Instantiate(componentsInChildren[i]);
			rigidbody.transform.position = componentsInChildren[i].transform.position;
			rigidbody.gameObject.AddComponent<RemoveAfterSeconds>().shrink = true;
			rigidbody.velocity = UnityEngine.Random.Range(0.5f, 1f) * lerpedVelocity;
			rigidbody.gameObject.AddComponent<SetInterpolation>();
		}
		ScreenShake.Instance.AddForce(base.transform.forward * screenShake, base.transform.position);
		InactivateGameObject(base.gameObject);
	}

	public void Initialize()
	{
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	private void InactivateGameObject(GameObject go)
	{
		if (ReleaseSelf != null)
		{
			ReleaseSelf();
		}
		else
		{
			go.SetActive(value: false);
		}
	}
}
