using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class RefreshButton : MonoBehaviour
	{
		public RectTransformSpinner spinner;

		private bool m_isUpdating;

		private void OnEnable()
		{
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			GetComponent<Button>().interactable = !m_isUpdating;
			if (spinner != null)
			{
				spinner.transform.localRotation = Quaternion.identity;
				spinner.enabled = m_isUpdating;
			}
		}

		public void StartUpdate()
		{
			m_isUpdating = true;
			UpdateDisplay();
			StartCoroutine(ModBrowser.instance.UpdateSubscriptions(OnUpdateComplete));
		}

		private void OnUpdateComplete()
		{
			if (this != null)
			{
				m_isUpdating = false;
				UpdateDisplay();
			}
		}
	}
}
