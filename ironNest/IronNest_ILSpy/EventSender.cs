using Cpp2ILInjected;
using UnityEngine;

public class EventSender : MonoBehaviour
{
	public string targetTag;

	public void Send()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_0046: Expected O, but got I4
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		if (!string.IsNullOrEmpty(targetTag))
		{
			EventReceiver[] array = Object.FindObjectsByType<EventReceiver>(FindObjectsSortMode.None);
			object obj = array + 32;
			object obj2 = 0;
			object obj3 = 0;
			object obj4 = 0;
			while ((nint)obj4 < array.Length)
			{
				if (((Component)obj).CompareTag(targetTag))
				{
					((EventReceiver)obj).Receive();
					obj3++;
				}
				obj2++;
				obj += 8;
				obj4 = obj2;
			}
			if (obj3 == null)
			{
				string text = base.name;
				string message = "[EventSender] '" + text + "': No active EventReceivers found with tag '" + targetTag + "'.";
				Debug.LogWarning(message, this);
			}
		}
		else
		{
			string text2 = base.name;
			string message2 = "[EventSender] '" + text2 + "': targetTag is empty — no receivers notified.";
			Debug.LogWarning(message2, this);
		}
	}

	public EventSender()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39FBD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		targetTag = "Untagged";
		base._002Ector();
	}
}
