using System.Collections.Generic;

public class WorkshopTags
{
	public static readonly string MOD_TAG = "Mod";

	public static readonly string LEVEL_TAG = "Level";

	public static readonly string CAMPAIGN_TAG = "Campaign";

	public static readonly string FEATURED_TAG = "featured";

	public static readonly string ALLOWFEATURED_TAG = "allow-featured";

	public static readonly string AUTOPLAY_TAG = "autoplay";

	public static readonly string UNBREAKABLE_TAG = "unbreakable";

	public static readonly string REQUIRES_MODS = "requires-mods";

	public static readonly string HYDRAULICS_TAG = "hydraulics";

	public static readonly string HYDRAULIC_CONTROLLER_TAG = "hydraulic-controller";

	public static readonly string SPRINGS_TAG = "springs";

	public static readonly string BUILD_REGIONS_TAG = "build-regions";

	public static readonly string PREBUILDS_TAG = "prebuilds";

	public static readonly string CUSTOM_SHAPES_TAG = "custom-shapes";

	public static readonly string AFFECTS_GAMEPLAY_TAG = "affects-gamplay";

	public static readonly string LANGUAGE_TAG = "language";

	public static readonly string UGC_VEHICLES_TAG = "ugc-vehicles";

	public static readonly string UGC_BOATS_PLANES_TAG = "ugc-boats-planes";

	public static readonly string UGC_DECOR_TAG = "ugc-decor";

	public static readonly string UGC_CUSTOM_SHAPES_TAG = "ugc-custom-shapes";

	public static Dictionary<WorkshopTagType, WorkshopTagToggle> m_TagToggles = new Dictionary<WorkshopTagType, WorkshopTagToggle>();

	public static int GetNumActiveTags(WorkshopTagMode modeFilter)
	{
		int num = 0;
		foreach (KeyValuePair<WorkshopTagType, WorkshopTagToggle> tagToggle in m_TagToggles)
		{
			WorkshopTagToggle value = tagToggle.Value;
			if (value.m_ToggleMode == modeFilter && (value.m_IncludeToggle.isOn || value.m_ExcludeToggle.isOn))
			{
				num++;
			}
		}
		return num;
	}

	public static List<string> GetRequiredTags(WorkshopTagMode modeFilter, List<string> outTags)
	{
		foreach (KeyValuePair<WorkshopTagType, WorkshopTagToggle> tagToggle in m_TagToggles)
		{
			WorkshopTagToggle value = tagToggle.Value;
			if (value.m_ToggleMode == modeFilter && value.m_IncludeToggle.isOn && !outTags.Contains(value.m_TagName))
			{
				outTags.Add(value.m_TagName);
			}
		}
		return outTags;
	}

	public static List<string> GetExcludeTags(WorkshopTagMode modeFilter, List<string> outTags)
	{
		foreach (KeyValuePair<WorkshopTagType, WorkshopTagToggle> tagToggle in m_TagToggles)
		{
			WorkshopTagToggle value = tagToggle.Value;
			if (value.m_ToggleMode == modeFilter && value.m_ExcludeToggle.isOn && !outTags.Contains(value.m_TagName))
			{
				outTags.Add(value.m_TagName);
			}
		}
		return outTags;
	}
}
