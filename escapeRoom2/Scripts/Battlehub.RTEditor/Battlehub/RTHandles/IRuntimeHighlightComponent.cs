using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface IRuntimeHighlightComponent
	{
		bool Enabled { get; set; }

		IEnumerable<Renderer> Rendererers { get; }

		event EventHandler HighlightChanged;

		void Highlight(GameObject go);

		void ClearHighlight();
	}
}
