using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public interface IBrowserView
	{
		GameObject gameObject { get; }

		CanvasGroup canvasGroup { get; }

		bool resetSelectionOnHide { get; }

		bool isRootView { get; }

		List<Selectable> onFocusPriority { get; }
	}
}
