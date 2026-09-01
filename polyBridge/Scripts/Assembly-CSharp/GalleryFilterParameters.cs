public class GalleryFilterParameters
{
	public static readonly string SORT_DATE_TAG = "created_at";

	public static readonly string SORT_BUDGET_LOW_TAG = "budget_low";

	public static readonly string SORT_BUDGET_HIGH_TAG = "budget_high";

	public static readonly string OWNERSHIP_FRIENDS_TAG = "friends";

	public static readonly string OWNERSHIP_MINE_TAG = "mine";

	public static readonly string AWS_TAG = "aws";

	public static readonly string CLOUDFLARE_TAG = "cf";

	public static readonly string FEATURED_TAG = "featured";

	public static readonly string UNDERBUDGET_TAG = "underbudget";

	public static readonly string UNBREAKING_TAG = "unbreaking";

	public static readonly string FAIL_TAG = "fail";

	public static readonly string CHEAT_TAG = "cheat";

	public static readonly string CURATED_TAG = "curated";

	public static readonly string MOD_TAG = "mod";

	public int m_PageNum;

	public string m_WorldId;

	public string m_LevelId;

	public string m_Sort;

	public string m_Ownership;

	public bool m_ShowOnlyFeatured;

	public bool m_UnderBudgetOnly;

	public bool m_UnbreakingOnly;

	public bool m_ShowOnlyWins;

	public bool m_IncludeCheats;

	public bool m_Curated;

	public void Clear()
	{
		m_PageNum = -1;
		m_WorldId = string.Empty;
		m_LevelId = string.Empty;
		m_Sort = string.Empty;
		m_Ownership = string.Empty;
		m_ShowOnlyFeatured = false;
		m_UnderBudgetOnly = false;
		m_UnbreakingOnly = false;
		m_ShowOnlyWins = true;
		m_IncludeCheats = false;
		m_Curated = false;
	}

	public void Set(int pageNum, string worldId, string levelId, string ownership, GallerySortBy sortBy, bool showOnlyFeatured, bool underBudgetOnly, bool unbreakingOnly, bool showOnlyWins, bool includeCheats, bool curated)
	{
		m_PageNum = pageNum;
		m_WorldId = worldId;
		m_LevelId = levelId;
		m_Sort = ResolveSortBy(sortBy);
		m_Ownership = ownership;
		m_ShowOnlyFeatured = showOnlyFeatured;
		m_UnderBudgetOnly = underBudgetOnly;
		m_UnbreakingOnly = unbreakingOnly;
		m_ShowOnlyWins = showOnlyWins;
		m_IncludeCheats = includeCheats;
		m_Curated = curated;
	}

	public void CopyFrom(GalleryFilterParameters other)
	{
		m_PageNum = other.m_PageNum;
		m_WorldId = other.m_WorldId;
		m_LevelId = other.m_LevelId;
		m_Sort = other.m_Sort;
		m_Ownership = other.m_Ownership;
		m_ShowOnlyFeatured = other.m_ShowOnlyFeatured;
		m_UnderBudgetOnly = other.m_UnderBudgetOnly;
		m_UnbreakingOnly = other.m_UnbreakingOnly;
		m_ShowOnlyWins = other.m_ShowOnlyWins;
		m_IncludeCheats = other.m_IncludeCheats;
		m_Curated = other.m_Curated;
	}

	public bool DifferentFrom(GalleryFilterParameters other)
	{
		if (m_WorldId != other.m_WorldId)
		{
			return true;
		}
		if (m_LevelId != other.m_LevelId)
		{
			return true;
		}
		if (m_Sort != other.m_Sort)
		{
			return true;
		}
		if (m_Ownership != other.m_Ownership)
		{
			return true;
		}
		if (m_ShowOnlyFeatured != other.m_ShowOnlyFeatured)
		{
			return true;
		}
		if (m_UnderBudgetOnly != other.m_UnderBudgetOnly)
		{
			return true;
		}
		if (m_UnbreakingOnly != other.m_UnbreakingOnly)
		{
			return true;
		}
		if (m_ShowOnlyWins != other.m_ShowOnlyWins)
		{
			return true;
		}
		if (m_IncludeCheats != other.m_IncludeCheats)
		{
			return true;
		}
		if (m_Curated != other.m_Curated)
		{
			return true;
		}
		return false;
	}

	public bool IsSortByBudget()
	{
		if (!(m_Sort == SORT_BUDGET_LOW_TAG))
		{
			return m_Sort == SORT_BUDGET_HIGH_TAG;
		}
		return true;
	}

	public string GetSortDirection()
	{
		if (!(m_Sort == SORT_BUDGET_LOW_TAG))
		{
			return "desc";
		}
		return "asc";
	}

	private string ResolveSortBy(GallerySortBy sortBy)
	{
		return sortBy switch
		{
			GallerySortBy.MOST_RECENT => SORT_DATE_TAG, 
			GallerySortBy.BUDGET_LOW => SORT_BUDGET_LOW_TAG, 
			GallerySortBy.BUDGET_HIGH => SORT_BUDGET_HIGH_TAG, 
			_ => SORT_DATE_TAG, 
		};
	}
}
