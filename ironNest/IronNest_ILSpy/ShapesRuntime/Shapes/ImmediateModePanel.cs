using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ImmediateModePanel : MonoBehaviour
{
	private ImmediateModeCanvas imCanvas;

	private ImmediateModeCanvas ImCanvas
	{
		get
		{
			if (imCanvas != null)
			{
				return imCanvas;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			ImmediateModeCanvas result = default(ImmediateModeCanvas);
			imCanvas = result;
			return result;
		}
	}

	public bool Valid
	{
		get
		{
			ImmediateModeCanvas immediateModeCanvas = ImCanvas;
			return immediateModeCanvas != null;
		}
	}

	public virtual void OnEnable()
	{
		ImmediateModeCanvas immediateModeCanvas = ImCanvas;
		if (immediateModeCanvas == null)
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			string message = "ImmediateModePanel attached to " + text + " is missing an ImmediateModeCanvas component on its canvas";
			Debug.LogWarning(message, this);
		}
		else
		{
			ImmediateModeCanvas immediateModeCanvas2 = ImCanvas;
			immediateModeCanvas2.panels.Add(this);
		}
	}

	public virtual void OnDisable()
	{
		ImmediateModeCanvas immediateModeCanvas = ImCanvas;
		if (immediateModeCanvas != null)
		{
			ImmediateModeCanvas immediateModeCanvas2 = ImCanvas;
			bool flag = immediateModeCanvas2.panels.Remove(this);
		}
	}

	internal unsafe void DrawPanel(ImCanvasContext ctx)
	{
		//IL_005c: Expected O, but got Ref
		Transform transform = base.transform;
		bool flag = (object)transform.GetType() != typeof(RectTransform);
		RectTransform rectTransform = null;
		if (!flag)
		{
			rectTransform = (RectTransform)transform;
		}
		Rect rect = rectTransform.rect;
		object obj = default(object);
		DrawPanelShapes((Rect)(&obj), ctx);
	}

	public virtual void DrawPanelShapes(Rect rect, ImCanvasContext ctx)
	{
	}
}
