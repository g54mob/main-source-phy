using UnityEngine;

public class TombKeyTrigger : MonoBehaviour
{
	public Transform[] key;

	private void OnTriggerEnter(Collider other)
	{
		for (int i = 0; i < key.Length; i++)
		{
			if (other.attachedRigidbody.gameObject == key[i])
			{
				WinCondition.currentObjsCompleted++;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		for (int i = 0; i < key.Length; i++)
		{
			if (other.attachedRigidbody.gameObject == key[i])
			{
				WinCondition.currentObjsCompleted--;
				break;
			}
		}
	}
}
