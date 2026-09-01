using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class BallDemoWall : MonoBehaviour
{
	protected RectTransform _rectTransform;

	protected BoxCollider2D _boxCollider2D;

	protected virtual void OnEnable()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		_rectTransform = rectTransform;
		GameObject gameObject2 = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		BoxCollider2D boxCollider2D = default(BoxCollider2D);
		_boxCollider2D = boxCollider2D;
		Rect rect = _rectTransform.rect;
		Rect rect2 = _rectTransform.rect;
		Vector2 size = default(Vector2);
		_boxCollider2D.size = size;
	}
}
