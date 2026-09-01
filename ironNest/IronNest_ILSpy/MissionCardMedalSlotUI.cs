using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionCardMedalSlotUI : MonoBehaviour
{
	public MedalCategoryDefinition category;

	public Image targetRenderer;

	public TMP_Text Text_MedalTitle;

	public TMP_Text Text_MedalHint;

	private MedalTier currentTier;

	private void Start()
	{
		currentTier = MedalTier.Unearned;
		if (targetRenderer == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Image image = default(Image);
			targetRenderer = image;
		}
		if (Text_MedalTitle != null)
		{
			Text_MedalTitle.enabled = false;
		}
		if (Text_MedalHint != null)
		{
			Text_MedalHint.enabled = false;
		}
	}

	public unsafe void ClearRuntimeTier()
	{
		//IL_0077: Expected I4, but got O
		//IL_008b: Expected O, but got I4
		//IL_00cd: Expected O, but got Ref
		currentTier = MedalTier.Unearned;
		if (targetRenderer != null)
		{
			bool flag = category != null;
			MedalTier medalTier = MedalTier.Unearned;
			if (flag)
			{
				Sprite spriteForTier = category.GetSpriteForTier(currentTier);
				medalTier = (MedalTier)spriteForTier;
			}
			targetRenderer.sprite = (Sprite)medalTier;
			if (category == null)
			{
			}
			object obj = default(object);
			targetRenderer.color = (Color)(&obj);
		}
	}

	public unsafe void SetTier(MedalTier tier)
	{
		//IL_00d0: Expected O, but got Ref
		currentTier = tier;
		if (targetRenderer != null)
		{
			Sprite sprite = ((!(category == null)) ? category.GetSpriteForTier(currentTier) : null);
			targetRenderer.sprite = sprite;
			if (category == null)
			{
			}
			object obj = default(object);
			targetRenderer.color = (Color)(&obj);
		}
	}

	public unsafe void UpdateUI()
	{
		//IL_00c6: Expected O, but got Ref
		if (targetRenderer != null)
		{
			Sprite sprite = ((!(category == null)) ? category.GetSpriteForTier(currentTier) : null);
			targetRenderer.sprite = sprite;
			if (category == null)
			{
			}
			object obj = default(object);
			targetRenderer.color = (Color)(&obj);
		}
	}

	public void OnPointerEnter()
	{
		if (Text_MedalTitle != null)
		{
			MedalCategoryDefinition medalCategoryDefinition = category;
			string text = medalCategoryDefinition.displayNameV2.Get();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			Text_MedalTitle.enabled = true;
		}
		if (Text_MedalHint != null)
		{
			MedalCategoryDefinition medalCategoryDefinition2 = category;
			string text2 = medalCategoryDefinition2.hintTextV2.Get();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			Text_MedalHint.enabled = true;
		}
	}

	public void OnPointerExit()
	{
		if (Text_MedalTitle != null)
		{
			Text_MedalTitle.enabled = false;
		}
		if (Text_MedalHint != null)
		{
			Text_MedalHint.enabled = false;
		}
	}
}
