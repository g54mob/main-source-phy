using UnityEngine;

public class DialInteractableColliderHelper : MonoBehaviour
{
	public DialInteractable parentDial;

	private void OnMouseDown()
	{
		if (parentDial != null)
		{
			DialInteractable dialInteractable = parentDial;
			if (dialInteractable.useLegacyMouseCallbacks)
			{
				dialInteractable.BeginDialDrag();
			}
		}
	}

	private void OnMouseUp()
	{
		if (parentDial != null)
		{
			DialInteractable dialInteractable = parentDial;
			if (dialInteractable.useLegacyMouseCallbacks)
			{
				dialInteractable.EndDialDrag();
			}
		}
	}
}
