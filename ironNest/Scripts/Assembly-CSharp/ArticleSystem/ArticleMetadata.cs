using UnityEngine;

namespace ArticleSystem
{
	[DisallowMultipleComponent]
	public class ArticleMetadata : MonoBehaviour
	{
		[Tooltip("Selection priority for this article. Higher values are placed first during the priority pass.\n\nGuidelines:\n- 100+ : Breaking / front-page articles\n-  50  : Standard news articles\n-   1  : Minor / supplementary articles\n-   0  : Filler (only used in the filler pass to top up columns after priority articles are placed)\n\nArticles with priority 0 are never placed in the priority pass.")]
		[Min(0f)]
		public int priority;

		[Tooltip("When enabled, this article is allowed to appear more than once within the same newspaper population pass — once per column, up to the limit set by 'Max Columns Per Pass'.\n\nUse this for decorative or structural elements that have no unique editorial content — for example:\n- Horizontal rule / line-break dividers\n- Section header banners\n- Small decorative vignettes\n- Advertisements\n\nWhen disabled (default), the article can appear at most once per pass across all columns.")]
		public bool reusable;

		[Tooltip("Only relevant when 'Reusable' is enabled.\n\nThe maximum number of columns this article may appear in during a single population pass. The packer selects WHICH columns receive it randomly each pass (using the pass RNG), so the same columns are not always chosen.\n\nExamples:\n- 1 : Appears in at most one column (behaves like non-reusable from a spread perspective).\n- 2 : May appear in up to 2 different columns per pass — recommended default for most reusable fillers.\n- 0 : No limit — may appear in every column. Use with caution on large newspapers.\n\nThe hard per-column uniqueness rule always applies regardless of this value: a reusable article can never appear more than once within the same column.")]
		[Min(0f)]
		public int maxColumnsPerPass;
	}
}
