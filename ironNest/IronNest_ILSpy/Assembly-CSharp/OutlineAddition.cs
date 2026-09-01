using Cpp2ILInjected;
using UnityEngine;

public class OutlineAddition : MonoBehaviour
{
	private OutlineController controller;

	private Outline outline;

	private void OnEnable()
	{
		if (controller == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("OutlineController");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			OutlineController outlineController = default(OutlineController);
			controller = outlineController;
		}
		if (controller != null)
		{
			if (this.outline == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Outline outline = default(Outline);
				this.outline = outline;
			}
			if (this.outline != null)
			{
				controller.AddOutline(this.outline);
			}
		}
	}

	private void OnDisable()
	{
		if (controller == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("OutlineController");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			OutlineController outlineController = default(OutlineController);
			controller = outlineController;
		}
		if (controller != null)
		{
			if (this.outline == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Outline outline = default(Outline);
				this.outline = outline;
			}
			if (this.outline != null)
			{
				controller.RemoveOutline(this.outline);
			}
		}
	}
}
