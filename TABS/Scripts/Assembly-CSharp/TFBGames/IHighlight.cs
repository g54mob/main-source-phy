using UnityEngine;

namespace TFBGames
{
	public interface IHighlight
	{
		bool IsHighlighted { get; }

		void BeginHighlight();

		void EndHighlight();

		void SetHighlightColor(Color newHighlightColor);
	}
}
