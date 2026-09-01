using Cpp2ILInjected;
using UnityEngine;

public class MissionCardMedalSlot : MonoBehaviour
{
	public MedalCategoryDefinition category;

	public SpriteRenderer targetRenderer;

	public MedalTier testTier;

	public bool autoRefresh = true;

	private bool _hasRuntimeTier;

	private MedalTier _runtimeTier;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		SpriteRenderer spriteRenderer = default(SpriteRenderer);
		targetRenderer = spriteRenderer;
		testTier = MedalTier.Unearned;
		autoRefresh = true;
	}

	private void OnEnable()
	{
		if (autoRefresh)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x18042A9A0\"");
		}
	}

	public void SetTier(MedalTier tier)
	{
		_runtimeTier = tier;
		_hasRuntimeTier = true;
		Refresh();
	}

	public void ClearRuntimeTier()
	{
		_hasRuntimeTier = false;
		_runtimeTier = MedalTier.Unearned;
		Refresh();
	}

	public unsafe void Refresh()
	{
		//IL_0063: Expected O, but got I4
		//IL_007a: Expected O, but got I4
		//IL_00c2: Expected O, but got Ref
		if (!(targetRenderer != null))
		{
			return;
		}
		if (category != null)
		{
			bool flag = _hasRuntimeTier;
			object obj = 56;
			if (!flag)
			{
				obj = 48;
			}
			MedalCategoryDefinition medalCategoryDefinition = category;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rdx_v7+this @ rcx (MissionCardMedalSlot)]");
			Sprite spriteForTier = medalCategoryDefinition.GetSpriteForTier(MedalTier.Unearned);
			targetRenderer.sprite = spriteForTier;
			object obj2 = default(object);
			targetRenderer.color = (Color)(&obj2);
		}
		else
		{
			targetRenderer.sprite = null;
		}
	}
}
