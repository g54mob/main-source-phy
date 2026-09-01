using UnityEngine;

public class FinishLineMultiple : MonoBehaviour
{
	public Transform[] objectsToSendFinishMessage;

	public string primaryGoalName = "null";

	public string subGoalName = "null";

	public bool hasTriggered;

	public bool getPrimaryFirst;

	public int primaryAmountToGet = 1;

	public int subAmountToGet = 1;

	private int primaryAmountGot;

	private int subAmountGot;

	private AudioSource myAudio;

	public bool addToObjectivesToWincondition;

	private void Start()
	{
		myAudio = base.transform.GetComponent<AudioSource>();
		myAudio.outputAudioMixerGroup = ReferenceMaster.GetMixer("SFX");
		if (addToObjectivesToWincondition && !StatMaster.levelSimulating)
		{
			WinCondition.Instance.objectiveObjectCount = primaryAmountToGet + subAmountToGet;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating && !hasTriggered && (bool)other.attachedRigidbody)
		{
			CheckOther(other);
		}
	}

	private void CheckOther(Collider other)
	{
		bool flag = other.attachedRigidbody.gameObject.name == primaryGoalName;
		bool flag2 = other.attachedRigidbody.gameObject.name == subGoalName;
		if (!flag && !flag2)
		{
			return;
		}
		bool flag3 = false;
		other.attachedRigidbody.gameObject.name += "_Completed";
		if (myAudio != null)
		{
			myAudio.Play();
		}
		if (flag)
		{
			if (primaryAmountGot < primaryAmountToGet)
			{
				WinCondition.currentObjsCompleted++;
				primaryAmountGot++;
				flag3 = true;
				getPrimaryFirst = false;
			}
		}
		else if (flag2 && !getPrimaryFirst && subAmountGot < subAmountToGet)
		{
			WinCondition.currentObjsCompleted++;
			subAmountGot++;
			flag3 = true;
		}
		if (flag3)
		{
			for (int i = 0; i < objectsToSendFinishMessage.Length; i++)
			{
				objectsToSendFinishMessage[i].GetComponent<FlashAlpha>().Flash(true);
			}
		}
	}
}
