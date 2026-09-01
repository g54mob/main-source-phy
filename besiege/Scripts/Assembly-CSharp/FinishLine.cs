using UnityEngine;

public class FinishLine : MonoBehaviour
{
	public Transform[] objectsToSendFinishMessage;

	public Transform[] objectsToSendFinishMessage2;

	public string nameToCheck = "null";

	public bool hasTriggered;

	public int amountToGet = 1;

	private int amountGot;

	private AudioSource myAudio;

	private void Start()
	{
		myAudio = base.transform.GetComponent<AudioSource>();
		if (StatMaster.isMP || myAudio != null)
		{
			myAudio.outputAudioMixerGroup = ReferenceMaster.GetMixer("SFX");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating && !hasTriggered && (bool)other.attachedRigidbody)
		{
			if (nameToCheck == "null")
			{
				CheckStartBlock(other);
			}
			else
			{
				CheckOther(other);
			}
		}
	}

	private void CheckStartBlock(Collider other)
	{
		BlockBehaviour component = other.attachedRigidbody.GetComponent<BlockBehaviour>();
		if (!object.ReferenceEquals(component, null) && component.Prefab.Type == BlockType.StartingBlock)
		{
			if (myAudio != null)
			{
				myAudio.Play();
			}
			WinCondition.currentObjsCompleted++;
			for (int i = 0; i < objectsToSendFinishMessage.Length; i++)
			{
				objectsToSendFinishMessage[i].gameObject.SendMessage("FinishLine");
			}
			hasTriggered = true;
		}
	}

	private void CheckOther(Collider other)
	{
		if (other.attachedRigidbody.gameObject.name != nameToCheck)
		{
			return;
		}
		other.attachedRigidbody.gameObject.name += "_Completed";
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
