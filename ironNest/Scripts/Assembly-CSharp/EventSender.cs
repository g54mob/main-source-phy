using UnityEngine;

public class EventSender : MonoBehaviour
{
	[Tooltip("Unity tag that identifies which EventReceiver objects should be notified.\nAll active EventReceivers in ALL loaded scenes whose GameObject.tag matches\nthis value will have their response event invoked.\n\nExample: \"Player\", \"AudioManager\", \"UIController\"")]
	public string targetTag;

	public void Send()
	{
	}
}
