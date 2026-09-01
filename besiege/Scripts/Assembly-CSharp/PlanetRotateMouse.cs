using System;
using UnityEngine;

public class PlanetRotateMouse : MonoBehaviour
{
	[Serializable]
	public class PlanetState
	{
		public int levelToUnlock = -1;

		public Vector3 euler;

		public bool IsUnlocked
		{
			get
			{
				return levelToUnlock != -1 && LEVELLORD.levelsComplete[levelToUnlock] == 1;
			}
		}
	}

	public float speed = 10f;

	public Vector3 startForce;

	public float startForceDuration = 2f;

	public Vector3 constantForcey;

	public bool usePlanetStates;

	public PlanetState Tolbrynd;

	public PlanetState Valfross;

	public PlanetState Krolmar;

	public PlanetState CampaignCleared;

	private float cTime;

	private Rigidbody rb;

	private Vector3 startEuler;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (usePlanetStates)
		{
			startEuler = base.transform.eulerAngles;
			if (CampaignCleared.IsUnlocked)
			{
				base.transform.eulerAngles = startEuler;
			}
			else if (Krolmar.IsUnlocked)
			{
				base.transform.eulerAngles = Krolmar.euler;
			}
			else if (Valfross.IsUnlocked)
			{
				base.transform.eulerAngles = Valfross.euler;
			}
			else if (Tolbrynd.IsUnlocked)
			{
				base.transform.eulerAngles = Tolbrynd.euler;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!StatMaster.inMenu && InputManager.RotateCameraKeyHeld())
		{
			rb.AddTorque(new Vector3(InputManager.MouseY(), 0f - InputManager.MouseX(), 0f) * speed * (Time.smoothDeltaTime * 125f) * (OptionsMaster.BesiegeConfig.CameraSensitivity / 100f));
		}
		rb.AddTorque(constantForcey);
		if (cTime < startForceDuration && startForce != Vector3.zero)
		{
			rb.AddTorque(startForce);
			cTime += Time.deltaTime;
		}
	}
}
