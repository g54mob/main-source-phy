using System.Collections.Generic;
using UnityEngine;

public class TombController : MonoBehaviour
{
	public List<GameObject> childObjects;

	public GameObject parent;

	public Transform door;

	public GameObject payoffCheck;

	private float moveStep;

	public float moveDuration;

	public PlaySoundOnMovement soundController;

	[HideInInspector]
	public int victoryCount;

	public float currentLerpTime;

	private void Start()
	{
		foreach (Transform item in parent.transform)
		{
			childObjects.Add(item.gameObject);
			BreakOnForceNoScaling componentInChildren = item.GetComponentInChildren<BreakOnForceNoScaling>();
			if ((bool)componentInChildren)
			{
				componentInChildren.enabled = false;
			}
		}
		currentLerpTime = 0f;
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		foreach (Transform item in parent.transform)
		{
			Rigidbody componentInChildren = item.GetComponentInChildren<Rigidbody>();
			if ((bool)componentInChildren)
			{
				componentInChildren.isKinematic = true;
			}
		}
		if (!payoffCheck.activeInHierarchy)
		{
			victoryCount++;
		}
		switch (victoryCount)
		{
		case 1:
			if (!(door.position.y >= 1f))
			{
				if ((double)door.position.y > 0.9)
				{
					soundController.moving = false;
				}
				else if (!soundController.moving)
				{
					soundController.moving = true;
				}
				currentLerpTime += Time.deltaTime;
				moveStep = currentLerpTime / moveDuration;
				door.position = Vector3.Lerp(door.position, new Vector3(door.position.x, 1f, door.position.z), moveStep);
			}
			break;
		case 2:
		{
			if ((double)door.position.y <= -13.9)
			{
				soundController.moving = false;
				break;
			}
			if (!soundController.moving)
			{
				soundController.moving = true;
			}
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > moveDuration)
			{
				currentLerpTime = moveDuration;
			}
			moveStep = currentLerpTime / moveDuration;
			Vector3 b = new Vector3(door.position.x, -14f, door.position.z);
			door.position = Vector3.Lerp(door.position, b, moveStep);
			break;
		}
		case 3:
			WinCondition.currentObjsCompleted++;
			{
				foreach (GameObject childObject in childObjects)
				{
					childObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
					childObject.GetComponentInChildren<BreakOnForceNoScaling>().enabled = true;
					childObject.GetComponentInChildren<BreakOnForceNoScaling>().Break();
				}
				break;
			}
		}
	}
}
