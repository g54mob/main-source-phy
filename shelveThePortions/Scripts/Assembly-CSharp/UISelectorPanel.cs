using UnityEngine;
using UnityEngine.Events;

public class UISelectorPanel : MonoBehaviour
{
	[SerializeField]
	private LocalizationTextActivator selectorText;

	[SerializeField]
	private UnityEvent OnPressNextEvent;

	[SerializeField]
	private UnityEvent OnPressBackEvent;

	public void SetSelectorText(string stringID, bool requireLocalization = true)
	{
		if (requireLocalization)
		{
			selectorText.UpdateText(stringID);
		}
		else
		{
			selectorText.SetText(stringID);
		}
	}

	public void OnPressNext()
	{
		if (OnPressNextEvent != null)
		{
			OnPressNextEvent.Invoke();
		}
	}

	public void OnPressBack()
	{
		if (OnPressBackEvent != null)
		{
			OnPressBackEvent.Invoke();
		}
	}
}
