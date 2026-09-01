using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class TriggerTagEventWatcher : MonoBehaviour
{
	private string requiredTag;

	private UnityEvent onTaggedEnter;

	private UnityEvent onTaggedExit;

	private UnityEvent onTaggedStay;

	private void Reset()
	{
		if (!TryGetComponent<Collider>(out var _))
		{
			GameObject gameObject = base.gameObject;
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		}
	}

	private void OnValidate()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A03B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (string.IsNullOrWhiteSpace(requiredTag))
		{
			requiredTag = "Untagged";
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(requiredTag) && onTaggedEnter != null)
		{
			onTaggedEnter.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(requiredTag) && onTaggedExit != null)
		{
			onTaggedExit.Invoke();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(requiredTag) && onTaggedStay != null)
		{
			onTaggedStay.Invoke();
		}
	}

	public TriggerTagEventWatcher()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A03C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		requiredTag = "Player";
		base._002Ector();
	}
}
