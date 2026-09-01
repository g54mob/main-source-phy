using System;
using UnityEngine;

public class AddForce : MonoBehaviour, GameObjectPooling.IPoolable
{
	public Vector3 force;

	public Vector3 worldForce;

	public Vector3 worldForce_Random;

	public Vector3 worldTorque;

	public Vector3 worldTorque_Random;

	public bool playOnStart = true;

	private Rigidbody[] rig;

	private bool m_initialized;

	private Vector3 originalAddForce;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Awake()
	{
		originalAddForce = force;
		rig = GetComponentsInChildren<Rigidbody>();
	}

	private void Start()
	{
		if (playOnStart)
		{
			Initialize();
		}
	}

	public void Initialize()
	{
		if (!m_initialized)
		{
			m_initialized = true;
			for (int i = 0; i < rig.Length; i++)
			{
				rig[i].AddForce(base.transform.TransformDirection(force), ForceMode.VelocityChange);
				Vector3 vector = new Vector3(UnityEngine.Random.Range(0f - worldForce_Random.x, worldForce_Random.x), UnityEngine.Random.Range(0f - worldForce_Random.y, worldForce_Random.y), UnityEngine.Random.Range(0f - worldForce_Random.z, worldForce_Random.z));
				rig[i].AddForce(worldForce + vector, ForceMode.VelocityChange);
				Vector3 vector2 = new Vector3(UnityEngine.Random.Range(0f - worldTorque_Random.x, worldTorque_Random.x), UnityEngine.Random.Range(0f - worldTorque_Random.y, worldTorque_Random.y), UnityEngine.Random.Range(0f - worldTorque_Random.z, worldTorque_Random.z));
				rig[i].AddTorque(worldTorque + vector2, ForceMode.VelocityChange);
			}
		}
	}

	public void Reset()
	{
		m_initialized = false;
		for (int i = 0; i < rig.Length; i++)
		{
			rig[i].velocity = Vector3.zero;
		}
		force = originalAddForce;
	}

	public void Release()
	{
	}
}
