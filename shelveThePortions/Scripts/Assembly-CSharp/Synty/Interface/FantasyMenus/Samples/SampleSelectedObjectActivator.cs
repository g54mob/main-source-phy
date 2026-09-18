using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSelectedObjectActivator : MonoBehaviour
	{
		[Header("References")]
		public Selectable selectable;

		public GameObject isOnObject;

		private void Start()
		{
			SetActiveObjects();
		}

		private void LateUpdate()
		{
			SetActiveObjects();
		}

		private void SetActiveObjects()
		{
			if (isOnObject != null)
			{
				if (!isOnObject.activeSelf && selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
				{
					isOnObject.SetActive(value: true);
				}
				if (isOnObject.activeSelf && selectable != null && EventSystem.current.currentSelectedGameObject != selectable.gameObject)
				{
					isOnObject.SetActive(value: false);
				}
			}
		}
	}
}
