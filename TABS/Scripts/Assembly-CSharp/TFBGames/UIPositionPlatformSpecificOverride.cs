using UnityEngine;
using UnityEngine.Serialization;

namespace TFBGames
{
	public class UIPositionPlatformSpecificOverride : PlatformSpecificOverride
	{
		[Tooltip("Overrides the RectTransform's local position.")]
		[FormerlySerializedAs("positionOverride")]
		[SerializeField]
		private Vector3 localPositionOverride = Vector3.zero;

		protected override void ApplyPlatformOverride()
		{
			if (base.transform is RectTransform rectTransform)
			{
				rectTransform.localPosition = localPositionOverride;
			}
		}
	}
}
