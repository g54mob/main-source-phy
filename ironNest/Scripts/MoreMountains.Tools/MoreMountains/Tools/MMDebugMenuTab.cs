using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuTab : MonoBehaviour
	{
		public Text TabText;

		public Image TabBackground;

		public Color SelectedBackgroundColor;

		public Color DeselectedBackgroundColor;

		public Color SelectedTextColor;

		public Color DeselectedTextColor;

		public int Index;

		public MMDebugMenuTabManager Manager;

		public bool ForceScaleOne;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void Select()
		{
		}

		public virtual void Deselect()
		{
		}
	}
}
