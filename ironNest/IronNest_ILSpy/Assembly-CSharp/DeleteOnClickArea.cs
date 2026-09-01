using Cpp2ILInjected;
using UnityEngine;

public class DeleteOnClickArea : MonoBehaviour
{
	private Canvas parentCanvas;

	private Camera eventCamera;

	private RectTransform[] allRects;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		Canvas canvas = default(Canvas);
		parentCanvas = canvas;
		Camera camera = ((!(parentCanvas != null)) ? Camera.main : parentCanvas.worldCamera);
		eventCamera = camera;
		RectTransform[] componentsInChildren = GetComponentsInChildren<RectTransform>(includeInactive: true);
		allRects = componentsInChildren;
	}

	private void Update()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0036: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		//IL_0061: Expected O, but got F4
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		RectTransform[] array = allRects;
		object obj = allRects + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)obj, (Vector2)mousePosition.x, eventCamera))
			{
				obj2++;
				obj += 8;
				obj3 = obj2;
				continue;
			}
			GameObject obj4 = base.gameObject;
			Object.Destroy(obj4);
			break;
		}
	}
}
