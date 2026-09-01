using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class DebugUIDefaultSelection : MonoBehaviour
	{
		[SerializeField]
		protected Selectable defaultSelected;

		[SerializeField]
		protected Transform selectablesParent;

		[SerializeField]
		protected bool autoGenerateNavigation;

		private List<Selectable> selectablesInParent;

		private void Start()
		{
		}
	}
}
