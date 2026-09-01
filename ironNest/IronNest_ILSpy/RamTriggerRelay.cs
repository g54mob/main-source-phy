using Cpp2ILInjected;
using UnityEngine;

public class RamTriggerRelay : MonoBehaviour
{
	private Collider ramTrigger;

	private RamFakeParentConstraintController controller;

	private ColliderUnityEvent onRamTriggerEnter = new ColliderUnityEvent();

	private ColliderUnityEvent onRamTriggerExit = new ColliderUnityEvent();

	public ColliderUnityEvent OnRamTriggerEnter => onRamTriggerEnter;

	public ColliderUnityEvent OnRamTriggerExit => onRamTriggerExit;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Collider collider = default(Collider);
		ramTrigger = collider;
	}

	private void OnValidate()
	{
		if (ramTrigger == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Collider collider = default(Collider);
			ramTrigger = collider;
		}
		if (ramTrigger != null && !ramTrigger.isTrigger)
		{
			string text = base.name;
			string message = "RamTriggerRelay on '" + text + "': ramTrigger is not marked 'Is Trigger'. Trigger events will not fire.";
			Debug.LogWarning(message, this);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!(controller != null) || controller.NotifyRamTriggerEnter(other, ramTrigger))
		{
			onRamTriggerEnter.Invoke(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!(controller != null) || controller.NotifyRamTriggerExit(other, ramTrigger))
		{
			onRamTriggerExit.Invoke(other);
		}
	}
}
