using UnityEngine;

public class FinishLineTag : MonoBehaviour
{
	public Transform[] objectsToSendFinishMessage;

	public Transform[] objectsToSendFinishMessage2;

	public string tagToCheck = string.Empty;

	public bool hasTriggered;

	public int amountToGet = 1;

	private int amountGot;

	public AudioSource myAudio;

	private void Start()
	{
		if (myAudio == null)
		{
			myAudio = base.transform.GetComponent<AudioSource>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating && !hasTriggered && (bool)other.attachedRigidbody)
		{
			CheckTag(other);
		}
	}

	private void CheckTag(Collider other)
	{
		GameObject gameObject = other.attachedRigidbody.gameObject;
		if (gameObject.CompareTag(tagToCheck))
		{
			return;
		}
		gameObject.tag += "_Completed";
		if (myAudio != null)
		{
			myAudio.Play();
		}
		WinCondition.currentObjsCompleted++;
		amountGot++;
		if (amountGot >= amountToGet)
		{
			hasTriggered = true;
			for (int i = 0; i < objectsToSendFinishMessage.Length; i++)
			{
				objectsToSendFinishMessage[i].GetComponent<FlashAlpha>().Flash();
			}
		}
		else
		{
			for (int j = 0; j < objectsToSendFinishMessage.Length; j++)
			{
				objectsToSendFinishMessage[j].GetComponent<FlashAlpha>().Flash(true);
			}
		}
	}
}
