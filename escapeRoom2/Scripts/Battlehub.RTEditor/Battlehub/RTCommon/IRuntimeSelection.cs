using System.Collections;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRuntimeSelection : IEnumerable
	{
		bool Enabled { get; set; }

		bool EnableUndo { get; set; }

		GameObject activeGameObject { get; set; }

		Object activeObject { get; set; }

		Object[] objects { get; set; }

		GameObject[] gameObjects { get; }

		Transform activeTransform { get; }

		int Length { get; }

		event RuntimeSelectionChanged SelectionChanged;

		bool IsSelected(Object obj);

		void Select(Object activeObject, Object[] selection);
	}
}
