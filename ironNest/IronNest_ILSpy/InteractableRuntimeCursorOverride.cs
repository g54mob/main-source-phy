using Cpp2ILInjected;
using UnityEngine;

public class InteractableRuntimeCursorOverride : MonoBehaviour
{
	private Interactable targetInteractable;

	private bool autoFindInteractable = true;

	private bool hasHoverOverride;

	private Texture2D hoverOverrideTexture;

	private bool hasGrabOverride;

	private Texture2D grabOverrideTexture;

	private bool debugLogs;

	public Interactable TargetInteractable => targetInteractable;

	public bool HasHoverOverride
	{
		get
		{
			if (!hasHoverOverride)
			{
				return false;
			}
			return hoverOverrideTexture != null;
		}
	}

	public bool HasGrabOverride
	{
		get
		{
			if (!hasGrabOverride)
			{
				return false;
			}
			return grabOverrideTexture != null;
		}
	}

	private void Awake()
	{
		if (targetInteractable == null && autoFindInteractable)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Interactable interactable = default(Interactable);
			targetInteractable = interactable;
		}
	}

	public unsafe bool TryGetHoverOverride(out Texture2D texture)
	{
		ref Texture2D reference;
		if (hasHoverOverride && hoverOverrideTexture != null)
		{
			reference = ref *(Texture2D*)hoverOverrideTexture;
			return true;
		}
		reference = ref *(Texture2D*)null;
		return false;
	}

	public unsafe bool TryGetGrabOverride(out Texture2D texture)
	{
		ref Texture2D reference;
		if (hasGrabOverride && grabOverrideTexture != null)
		{
			reference = ref *(Texture2D*)grabOverrideTexture;
			return true;
		}
		reference = ref *(Texture2D*)null;
		return false;
	}

	public void SetHoverOverride(Texture2D texture)
	{
		hoverOverrideTexture = texture;
		bool flag = texture != null;
		bool flag2 = !debugLogs;
		hasHoverOverride = flag;
		if (!flag2)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg2 = ((!(texture != null)) ? "<null>" : texture.name);
			object arg3 = default(object);
			string message = $"[InteractableRuntimeCursorOverride:{arg}] HoverOverride set. Active={arg3} Tex={arg2}";
			Debug.Log(message, this);
		}
	}

	public void ClearHoverOverride()
	{
		hasHoverOverride = false;
		hoverOverrideTexture = null;
		if (debugLogs)
		{
			string text = base.name;
			string message = "[InteractableRuntimeCursorOverride:" + text + "] HoverOverride cleared.";
			Debug.Log(message, this);
		}
	}

	public void SetGrabOverride(Texture2D texture)
	{
		grabOverrideTexture = texture;
		bool flag = texture != null;
		bool flag2 = !debugLogs;
		hasGrabOverride = flag;
		if (!flag2)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string arg2 = ((!(texture != null)) ? "<null>" : texture.name);
			object arg3 = default(object);
			string message = $"[InteractableRuntimeCursorOverride:{arg}] GrabOverride set. Active={arg3} Tex={arg2}";
			Debug.Log(message, this);
		}
	}

	public void ClearGrabOverride()
	{
		hasGrabOverride = false;
		grabOverrideTexture = null;
		if (debugLogs)
		{
			string text = base.name;
			string message = "[InteractableRuntimeCursorOverride:" + text + "] GrabOverride cleared.";
			Debug.Log(message, this);
		}
	}

	public void ClearAll()
	{
		ClearHoverOverride();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 11 Invalid \"Jump target not found in method: 0x1804445A0\"");
	}
}
