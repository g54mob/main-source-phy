using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	public float power = 2f;

	public float duration = 0.35f;

	public float slowDownAmount = 1f;

	public bool shouldShake;

	[SerializeField]
	private Transform myCamera;

	private Vector3 startPosition;

	private float initialDuration;

	private bool acquiredInitialPosition;

	private void Start()
	{
		myCamera = Camera.main.transform;
		startPosition = myCamera.localPosition;
		initialDuration = duration;
	}

	private void Update()
	{
		if (shouldShake)
		{
			if (!acquiredInitialPosition)
			{
				startPosition = myCamera.localPosition;
				acquiredInitialPosition = true;
			}
			myCamera.GetComponent<MouseOrbit>().enabled = false;
			if (duration > 0f)
			{
				myCamera.localPosition = startPosition + Random.insideUnitSphere * power;
				duration -= Time.deltaTime * slowDownAmount;
				return;
			}
			shouldShake = false;
			duration = initialDuration;
			myCamera.localPosition = startPosition;
			myCamera.GetComponent<MouseOrbit>().enabled = true;
		}
	}
}
