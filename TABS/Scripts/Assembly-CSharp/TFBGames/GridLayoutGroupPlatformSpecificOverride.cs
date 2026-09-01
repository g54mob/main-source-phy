using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class GridLayoutGroupPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private Vector2 overrideCellSize = new Vector2(100f, 100f);

		[SerializeField]
		private Vector2 overrideSpacing = new Vector2(10f, 50f);

		[SerializeField]
		private TextAnchor childAlignment;

		private GridLayoutGroup layoutGroup;

		protected void Start()
		{
			if (platformsToOverride.HasFlag(GlobalSettingsHandler.CurrentPlatform))
			{
				layoutGroup.childAlignment = childAlignment;
			}
		}

		protected override void ApplyPlatformOverride()
		{
			layoutGroup = GetComponent<GridLayoutGroup>();
			layoutGroup.cellSize = overrideCellSize;
			layoutGroup.spacing = overrideSpacing;
		}
	}
}
