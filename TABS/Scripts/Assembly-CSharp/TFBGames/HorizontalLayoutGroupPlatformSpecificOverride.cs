using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(HorizontalLayoutGroup))]
	public class HorizontalLayoutGroupPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private RectOffset paddingOverride;

		[SerializeField]
		private float spacingOverride;

		[SerializeField]
		private TextAnchor childAlignmentOverride;

		[SerializeField]
		private bool controlChildSizeWidthOverride;

		[SerializeField]
		private bool controlChildSizeHeightOverride;

		[SerializeField]
		private bool useChildScaleWidthOverride;

		[SerializeField]
		private bool useChildScaleHeightOverride;

		[SerializeField]
		private bool childForceExpandWidthOverride;

		[SerializeField]
		private bool childForceExpandHeightOverride;

		private HorizontalLayoutGroup horizontalLayoutGroup;

		protected override void ApplyPlatformOverride()
		{
			horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
			horizontalLayoutGroup.padding = paddingOverride;
			horizontalLayoutGroup.spacing = spacingOverride;
			horizontalLayoutGroup.childAlignment = childAlignmentOverride;
			horizontalLayoutGroup.childControlWidth = controlChildSizeWidthOverride;
			horizontalLayoutGroup.childControlHeight = controlChildSizeHeightOverride;
			horizontalLayoutGroup.childScaleWidth = useChildScaleWidthOverride;
			horizontalLayoutGroup.childScaleHeight = useChildScaleHeightOverride;
			horizontalLayoutGroup.childForceExpandWidth = childForceExpandWidthOverride;
			horizontalLayoutGroup.childForceExpandHeight = childForceExpandHeightOverride;
		}
	}
}
