using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class TriggerTagEventWatcher : MonoBehaviour
{
	[Header("Filter")]
	[Tooltip("Only colliders on GameObjects with this Tag will trigger the events.\nTip: Use Unity's Tag Manager to create tags, then assign them to the desired objects.")]
	[SerializeField]
	private string requiredTag;

	[Header("Events")]
	[Tooltip("Invoked when a collider with the Required Tag ENTERS this trigger.\nUse this for one-shot reactions (open door, start dialogue, etc.).")]
	[SerializeField]
	private UnityEvent onTaggedEnter;

	[Tooltip("Invoked when a collider with the Required Tag EXITS this trigger.\nUse this for cleanup or reversing an enter action.")]
	[SerializeField]
	private UnityEvent onTaggedExit;

	[Tooltip("Invoked every physics step (FixedUpdate) while a collider with the Required Tag STAYS inside this trigger.\nUse sparingly; this can fire frequently.")]
	[SerializeField]
	private UnityEvent onTaggedStay;

	private void Reset()
	{
	}

	private void OnValidate()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}
}
