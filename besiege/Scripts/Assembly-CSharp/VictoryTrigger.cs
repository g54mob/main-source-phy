using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
	public bool triggerEnter;

	public GameObject objectToCheck;

	public GameObject victoryParticles;

	private Vector3 startPos;

	private Vector3 endPos;

	private float currentLerpTime;

	public float lerpTime;

	public Transform door;

	public Transform endDoorPos;

	private float timer;

	private void Start()
	{
		startPos = door.position;
		endPos = endDoorPos.position;
		timer = lerpTime;
	}

	private void Update()
	{
		if (WinCondition.hasWon)
		{
			if (door.position != endPos)
			{
				MoveDoor();
			}
			if (timer <= lerpTime / 2f)
			{
				victoryParticles.SetActive(true);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (triggerEnter && other.attachedRigidbody.gameObject.name == objectToCheck.name)
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	public void MoveDoor()
	{
		currentLerpTime += Time.deltaTime;
		timer -= Time.deltaTime;
		if (currentLerpTime > lerpTime)
		{
			currentLerpTime = lerpTime;
		}
		float t = currentLerpTime / lerpTime;
		door.position = Vector3.Lerp(startPos, endPos, t);
	}
}
