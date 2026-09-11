using System;
using UnityEngine;

namespace Vertx.Debugging
{
	public abstract class DebugComponentBase : MonoBehaviour
	{
		[Serializable]
		public struct ColorDurationPair
		{
			public Color Color;

			public float Duration;

			public ColorDurationPair(Color color, float duration)
			{
				Color = color;
				Duration = duration;
			}

			public ColorDurationPair(Color color)
			{
				Color = color;
				Duration = 0f;
			}
		}

		[SerializeField]
		private bool _drawOnlyWhenSelected = true;

		protected abstract bool ShouldDraw();

		protected abstract void Draw();
	}
}
