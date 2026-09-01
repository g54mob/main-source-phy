using UnityEngine;

namespace BitCode.UI
{
	public interface IRadialMenuElementPlacer<out TData>
	{
		float Radius { get; }

		float ResolutionScaleFactor { get; }

		float LayoutScaleFactor { get; }

		void UpdateArrow(RectTransform arrowTransform, Vector2 arrowDirection);

		void UpdateItemInRing(IRadialMenuItem<TData> item, int index, Vector2 center, float deltaAngle, float offsetStartAngle);

		void UpdateItemInSpiral(IRadialMenuItem<TData> item, int index, int selectedIndex, int numItems, Vector2 selectedVector, int frontWindow, int backWindow, float amountBetween, float deltaAngle);

		void SetScaleFactor(float resolutionScale, float layoutScale);
	}
}
