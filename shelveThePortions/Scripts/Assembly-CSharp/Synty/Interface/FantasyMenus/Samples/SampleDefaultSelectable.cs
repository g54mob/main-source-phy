using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleDefaultSelectable : MonoBehaviour
	{
		[Header("References")]
		public Selectable defaultSelectable;

		private bool started;

		private void Start()
		{
			if (defaultSelectable != null)
			{
				defaultSelectable.Select();
			}
			started = true;
		}

		private void OnEnable()
		{
			if (started && defaultSelectable != null)
			{
				defaultSelectable.Select();
			}
		}

		public void Select()
		{
			if (defaultSelectable != null)
			{
				defaultSelectable.Select();
			}
		}
	}
}
