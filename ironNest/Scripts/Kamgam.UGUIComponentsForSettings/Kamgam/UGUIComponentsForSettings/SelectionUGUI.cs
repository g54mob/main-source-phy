using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.UGUIComponentsForSettings
{
	public class SelectionUGUI : MonoBehaviour
	{
		[FormerlySerializedAs("SelectionEventListneners")]
		public SelectionEventListener[] SelectionEventListeners;

		[Tooltip("Will be active while it is selected.")]
		public GameObject Selected;

		[Tooltip("Enable if you do not want the selected highlight to show up if coming from the mouse.")]
		public bool IgnoreSelectionsFromMouse;

		protected float m_lastMouseUseTime;

		public void Start()
		{
		}

		protected SelectionEventListener FindFirstActiveListener()
		{
			return null;
		}

		public void ConnectToListeners()
		{
		}

		public void DisconnectFromListeners()
		{
		}

		protected void onSelectionChanged(bool isSelected)
		{
		}

		protected bool mouseUsed(bool ignore)
		{
			return false;
		}

		protected void updateLastMouseUseTime()
		{
		}

		protected bool mouseWasRecentlyUsed(float maxDelay = 0.3f)
		{
			return false;
		}
	}
}
