using UnityEngine;

[AddComponentMenu("UI/UI Hover Area")]
public class UIHoverArea : MonoBehaviour
{
	public bool isMouseOver;

	private void OnMouseEnter()
	{
		isMouseOver = true;
	}

	private void OnMouseExit()
	{
		isMouseOver = false;
	}
}
