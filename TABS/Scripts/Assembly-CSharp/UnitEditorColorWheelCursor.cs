using UnityEngine;

public class UnitEditorColorWheelCursor : MonoBehaviour
{
	public Transform rotationPart;

	public Transform cursorPart;

	public GameObject cross;

	public void SetCursorAngle(float angle, bool OnExitButton, bool onWheel)
	{
		float y = 44.5f;
		if (onWheel || OnExitButton)
		{
			rotationPart.rotation = Quaternion.Lerp(rotationPart.rotation, Quaternion.Euler(0f, 0f, angle + 180f), Time.unscaledDeltaTime * 20f);
		}
		if (OnExitButton || !onWheel)
		{
			y = 0f;
		}
		cursorPart.localPosition = Vector3.Lerp(cursorPart.localPosition, new Vector3(0f, y, 0f), 15f * Time.deltaTime);
		if (OnExitButton && cursorPart.localPosition.y < 5f)
		{
			cursorPart.gameObject.SetActive(value: false);
			cross.SetActive(value: true);
		}
		else
		{
			cursorPart.gameObject.SetActive(value: true);
			cross.SetActive(value: false);
		}
	}
}
