using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	private string promptText = "Interact with {objectName}";

	private bool autoReplaceObjectName = true;

	private Texture2D cursorOverride;

	private Texture2D cursorGrabOverride;

	private bool isInteractable = true;

	private bool isPassive;

	private bool restrictToAllowedColliders;

	private List<Collider> allowedColliders;

	private bool populateFromChildrenOneShot;

	public bool IsInteractable => isInteractable;

	public bool IsPassive => isPassive;

	public void SetInteractable(bool value)
	{
		isInteractable = value;
	}

	public string GetResolvedPrompt()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A95C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = !autoReplaceObjectName;
		string text = promptText;
		if (!flag && !string.IsNullOrEmpty(promptText))
		{
			GameObject gameObject = base.gameObject;
			if ((object)gameObject != null)
			{
				string newValue = gameObject.name;
				if (promptText != null)
				{
					string text2 = promptText.Replace("{objectName}", newValue);
					text = text2;
					goto IL_011c;
				}
			}
			goto IL_00e5;
		}
		goto IL_011c;
		IL_00e5:
		return (string)(object)new NullReferenceException();
		IL_011c:
		if (text != null)
		{
			return text.Replace("{prompt}", promptText);
		}
		goto IL_00e5;
	}

	public unsafe bool TryGetCursor(out Texture2D texture, out Vector2 hotspot)
	{
		//IL_005a: Expected I, but got O
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		ref Vector2 reference = ref *(Vector2*)Vector2.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		ref Texture2D reference2;
		if (isInteractable && !(cursorOverride == null))
		{
			reference2 = ref *(Texture2D*)cursorOverride;
			return true;
		}
		reference2 = ref *(Texture2D*)null;
		return false;
	}

	public unsafe bool TryGetGrabCursor(out Texture2D texture, out Vector2 hotspot)
	{
		//IL_005a: Expected I, but got O
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		ref Vector2 reference = ref *(Vector2*)Vector2.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		ref Texture2D reference2;
		if (isInteractable && !(cursorGrabOverride == null))
		{
			reference2 = ref *(Texture2D*)cursorGrabOverride;
			return true;
		}
		reference2 = ref *(Texture2D*)null;
		return false;
	}

	public bool MatchesHitCollider(Collider hitCollider)
	{
		if (isInteractable)
		{
			if (!restrictToAllowedColliders)
			{
				return true;
			}
			if (hitCollider != null && allowedColliders != null)
			{
				List<Collider> list = allowedColliders;
				if (list._size != 0)
				{
					return list.Contains(hitCollider);
				}
			}
		}
		return false;
	}

	public Interactable()
	{
		List<Collider> list = new List<Collider>();
		allowedColliders = list;
		base._002Ector();
	}
}
