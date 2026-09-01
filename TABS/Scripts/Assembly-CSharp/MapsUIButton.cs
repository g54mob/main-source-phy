using System.Collections;
using Landfall.TABS;
using UnityEngine;

public class MapsUIButton : MonoBehaviour
{
	public UIMovementAnimation UIMovementAnimation;

	public PlacementUI PlacementUI;

	public MapSelectionCanvas mapSelection;

	private UIMapSelector uIMapSelector;

	private void Awake()
	{
		uIMapSelector = GetComponentInParent<UIMapSelector>();
		UIMovementAnimation.OnCompleteState02 += ToggleCanvas;
	}

	private void ToggleCanvas()
	{
		if (uIMapSelector != null)
		{
			uIMapSelector.SetToggleCanvas(state: false);
		}
	}

	public void OnClick()
	{
		if (UIMovementAnimation.m_state == UIMovementAnimation.State.State02)
		{
			UIMovementAnimation.SetState(UIMovementAnimation.State.State01);
			mapSelection.ShowMap();
			if (uIMapSelector != null)
			{
				uIMapSelector.SetToggleCanvas(state: true);
			}
		}
	}

	public void Close(bool noUIBackIn = false)
	{
		if (base.gameObject.activeInHierarchy)
		{
			StopAllCoroutines();
			StartCoroutine(DelayedHide(noUIBackIn));
		}
		mapSelection.HideMap();
	}

	private IEnumerator DelayedHide(bool noUIBackIn = false)
	{
		yield return new WaitForSeconds(0.05f);
		UIMovementAnimation.SetState(UIMovementAnimation.State.State02);
	}
}
