using System;
using UnityEngine;

[RequireComponent(typeof(DynamicText))]
public class FileBrowserPaginationButton : ClickBehaviour
{
	public Action<int> ButtonClicked;

	[SerializeField]
	private Color activeColor = Color.white;

	[SerializeField]
	private Color inactiveColor = Color.gray;

	private int buttonNumber;

	private bool currentPage;

	private DynamicText numberText;

	public void Initialize()
	{
		numberText = GetComponent<DynamicText>();
	}

	public void SetButtonNumber(int num, bool current)
	{
		buttonNumber = num;
		currentPage = current;
		numberText.color = ((!currentPage) ? inactiveColor : activeColor);
		if (buttonNumber >= 1)
		{
			numberText.textSB.Length = 0;
			numberText.textSB.Append(buttonNumber);
			numberText.FinishedTextSB();
		}
		else
		{
			numberText.SetText(string.Empty);
		}
		BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
		component.size = numberText.bounds.size;
		component.center = numberText.bounds.center;
	}

	public override void OnClicked()
	{
		if (buttonNumber >= 1 && !currentPage && ButtonClicked != null)
		{
			ButtonClicked(buttonNumber);
		}
	}
}
