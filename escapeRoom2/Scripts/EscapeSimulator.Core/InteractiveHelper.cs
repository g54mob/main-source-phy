using UnityEngine;

public class InteractiveHelper : MonoBehaviour
{
	public Interactive interactive;

	public Vector3 worldPos;

	public RectTransform canvasRect;

	public Camera refCamera;

	public RectTransform rectTransform;

	private void OnEnable()
	{
		rectTransform = GetComponent<RectTransform>();
	}
}
