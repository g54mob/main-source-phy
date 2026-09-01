using System;
using Cpp2ILInjected;
using UnityEngine;

public class LinearSliderInteractableColliderHelper : MonoBehaviour
{
	public LinearSliderInteractable parentSlider;

	private void OnMouseDown()
	{
		if (parentSlider != null)
		{
			LinearSliderInteractable linearSliderInteractable = parentSlider;
			if (linearSliderInteractable.useLegacyMouseCallbacks)
			{
				linearSliderInteractable.BeginSliderDrag();
			}
		}
	}

	private void OnMouseUp()
	{
		if (!(parentSlider != null))
		{
			return;
		}
		LinearSliderInteractable linearSliderInteractable = parentSlider;
		if (linearSliderInteractable.useLegacyMouseCallbacks && linearSliderInteractable.isDragging)
		{
			linearSliderInteractable.isDragging = false;
			linearSliderInteractable.ReleaseBrokerDragLockIfHeld();
			if (linearSliderInteractable.OnRelease != null)
			{
				linearSliderInteractable.OnRelease.Invoke();
			}
			Action onEndSliderDrag = linearSliderInteractable.OnEndSliderDrag;
			if (linearSliderInteractable.OnEndSliderDrag != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v77.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}
}
