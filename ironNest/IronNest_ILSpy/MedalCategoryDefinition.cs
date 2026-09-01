using Cpp2ILInjected;
using Localisation;
using UnityEngine;

public class MedalCategoryDefinition : ScriptableObject
{
	public string id;

	public TextIdentifier displayNameV2;

	public TextIdentifier hintTextV2;

	public Sprite unearnedSprite;

	public Color worldTint;

	public Sprite bronzeSprite;

	public MedalConditionSet BronzeConditions;

	public Sprite silverSprite;

	public MedalConditionSet SilverConditions;

	public Sprite goldSprite;

	public MedalConditionSet GoldConditions;

	public Sprite GetSpriteForTier(MedalTier tier)
	{
		//IL_00ca: Expected O, but got I4
		//IL_00a9: Expected O, but got I4
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0102: Expected O, but got I
		//IL_0086: Expected O, but got I4
		//IL_00b7: Expected O, but got I4
		//IL_0063: Expected O, but got I4
		object obj = tier - 1;
		bool flag = tier == MedalTier.Bronze;
		bool flag2;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 != 1)
				{
					return unearnedSprite;
				}
				flag2 = goldSprite != null;
				object obj3 = 104;
			}
			else
			{
				flag2 = silverSprite != null;
				object obj3 = 88;
			}
		}
		else
		{
			flag2 = bronzeSprite != null;
			object obj3 = 72;
		}
		if (!flag2)
		{
			object obj3 = 48;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rcx_v3+this @ rcx (MedalCategoryDefinition)]");
		return (Sprite)0;
	}

	public string GetIdSafe()
	{
		if (!string.IsNullOrWhiteSpace(id))
		{
			return id;
		}
		return base.name;
	}

	public MedalCategoryDefinition()
	{
		//IL_0072: Expected O, but got I
		id = "";
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		worldTint = (Color)0;
		MedalConditionSet medalConditionSet = new MedalConditionSet();
		MedalConditionSet.ConditionPair[] conditions = new MedalConditionSet.ConditionPair[0];
		medalConditionSet.Conditions = conditions;
		BronzeConditions = medalConditionSet;
		MedalConditionSet medalConditionSet2 = new MedalConditionSet();
		MedalConditionSet.ConditionPair[] conditions2 = new MedalConditionSet.ConditionPair[0];
		medalConditionSet2.Conditions = conditions2;
		SilverConditions = medalConditionSet2;
		MedalConditionSet medalConditionSet3 = new MedalConditionSet();
		MedalConditionSet.ConditionPair[] conditions3 = new MedalConditionSet.ConditionPair[0];
		medalConditionSet3.Conditions = conditions3;
		GoldConditions = medalConditionSet3;
		base._002Ector();
	}
}
