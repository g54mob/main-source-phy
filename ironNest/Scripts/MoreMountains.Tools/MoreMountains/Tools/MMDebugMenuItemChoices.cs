using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuItemChoices : MonoBehaviour
	{
		[Header("Bindings")]
		public Sprite SelectedSprite;

		public Sprite OffSprite;

		public Color OnColor;

		public Color OffColor;

		public Color AccentColor;

		public List<MMDebugMenuChoiceEntry> Choices;

		public virtual void TriggerButtonEvent(int index)
		{
		}

		public virtual void Select(int index)
		{
		}

		public virtual void Deselect()
		{
		}
	}
}
