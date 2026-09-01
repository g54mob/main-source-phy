using System;
using UnityEngine;

namespace Interface.QuickSelect
{
	public abstract class QuickMenu : MonoBehaviour
	{
		[Serializable]
		public class MenuOption
		{
			public Sprite Icon;

			public int LocalisationId;
		}

		public MenuOption[] Options;

		public abstract void OnSelect(int index);
	}
}
